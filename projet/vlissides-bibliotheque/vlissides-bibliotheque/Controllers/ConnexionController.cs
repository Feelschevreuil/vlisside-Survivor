using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    /// <summary>
    /// Classe <c>ConnexionController</c> gère les url(s) pour les pages
    /// relatives à la connexion d'un utilisateur.
    /// </summary>
    [AllowAnonymous]
    public class ConnexionController : Controller
    {
        private readonly SignInManager<Utilisateur> _signInManager;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IUtilisateur _utilisateur;

        public ConnexionController(
            SignInManager<Utilisateur> signInManager,
            UserManager<Utilisateur> userManager,
            ApplicationDbContext context,
            IUtilisateur utilisateur
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _utilisateur = utilisateur;
        }

        /// <summary>
        /// Retourne la page de connexion pour un utilisateur.
        /// </summary>
        /// <returns>Page de connexion.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return View();
            }
            return Content("Imposssible de se connecter quand connecté.");
        }

        /// <summary>
        /// Valide la connexion de l'utilisateur.
        /// </summary>
        /// <returns>
        /// Succès : Page d'accueil avec utilisateur connecté.
        /// <br></br>
        /// Erreurs : Page de connexion avec messages d'erreur.
        /// </returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> IndexAsync(ConnexionVM vm)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    if (!result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Tentative de connexion invalide.");
                        return View(vm);
                    }
                }

                return View(vm);
            }

            return Content("Imposssible de se connecter quand connecté.");
        }

        /// <summary>
        /// Retourne la page de création d'un nouvel utilisateur.
        /// </summary>
        /// <returns>Page d'inscription.</returns>
        [HttpGet]
        public IActionResult Inscription()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return View(_utilisateur.NewInscriptionVM());
            }
            return Content("Imposssible de s'inscrire quand connecté.");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> InscriptionAsync(InscriptionVM vm)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                ModelState.Remove(nameof(vm.ProgrammeEtudes));
                ModelState.Remove(nameof(vm.Provinces));

                if (vm.CodePostal != null)
                {
                    vm.CodePostal = vm.CodePostal.ToUpper();
                }

                if (ModelState.IsValid)
                {

                    Adresse adresse = new()
                    {
                        App = vm.App,
                        CodePostal = vm.CodePostal,
                        NumeroCivique = Convert.ToInt32(vm.NoCivique),
                        Rue = vm.Rue,
                        Ville = vm.Ville,
                        ProvinceId = (int)vm.ProvinceId,
                    };

                    _context.Adresses.Add(adresse);
                    _context.SaveChanges();

                    // model binding
                    Etudiant etudiant = new()
                    {
                        Email = vm.Courriel,
                        UserName = vm.Courriel,
                        Nom = vm.Nom,
                        Prenom = vm.Prenom,
                        PhoneNumber = vm.NoTelephone,
                        ProgrammeEtudeId = (int)vm.ProgrammeEtudeId,
                        AdresseId = adresse.AdresseId,
                        Adresse = adresse,
                        EmailConfirmed = true
                    };

                    // création
                    var result = await _userManager.CreateAsync(etudiant, vm.Password);

                    if (result.Succeeded)
                    {

                        // ajouter rôle
                        await _userManager.AddToRoleAsync(etudiant, "Etudiant");

                        // connecter le nouvel étudiant
                        await _signInManager.SignInAsync(etudiant, isPersistent: false);
                        return RedirectToAction("Accueil", "Accueil");
                    }

                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "PasswordTooShort")
                        {
                            ModelState.AddModelError(string.Empty, "Le mot de passe doit être d'au moins 6 caractères.");
                        }
                        if (error.Code == "PasswordRequiresNonAlphanumeric")
                        {
                            ModelState.AddModelError(string.Empty, "Le mot de passe doit avoir au moins un caractère spécial (Ex: !, $, %, ?, &, *, etc...).");
                        }
                        if (error.Code == "PasswordRequiresLower")
                        {
                            ModelState.AddModelError(string.Empty, "Le mot de passe doit avoir au moins une lettre minuscule.");
                        }
                        if (error.Code == "PasswordRequiresUpper")
                        {
                            ModelState.AddModelError(string.Empty, "Le mot de passe doit avoir au moins une lettre majuscule.");
                        }
                    }
                }

                vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
                vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));

                return View(vm);
            }
            return Content("Imposssible de s'inscrire quand connecté.");
        }

        /// <summary>
        /// Retourne la page de déconnexion pour un utilisateur.
        /// </summary>
        /// <returns>Page d'accueil.</returns>
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return Redirect(@"~/");
        }
    }
}
