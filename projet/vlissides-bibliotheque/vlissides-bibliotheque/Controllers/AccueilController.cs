using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using System.Collections;

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
            IEnumerable<Evenement> bdEvenements = _context.Evenements;
            IEnumerable<Evenement> listEvenements = bdEvenements.OrderBy(i => i.Debut).Take(4);

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = GetTuileLivreBibliotequeVMs(), evenements = (List<Evenement>)listEvenements };

            return View(recommendationPromotions);
        }

        public IActionResult Actualiter()
        {
            
            List<Evenement> listEvenements = new()
            {
               
            };

            return View(listEvenements);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeVMs()
        {
            List<TuileLivreBibliotequeVM> listTuileLivreBibliotequeVMs = new();
            IEnumerable<LivreBibliotheque> listLivreBibliotheque = _context.LivresBibliotheque;
            IEnumerable<LivreBibliotheque> listQuatreLivre = listLivreBibliotheque.Take(4);
            IEnumerable<CoursLivre> bdCoursLivre = _context.CoursLivres;
            IEnumerable<EvaluationLivre> bdEvaluationLivre = _context.EvaluationsLivres;

            foreach (LivreBibliotheque livre in listQuatreLivre)
            {
                TuileLivreBibliotequeVM tuileVM = new()
                {
                    coursProfesseurs = _context.CoursProfesseurs.ToList().Find(x => x.CoursId == bdCoursLivre.ToList().Find(x => x.LivreBibliothequeId == livre.LivreId).CoursId),
                    complementaire = bdCoursLivre.ToList().Find(x=>x.LivreBibliothequeId == livre.LivreId).Complementaire
                };
                if (tuileVM.complementaire)
                {
                    tuileVM.livreBibliothequesEvaluation = bdEvaluationLivre.ToList().Find(x => x.LivreBibliothequeId == livre.LivreId);
                }
                listTuileLivreBibliotequeVMs.Add(tuileVM);
            }

            return listTuileLivreBibliotequeVMs;
        }
    }
}