using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace vlissides_bibliotheque.Controllers
{

    [Authorize]
    public class PaiementController : Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly ApplicationDbContext _context;
        // TODO: both are separated??
        private readonly UserManager<Etudiant> _userManagerEtudiant;
        private readonly UserManager<Utilisateur> _userManagerAdmin;

        public PaiementController
        (
            ILogger<AccueilController> logger, 
            ApplicationDbContext context,
            UserManager<Etudiant> userManagerEtudiant,
            UserManager<Utilisateur> userManagerAdmin
        )
        {

            _userManagerAdmin = userManagerAdmin;
            _userManagerEtudiant = userManagerEtudiant;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult Index ([FromBody] FactureEtudiantDTO factureEtudiantDTO)
        {

            if(ModelState.IsValid)
            {

                FactureEtudiantService factureEtudiantService;
                FactureEtudiant factureEtudiant;
                string userId;

                userId = User.FindFirst(ClaimTypes.NameIdentifier).ToString();

                factureEtudiantService = new(_context);
                factureEtudiant = factureEtudiantService
                    .Create
                    (
                        userId, 
                        factureEtudiantDTO.PrixEtatsLivres
                    );

                if(factureEtudiant != null)
                {

                    // TODO: retourner la page pour qu'il entre ses informations pour payer
                    
                    return Content("u gotta pay now");
                }
                else
                {

                    // TODO: retourner un message d'erreur (aucun livre choisi diponible)

                    return Content("not valid");
                }
            }

            return Content("What did you send?");
        }
    }
}
