using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using System.Collections;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Exercice_Ajax.DTO;
using Newtonsoft.Json;

namespace vlissides_bibliotheque.Controllers
{
    [AllowAnonymous]
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
            List<Evenement> listEvenements = _context.Evenements.OrderByDescending(i => i.Debut).Take(4).ToList();

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = LivreEnTuile.GetQuatreLivresVM(_context), evenements = GetEvenement.GetEvenements(listEvenements) };

            return View(recommendationPromotions);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string ChangerPrix([FromBody] PrixAfficher prixAfficher)
        {

            LivreBibliotheque livre = _context.LivresBibliotheque.Where(livre => livre.LivreId == prixAfficher.Id).FirstOrDefault();
            List<PrixEtatLivre> etat = _context.PrixEtatsLivres
                .Include(x=>x.LivreBibliotheque)
                .ToList()
                .FindAll(x => x.LivreBibliotheque.LivreId == livre.LivreId);

            // TODO: check if this thing even works x)
            PrixEtatLivre etatLivreRechercher = etat.Find(x=>(int)x.EtatLivre == prixAfficher.Etat);

            string prix;
            if (etatLivreRechercher != null)
            { 
                prix = etatLivreRechercher.Prix.ToString(); 
            }else{ 
                prix = "À venir"; 
            };

            PrixJson prixJson = new PrixJson() { Id = prixAfficher.Id, prix = prix };
            string json = JsonConvert.SerializeObject(prixJson);

            return json;
        }
    }
}
