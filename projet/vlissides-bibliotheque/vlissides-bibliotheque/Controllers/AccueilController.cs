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
            List<LivreBibliotheque> listeLivres  = new() 
            { 
                new LivreBibliotheque(){ Isbn ="1676362s",DateEdition=DateTime.Now,Resume="bio",Titre="DSA",Id=0,EtatLivreId=0, ProgrammeEtudeId=0},
                new LivreBibliotheque(){ Isbn="osidfids",DateEdition=DateTime.Today,Resume="fd",Titre="fsdfsd",Id=1,EtatLivreId=0,ProgrammeEtudeId=2},
                new LivreBibliotheque(){ Isbn="jshfiffdddddd",DateEdition=DateTime.MaxValue,Resume="fjd",Id=2,EtatLivreId=2,ProgrammeEtudeId=3}
            };

            return View(listeLivres);
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