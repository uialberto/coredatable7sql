using AppWeb.Models.AccountViewModels;
using AppWeb.Models.SecurityViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppWeb.Controllers
{
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
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
            return View(model);
        }
    }
}
