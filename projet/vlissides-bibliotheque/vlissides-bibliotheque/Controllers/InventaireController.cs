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


        public InventaireController(ILogger<InventaireController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment,LivresBibliothequeDAO livresBibliothequeDAO)
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
            foreach (LivreBibliotheque livre in BDlivreBibliotheques)
            {
                var livreConvertie = livre.GetTuileLivreBibliotequeVMs(_context);
                inventaireBibliotheque.Add(livreConvertie);
            };

            InventaireLivreBibliotheque inventaireLivreBibliotheque = new() { tuileLivreBiblioteques = inventaireBibliotheque };
            return View(inventaireLivreBibliotheque);

        }

        public IActionResult Detail(int id)
        {

            LivreBibliotheque livreBibliotheque = _context.LivresBibliotheque.ToList().Find(x => x.LivreId == id);
            if (livreBibliotheque != null)
            {
                return View(LivreEnTuile.GetTuileLivreBibliotequeVMs(livreBibliotheque, _context));
            }


            return Content("Ce livre n'existe pas dans la base de données.");
        }

        [Authorize(Roles = RolesName.Admin)]
        [HttpGet]
        public ActionResult creer()
        {

            AssocierLivreCours nouveauLivre = new AssocierLivreCours { 
                Auteurs =ListDropDown.ListDropDownAuteurs(_context), 
                MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context),
            checkBoxCours = CoursCheckedBox.GetCours(_context)};
            return View(nouveauLivre);
        }

        [Authorize(Roles = RolesName.Admin)]
        [HttpPost]
        public async Task<ActionResult> creer([FromBody] AssocierLivreCours form)
        {
            ModelState.Remove("Auteurs");
            ModelState.Remove("MaisonsDeditions");
            ModelState.Remove("ListeCoursAssocie");
            ModelState.Remove("ListeCoursComplete");
            ModelState.Remove("checkBoxCours");
            

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

                AuteurLivre auteurLivre = new AuteurLivre()
                {
                    AuteurId =(int)form.AuteurId,
                    LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                };
                _context.AuteursLivres.Add(auteurLivre);
                _context.SaveChanges();

                _context.PrixEtatsLivres.AddRange(GestionPrix.AssocierPrixEtat(nouveauLivreBibliothèque, form,_context));
                _context.SaveChanges();


                return View("succesAjoutLivre", nouveauLivreBibliothèque);
            }

            form.Auteurs = ListDropDown.ListDropDownAuteurs(_context);
            form.MaisonsDeditions =ListDropDown.ListDropDownMaisonDedition(_context);
            form.checkBoxCours = CoursCheckedBox.GetCours(_context);
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
                AuteurId = auteurLivre.AuteurId,
                MaisonDeditionId = livreBibliothequeRechercher.MaisonEditionId,
                Auteurs = ListDropDown.ListDropDownAuteurs(_context),
                MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context),
                DatePublication = livreBibliothequeRechercher.DatePublication,
                ISBN = livreBibliothequeRechercher.Isbn,
                Titre = livreBibliothequeRechercher.Titre,
                Resume = livreBibliothequeRechercher.Resume,
                Photo = livreBibliothequeRechercher.PhotoCouverture,
                PossedeNeuf = true,
                PossedeNumerique = true,
                checkBoxCours = CoursCheckedBox.GetCoursLivre(_context, livreBibliothequeRechercher),

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
            ModelState.Remove("Auteurs");
            ModelState.Remove("MaisonsDeditions");
            ModelState.Remove("ListeCours");
            ModelState.Remove("checkBoxCours");

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

                AuteurLivre auteurLivre = _context.AuteursLivres.ToList().Find(x=>x.LivreBibliothequeId == form.IdDuLivre);
                if(auteurLivre != null && auteurLivre.AuteurId != form.AuteurId)
                {
                    _context.AuteursLivres.Remove(auteurLivre);
                    _context.SaveChanges();

                    AuteurLivre nouveauAuteurLivre = new()
                    {
                        AuteurId = (int)form.AuteurId,
                        LivreBibliothequeId = LivreBibliothèqueModifier.LivreId
                    };
                    _context.AuteursLivres.Add(nouveauAuteurLivre);
                    _context.SaveChanges();
                }

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

            form.Auteurs = ListDropDown.ListDropDownAuteurs(_context);
            form.MaisonsDeditions = ListDropDown.ListDropDownMaisonDedition(_context);
            form.checkBoxCours = CoursCheckedBox.GetCoursLivre(_context, LivreBibliothèqueModifier);
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
            _context.CoursLivres.RemoveRange(ListCoursRelier);
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

    }
}
