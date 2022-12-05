using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class InventaireController : Controller
    {
        private readonly ILogger<InventaireController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LivresBibliothequeDAO _livresBibliothequeDAO;


        public InventaireController(ILogger<InventaireController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, LivresBibliothequeDAO livresBibliothequeDAO)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _livresBibliothequeDAO = livresBibliothequeDAO;
        }

        public IActionResult Bibliotheque()
        {
            List<TuileLivreBibliotequeVM> inventaireBibliotheque = new();
            List<LivreBibliotheque> BDlivreBibliotheques = _context.LivresBibliotheque
                .Include(x => x.MaisonEdition)
                .OrderByDescending(i => i.DatePublication)
                .ToList();
            List<CoursLivre> bdCoursLivre = _context.CoursLivres
               .Include(x => x.Cours)
               .Include(x => x.LivreBibliotheque)
               .Include(x => x.Cours.ProgrammeEtude)
               .ToList();
            List<AuteurLivre> bdAuteurLivres = _context.AuteursLivres
                .Include(x => x.Auteur)
                .Include(x => x.LivreBibliotheque)
                .ToList();
            List<PrixEtatLivre> bdPrixLivre = _context.PrixEtatsLivres
                .ToList();


            foreach (LivreBibliotheque livre in BDlivreBibliotheques)
            {
                var livreConvertie = livre.GetTuileLivreBibliotequeVMs(bdCoursLivre, bdPrixLivre, bdAuteurLivres);
                inventaireBibliotheque.Add(livreConvertie);
            };

            InventaireLivreBibliothequeVM inventaireLivreBibliotheque = new() { tuileLivreBiblioteques = inventaireBibliotheque };

            for (int i = 0; i < inventaireLivreBibliotheque.tuileLivreBiblioteques.Count; i++)
            {
                List<AuteurLivre> auteursLivresTrouve = bdAuteurLivres.FindAll(e => e.LivreBibliothequeId == inventaireLivreBibliotheque.tuileLivreBiblioteques[i].livreBibliotheque.LivreId);

                if (auteursLivresTrouve != null)
                {
                    if (bdAuteurLivres.Count > 0)
                    {
                        List<Auteur> auteurs = new List<Auteur>();
                        foreach (AuteurLivre auteurLivre in auteursLivresTrouve)
                        {
                            auteurs.Add(auteurLivre.Auteur);
                        }
                        inventaireLivreBibliotheque.tuileLivreBiblioteques[i].auteurs = auteurs;
                    }
                }

            }
            return View(inventaireLivreBibliotheque);
        }

        public IActionResult Detail(int id)
        {

            LivreBibliotheque livreBibliotheque = _context.LivresBibliotheque.ToList().Find(x => x.LivreId == id);
            if (livreBibliotheque != null)
            {
                List<CoursLivre> bdCoursLivre = _context.CoursLivres
                   .Include(x => x.Cours)
                   .Include(x => x.LivreBibliotheque)
                   .Include(x => x.Cours.ProgrammeEtude)
                   .ToList();
                List<AuteurLivre> bdAuteurLivres = _context.AuteursLivres
                    .Include(x => x.Auteur)
                    .Include(x => x.LivreBibliotheque)
                    .ToList();
                List<PrixEtatLivre> bdPrixLivre = _context.PrixEtatsLivres
                    .ToList();
                return View(LivreEnTuile.GetTuileLivreBibliotequeVMs(livreBibliotheque, bdCoursLivre, bdPrixLivre, bdAuteurLivres));
            }


            return Content("Ce livre n'existe pas dans la base de données.");
        }

        [Authorize(Roles = RolesName.Admin)]
        [HttpGet]
        public ActionResult creer()
        {

            AssocierLivreCours nouveauLivre = new AssocierLivreCours
            {
                checkBoxAuteurs = CheckedBox.GetAuteurs(_context),
                checkBoxCours = CheckedBox.GetCours(_context),
                MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context),
               
            };
            return View(nouveauLivre);
        }

        [Authorize(Roles = RolesName.Admin)]
        [HttpPost]
        public async Task<ActionResult> creer([FromBody] AssocierLivreCours form)
        {
            ModelState.Remove(nameof(AssocierLivreCours.MaisonsDeditions));
            ModelState.Remove(nameof(AssocierLivreCours.checkBoxCours));
            ModelState.Remove(nameof(AssocierLivreCours.checkBoxAuteurs));
            ModelState.Remove(nameof(AssocierLivreCours.DateFormater));

            if (ModelState.IsValid)
            {
                LivreBibliotheque nouveauLivreBibliothèque = new LivreBibliotheque()
                {
                    LivreId = 0,
                    MaisonEditionId = (int)form.MaisonDeditionId,
                    Isbn = form.ISBN,
                    Titre = form.Titre,
                    Resume = form.Resume,
                    PhotoCouverture = form.Photo,
                    DatePublication = form.DatePublication,
                };

                _context.LivresBibliotheque.Add(nouveauLivreBibliothèque);
                _context.SaveChanges();

                List<Cours> coursBD = _context.Cours.ToList();
                List<Auteur> auteursBD = _context.Auteurs.ToList();
                foreach (int coursId in form.Cours)
                {
                    Cours idCoursRechercher = coursBD.Find(x => x.CoursId == coursId);

                    CoursLivre nouvelleAssociation = new()
                    {
                        CoursLivreId = 0,
                        CoursId = idCoursRechercher.CoursId,
                        LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                    };

                    _context.CoursLivres.Add(nouvelleAssociation);
                    _context.SaveChanges();
                }

                foreach (int auteurId in form.Auteurs)
                {
                    Auteur idAuteursRechercher = auteursBD.Find(x => x.AuteurId == auteurId);

                    AuteurLivre nouvelleAssociation = new()
                    {
                        AuteurId = idAuteursRechercher.AuteurId,
                        LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                    };

                    _context.AuteursLivres.Add(nouvelleAssociation);
                    _context.SaveChanges();
                }

                _context.PrixEtatsLivres.AddRange(GestionPrix.AssocierPrixEtat(nouveauLivreBibliothèque, form, _context));
                _context.SaveChanges();
                return View("succesAjoutLivre", nouveauLivreBibliothèque);
            }

            
            form.MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context);
            form.checkBoxCours = CheckedBox.GetCours(_context);
            form.checkBoxAuteurs = CheckedBox.GetAuteurs(_context);
            return View(form);
        }


        [Authorize(Roles = RolesName.Admin)]
        [HttpGet]
        public async Task<ActionResult> modifier(long id)
        {
            if (id == null)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }

            LivreBibliotheque livreBibliothequeRechercher = _livresBibliothequeDAO.Get(id);
            AuteurLivre auteurLivre = _context.AuteursLivres.ToList().Find(x => x.LivreBibliothequeId == id);
            List<PrixEtatLivre> prixEtatLivre = _context.PrixEtatsLivres.ToList().FindAll(x => x.LivreBibliothequeId == id);

            ModificationLivreVM ModifierLivre = new()
            {
                IdDuLivre = livreBibliothequeRechercher.LivreId,
                MaisonDeditionId = livreBibliothequeRechercher.MaisonEditionId,
                MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context),
                DatePublication = livreBibliothequeRechercher.DatePublication,
                ISBN = livreBibliothequeRechercher.Isbn,
                Titre = livreBibliothequeRechercher.Titre,
                Resume = livreBibliothequeRechercher.Resume,
                Photo = livreBibliothequeRechercher.PhotoCouverture,
                PossedeNeuf = true,
                PossedeNumerique = true,
                checkBoxCours = CheckedBox.GetCoursLivre(_context, livreBibliothequeRechercher),
                checkBoxAuteurs = CheckedBox.GetAuteurs(_context),
            };

            var prixNeuf = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.NEUF);
            var prixDigital = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.NUMERIQUE);
            var prixUsage = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.USAGE);

            if (prixNeuf != null) { ModifierLivre.PrixNeuf = prixNeuf.Prix; } else { ModifierLivre.PrixNeuf = 0; };
            if (prixDigital != null) { ModifierLivre.PrixNumerique = prixDigital.Prix; } else { ModifierLivre.PrixNumerique = 0; };
            if (prixUsage != null) { ModifierLivre.PrixUsage = prixUsage.Prix; ModifierLivre.QuantiteUsagee = prixUsage.QuantiteUsage; } else { ModifierLivre.PrixUsage = 0; ModifierLivre.QuantiteUsagee = 0; };


            return View(ModifierLivre);

        }


        [Authorize(Roles = RolesName.Admin)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> modifier(ModificationLivreVM form)
        {
            ModelState.Remove(nameof(ModificationLivreVM.MaisonsDeditions));
            ModelState.Remove(nameof(ModificationLivreVM.checkBoxCours));
            ModelState.Remove(nameof(ModificationLivreVM.checkBoxAuteurs));

            LivreBibliotheque LivreBibliothèqueModifier = _livresBibliothequeDAO.Get(form.IdDuLivre);

            if (ModelState.IsValid)
            {
                LivreBibliothèqueModifier.MaisonEditionId = (int)form.MaisonDeditionId;
                LivreBibliothèqueModifier.Isbn = form.ISBN;
                LivreBibliothèqueModifier.Titre = form.Titre;
                LivreBibliothèqueModifier.Resume = form.Resume;
                LivreBibliothèqueModifier.PhotoCouverture = form.Photo;
                LivreBibliothèqueModifier.DatePublication = form.DatePublication;

                _context.LivresBibliotheque.Update(LivreBibliothèqueModifier);
                _context.SaveChanges();

                GestionPrix.UpdateLesPrix(LivreBibliothèqueModifier, form, _context);
                return View("succesModifierLivre", LivreBibliothèqueModifier);
            }

            var ModelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            foreach (var error in ModelErrors)
            {
                if (error.Exception == null)
                {
                    ModelState.AddModelError(string.Empty, "Le format d'un prix est incorrect. Voici un exemple du bon format: 10,45");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }

            form.MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context);
            form.checkBoxCours = CheckedBox.GetCoursLivre(_context, LivreBibliothèqueModifier);
            form.checkBoxAuteurs = CheckedBox.GetAuteursLivre(_context, LivreBibliothèqueModifier);
            return View(form);
        }

        [Authorize(Roles = RolesName.Admin)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> supprimer(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }

            var livreSupprimer = _context.LivresBibliotheque.ToList().Find(x => x.LivreId == id);
            if (livreSupprimer == null)
            {
                Response.StatusCode = 404;
                return Content("Ce livre n'existe pas dans la base de données");
            };


            List<CoursLivre> ListCoursRelier = _context.CoursLivres.ToList().FindAll(x => x.LivreBibliothequeId == livreSupprimer.LivreId);
            List<AuteurLivre> ListAuteursRelier = _context.AuteursLivres.ToList().FindAll(x => x.LivreBibliothequeId == livreSupprimer.LivreId);
            _context.CoursLivres.RemoveRange(ListCoursRelier);
            _context.AuteursLivres.RemoveRange(ListAuteursRelier);
            _context.LivresBibliotheque.Remove(livreSupprimer);
            _context.SaveChanges();

            return RedirectToAction("Bibliotheque");
        }

        [HttpPost]
        public string AssignerCoursLivre([FromBody] CoursAssocier coursAssocier)
        {
            List<Cours> listCours = _context.Cours.ToList();
            List<CoursLivre> listCoursLivre = _context.CoursLivres.ToList();
            List<CoursLivre> coursAssocierLivre = listCoursLivre.FindAll(x => x.LivreBibliothequeId == coursAssocier.livreId);
            List<Cours> listCoursCocher = new();
            List<Cours> listCoursDecocher = new();

            foreach (Cours cour in listCours)
            {
                Cours coursCocher = listCours.Find(x => x.CoursId == coursAssocier.CoursId.Find(x => x.Equals(cour.CoursId)));
                if (coursCocher != null)
                {
                    listCoursCocher.Add(coursCocher);
                }
                else
                {
                    listCoursDecocher.Add(cour);
                }
            }


            foreach (Cours cours in listCoursCocher)
            {
                if (coursAssocierLivre.Find(x => x.CoursId == cours.CoursId && x.LivreBibliothequeId == coursAssocier.livreId) == null)
                {
                    CoursLivre nouveauCoursLivre = new()
                    {
                        CoursId = cours.CoursId,
                        LivreBibliothequeId = coursAssocier.livreId
                    };
                    _context.CoursLivres.Add(nouveauCoursLivre);
                    _context.SaveChanges();
                }
            }

            foreach (Cours cours1 in listCoursDecocher)
            {
                CoursLivre coursLivre = coursAssocierLivre.Find(x => x.CoursId == cours1.CoursId && x.LivreBibliothequeId == coursAssocier.livreId);
                if (coursLivre != null)
                {
                    _context.CoursLivres.Remove(coursLivre);
                    _context.SaveChanges();
                }
            }
            return null;
        }
        [HttpPost]
        public string AssignerAuteursLivre([FromBody] AuteursAssocier auteursAssocier)
        {
            List<Auteur> listAuteurs = _context.Auteurs.ToList();
            List<AuteurLivre> listAuteursLivre = _context.AuteursLivres.ToList();
            List<AuteurLivre> auteursAssocierLivre = listAuteursLivre.FindAll(x => x.LivreBibliothequeId == auteursAssocier.livreId);
            List<Auteur> listAuteursCocher = new();
            List<Auteur> listAuteursDecocher = new();

            foreach (Auteur auteur in listAuteurs)
            {
                Auteur auteursCocher = listAuteurs.Find(x => x.AuteurId == auteursAssocier.AuteursId.Find(x => x.Equals(auteur.AuteurId)));
                if (auteursCocher != null)
                {
                    listAuteursCocher.Add(auteursCocher);
                }
                else
                {
                    listAuteursDecocher.Add(auteur);
                }
            }


            foreach (Auteur auteurs in listAuteursCocher)
            {
                if (auteursAssocierLivre.Find(x => x.AuteurId == auteurs.AuteurId && x.LivreBibliothequeId == auteursAssocier.livreId) == null)
                {
                    AuteurLivre nouveauAuteurLivre = new()
                    {
                        AuteurId = auteurs.AuteurId,
                        LivreBibliothequeId = auteursAssocier.livreId
                    };
                    _context.AuteursLivres.Add(nouveauAuteurLivre);
                    _context.SaveChanges();
                }
            }

            foreach (Auteur auteur1 in listAuteursDecocher)
            {
                AuteurLivre auteursLivre = auteursAssocierLivre.Find(x => x.AuteurId == auteur1.AuteurId && x.LivreBibliothequeId == auteursAssocier.livreId);
                if (auteursLivre != null)
                {
                    _context.AuteursLivres.Remove(auteursLivre);
                    _context.SaveChanges();
                }
            }
            return null;
        }

    }
}
