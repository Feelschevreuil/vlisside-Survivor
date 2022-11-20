using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Constantes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Index()//[FromBody] FactureEtudiantDTO factureEtudiantDTO)
        {

            /*
            if(ModelState.IsValid)
            {
            */

                Etudiant etudiant;
                FactureEtudiantService factureEtudiantService;
                FactureEtudiant factureEtudiant;
                string userId;
                ConfigurationService configurationService;

                configurationService = new
                (
                    ConstantesConfiguration.FICHIER_CONFIGURATION_PRINCIPAL
                );

                etudiant = await _userManagerEtudiant
                    .GetUserAsync(HttpContext.User);

                factureEtudiantService = new(_context, configurationService);

                factureEtudiant = factureEtudiantService
                    .Create
                    (
                        etudiant, 
                        new List<int>() {121, 115, 107}
                    );

                if(factureEtudiant != null)
                {

                    // TODO: retourner la page pour qu'il entre ses informations pour payer
                    //
                    string apiKeyPublique;

                    apiKeyPublique = configurationService
                        .GetProprieteDeSection
                        (
                            ConstantesConfiguration.PROPRIETE_STRIPE, 
                            ConstantesConfiguration.PROPRIETE_STRIPE_CLE_API_PUBLIQUE
                        );
                        
                    return Content("u gotta pay now");
                }
                else
                {

                    // TODO: retourner un message d'erreur (aucun livre choisi diponible)

                    return Content("not valid");
                }
                

                /*
            }
            */

            //return Content("What did you send? : " + userId2);
        }
    }
}
