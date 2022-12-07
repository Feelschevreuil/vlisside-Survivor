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

                etudiant = GetLoggedInEtudiant();

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

                    achatVM.NomEtudiant = etudiant.Nom;

                    return View(achatVM);
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest();
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

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Annuler()
        {

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentCanceled)
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

                        FactureEtudiantService factureEtudiantService;
                        factureEtudiantService = new(_context);

                        factureEtudiantService.AnnulerFacture(factureEtudiant);

                        return Ok();
                    }
                }

                return BadRequest();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Confirmer()
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

                        factureEtudiant.Statut = StatutFactureEnum.TRANSIT;
                        _context.SaveChanges();
                    }
                    else
                    {

                        return BadRequest();
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

        /// <summary>
        /// Annule le paiement d'une commande si elle est encore en attente.
        /// </summary>
        /// <param name="id">Id de la <c>FactureEtudiant</c> à annuler.</param>
        public IActionResult Annuler(int id)
        {

            if(id > 0)
            {

                Etudiant etudiant;
                FactureEtudiant factureEtudiant;
                FacturesEtudiantsDAO factureEtudiantDAO;
                FactureEtudiantService factureEtudiantService;

                factureEtudiantService = new(_context);
                factureEtudiantDAO = new(_context);

                // TODO: sortir d'ici
                etudiant = _context
                    .Etudiants
                        .Include(etudiant => etudiant.Adresse)
                        .FirstOrDefault
                        (
                            etudiant => etudiant.Id == 
                                User.FindFirstValue(ClaimTypes.NameIdentifier)
                        );

                factureEtudiant = factureEtudiantDAO.Get(id);

                if
                (
                    factureEtudiant != null &&
                    factureEtudiant.Etudiant == etudiant
                )
                {

                    factureEtudiantService.AnnulerPaiement
                    (
                        factureEtudiant
                    );

                    return Ok();
                }
            }

            return BadRequest();
        }

        [Authorize(Roles = RolesName.Etudiant)]
        [HttpPost]
        public IActionResult ModifierAdresse([FromBody] AchatInformationsLivraisonDTO achatInformationsLivraisonDTO)
        {

            if(ModelState.IsValid)
            {
                
                Etudiant etudiant;
                FacturesEtudiantsDAO facturesEtudiantsDAO;
                FactureEtudiant factureEtudiant;

                facturesEtudiantsDAO = new(_context);

                etudiant = GetLoggedInEtudiant();

                factureEtudiant = facturesEtudiantsDAO
                    .Get(achatInformationsLivraisonDTO.FactureEtudiantId);

                if
                (
                    factureEtudiant != null &&
                    factureEtudiant.Etudiant == etudiant &&
                    factureEtudiant.Statut == StatutFactureEnum.ATTENTE_PAIEMENT
                )
                {

                    FactureEtudiantService factureEtudiantService;
                    AchatInformationsLivraisonVM achatInformationsLivraisonVM;
                    Adresse adresseInformationsModifications;
                    Adresse adresseModifiee;

                    factureEtudiantService = new(_context);

                    adresseInformationsModifications = AdresseService
                        .GetFromInformationsLivraison(achatInformationsLivraisonDTO);

                    adresseModifiee = factureEtudiantService
                        .ModifierAdresse
                        (
                            factureEtudiant,
                            adresseInformationsModifications
                        );

                    if(adresseModifiee != null)
                    {

                        achatInformationsLivraisonVM = new()
                        {
                            AdresseModifiee = true,
                            Ville = adresseModifiee.Ville,
                            NumeroCivique = adresseModifiee.NumeroCivique,
                            App = adresseModifiee.App,
                            Rue = adresseModifiee.Rue,
                            CodePostal = adresseModifiee.CodePostal
                        };
                    }
                    else
                    {

                        achatInformationsLivraisonVM = new()
                        {
                            Ville = achatInformationsLivraisonDTO.Ville,
                            NumeroCivique = achatInformationsLivraisonDTO.NumeroCivique,
                            App = achatInformationsLivraisonDTO.App,
                            Rue = achatInformationsLivraisonDTO.Rue,
                            CodePostal = achatInformationsLivraisonDTO.CodePostal
                        };
                    }

                    return PartialView("_AdresseCommande", achatInformationsLivraisonVM);
                }

                return Unauthorized();
            }
            else if(achatInformationsLivraisonDTO != null);
            {

                AchatInformationsLivraisonVM achatInformationsLivraisonVM;

                achatInformationsLivraisonVM = new()
                {
                    Ville = achatInformationsLivraisonDTO.Ville,
                    NumeroCivique = achatInformationsLivraisonDTO.NumeroCivique,
                    App = achatInformationsLivraisonDTO.App,
                    Rue = achatInformationsLivraisonDTO.Rue,
                    CodePostal = achatInformationsLivraisonDTO.CodePostal
                };

                return PartialView("_AdresseCommande", achatInformationsLivraisonVM);
            }

            return BadRequest();
        }

        [Authorize(Roles = RolesName.Etudiant)]       
        public IActionResult Historique(int page)
        {

            FacturesEtudiantsDAO facturesEtudiantsDAO;
            List<FactureEtudiant> facturesEtudiant;
            Etudiant etudiant;

            facturesEtudiantsDAO = new(_context);
            etudiant = GetLoggedInEtudiant();

            facturesEtudiant = facturesEtudiantsDAO
                .GetAllByEtudiant(etudiant).ToList();

            return View();
        }

        // TODO: sortir dans le DAO!
        private Etudiant GetLoggedInEtudiant()
        {

            Etudiant etudiant;

            etudiant = _context
                    .Etudiants
                        .Include(etudiant => etudiant.Adresse)
                        .FirstOrDefault
                        (
                            etudiant => etudiant.Id == 
                                User.FindFirstValue(ClaimTypes.NameIdentifier)
                        );

            return etudiant;
        }
    }
}
