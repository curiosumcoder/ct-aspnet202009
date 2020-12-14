using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Store.UI.Intranet.Models;

namespace Northwind.Store.UI.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //throw new ApplicationException("Esta es una prueba!");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var exceptionMessage = "";

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error is ApplicationException)
            {
                var ex = exceptionHandlerPathFeature?.Error;

                exceptionMessage = $"Error en la aplicación: {ex.Message}";                
                _logger.LogError(exceptionMessage);
            }
            if (exceptionHandlerPathFeature?.Path == "/")
            {
                exceptionMessage += " desde la raíz.";
            }

            return View("Error", new ErrorViewModel { RequestId = requestId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorWithCode(string code)
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var errorStatusCode = code;
            var originalURL = "";

            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeReExecuteFeature != null)
            {
                originalURL =
                    statusCodeReExecuteFeature.OriginalPathBase
                    + statusCodeReExecuteFeature.OriginalPath
                    + statusCodeReExecuteFeature.OriginalQueryString;
            }

            return View("Error", new ErrorViewModel { RequestId = requestId });
        }
    }
}
