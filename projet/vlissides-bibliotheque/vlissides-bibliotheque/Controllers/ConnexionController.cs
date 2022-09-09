using Microsoft.AspNetCore.Mvc;

namespace vlissides_bibliotheque.Controllers
{
    /// <summary>
    /// Classe <c>ConnexionController</c> gère les url(s) pour les pages
    /// relatives à la connexion d'un utilisateur.
    /// </summary>
    public class ConnexionController : Controller
    {
        /// <summary>
        /// Retourne la page de connexion pour un utilisateur.
        /// </summary>
        /// <returns>Page de connexion.</returns>
        public IActionResult Index()
        {
            return View();
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
