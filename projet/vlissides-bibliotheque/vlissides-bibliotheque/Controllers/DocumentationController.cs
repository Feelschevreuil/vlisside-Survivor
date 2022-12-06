using Microsoft.AspNetCore.Mvc;

namespace vlissides_bibliotheque.Controllers
{
    public class DocumentationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
