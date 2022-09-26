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
        private readonly SignInManager<Utilisateur> _signInManager;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly ApplicationDbContext _context;

        public GestionProfilController(
            SignInManager<Utilisateur> signInManager,
            UserManager<Utilisateur> userManager,
            ApplicationDbContext context
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retourne la page de création d'un nouvel utilisateur.
        /// </summary>
        /// <returns>Page d'inscription.</returns>
        [HttpGet]
        public IActionResult Inscription()
        {
            InscriptionVM vm = new() {
                ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom))
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> InscriptionAsync(InscriptionVM vm)
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
    }
}
