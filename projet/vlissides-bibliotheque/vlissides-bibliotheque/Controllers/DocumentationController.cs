using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.ViewModels;

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
            List<DocumentationVM> Fonctionnalites = new() {
                new DocumentationVM() {
                    Titre = "Accueil",
                    Description = "La page d'accueil présente une vue d'ensemble des services de la plateforme.",
                    Url = "/",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Connexion",
                    Description = "",
                    Url = "/Connexion",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Inscription",
                    Description = "",
                    Url = "/Connexion/Inscription",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Gestion du profil",
                    Description = "",
                    Url = "/GestionProfil",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Bibliothèque",
                    Description = "",
                    Url = "/Inventaire/Bibliotheque",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Boutique étudiante",
                    Description = "",
                    Url = "/Usage/Usage",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Panier",
                    Description = "",
                    Url = "/Achat",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Événement",
                    Description = "",
                    Url = "/Evenement/Evenements",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Recommandations",
                    Description = "",
                    Url = "/",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Recherche de livres",
                    Description = "",
                    Url = "/",
                    Sections = new() {

                    }
                },
                new DocumentationVM() {
                    Titre = "Tableau de bord",
                    Description = "",
                    Url = "/TableauDeBord",
                    Sections = new() {

                    }
                }
            };
            return View(Fonctionnalites);
        }
    }
}
