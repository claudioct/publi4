using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Publi4.Controllers
{
    [Authorize(Roles = "Administrador,Redator,Cliente")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Minor()
        {
            return View();
        }

    }
}
