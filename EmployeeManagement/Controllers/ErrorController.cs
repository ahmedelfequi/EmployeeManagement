using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;
        public ErrorController (ILogger<ErrorController>logger)
        {
            this.logger = logger;
        }

        // AllowAnonymous means that this method doesn't require authorization
        
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult ExceptionHandler()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("Error");
        }


        [Route("Error/{statusCode}")]
        public IActionResult ErrorHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Error! Your Requested Page isn't found";
                    break;
            }
            return View("NotFound");
        }


    }
}
