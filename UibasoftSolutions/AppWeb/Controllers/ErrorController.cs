using AppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppWeb.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("unauthorized")]
        public IActionResult Error401()
        {
            return View("NotAuthorized", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, DoRedirect = true });
        }
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, DoRedirect = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("404")]
        public IActionResult Error404()
        {
            return View("NotFound", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, DoRedirect = true });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("{code:int}")]
        public IActionResult Error(int code)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, DoRedirect = true });
        }
    }
}
