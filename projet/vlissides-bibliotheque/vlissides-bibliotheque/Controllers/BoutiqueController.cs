﻿using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    public class BoutiqueController : Controller
    {
        private readonly ILogger<InventaireController> _logger;
        private readonly ApplicationDbContext _context;

        public BoutiqueController(ILogger<InventaireController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult ma_boutique()
        {
            List<Evenement> listEvenements = new()
            {
              

            };
            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = GetTuileLivreBibliotequeVMs(), evenements = listEvenements };

            return View(recommendationPromotions);

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
