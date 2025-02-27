using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using vlissides_bibliotheque.Commun;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    [AllowAnonymous]
    public class AccueilController : Controller
    {
        private readonly ILivreBibliotheque _livreService;
        private readonly IEvenementVM _evenementService;


        public AccueilController(ILivreBibliotheque livreService, IEvenementVM evenementService)
        {
            _livreService = livreService;
            _evenementService = evenementService;
        }


        [Route("")]
        public IActionResult Accueil()
        {
            AccueilVM recommendationPromotions = new()
            {
                tuileLivreBibliotequeVMs = _livreService.GetTuileLivreBibliotequeAccueil(),
                evenements = _evenementService.GetEvenementAccueil(),
            };

            return View(recommendationPromotions);
        }

        public string ChangerPrix([FromBody] PrixAfficher prixAfficher)
        {
            string prix = "-";
            var livre = _livreService.GetLivre(prixAfficher.Id);
            
            switch (prixAfficher.Etat)
            {
                case (int)Enumeration.EtatLivreEnum.NEUF:
                    prix = livre.Prix.PrixNeuf.ToString("F2");
                    break;

                case (int)Enumeration.EtatLivreEnum.NUMERIQUE:
                    prix = livre.Prix.PrixNumerique.ToString("F2");
                    break;

                case (int)Enumeration.EtatLivreEnum.USAGE:
                    prix = livre.Prix.PrixUsager.ToString("F2");
                    break;
            }

            return JsonSerializer.Serialize(new PrixJson() { Id = prixAfficher.Id, prix = prix });
        }
    }
}
