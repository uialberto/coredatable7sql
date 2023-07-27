using Microsoft.AspNetCore.Mvc;

namespace AppWeb.Controllers.Security
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
