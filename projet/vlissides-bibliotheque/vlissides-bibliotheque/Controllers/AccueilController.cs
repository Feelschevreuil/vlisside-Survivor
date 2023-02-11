using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Exercice_Ajax.DTO;
using Newtonsoft.Json;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Interface;
using vlissides_bibliotheque.Extensions;

namespace vlissides_bibliotheque.Controllers
{
    [AllowAnonymous]
    public class AccueilController : Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ILivreEnTuile _livre;
        private readonly IEvenementVM _evenement;


        public AccueilController(ILogger<AccueilController> logger, ApplicationDbContext context, ILivreEnTuile livre, IEvenementVM evenement)
        {
            _logger = logger;
            _context = context;
            _livre = livre;
            _evenement = evenement;
        }
        [Route("")]
        public async Task<IActionResult> Accueil()
        {
            AccueilVM recommendationPromotions = new()
            {
                tuileLivreBibliotequeVMs = await _livre.GetTuileLivreBibliotequeAccueil(),
                evenements = await _evenement.GetEvenementAccueil(),
            };
            return View(recommendationPromotions);
        }

        public string ChangerPrix([FromBody] PrixAfficher prixAfficher)
        {
            LivreBibliotheque livre = _context.LivresBibliotheque.Where(livre => livre.LivreId == prixAfficher.Id).FirstOrDefault();
            List<PrixEtatLivre> etat = _context.PrixEtatsLivres
                .Include(x => x.LivreBibliotheque)
                .Where(x => x.LivreBibliotheque.LivreId == livre.LivreId)
                .ToList();
            PrixEtatLivre etatLivreRechercher = etat.Find(x => (int)x.EtatLivre == prixAfficher.Etat);

            string prix;
            if (etatLivreRechercher != null)
            {
                prix = etatLivreRechercher.Prix.ToString();
            }
            else
            {
                prix = "À venir";
            };

            PrixJson prixJson = new PrixJson() { Id = prixAfficher.Id, prix = prix };
            string json = JsonConvert.SerializeObject(prixJson);

            return json;
        }
    }
}
