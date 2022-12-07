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
                    Description = "Présente une vue d'ensemble des services de la plateforme.",
                    Url = "/",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = "",
                            LienImage = "acces-accueil.png"
                        },
                        new SectionDocumentationVM() {
                            Titre = "Recommandations",
                            Description = "Cette section offre un mince aperçu des livres disponibles en fonction de l'utilisateur connecté et des informations récoltés à son sujet selon ses activités sur la plateforme.",
                            LienImage = "accueil-recommandations.png"
                        },
                        new SectionDocumentationVM() {
                            Titre = "Événements",
                            Description = "Cette section montre sommairement les événements disponibles : des activités, des commanditaires ou encore des compétitions peuvent s'y retrouver.",
                            LienImage = "accueil-evenements.png"
                        }
                    }
                },
                new DocumentationVM() {
                    Titre = "Connexion",
                    Description = "Permet à un utilisateur déjà inscrit de se connecter à son compte.",
                    Url = "/Connexion",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = "connexion.png"
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = "Appuyer sur l'icône de connexion à l'extrême droite de la barre de navigation, puis sélectionner l'option \"Se connecter\".",
                            NB = "Un étudiant ou toute autre personne ne faisant pas partie du Collège de la connaissance aléatoire ne peuvent malheureusment pas se connecter, car il leur est impossible de se créer un compte.",
                            LienImage = "acces-connexion.png"
                        },
                    }
                },
                new DocumentationVM() {
                    Titre = "Inscription",
                    Description = "Permet à un utilisateur qui n'est pas déjà inscrit de se créer un compte pour ensuite s'y connecter.",
                    Url = "/Connexion/Inscription",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = "inscription.png"
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = "Appuyer sur l'icône de connexion à l'extrême droite de la barre de navigation, puis sélectionner l'option \"S'inscrire\".",
                            NB = "Un étudiant ou toute autre personne ne faisant pas partie du Collège de la connaissance aléatoire ne peuvent malheureusment pas se créer un compte.",
                            LienImage = "acces-connexion.png"
                        },
                        new SectionDocumentationVM() {
                            Titre = "Identification",
                            Description = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Sécurité",
                            Description = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Scolarité",
                            Description = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Adresse",
                            Description = ""
                        }
                    }
                },
                new DocumentationVM() {
                    Titre = "Gestion du profil",
                    Description = "Offre une vue d'ensemble sur les paramètres de l'utilisateur connecté. Permet aussi de mdifier ces paramètres.",
                    Url = "/GestionProfil",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = "Appuyer sur l'icône de profil à l'extrême droite de la barre de navigation, puis sélectionner l'option \"Profil\".",
                            LienImage = "acces-deconnexion.png"
                        },
                        new SectionDocumentationVM() {
                            Titre = "Identification",
                            Description = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Scolarité",
                            Description = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Adresse",
                            Description = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Se déconnecter",
                            Description = "Appuyer sur l'icône de profil à l'extrême droite de la barre de navigation, puis sélectionner l'option \"Déconnexion\".",
                            LienImage = "acces-deconnexion.png"
                        }
                    }
                },
                new DocumentationVM() {
                    Titre = "Bibliothèque",
                    Description = "",
                    Url = "/Inventaire/Bibliotheque",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = "",
                            LienImage = "acces-bibliotheque.png"
                        },
                        new SectionDocumentationVM() {
                            Titre = "Format",
                            Description = "Les livres sont offerts sous 3 formats différents au maximum, soient usagé, neuf ou numérique.",
                            NB = "Il est possible qu'un livre ne présente pas tous les formats selon les disponibilités en inventaire."
                        },
                        new SectionDocumentationVM() {
                            Titre = "Ajouter",
                            Description = "Possibilité d'ajouter un livre sous un format spécifique au panier."
                        },
                        new SectionDocumentationVM() {
                            Titre = "Supprimer",
                            Description = "Possibilité d'enlever un livre sous un format spécifique du panier."
                        }
                        
                    }
                },
                new DocumentationVM() {
                    Titre = "Boutique étudiante",
                    Description = "Offre la possibilité aux étudiants d'acheter des livres vendus par d'autres étudiants. Il est donc possible pour tous les étudiants de vendre ses propres livres.",
                    Url = "/Usage/Usage",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = "",
                            LienImage = "acces-boutique.png"
                        },
                        new SectionDocumentationVM() {
                            Titre = "Achat",
                            Description = "L'achat des livres des boutiques étudiantes ne dépend pas du système de la plateforme. Ainsi, la responsabilité revient aux étudiants de spécifier les modalités pour les moyens de transaction."
                        }
                    }
                },
                new DocumentationVM() {
                    Titre = "Panier",
                    Description = "Contient tous les livres désirés par l'utilisateur afin de lui permettre de les acheter.",
                    Url = "/Achat",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = ""
                        },
                    }
                },
                new DocumentationVM() {
                    Titre = "Événements",
                    Description = "Présente des actualités, des activités, des commanditaires ou encore des compétitions.",
                    Url = "/Evenement/Index",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = "",
                            LienImage = "acces-evenements.png"
                        },
                    }
                },
                new DocumentationVM() {
                    Titre = "Recherche de livres",
                    Description = "Permet de chercher des livres de manière plus spécifique.",
                    Url = "/",
                    Sections = new() {
                        new SectionDocumentationVM() {
                            Titre = "Vue d'ensemble",
                            LienImage = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Comment y accéder ?",
                            Description = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Recherche simple",
                            Description = ""
                        },
                        new SectionDocumentationVM() {
                            Titre = "Recherche avancée",
                            Description = ""
                        }
                    }
                }
            };
            return View(Fonctionnalites);
        }
    }
}
