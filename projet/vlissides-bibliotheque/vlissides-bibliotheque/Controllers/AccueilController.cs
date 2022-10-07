using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    public class AccueilController : Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly ApplicationDbContext _context;

        public AccueilController(ILogger<AccueilController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Route("")]
        public IActionResult Accueil()
        {

            Commanditaire commanditaire = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };


            List<Evenement> listEvenements = new()
            {

               new Evenement(){EvenementId=0,Commanditaire= commanditaire,CommanditaireId=0,Debut=DateTime.Now, Fin=DateTime.MaxValue,Description=commanditaire.Message,Nom="Pomme"}


            };

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = GetTuileLivreBibliotequeVMs(), evenements = listEvenements };

            return View(recommendationPromotions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Actualiter()
        {
            Commanditaire commanditaire = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };



            List<Evenement> listEvenements = new()
            {

               new Evenement(){EvenementId=0,Commanditaire= commanditaire,CommanditaireId=0,Debut=DateTime.Now, Fin=DateTime.MaxValue,Description=commanditaire.Message,Nom="Pomme"},

               new Evenement(){EvenementId=0,Commanditaire= commanditaire,CommanditaireId=0,Debut=DateTime.MinValue, Fin=DateTime.MaxValue,Description=commanditaire.Message,Nom="Banane"}


            };


            return View(listEvenements);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public List<CoursProfesseur> GetCoursProfesseurs()
        {
            List<CoursProfesseur> listCoursProfesseurs = new();
            List<Cours> listCours = _context.Cours.ToList();
            List<Professeur> listProfesseur = _context.Professeur.ToList();
            for (int i = 0; i < _context.Cours.Count(); i++)
            {
                listCoursProfesseurs.Add(new CoursProfesseur { Cours = listCours[i], Professeur = new Professeur { Nom= "Berry", Prenom="Jerry"} });

            }

            return listCoursProfesseurs;
        }


        public List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeVMs()
        {
            List<TuileLivreBibliotequeVM> listTuileLivreBibliotequeVMs = new();


            LivreBibliotheque livreBibliotheque = _context.LivresBibliotheques.First();
            Evaluation evaluation = new() { Etoiles = 4 };
            EvaluationLivre UneEvaluationLivre = new() { LivreBibliotheque = livreBibliotheque, Evaluation = evaluation };


            listTuileLivreBibliotequeVMs.Add(new TuileLivreBibliotequeVM { livreBibliothequesEvaluation = UneEvaluationLivre, coursProfesseurs = GetCoursProfesseurs() });



            return listTuileLivreBibliotequeVMs;

        }
    }
}