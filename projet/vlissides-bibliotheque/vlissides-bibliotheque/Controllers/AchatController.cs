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

//TODO: delete DAO ref
using vlissides_bibliotheque.DAO;

namespace vlissides_bibliotheque.Controllers
{

    [Authorize(Roles = RolesName.Etudiant)]
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

        // POST: /achat/creer
        // GET pour le moment, pour tester
        [HttpPost]
        public async Task<IActionResult> Creer([FromBody] Panier panier)
        {

            if(ModelState.IsValid && panier.QuantitesLivresPositives())
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

                etudiant = await _userManagerEtudiant
                    .GetUserAsync(HttpContext.User);

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

                if(!CollectionUtils.CollectionNulleOuVide(commandesEtudiants))
                {

                    factureEtudiant = factureEtudiantService
                        .Creer(commandesEtudiants, facturesEtudiantsDAO);
                }

                if(commandeEtudiantService.CommandesValides(commandesEtudiants))
                {

                    factureEtudiant = factureEtudiantService.Creer
                    (
                        commandesEtudiants,
                        facturesEtudiantsDAO
                    );

                    achatVM = factureEtudiantService.CreerAchatVM
                    (
                        factureEtudiant,
                        commandesEtudiants
                    );

                    return View("Index2", achatVM);
                }

                return Content("Commandes invalides!");
            }

            return Content("What did you do?");
        }

        // GET: /paiement/test/
        // TODO: enlever! route uniquement pour le design!
        public async Task<IActionResult> Index2()
        {

            List<CommandePartielle> commandesPartielles;
            AchatVM paiementVM;

            Etudiant etudiant;

            etudiant = _context
                        .Etudiants
                            .Where
                            (
                                etudiant =>
                                    etudiant.Id == User
                                                    .FindFirstValue
                                                    (
                                                        ClaimTypes.NameIdentifier
                                                    )
                            )
                            .Include
                            (
                                etudiant => etudiant.Adresse
                            )
                            .FirstOrDefault();

            commandesPartielles = new()
            {
                new CommandePartielle() 
                {
                    EtatLivre = EtatLivreEnum.NEUF,
                    Isbn = "6666666669",
                    Prix = 69.99,
                    Quantite = 1,
                    Titre = "foobar 0"
                },
                new CommandePartielle() 
                {
                    EtatLivre = EtatLivreEnum.NUMERIQUE,
                    Isbn = "6666666669",
                    Prix = 69.99,
                    Quantite = 1,
                    Titre = "foobar 1"
                },
                new CommandePartielle() 
                {
                    EtatLivre = EtatLivreEnum.USAGE,
                    Isbn = "6666666669",
                    Prix = 69.99,
                    Quantite = 1,
                    Titre = "foobar 2"
                }
            };
            
            AchatInformationsLivraisonVM paiementInformationsLivraison;

            paiementInformationsLivraison = new()
            {
                AdresseLivraison = etudiant.Adresse,
                NumeroTelephone = etudiant.PhoneNumber
            };

            paiementVM = new()
            {
                PublicApiKey = "pk_test_51M59yHIKN7Q7tR49S3LZio0cmMAIwrANCcJ0SbvSQlfyslPoKR3GTHJmApQzoxlYKwqjNrDN3sfOFx7FS71o5i1T008GU1VoRg",
                ClientSecret = "pi_3M6LUeIKN7Q7tR491FXtOGwz_secret_IhEOrxoyxMFQOv63zlWpNEEKS",
                AchatInformationsLivraison = paiementInformationsLivraison,
                CommandesPartielles = commandesPartielles,
                Tvq = 0.0M,
                Tps = 0.05M,
                Total = 999.99,
                StatutFacture = StatutFactureEnum.ATTENTE_PAIEMENT
            };

            return View(paiementVM);
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
