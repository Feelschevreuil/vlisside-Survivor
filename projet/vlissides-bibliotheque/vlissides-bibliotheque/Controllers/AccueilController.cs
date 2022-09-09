using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Controllers
{
    public class AccueilController : Controller
    {
        private readonly ILogger<AccueilController> _logger;

        public AccueilController(ILogger<AccueilController> logger)
        {
            _logger = logger;
        }
        [Route("")]
        public IActionResult Accueil()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}