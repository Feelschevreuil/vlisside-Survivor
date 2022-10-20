using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using System.Collections;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

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
            List<Evenement> listEvenements = bdEvenements.OrderBy(i => i.Debut).Take(4).ToList();

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = GetTuileLivreBibliotequeVMs(), evenements = listEvenements };

            return View(recommendationPromotions);
        }

        public IActionResult Actualiter()
        {

            List<Evenement> listEvenements = new();
            listEvenements = _context.Evenements.ToList();
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
            IEnumerable<EvaluationLivre> bdEvaluationsLivre = _context.EvaluationsLivres;

            try
            {
                foreach (LivreBibliotheque livre in listQuatreLivre)
                {
                    TuileLivreBibliotequeVM tuileVM = new()
                    {
                        livreBibliotheque = livre,
                    };

                    if (bdCoursLivre.ToList().Find(x => x.LivreBibliothequeId == livre.LivreId) != null ) 
                    {
                        tuileVM.coursLivre = _context.CoursLivres
                            .Include(x=>x.Cours)
                            .Include(x=>x.Cours.ProgrammeEtude)
                            .ToList()
                            .Find(x => x.LivreBibliothequeId == livre.LivreId);
                        tuileVM.complementaire = bdCoursLivre.ToList().Find(x => x.LivreBibliothequeId == livre.LivreId).Complementaire;
                    }

                    if (tuileVM.complementaire)
                    {
                        tuileVM.livreEvaluation = bdEvaluationsLivre.ToList().FindAll(x => x.LivreBibliothequeId == livre.LivreId);
                    }
                    listTuileLivreBibliotequeVMs.Add(tuileVM);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            return listTuileLivreBibliotequeVMs;
        }
    }
}