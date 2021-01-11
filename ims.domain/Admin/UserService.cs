
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ims.data.Entities;
using ims.data;
using ims.domain;
using ims.domain.Admin;
using ims.domain.Exceptions;
using ims.domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ims.domain.Admin
{
    public interface IUserService
    {
        void SetSession(UserSession session);

        void LoginUser(LoginViewModel loginView);

        User RegisterUser(RegisterViewModel user);

        void RevokeUserRole(string username, int[] roles);
        void AddUserRole(string username, int[] roles);

        IList<UserDetialViewModel> GetAllUsers();
        bool CheckUser(string username);
        void UpdateUser(UserViewModel userModle);
        void ActivateUser(string username);
        void DeactivateUser(string username);
        IList<UserActionViewModel> GetAllActions();
        User GetUser(string username);
        IList<UserViewModel> GetUsers(string query);
        bool HasRole(string username, int role);
        void ResetPassword(string username, string newPasswrod);
        void ChangePasswrod(string username, string oldPassword, string newPassword);
        object GetRoles();
        long[] GetUserRoles(string username);
        UserViewModel GetUser(int id);
        User[] GetUsers(string[] username);
      //  Staff GetStaff(long UserID);
    }

    public class UserService : IUserService
    {
        private readonly IUserActionService _actionService;
        private readonly StoreDbContext _context;
        private UserSession _userSession;

        public UserService(StoreDbContext context, IUserActionService actionService)
        {
            _context = context;
            _actionService = actionService;
        }

        public void SetSession(UserSession session)
        {
            _userSession = session;
        }

        public void LoginUser(LoginViewModel loginView)
        {
            var hashPassword = loginView.password.Hash();
            try
            {
                var dbUser = _context.User.First(u => u.Username.ToLower() == loginView.username.ToLower() && u.Password == hashPassword);
                if (dbUser.Status == 0) throw new AccessDeniedException("User Deactivated");

                _actionService.AddUserAction(new UserSession { Username = dbUser.Username }, UserActionType.Login);
                _context.SaveChanges();
            }
            catch (InvalidOperationException e)
            {
                Console.Error.WriteLine(e);
                throw new AccessDeniedException("Invalid Username or Password, Please Try Again");
            }
        }

        public User[] GetUsers(string[] username)
        {
            var users = _context.User.Where(m => username.Any(u => u.ToLower() == m.Username.ToLower())).ToArray();
            return users;
        }

        public User RegisterUser(RegisterViewModel userVm)
        {


            var users = _context.User.Where(m => m.Username.ToLower() == userVm.Username.ToLower()).Count();
            if(users > 0)
            {
                throw new UsernameException("The Username is already taken.");
            }
            var user = new User
            {
                Username = userVm.Username,
                Fullname = userVm.FullName,
                Password = userVm.Password.Hash(),
                Status = 1,
                PhoneNo = userVm.PhoneNo,
                Position = userVm.Position,
                
            };


            _context.User.Add(user);

            foreach (var ur in userVm.Roles)
            {
                var role = GetRole(ur);

                var userRole = new UserRole
                {
                    User = user,
                    Role = role
                };
                _context.UserRole.Add(userRole);
            }

            _context.SaveChanges(_userSession.Username, (int)UserActionType.Register);
            return user;
        }

        public void ChangePasswrod(string username, string oldPassword, string newPassword)
        {
            var hashPassword = oldPassword.Hash();

            var user = GetUser(username);

            if (!user.Password.Equals(hashPassword))
                throw new AccessDeniedException("Incorrect Old Password");
            user.Password = newPassword.Hash();

            _context.User.Update(user);
            var userAction = _actionService.AddUserAction(_userSession, UserActionType.ChangePassword);
            _context.SaveChanges(username, userAction);
        }

        public void ResetPassword(string username, string newPassword)
        {
            var user = GetUser(username);


            user.Password = newPassword.Hash();

            _context.SaveChanges(_userSession.Username, (int)UserActionType.ResetPassword);
        }

        public void AddUserRole(string username, int[] roles)
        {
            var user = GetUser(username);
            var userRoles = _context.UserRole.Where(u => u.Userid == user.Id).Select(ur => ur.Role.Id);
            foreach (var role in roles)
            {
                if (userRoles.Contains(role)) continue;
                var userRole = new UserRole { User = user, Role = GetRole(role) };
                _context.UserRole.Add(userRole);
            }

            var userAction = _actionService.AddUserAction(_userSession, UserActionType.Register);
            _context.SaveChanges(_userSession.Username, userAction);
        }

        public void RevokeUserRole(string username, int[] roles)
        {
            var user = GetUser(username);


            foreach (var role in roles)
            {
                var userRole = _context.UserRole.First(ur => ur.Userid == user.Id && ur.Role.Id == role);

                _context.UserRole.Remove(userRole);
            }

            _context.SaveChanges(_userSession.Username, (int)UserActionType.RevokeRole);
        }

        public long[] GetUserRoles(string username)
        {
            return _context.UserRole.Include(m => m.User).Where(ur => ur.User.Username.ToLower().Equals(username.ToLower())).Select(ur => ur.Roleid).ToArray();
        }

        public object GetRoles()
        {
            return _context.UserRole.Where(ur => ur.User.Username.Equals(_userSession.Username))
                .Select(ur => new { id = ur.Roleid, name = ur.Role.Name }).ToList();
        }

        public void DeactivateUser(string username)
        {
            var user = GetUser(username);

            user.Status = 0;
            _context.User.Update(user);

            var userAction = _actionService.AddUserAction(_userSession, UserActionType.DeactivateUser);
            _context.SaveChanges(_userSession.Username, userAction);
        }

        public void ActivateUser(string username)
        {
            var user = GetUser(username);
            user.Status = 1;

            _context.User.Update(user);

            var userAction = _actionService.AddUserAction(_userSession, UserActionType.ActivateUser);
            _context.SaveChanges(_userSession.Username, userAction);
        }

        public IList<UserViewModel> GetUsers(string filter)
        {
            var users = _context.User.Where(u => u.Username.Contains(filter) || u.Fullname.Contains(filter));
            var userVms = new List<UserViewModel>();

            foreach (var user in users)
            {
                userVms.Add(new UserViewModel
                {
                    UserName = user.Username,
                    FullName = user.Fullname,
                    // PhoneNo = user.PhoneNo,
                    Status = user.Status,
                    Roles = _context.UserRole.Where(u => u.Userid == user.Id).Select(ur => ur.Role.Id).ToArray(),
                    Id = user.Id

                });
            }

            return userVms;
        }

        public IList<UserDetialViewModel> GetAllUsers()
        {
            var users = _context.User;

            var userVms = new List<UserDetialViewModel>();

            foreach (var user in users)
            {
                var userVm = new UserDetialViewModel
                {
                    UserName = user.Username,
                    FullName = user.Fullname,
                    PhoneNo = user.PhoneNo,
                    Status = user.Status,
                    Id = user.Id,
                    Position = user.Position
                };
                userVm.Roles = _context.UserRole.Where(u => u.Userid == user.Id).Select(ur => ur.Role.Name).ToArray();


                var time = _context.UserAction.Where(u => u.Username == user.Username && u.Actiontypeid == 1)
                    .Select(ua => ua.Timestamp).DefaultIfEmpty(0).Max();
                var lastSeen = time ?? -1;
                var dt = new DateTime(lastSeen);
                userVm.LastSeen = string.Format("{0:G}", dt);
                var regDate = new DateTime(user.Regon);
                userVm.RegOn = string.Format("{0:G}", regDate);

                userVms.Add(userVm);
            }

            return userVms;
        }


        public IList<UserActionViewModel> GetAllActions()
        {
            var actions = _context.UserAction.ToList().OrderByDescending(action => action.Id);

            var actionVms = new List<UserActionViewModel>();

            foreach (var userAction in actions)
            {
                var username = GetUser(userAction.Username).Username;
                var fullname = GetUser(userAction.Username).Fullname;
                var actionType = GetActionType(userAction.Actiontypeid).Name;
                var lastAction = userAction.Timestamp ?? -1;
                var dt = new DateTime(lastAction);
                if (!actionType.Equals("Login"))
                {
                    actionVms.Add(new UserActionViewModel
                    {

                        UserName = username,
                        Action = actionType,
                        ActionTime = dt,
                        FullName = fullname
                    });
                }
            }

            return actionVms;
        }

        public void UpdateUser(UserViewModel userVm)
        {
            var user = GetUser(userVm.Id);

            user.Fullname = userVm.FullName;
            user.Username = userVm.UserName;
            user.PhoneNo = userVm.PhoneNo;
            user.Position = userVm.Position;

            var userRoles = _context.UserRole.Where(u => u.Userid == user.Id);

            //Remove Previous roles
            foreach (var role in userRoles)
            {
                _context.UserRole.Remove(role);
            }

            _context.SaveChanges();


            foreach (var role in userVm.Roles)
            {
                var ur = _context.Role.First(r => r.Id.Equals(role));

                var userRole = new UserRole { User = user, Role = ur };

                _context.UserRole.Add(userRole);
            }

            _context.User.Update(user);

            _context.SaveChanges(_userSession.Username, (int)UserActionType.UpdateUser);
        }

        public User GetUser(string username)
        {
            return _context.User.First(u => u.Username.ToLower() == username.ToLower());
        }

        public bool HasRole(string username, int role)
        {
            var userId = GetUser(username).Id;
            var roleId = GetRole(role).Id;

            return _context.UserRole.Any(ur => ur.Roleid == roleId && ur.Userid == userId);
        }

        public bool CheckUser(string username)
        {
            return _context.User.Any(u => u.Username == username);
        }

        private ActionType GetActionType(int id)
        {
            return _context.ActionType.First(at => at.Id == id);
        }

        private UserAction GetUserAction(User user, UserActionType type)
        {
            var action = new UserAction
            {
                Timestamp = DateTime.Now.Ticks,
                Username = user.Username,
                Actiontypeid = (int)type,
            };


            return action;
        }

        private User GetUser(long id)
        {
            return _context.User.First(u => u.Id == id);
        }

        private Role GetRole(int role)
        {
            return _context.Role.First(r => r.Id == role);
        }

        public UserViewModel GetUser(int id)
        {
            var user = GetUser((long)id);
            var roles = GetUserRoles(user.Username);
            return new UserViewModel()
            {
                FullName = user.Fullname,
                Id = user.Id,
                Status = user.Status,
                UserName = user.Fullname,
                Roles = roles,
                PhoneNo  = user.PhoneNo,
                Position = user.Position
            };
        }
    }



}
