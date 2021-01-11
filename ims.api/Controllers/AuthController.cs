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
    public class AuthController : BaseController
    {

            private IUserFacade _userFacade;
            static Dictionary<String, UserSession> sessions = new Dictionary<string, UserSession>();


            public static UserSession GetSession(String sid)
            {
                if (sessions.ContainsKey(sid))
                    return sessions[sid];
                return null;
            }


            public AuthController(IUserFacade userFacade)
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
            public IActionResult Login(LoginViewModel loginViewModel)
            {
                try
                {
                    int role;
                    //    Staff st = new Staff();
                    var user = new User();
                    _userFacade.LoginUser(null, loginViewModel);
                    role = (int)_userFacade.GetUserRoles(loginViewModel.username)[0];
                    user = _userFacade.GetUser(loginViewModel.username);



                    var us = new UserSession
                    {
                        Username = loginViewModel.username,
                        CreatedTime = DateTime.Now,
                        LastSeen = DateTime.Now,
                        Role = role,
                        Id = user.Id,
                        // StaffID = st != null ? st.Id : 0,
                    };
                    HttpContext.Session.SetString("sessionInfo", JsonConvert.SerializeObject(us));

                    return RedirectToAction("Index", "Store");
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

                HttpContext.Session.SetString("sessionInfo",  JsonConvert.SerializeObject( new UserSession
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
                    return RedirectToAction("Index");
                }
                catch (UsernameException ex)
                {

                    ModelState.AddModelError("Username", ex.Message);
                    return View();
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
                if (!ModelState.IsValid) return Json(false);

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