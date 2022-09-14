using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

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

            List<EvaluationLivre> listeLivres  = new() 
            { 
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoile=7,Date=DateTime.Now,Titre="",Id=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DateEdition=DateTime.Now,Resume="bio",Titre="DSA",Id=0,EtatLivreId=0, ProgrammeEtudeId=0}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoile=9,Date=DateTime.Now,Titre="",Id=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn="osidfids",DateEdition=DateTime.Today,Resume="fd",Titre="fsdfsd",Id=1,EtatLivreId=0,ProgrammeEtudeId=2 } },
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoile=1,Date=DateTime.Now,Titre="",Id=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn="jshfiffdddddd",DateEdition=DateTime.MaxValue,Titre="AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",Resume="fjd",Id=2,EtatLivreId=2,ProgrammeEtudeId=3} }
            };

            List<Commanditaire> commanditaires = new()
            {
                new Commanditaire(){Courriel="aaaaaaa@gmail.cum",Id=0,Message="VENEZ ACHETER NOS DÉLICIEUX BISCUITS",Nom="BakeryChezMarki's",Url="http//BiscuitsChezMary's.cum" }

            };

            RecommendationPromotionsVM recommendationPromotions = new() { livreBibliothequesEvaluation=listeLivres, commanditaires=commanditaires };

            return View(recommendationPromotions);
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