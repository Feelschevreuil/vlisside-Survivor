using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.DTO.Ajax;

namespace vlissides_bibliotheque.Controllers
{
    [AllowAnonymous]
    public class AccueilController : Controller
    {
        private readonly ILivreBibliotheque _livreService;
        private readonly IEvenementVM _evenementService;
        private readonly IPrix _prixService;
        private readonly IDAO<LivreBibliotheque> _livreDAO;


        public AccueilController(ILivreBibliotheque livreService, IEvenementVM evenementService, IPrix prixService, IDAO<LivreBibliotheque> livreDAO)
        {
            _livreService = livreService;
            _evenementService = evenementService;
            _prixService = prixService;
            _livreDAO = livreDAO;
        }
        [Route("")]
        public async Task<IActionResult> Accueil()
        {
            AccueilVM recommendationPromotions = new()
            {
                tuileLivreBibliotequeVMs = await _livreService.GetTuileLivreBibliotequeAccueil(),
                evenements = await _evenementService.GetEvenementAccueil(),
            };
            return View(recommendationPromotions);
        }

        public async Task<string> ChangerPrix([FromBody] PrixAfficher prixAfficher)
        {
            LivreBibliotheque livre = _livreDAO.GetById(prixAfficher.Id);
            List<PrixEtatLivre> etat = await _prixService.GetPrixLivre(prixAfficher.Id);
            PrixEtatLivre etatLivreRechercher = etat.Single(x => (int)x.EtatLivre == prixAfficher.Etat);
            string prix;

            if (etatLivreRechercher != null)
            {
                prix = etatLivreRechercher.Prix.ToString();
            }
            else
            {
                prix = "-";
            };

            return JsonConvert.SerializeObject(new PrixJson() { Id = prixAfficher.Id, prix = prix });
        }
    }
}
