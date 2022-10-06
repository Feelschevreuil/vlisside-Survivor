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

            Adresse adresseFacturation = utilisateurCourant.GetAdresseFacturation(_context);

            GestionProfilVM vm = new() {
                Courriel = utilisateurCourant.Email,
                Nom = utilisateurCourant.Nom,
                Prenom = utilisateurCourant.Prenom,
                NoTelephone = utilisateurCourant.PhoneNumber,
                ProgrammeEtudeId = utilisateurCourant.ProgrammeEtudeId,
                ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom)),

                NoCivique = adresseFacturation.NumeroCivique.ToString(),
                Rue = adresseFacturation.Rue,
                Ville = adresseFacturation.Ville,
                App = adresseFacturation.App,
                CodePostal = adresseFacturation.CodePostal,
                ProvinceId = adresseFacturation.Province.ProvinceId,

                Provinces = new SelectList(_context.Province.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom)),
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(GestionProfilVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtudes));
            ModelState.Remove(nameof(vm.Provinces));

            if (ModelState.IsValid) {

                Etudiant utilisateurCourant = await GetUtilisateurCourantAsync();

                Adresse adresseFacturation = utilisateurCourant.GetAdresseFacturation(_context);

                adresseFacturation.App = vm.App;
                adresseFacturation.CodePostal = vm.CodePostal;
                adresseFacturation.NumeroCivique = Convert.ToInt32(vm.NoCivique);
                adresseFacturation.Rue = vm.Rue;
                adresseFacturation.Ville = vm.Ville;
                adresseFacturation.ProvinceId = vm.ProvinceId;

                utilisateurCourant.Email = vm.Courriel;
                utilisateurCourant.UserName = vm.Courriel;
                utilisateurCourant.Nom = vm.Nom;
                utilisateurCourant.Prenom = vm.Prenom;
                utilisateurCourant.PhoneNumber = vm.NoTelephone;
                utilisateurCourant.ProgrammeEtudeId = vm.ProgrammeEtudeId;

                _context.SaveChanges();
            }

            vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
            vm.Provinces = new SelectList(_context.Province.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));

            return View(vm);
        }

        private async Task<Etudiant> GetUtilisateurCourantAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
