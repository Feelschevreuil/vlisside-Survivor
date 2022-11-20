using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

//TODO: delete DAO ref
using vlissides_bibliotheque.DAO;

namespace vlissides_bibliotheque.Controllers
{

    [Authorize(Roles = RolesName.Etudiant)]
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

        // POST: /paiements
        // GET pour le moment, pour tester
        // [HttpPost]
        public async Task<IActionResult> Index2()//[FromBody] FactureEtudiantDTO factureEtudiantDTO)
        {

            /*
            if(ModelState.IsValid)
            {
            */
                Etudiant etudiant;
                FactureEtudiantService factureEtudiantService;
                ConfigurationService configurationService;
                CommandeEtudiantService commandeEtudiantService;
                PaiementVM paiementVM;

                configurationService = new
                (
                    ConstantesConfiguration.FICHIER_CONFIGURATION_PRINCIPAL
                );

                commandeEtudiantService = new(_context);

                etudiant = await _userManagerEtudiant
                    .GetUserAsync(HttpContext.User);

                factureEtudiantService = new
                (
                    _context, 
                    configurationService, 
                    commandeEtudiantService,
                    etudiant
                );

                paiementVM = factureEtudiantService
                    .CreateViewModel
                    (
                        new List<int>() {121, 115, 107}
                    );

                if(paiementVM != null)
                {

                    // TODO: retourner la page pour qu'il entre ses informations pour payer
                        
                    return View(paiementVM);
                }
                else
                {

                    // TODO: retourner un message d'erreur (aucun livre choisi diponible)
                    return StatusCode(401);
                }
                /*
            }
            */

            //return Content("What did you send? : " + userId2);
        }

        // GET: /paiement/test/
        // TODO: enlever! route uniquement pour le design!
        public async Task<IActionResult> Index()
        {

            List<CommandePartielleVM> commandesPartielles;
            PaiementVM paiementVM;
            Etudiant etudiant;

            etudiant = await _userManagerEtudiant
                    .GetUserAsync(HttpContext.User);

            commandesPartielles = new()
            {
                new CommandePartielleVM() 
                {
                    EtatLivre = EtatLivreEnum.NEUF,
                    Isbn = "6666666669",
                    PrixUnitaireGele = 69.99,
                    Quantite = 1,
                    Titre = "foobar 0"
                },
                new CommandePartielleVM() 
                {
                    EtatLivre = EtatLivreEnum.NUMERIQUE,
                    Isbn = "6666666669",
                    PrixUnitaireGele = 69.99,
                    Quantite = 1,
                    Titre = "foobar 1"
                },
                new CommandePartielleVM() 
                {
                    EtatLivre = EtatLivreEnum.USAGE,
                    Isbn = "6666666669",
                    PrixUnitaireGele = 69.99,
                    Quantite = 1,
                    Titre = "foobar 2"
                }
            };

            paiementVM = new()
            {
                PublicApiKey = "pk_test_51M59yHIKN7Q7tR49S3LZio0cmMAIwrANCcJ0SbvSQlfyslPoKR3GTHJmApQzoxlYKwqjNrDN3sfOFx7FS71o5i1T008GU1VoRg",
                PaymentIntentId = "pi_3M6LUeIKN7Q7tR491FXtOGwz",
                ClientSecret = "pi_3M6LUeIKN7Q7tR491FXtOGwz_secret_IhEOrxoyxMFQOv63zlWpNEEKS",
                AdresseLivraison = etudiant.Adresse,
                CommandesPartielles = commandesPartielles,
                Tvq = 0.0M,
                Tps = 0.05M,
                Total = 999.99,
                StatutFacture = StatusFacture.ATTENTE_PAIEMENT
            };

            return View(paiementVM);
        }
    }
}
