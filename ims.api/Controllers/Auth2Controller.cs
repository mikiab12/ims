using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ims.api.Filter;
using ims.data.Entities;
using ims.domain.Admin;
using ims.domain.Exceptions;
using ims.domain.Extensions;
using ims.domain.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ims.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Auth2Controller : BaseController
    {
        private IUserFacade _userFacade;
        static Dictionary<String, UserSession> sessions = new Dictionary<string, UserSession>();


        public static UserSession GetSession(String sid)
        {
            if (sessions.ContainsKey(sid))
                return sessions[sid];
            return null;
        }


        public Auth2Controller(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }


        public IActionResult Index()
        {
            var users = _userFacade.GetAllUsers(new UserSession());
            return View(users);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckLogin(LoginViewModel loginViewModel)
        {
            try
            {
                int role;
                long[] roles;
                //    Staff st = new Staff();
                var user = new User();
                _userFacade.LoginUser(null, loginViewModel);
                roles = _userFacade.GetUserRoles(loginViewModel.username);
                user = _userFacade.GetUser(loginViewModel.username);



                var us = new UserSession
                {
                    Username = loginViewModel.username,
                    CreatedTime = DateTime.Now,
                    LastSeen = DateTime.Now,
                    //Role = role,
                    Roles = roles,
                    Id = user.Id,
                    // StaffID = st != null ? st.Id : 0,
                };
                if(roles.Length == 1)
                {
                    us.Role = us.Roles[0];
                    HttpContext.Session.SetString("sessionInfo", JsonConvert.SerializeObject(us));
                }
                //HttpContext.Session.SetString("sessionInfo", JsonConvert.SerializeObject(us));
                return SuccessfulResponse(us);
            }

            catch (Exception e)
            {
                return ErrorResponse(e);
            }
        }

        [HttpGet]
        public IActionResult GetUserByUsername(string username)
        {
            try
            {
                var user = _userFacade.GetUser(username);
                return SuccessfulResponse(user);
            }
            catch (Exception ex)
            {

                return ErrorResponse(ex);
            }

        }

        [HttpPost]
        public IActionResult GetUsersByUsername(string[] usernames)
        {
            try
            {
                var resp = _userFacade.GetUsers(usernames);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {

                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                int role;
                //    Staff st = new Staff();
                long[] roles;
                var user = new User();
                _userFacade.LoginUser(null, loginViewModel);
                roles = _userFacade.GetUserRoles(loginViewModel.username);
                user = _userFacade.GetUser(loginViewModel.username);

                var us = new UserSession
                {
                    Username = loginViewModel.username,
                    CreatedTime = DateTime.Now,
                    LastSeen = DateTime.Now,
                    Role = loginViewModel.Role,
                    Id = user.Id,
                    Roles = roles
                    // StaffID = st != null ? st.Id : 0,
                };
                HttpContext.Session.SetString("sessionInfo", JsonConvert.SerializeObject(us));
                return SuccessfulResponse(us);
            }

            catch (Exception e)
            {
                return ErrorResponse(e);
            }
        }

        [HttpGet]
        [Roles]
        public IActionResult GetRoles()
        {
            try
            {
                var session = GetSession();
                var roles = _userFacade.GetRoles(session);
                return Json(roles);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPost]
        [Roles]
        public IActionResult SetRole([FromBody] JObject userRole)
        {
            var role = (int)userRole["role"];
            var session = GetSession();

            HttpContext.Session.SetString("sessionInfo", JsonConvert.SerializeObject(new UserSession
            {
                Username = session.Username,
                CreatedTime = DateTime.Now,
                LastSeen = DateTime.Now,
                Role = role
            }));

            return Json(new { message = "success" });
        }

        [Roles((long)UserRoles.Admin)]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Roles((long)UserRoles.Admin)]
        [HttpPost]
        public IActionResult Register(RegisterViewModel userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }


                var session = GetSession();
                _userFacade.RegisterUser(session, userModel);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {

                return ErrorResponse(ex);
            }


        }

        [Roles]
        public IActionResult ChangePassword([FromBody] ChangePasswordViewModel changePassVm)
        {
            if (!ModelState.IsValid) return Json(false);

            try
            {
                var session = GetSession();
                _userFacade.ChangePassword(session, session.Username, changePassVm.OldPassword,
                    changePassVm.NewPassword);
                return Json(new { message = "success" });
            }
            catch (Exception e)
            {
                return Json(new { errorCode = e.GetType().Name, message = e.Message });
            }
        }

        [Roles((long)UserRoles.Admin)]
        public IActionResult ResetPassword([FromBody] ResetPasswordViewModel resetVm)
        {
            try
            {
                _userFacade.ResetPassword(GetSession(), resetVm.UserName, resetVm.NewPassword);
                return Json(new { message = "success" });
            }
            catch (Exception e)
            {
                return Json(new { errorCode = e.GetType().Name, message = e.Message });
            }
        }

        [Roles((long)UserRoles.Admin)]
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                return Json(_userFacade.GetAllUsers(GetSession()));
            }
            catch (Exception e)
            {
                return Json(new { errorCode = 500, message = e.Message });
            }
        }

        [Roles((long)UserRoles.Admin)]
        [HttpGet]
        public IActionResult GetUser(int id)
        {
            try
            {
                var s = _userFacade.GetUser(id);
                return SuccessfulResponse(s);
            }
            catch(Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [Roles((long)UserRoles.Admin)]
        [HttpGet]
        public IActionResult CheckUser(string username)
        {
            try
            {
                return Json(_userFacade.CheckUser(GetSession(), username));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return Json(false);
            }
        }

        [HttpPost]
        [Roles((long)UserRoles.Admin)]
        public IActionResult Update([FromBody] UserViewModel userVm)
        {
            if (!ModelState.IsValid) return Json(false);

            try
            {
                _userFacade.UpdateUser(GetSession(), userVm);
                return new JsonResult(new { message = "Success" }) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(500, new { message = "Internal server error occurred" });
            }
        }

        [Roles((long)UserRoles.Admin)]
        [HttpPost]
        public IActionResult AddRole([FromBody] UserRoleViewModel roleVm)
        {
            if (!ModelState.IsValid) return Json(false);

            try
            {
                _userFacade.AddUserRole(GetSession(), roleVm.UserName, roleVm.Roles);
                return Json(true);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(500, new { message = "Internal server error occurred" });
            }
        }

        [Roles((long)UserRoles.Admin)]
        [HttpPost]
        public IActionResult Deactivate([FromBody] JObject user)
        {
            if (!ModelState.IsValid) return Json(false);

            try
            {
                var username = (string)user["username"];
                _userFacade.DeactivateUser(GetSession(), username);
                return Json(true);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(500, new { message = "Internal server error occurred" });
            }
        }

        [Roles((long)UserRoles.Admin)]
        [HttpPost]
        public IActionResult Activate([FromBody] JObject user)
        {
            if (!ModelState.IsValid) return Json(false);

            try
            {
                var username = (string)user["username"];
                _userFacade.ActivateUser(GetSession(), username);
                return Json(true);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(500, new { message = "Internal server error occurred" });
            }
        }


        [Roles]
        [HttpGet]
        public IActionResult Search(string query)
        {
            try
            {
                if (string.IsNullOrEmpty(query)) return Json(_userFacade.GetAllUsers(GetSession()));

                return Json(_userFacade.GetUsers(GetSession(), query));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return StatusCode(500, new { message = "Internal server error occurred" });
            }
        }


        // [Roles]
        [HttpGet]
        public IActionResult Logout()
        {
            base.HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
