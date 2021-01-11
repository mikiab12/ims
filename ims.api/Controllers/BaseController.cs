using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ims.domain.Exceptions;
using ims.domain.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ims.api.Controllers
{
    public class BaseController : Controller
    {
        public UserSession GetSession()
        {

            var session = HttpContext.Session.GetString("sessionInfo");
            if (session == null)
            {
                throw new NotAuthenticatedException("Not Authenticated");
            }
            return JsonConvert.DeserializeObject<UserSession>(session);
        }


        public IActionResult ErrorResponse(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());

            if (ex is NotAuthenticatedException)
            {
                return StatusCode(403, ex.Message);
            }
            else
            {
                return StatusCode(501, ex.Message);
            }

        }

        public IActionResult SuccessfulResponse(Object response)
        {
            return Ok(response);

        }
    }

    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private static void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            SetExceptionResult(context, exception);
           
        }

        private static void SetExceptionResult(
            ExceptionContext context,
            Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());

            if (ex is NotAuthenticatedException)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = ex.Message,
                    ContentType = "text/plain",
                };
            }
            else
            {
                context.Result = new ContentResult
                {
                    StatusCode = 501,
                    Content = ex.Message,
                    ContentType = "text/plain"
                };
            }
        }
    }
}