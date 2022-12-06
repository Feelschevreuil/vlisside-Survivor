using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace vlissides_bibliotheque.Controllers
{
    /// <summary>
    /// Classe <c>DocumentationController</c> oriente l'utilisateur 
    /// dans son utilisation du site.
    /// </summary>
    public class DocumentationController : Controller
    {
        /// <summary>
        /// Retourne la page de documentation pour un utilisateur.
        /// </summary>
        /// <returns>Page de documentation.</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
