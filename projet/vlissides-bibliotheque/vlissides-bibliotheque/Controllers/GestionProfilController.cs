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
                NoCiviqueFacturation = adresseFacturation.NumeroCivique.ToString(),
                RueFacturation = adresseFacturation.Rue,
                VilleFacturation = adresseFacturation.Ville,
                AppFacturation = adresseFacturation.App,
                CodePostalFacturation = adresseFacturation.CodePostal,
                ProvinceFacturationId = adresseFacturation.Province.ProvinceId,
                NoCiviqueLivraison = adresseLivraison.NumeroCivique.ToString(),
                RueLivraison = adresseLivraison.Rue,
                VilleLivraison = adresseLivraison.Ville,
                AppLivraison = adresseLivraison.App,
                CodePostalLivraison = adresseLivraison.CodePostal,
                ProvinceLivraisonId = adresseLivraison.Province.ProvinceId,
                Provinces = new SelectList(_context.Province.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom)),
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(GestionProfilVM vm)
        {
            if (ModelState.IsValid) {

                Etudiant utilisateurCourant = await GetUtilisateurCourantAsync();

                Adresse adresseLivraison = utilisateurCourant.GetAdresseLivraison(_context);

                Adresse adresseFacturation = utilisateurCourant.GetAdresseFacturation(_context);

                adresseLivraison.App = vm.AppLivraison;
                adresseLivraison.CodePostal = vm.CodePostalLivraison;
                adresseLivraison.NumeroCivique = Convert.ToInt32(vm.NoCiviqueLivraison);
                adresseLivraison.Rue = vm.RueLivraison;
                adresseLivraison.Ville = vm.VilleLivraison;

                adresseFacturation.App = vm.AppFacturation;
                adresseFacturation.CodePostal = vm.CodePostalFacturation;
                adresseFacturation.NumeroCivique = Convert.ToInt32(vm.NoCiviqueFacturation);
                adresseFacturation.Rue = vm.RueFacturation;
                adresseFacturation.Ville = vm.VilleFacturation;

                utilisateurCourant.Email = vm.Courriel;
                utilisateurCourant.UserName = vm.Courriel;
                utilisateurCourant.Nom = vm.Nom;
                utilisateurCourant.Prenom = vm.Prenom;
                utilisateurCourant.PhoneNumber = vm.NoTelephone;
                utilisateurCourant.ProgrammeEtudeId = vm.ProgrammeEtudeId;

                _context.SaveChanges();
            }

            vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));

            return View(vm);
        }

        private async Task<Etudiant> GetUtilisateurCourantAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
