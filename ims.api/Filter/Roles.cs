using ims.domain.Exceptions;
using ims.domain.Extensions;
using ims.domain.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ims.api.Filter
{
    public class Roles : ActionFilterAttribute
    {
        public Roles(params long[] values)
        {
            AllowedUsers = values;
        }

        public long[] AllowedUsers { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            try
            {

                var userSession = session.GetSession<UserSession>("sessionInfo");
                if (userSession == null)
                {
                    throw new NotAuthenticatedException("Not Authenticated");
                }
                userSession.LastSeen = DateTime.Now;
                session.SetSession("sessionInfo", userSession);

                if (AllowedUsers == null || AllowedUsers.Length <= 0) return;

                if (!AllowedUsers.Contains(userSession.Role))
                    //    context.Result = new IActionResult()
                    // context.Result = new RedirectToActionResult("Login","Account",new { });
                    throw new AccessDeniedException("You are not allowed to access this resource");
            }

            catch (ArgumentNullException e)
            {
                throw new NotAuthenticatedException("Not Authenticated");
                //new BadRequestObjectResult(new { status = 403, message = "Forbidden" });
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
