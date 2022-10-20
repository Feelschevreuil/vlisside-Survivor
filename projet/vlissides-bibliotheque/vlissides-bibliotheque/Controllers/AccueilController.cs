using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
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
            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = GetTuileLivreBibliotequeVMs(), evenements = listEvenements };

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
            List<LivreBibliotheque> listLivreBibliotheque = _context.LivresBibliotheque.ToList();
            Random random = new Random();

            return listTuileLivreBibliotequeVMs;

        }
    }
}