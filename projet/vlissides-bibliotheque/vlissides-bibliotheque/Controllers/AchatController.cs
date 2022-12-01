using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using vlissides_bibliotheque.Models.Achat;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;

//TODO: delete DAO ref
using vlissides_bibliotheque.DAO;

namespace vlissides_bibliotheque.Controllers
{

    public class AchatController: Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Etudiant> _userManagerEtudiant;

        public AchatController
        (
            ILogger<AccueilController> logger, 
            ApplicationDbContext context,
            UserManager<Etudiant> userManagerEtudiant
        )
        {

            _userManagerEtudiant = userManagerEtudiant;
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = RolesName.Etudiant)]
        public IActionResult Index(int id)
        {

            if(id > 0)
            {

                Etudiant etudiant;
                FacturesEtudiantsDAO facturesEtudiantsDAO;
                FactureEtudiantService factureEtudiantService;
                CommandeEtudiantService commandeEtudiantService;
                ConfigurationService configurationService;
                FactureEtudiant factureEtudiant;
                List<CommandeEtudiant> commandesEtudiants;
                AchatVM achatVM;

                facturesEtudiantsDAO = new(_context);

                configurationService = new
                (
                    ConstantesConfiguration.FICHIER_CONFIGURATION_PRINCIPAL
                );

                // TODO: sortir d'ici
                etudiant = _context
                    .Etudiants
                        .Include(etudiant => etudiant.Adresse)
                        .FirstOrDefault
                        (
                            etudiant => etudiant.Id == 
                                User.FindFirstValue(ClaimTypes.NameIdentifier)
                        );

                factureEtudiant = facturesEtudiantsDAO.Get(id);

                if(factureEtudiant != null && factureEtudiant.Etudiant == etudiant)
                {
                    commandeEtudiantService = new(_context);

                    factureEtudiantService = new
                    (
                        _context, configurationService, commandeEtudiantService, etudiant
                    );

                    //TODO: enlever red neck
                    commandesEtudiants = _context
                        .CommandesEtudiants
                        .Where
                        (
                            commandeEtudiant => 
                                commandeEtudiant.FactureEtudiantId == 
                                    factureEtudiant.FactureEtudiantId
                        )
                        .ToList();

                    achatVM = factureEtudiantService.CreerAchatVM
                    (
                        factureEtudiant,
                        commandesEtudiants
                    );

                    return View(achatVM);
                }
                else
                {
                    return Content("1");
                }
            }

            return StatusCode(401);
        }

        // POST: /achat/creer
        [HttpPost]
        [Authorize(Roles = RolesName.Etudiant)]
        public async Task<IActionResult> Creer([FromBody] Panier panier)
        {

            if(panier != null && panier.QuantitesLivresPositives())
            {

                //TODO: sortir dans un service
                Etudiant etudiant;
                FactureEtudiantService factureEtudiantService;
                FacturesEtudiantsDAO facturesEtudiantsDAO;
                ConfigurationService configurationService;
                CommandeEtudiantService commandeEtudiantService;
                List<CommandeEtudiant> commandesEtudiants;
                FactureEtudiant factureEtudiant;
                AchatVM achatVM;

                facturesEtudiantsDAO = new(_context);

                configurationService = new
                (
                    ConstantesConfiguration.FICHIER_CONFIGURATION_PRINCIPAL
                );

                commandeEtudiantService = new(_context);

                // TODO: sortir d'ici
                etudiant = _context
                    .Etudiants
                        .Include(etudiant => etudiant.Adresse)
                        .FirstOrDefault
                        (
                            etudiant => etudiant.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)
                        );

                factureEtudiantService = new
                (
                    _context, configurationService, commandeEtudiantService, etudiant
                );

                commandesEtudiants = new();

                if(!CollectionUtils.CollectionNulleOuVide(panier.Neufs))
                {

                    commandesEtudiants.AddRange
                    (
                        commandeEtudiantService
                            .GetCommandesEtudiantByLivreDesire(panier.Neufs, EtatLivreEnum.NEUF)
                    );
                }

                if(!CollectionUtils.CollectionNulleOuVide(panier.Numeriques))
                {

                    commandesEtudiants.AddRange
                    (
                        commandeEtudiantService
                            .GetCommandesEtudiantByLivreDesire(panier.Numeriques, EtatLivreEnum.NUMERIQUE)
                    );
                }

                if(!CollectionUtils.CollectionNulleOuVide(panier.Usages))
                {

                    commandesEtudiants.AddRange
                    (
                        commandeEtudiantService
                            .GetCommandesEtudiantByLivreDesire(panier.Usages, EtatLivreEnum.USAGE)
                    );
                }

                if
                (
                    !CollectionUtils.CollectionNulleOuVide(commandesEtudiants) && 
                        commandeEtudiantService.CommandesValides(commandesEtudiants)
                )
                {

                    factureEtudiant = factureEtudiantService.Creer
                    (
                        commandesEtudiants,
                        facturesEtudiantsDAO
                    );

                    // TODO: enum
                    return Content(factureEtudiant.FactureEtudiantId.ToString());
                }
            }

            return StatusCode(401);
        }

        [HttpPost]
        public IActionResult ConfirmerPaiement()
        {

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {

                    FactureEtudiant factureEtudiant;
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    // TODO: sortir dans service + DAO
                    factureEtudiant = _context
                        .FacturesEtudiants
                        .Where
                        (
                            factureEtudiant =>
                                factureEtudiant.PaymentIntentId == paymentIntent.Id
                        )
                        .FirstOrDefault();

                    if(factureEtudiant != null)
                    {

                        factureEtudiant.Statut == StatutFactureEnum.TRANSIT;
                        _context.SaveChanges();
                    }

                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

        /*
        [HttpPost]
        public IActionResult ModifierAdresseFacture(ConfirmerCommandeDTO confirmerCommandeDTO)
        {

            if(ModelState.IsValid)
            {
                
                FactureEtudiant factureEtudiant;
                

                factureEtudiant = 
                 
                return StatusCode(ConstantesStatusCodeHTTP.CORRECT);
            }
            else
            {

                PaiementInformationsLivraisonVM paiementInformationsLivraison;

                paiementInformationsLivraison = new()
                {
                    AdresseLivraison = confirmerCommandeDTO.AdresseLivraison,
                    NumeroTelephone = confirmerCommandeDTO.NumeroTelephone
                }

                Response.StatusCode = ConstantesStatusCodeHTTP.ERREURS_CHAMPS;

                return View("", paiementInformationsLivraison);
            }
        }
        */
    }
}
