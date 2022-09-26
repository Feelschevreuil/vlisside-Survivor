using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    public class GestionProfilController : Controller
    {
        private readonly SignInManager<Etudiant> _signInManager;
        private readonly UserManager<Etudiant> _userManager;
        private readonly ApplicationDbContext _context;

        public GestionProfilController(
            SignInManager<Etudiant> signInManager,
            UserManager<Etudiant> userManager,
            ApplicationDbContext context
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Retourne la page de modification de l'étudiant courant.
        /// </summary>
        /// <returns>¨Page de modification d'étudiant.</returns>
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            Etudiant utilisateurCourant = await GetUtilisateurCourantAsync();

            Adresse adresseLivraison = utilisateurCourant.GetAdresseLivraison(_context);

            Adresse adresseFacturation = utilisateurCourant.GetAdresseFacturation(_context);

            GestionProfilVM vm = new() {
                Courriel = utilisateurCourant.Email,
                Nom = utilisateurCourant.Nom,
                Prenom = utilisateurCourant.Prenom,
                NoTelephone = utilisateurCourant.PhoneNumber,
                ProgrammeEtudeId = utilisateurCourant.ProgrammeEtudeId,
                ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom)),
                AdresseFacturationId = adresseFacturation.AdresseId,
                AdresseFacturation = adresseFacturation,
                AdresseLivraisonId = adresseLivraison.AdresseId,
                AdresseLivraison = adresseLivraison,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(InscriptionVM vm)
        {
            if (ModelState.IsValid) {

                Adresse adresse = new() {
                    App = vm.App,
                    CodePostal = vm.CodePostal,
                    NumeroCivique = Convert.ToInt32(vm.NoCivique),
                    Rue = vm.Rue,
                    Ville = vm.Ville
                };

                _context.Adresses.Add(adresse);
                _context.SaveChanges();

                // model binding
                Etudiant etudiant = new() {
                    Email = vm.Courriel,
                    UserName = vm.Courriel,
                    Nom = vm.Nom,
                    Prenom = vm.Prenom,
                    PhoneNumber = vm.NoTelephone,
                    ProgrammeEtudeId = vm.ProgrammeEtudeId,
                    AdresseFacturationId = adresse.AdresseId,
                    AdresseFacturation = adresse,
                    AdresseLivraisonId = adresse.AdresseId,
                    AdresseLivraison = adresse
                };

                // création
                var result = await _userManager.CreateAsync(etudiant, vm.Password);

                if (result.Succeeded) {

                    // ajouter rôle
                    await _userManager.AddToRoleAsync(etudiant, "Etudiant");

                    // connecter le nouvel étudiant
                    await _signInManager.SignInAsync(etudiant, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));

            return View(vm);
        }

        public async Task<Etudiant> GetUtilisateurCourantAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
