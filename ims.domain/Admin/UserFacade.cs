﻿
using ims.data.Entities;
using ims.domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ims.domain.Admin
{
    public interface IUserFacade
    {
        void LoginUser(UserSession session, LoginViewModel loginView);
        User RegisterUser(UserSession session, RegisterViewModel registerViewModel);
        void ChangePassword(UserSession session, string username, string oldPassword, string newPassword);
        IList<UserDetialViewModel> GetAllUsers(UserSession session);
        void DeactivateUser(UserSession session, string username);
        void ActivateUser(UserSession session, string username);
        void UpdateUser(UserSession session, UserViewModel userModel);
        void ResetPassword(UserSession getSession, string resetedUsername, string newPassword);
        bool CheckUser(UserSession userSession, string username);
        IList<UserActionViewModel> GetAllAction(UserSession getSession);
        IList<UserViewModel> GetUsers(UserSession userSession, string query);
        void AddUserRole(UserSession userSession, string username, int[] roles);

        object GetRoles(UserSession session);
        long[] GetUserRoles(string username);
        User GetUser(string username);
        UserViewModel GetUser(int id);
        User[] GetUsers(string[] username);
      //  Staff GetStaff(long UserID);
    }

    public class UserFacade : IUserFacade
    {
        private readonly IUserService _userService;

        public UserFacade(IUserService service)
        {
            _userService = service;
        }

        public void LoginUser(UserSession userSession, LoginViewModel loginView)
        {
            _userService.SetSession(userSession);
            _userService.LoginUser(loginView);
        }

        public User RegisterUser(UserSession userSession, RegisterViewModel registerViewModel)
        {
            _userService.SetSession(userSession);
            var user = _userService.RegisterUser(registerViewModel);
            return user;
        }

        public void ChangePassword(UserSession userSession, string username, string oldPassword, string newPassword)
        {
            _userService.SetSession(userSession);
            _userService.ChangePasswrod(username, newPassword, oldPassword);
        }

        public void ResetPassword(UserSession userSession, string username, string newPassword)
        {
            _userService.SetSession(userSession);
            _userService.ResetPassword(username, newPassword);
        }

        public IList<UserViewModel> GetUsers(UserSession userSession, string query)
        {
            _userService.SetSession(userSession);

            return _userService.GetUsers(query);
        }

        public object GetRoles(UserSession session)
        {
            _userService.SetSession(session);

            return _userService.GetRoles();
        }

        public IList<UserDetialViewModel> GetAllUsers(UserSession userSession)
        {
            _userService.SetSession(userSession);

            return _userService.GetAllUsers();
        }



        public IList<UserActionViewModel> GetAllAction(UserSession userSession)
        {
            _userService.SetSession(userSession);
            return _userService.GetAllActions();
        }

        public void AddUserRole(UserSession userSession, string username, int[] roles)
        {
            _userService.SetSession(userSession);
            _userService.AddUserRole(username, roles);
        }

        public void DeactivateUser(UserSession userSession, string username)
        {
            _userService.SetSession(userSession);
            _userService.DeactivateUser(username);
        }

        public void ActivateUser(UserSession userSession, string username)
        {
            _userService.SetSession(userSession);
            _userService.ActivateUser(username);
        }

        public void UpdateUser(UserSession userSession, UserViewModel userVm)
        {
            _userService.SetSession(userSession);
            _userService.UpdateUser(userVm);
        }

        public bool CheckUser(UserSession userSession, string username)
        {
            _userService.SetSession(userSession);

            return _userService.CheckUser(username);
        }

        public long[] GetUserRoles(string username)
        {
            return _userService.GetUserRoles(username);
        }

        public User GetUser(string username)
        {
            return _userService.GetUser(username);
        }

        public UserViewModel GetUser(int id)
        {
            return _userService.GetUser(id);
        }

        public User[] GetUsers(string[] username)
        {
            return _userService.GetUsers(username);
        }
    }



}
