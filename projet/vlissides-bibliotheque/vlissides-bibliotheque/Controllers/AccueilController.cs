using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using Microsoft.AspNetCore.Authorization;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.DTO.Ajax;
using System.Text.Json;

namespace vlissides_bibliotheque.Controllers
{
    [AllowAnonymous]
    public class AccueilController : Controller
    {
        private readonly ILivreBibliotheque _livreService;
        private readonly IEvenementVM _evenementService;
        private readonly IDAO<LivreBibliotheque> _livreDAO;


        public AccueilController(ILivreBibliotheque livreService, IEvenementVM evenementService,IDAO<LivreBibliotheque> livreDAO)
        {
            _livreService = livreService;
            _evenementService = evenementService;
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
            string prix = "-";

            return JsonSerializer.Serialize(new PrixJson() { Id = prixAfficher.Id, prix = prix });
        }
    }
}
