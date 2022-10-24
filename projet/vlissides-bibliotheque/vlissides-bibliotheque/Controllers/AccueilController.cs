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
using System.Xml.Linq;
using Exercice_Ajax.DTO;
using Newtonsoft.Json;

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
            List<Evenement> listEvenements = _context.Evenements.OrderBy(i => i.Debut).Take(4).ToList();

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = GetQuatreLivres.GetInventaireBibliotequeVMs(_context), evenements = listEvenements };

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




            return listTuileLivreBibliotequeVMs;
        }

        public string ChangerPrix([FromBody] PrixAfficher prixAfficher)
        {
            LivreBibliotheque livre = _context.LivresBibliotheque.ToList().Find(x => x.LivreId == prixAfficher.Id);
            List<PrixEtatLivre> etat = _context.PrixEtatsLivres.ToList().FindAll(x => x.LivreBibliotheque.LivreId == livre.LivreId);


            PrixEtatLivre etatLivreRechercher = etat.Find(x => x.EtatLivreId == _context.EtatsLivres.ToList().Find(x => x.Nom == prixAfficher.Etat).EtatLivreId);

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