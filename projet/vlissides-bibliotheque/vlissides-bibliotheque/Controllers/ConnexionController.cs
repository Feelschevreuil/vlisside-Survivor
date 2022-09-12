using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    /// <summary>
    /// Classe <c>ConnexionController</c> gère les url(s) pour les pages
    /// relatives à la connexion d'un utilisateur.
    /// </summary>
    public class ConnexionController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public ConnexionController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Retourne la page de connexion pour un utilisateur.
        /// </summary>
        /// <returns>Page de connexion.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(ConnexionVM vm)
        {
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded) {
                    return View();
                }
                if (!result.IsLockedOut) {
                    ModelState.AddModelError(string.Empty, "Tentative de connexion invalide.");
                    return View(vm);
                }
            }

            return View(vm);
        }

        /// <summary>
        /// Retourne la page de création d'un nouvel utilisateur.
        /// </summary>
        /// <returns>Page d'inscription.</returns>
        public IActionResult Inscription()
        {
            return View();
        }

        /// <summary>
        /// Retourne la page de déconnexion pour un utilisateur.
        /// </summary>
        /// <returns>Page d'accueil.</returns>
        public IActionResult Logout()
        {
            return View();
        }
    }
}
