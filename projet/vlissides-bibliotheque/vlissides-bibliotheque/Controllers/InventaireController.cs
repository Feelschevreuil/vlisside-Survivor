using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class InventaireController : Controller
    {
        private readonly ILogger<InventaireController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InventaireController(ILogger<InventaireController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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

            CreationLivreVM nouveauLivre = new CreationLivreVM { Auteurs = ListDropDownAuteurs(), MaisonsDeditions = ListDropDownMaisonDedition(), ListeCoursComplete = ListDropDownCours() };
            return View(nouveauLivre);
        }

        [Authorize(Roles = RolesName.Admin)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> creer(CreationLivreVM form)
        {
            ModelState.Remove("Auteurs");
            ModelState.Remove("MaisonsDeditions");
            ModelState.Remove("ListeCours");
            ModelState.Remove("ListeCoursAssocie");
            ModelState.Remove("ListeCoursComplete");
            ModelState.Remove("CoursId");

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

                //CoursLivre nouvelleAssociation = new()
                //{
                //    CoursLivreId = 0,
                //    CoursId = (int)form.CoursId,
                //    LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                //    Complementaire = form.Obligatoire
                //};

                //_context.CoursLivres.Add(nouvelleAssociation);
                //_context.SaveChanges();


                AuteurLivre auteurLivre = new AuteurLivre()
                {
                    AuteurId = (int)form.AuteurId,
                    LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                };
                _context.AuteursLivres.Add(auteurLivre);
                _context.SaveChanges();

                _context.PrixEtatsLivres.AddRange(AssocierPrixEtat(nouveauLivreBibliothèque, form));
                _context.SaveChanges();


                return View("succesAjoutLivre", nouveauLivreBibliothèque);
            }
            form.Auteurs = ListDropDownAuteurs();
            form.ListeCoursComplete = ListDropDownCours();
            form.MaisonsDeditions = ListDropDownMaisonDedition();
            return View(form);

        }


        [Authorize(Roles = RolesName.Admin)]
        [HttpGet]
        public async Task<ActionResult> modifier(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }

            LivreBibliotheque livreBibliothequeRechercher = _context.LivresBibliotheque.ToList().Find(x => x.LivreId == id);
            AuteurLivre auteurLivre = _context.AuteursLivres.ToList().Find(x => x.LivreBibliothequeId == id);
            CoursLivre coursLivre = _context.CoursLivres.ToList().Find(x => x.LivreBibliothequeId == id);
            List<PrixEtatLivre> prixEtatLivre = _context.PrixEtatsLivres.ToList().FindAll(x => x.LivreBibliothequeId == id);
            List<EtatLivre> etatLivres = _context.EtatsLivres.ToList();


            int idLivreNeuf = etatLivres.Find(y => y.Nom == NomEtatLivre.NEUF).EtatLivreId;
            int idLivreNumerique = etatLivres.Find(y => y.Nom == NomEtatLivre.DIGITAL).EtatLivreId;
            int idLivreUsager = etatLivres.Find(y => y.Nom == NomEtatLivre.USAGE).EtatLivreId;


            var pasEtatAuLivre = _context.PrixEtatsLivres.ToList().FindAll(x => x.LivreBibliotheque.LivreId == livreBibliothequeRechercher.LivreId);
            var PasNumerique = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreNumerique);
            var pasUsager = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreUsager);
            var pasDeNeuf = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreNeuf);



            ModificationLivreVM ModifierLivre = new()
            {
                IdDuLivre = livreBibliothequeRechercher.LivreId,
                AuteurId = auteurLivre.AuteurId,
                MaisonDeditionId = livreBibliothequeRechercher.MaisonEditionId,
                Auteurs = ListDropDownAuteurs(),
                ListeCours = ListDropDownCours(),
                MaisonsDeditions = ListDropDownMaisonDedition(),
                DatePublication = livreBibliothequeRechercher.DatePublication,
                ISBN = livreBibliothequeRechercher.Isbn,
                Titre = livreBibliothequeRechercher.Titre,
                Resume = livreBibliothequeRechercher.Resume,
                Photo = livreBibliothequeRechercher.PhotoCouverture,
                PossedeNeuf = true,
                PossedeNumerique = true,
                checkBoxCours = CoursCheckedBox.GetCoursLivre(_context, livreBibliothequeRechercher),

            };

            var prixNeuf = prixEtatLivre.Find(x => x.EtatLivreId == idLivreNeuf);
            var prixDigital = prixEtatLivre.Find(x => x.EtatLivreId == idLivreNumerique);
            var prixUsage = prixEtatLivre.Find(x => x.EtatLivreId == idLivreUsager);

            if (prixNeuf != null) { ModifierLivre.PrixNeuf = prixNeuf.Prix; } else { ModifierLivre.PrixNeuf = 0; };
            if (prixDigital != null) { ModifierLivre.PrixNumerique = prixDigital.Prix; } else { ModifierLivre.PrixNumerique = 0; };
            if (prixUsage != null) { ModifierLivre.PrixUsage = prixUsage.Prix; } else { ModifierLivre.PrixUsage = 0; };


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
            ModelState.Remove("CoursId");

            LivreBibliotheque LivreBibliothèqueModifier = _context.LivresBibliotheque.ToList().Find(x => x.LivreId == form.IdDuLivre);

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
                        AuteurId = (int) form.AuteurId,
                        LivreBibliothequeId = LivreBibliothèqueModifier.LivreId
                    };
                    _context.AuteursLivres.Add(nouveauAuteurLivre);
                    _context.SaveChanges();
                }

                UpdateLesPrix(LivreBibliothèqueModifier, form);
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

            form.Auteurs = ListDropDownAuteurs();
            form.ListeCours = ListDropDownCours();
            form.MaisonsDeditions = ListDropDownMaisonDedition();
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

        public List<SelectListItem> ListDropDownAuteurs()
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un auteur" });

            foreach (var e in _context.Auteurs)
                Liste.Add(new SelectListItem { Value = e.AuteurId.ToString(), Text = e.Nom + ", " + e.Prenom });

            return Liste;
        }

        public List<SelectListItem> ListDropDownMaisonDedition()
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez une maison d'édition" });

            foreach (var e in _context.MaisonsEdition)
                Liste.Add(new SelectListItem { Value = e.MaisonEditionId.ToString(), Text = e.Nom });

            return Liste;
        }

        public List<SelectListItem> ListDropDownCours()
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un cours" });

            foreach (var e in _context.Cours)
                Liste.Add(new SelectListItem { Value = e.CoursId.ToString(), Text = e.Code + " " + e.Nom + " " + e.AnneeParcours });

            return Liste;
        }

        public List<PrixEtatLivre> AssocierPrixEtat(LivreBibliotheque LivreEtatPrix, CreationLivreVM form)
        {
            List<PrixEtatLivre> ListPrixEtat = new();

            if (form.PrixNeuf != null && form.PrixNeuf != 0)
            {
                PrixEtatLivre AssociationPrixNeuf = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.NEUF).EtatLivreId,
                    Prix = form.PrixNeuf,
                };
                ListPrixEtat.Add(AssociationPrixNeuf);
            }
            if (form.PrixNumerique == null && form.PrixNumerique != 0)
            {
                PrixEtatLivre AssociationPrixNumérique = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.DIGITAL).EtatLivreId,
                    Prix = form.PrixNumerique,
                };
                ListPrixEtat.Add(AssociationPrixNumérique);
            }
            if (form.PrixUsage == null && form.PrixUsage != 0)
            {
                PrixEtatLivre AssociationPrixUsager = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.USAGE).EtatLivreId,
                    Prix = form.PrixUsage,
                };
                ListPrixEtat.Add(AssociationPrixUsager);
            }

            return ListPrixEtat;
        }
        public bool UpdateLesPrix(LivreBibliotheque LivreEtatPrix, ModificationLivreVM form)
        {
            if (form.PrixNeuf == null) { form.PrixNeuf = 0; };
            if (form.PrixNumerique == null) { form.PrixNumerique = 0; };
            if (form.PrixUsage == null) { form.PrixUsage = 0; form.QuantiteUsagee = 0; };

            List<PrixEtatLivre> listPrixEtat = _context.PrixEtatsLivres
                .Include(x => x.LivreBibliotheque)
                .Include(x => x.EtatLivre)
                .ToList();


            PrixEtatLivre prixNeuf = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.NEUF);

            PrixEtatLivre prixDigital = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.DIGITAL);

            PrixEtatLivre prixUsager = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.USAGE);

            if (prixNeuf != null)
            {
                prixNeuf.Prix = (double)form.PrixNeuf;
                _context.PrixEtatsLivres.Update(prixNeuf);
                _context.SaveChanges();
            }
            if (prixDigital != null)
            {
                prixDigital.Prix = (double)form.PrixNumerique;
                _context.PrixEtatsLivres.Update(prixDigital);
                _context.SaveChanges();
            }
            if (prixUsager != null)
            {
                prixUsager.Prix = (double)form.PrixUsage;
                _context.PrixEtatsLivres.Update(prixUsager);
                _context.SaveChanges();
            }


            return true;
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