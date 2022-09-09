using Microsoft.AspNetCore.Mvc;

namespace vlissides_bibliotheque.Controllers
{
    public class ConnexionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inscription()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
