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
        [HttpPost]
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

                    if(factureEtudiant != null)
                    {

                        achatVM = factureEtudiantService.CreerAchatVM
                        (
                            factureEtudiant,
                            commandesEtudiants
                        );

                        return View("Index2", achatVM);
                    }

                    return Content("Commandes invalides!");
                }

                return Content("Commandes invalides!");
            }

            return Content("What did you do?");
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
