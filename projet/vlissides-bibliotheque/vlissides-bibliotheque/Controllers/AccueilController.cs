using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.DAO;

namespace vlissides_bibliotheque.Controllers
{
    [AllowAnonymous]
    public class AccueilController : Controller
    {
        private readonly ILivreBibliotheque _livreService;
        private readonly IEvenementVM _evenementService;
        private readonly IDAO<LivreBibliotheque> _livreDAO;
        private readonly IDAO<PrixEtatLivre> _PrixEtatLivreDAO;


        public AccueilController(ILivreBibliotheque livreService, IEvenementVM evenementService,IDAO<LivreBibliotheque> livreDAO, IDAO<PrixEtatLivre> PrixEtatLivreDAO)
        {
            _livreService = livreService;
            _evenementService = evenementService;
            _livreDAO = livreDAO;
            _PrixEtatLivreDAO = PrixEtatLivreDAO;
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
            PrixEtatLivre? etatLivreRechercher = _PrixEtatLivreDAO.GetById(prixAfficher.Etat);
            string prix = "-";

            if (etatLivreRechercher != null)
                prix = etatLivreRechercher.Prix.ToString();

            return JsonConvert.SerializeObject(new PrixJson() { Id = prixAfficher.Id, prix = prix });
        }
    }
}
