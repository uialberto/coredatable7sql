using AppWeb.Models.AccountViewModels;
using AppWeb.Models.SecurityViewModels;
using AppWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using AppWeb.ViewModel;

namespace AppWeb.Controllers
{
    [AllowAnonymous]
    //[Route("sec")]
    public class SecurityController : Controller
    {
        private readonly ILogger<SecurityController> _logger;
        public SecurityController(ILogger<SecurityController> logger)
        {
            _logger = logger;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("unauthorized")]
        public IActionResult Error401()
        {
            return View("NotAuthorized", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, DoRedirect = true });
        }

        //[HttpGet("users")]
        public IActionResult Users()
        {
            var idUser = Guid.NewGuid().ToString();
            var model = new ProfileViewModel
            {
                Id = idUser,
                Username = User.Identity.Name,
                Nombres = User.Claims.FirstOrDefault(ele => ele.Type == ClaimTypes.GivenName)?.Value ?? string.Empty,
                Apellidos = User.Claims.FirstOrDefault(ele => ele.Type == ClaimTypes.Surname)?.Value ?? string.Empty,
                Claims = User.Claims,
                ChangePassword = new ChangePasswordViewModel()
                {
                    Id = idUser
                },
            };
            return View("Views/Security/Users.cshtml", new UsuarioViewModel());
        }

        [HttpGet("roles")]
        public IActionResult Roles()
        {
            var idUser = Guid.NewGuid().ToString();
            var model = new ProfileViewModel
            {
                Id = idUser,
                Username = User.Identity.Name,
                Nombres = User.Claims.FirstOrDefault(ele => ele.Type == ClaimTypes.GivenName)?.Value ?? string.Empty,
                Apellidos = User.Claims.FirstOrDefault(ele => ele.Type == ClaimTypes.Surname)?.Value ?? string.Empty,
                Claims = User.Claims,
                ChangePassword = new ChangePasswordViewModel()
                {
                    Id = idUser
                },
            };
            return View("Views/Security/Roles.cshtml", model);
        }
    }
}
