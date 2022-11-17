using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
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

            AssocierLivreCours nouveauLivre = new AssocierLivreCours { 
                Auteurs = ListDropDownAuteurs(), 
                MaisonsDeditions = ListDropDownMaisonDedition(),
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
                foreach (int coursId in form.CoursId)
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

                _context.PrixEtatsLivres.AddRange(AssocierPrixEtat(nouveauLivreBibliothèque, form));
                _context.SaveChanges();


                return View("succesAjoutLivre", nouveauLivreBibliothèque);
            }

            form.Auteurs = ListDropDownAuteurs();
            form.MaisonsDeditions = ListDropDownMaisonDedition();
            form.checkBoxCours = CoursCheckedBox.GetCours(_context);
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

            var prixNeuf = prixEtatLivre.Find(x => x.EtatLivreId == idLivreNeuf);
            var prixDigital = prixEtatLivre.Find(x => x.EtatLivreId == idLivreNumerique);
            var prixUsage = prixEtatLivre.Find(x => x.EtatLivreId == idLivreUsager);

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
                        AuteurId = (int)form.AuteurId,
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
            if (form.PrixNumerique != null && form.PrixNumerique != 0)
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
            if (form.PrixUsage != null && form.PrixUsage != 0)
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
            List<PrixEtatLivre> listPrixEtat = _context.PrixEtatsLivres
                .Include(x => x.LivreBibliotheque)
                .Include(x => x.EtatLivre)
                .ToList();

            PrixEtatLivre prixNeuf = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.NEUF);

            PrixEtatLivre prixDigital = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.DIGITAL);

            PrixEtatLivre prixUsager = listPrixEtat.Find(x => x.LivreBibliotheque == LivreEtatPrix && x.EtatLivre.Nom == NomEtatLivre.USAGE);


            if (form.PrixNeuf == null || form.PrixNeuf == 0 && prixNeuf !=null)
            {
                _context.PrixEtatsLivres.Remove(prixNeuf);
                _context.SaveChanges();
            };

            if (form.PrixNumerique == null || form.PrixNumerique == 0 && prixDigital != null)
            {
                _context.PrixEtatsLivres.Remove(prixDigital);
                _context.SaveChanges();
            };
            if (form.PrixUsage == null || form.PrixUsage == 0 && prixUsager != null)
            {
                _context.PrixEtatsLivres.Remove(prixUsager);
                _context.SaveChanges();
            };


            if (prixNeuf != null && form.PrixNeuf != 0)
            {
                prixNeuf.Prix = (double)form.PrixNeuf;
                _context.PrixEtatsLivres.Update(prixNeuf);
                _context.SaveChanges();
            }
            else if(form.PrixNeuf != 0)
            {
                PrixEtatLivre nouveauPrixNeuf = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.NEUF).EtatLivreId,
                    Prix = (double)form.PrixNeuf,
                };
                _context.PrixEtatsLivres.Add(nouveauPrixNeuf);
                _context.SaveChanges();
            }


            if (prixDigital != null && form.PrixNumerique != 0)
            {
                prixDigital.Prix = (double)form.PrixNumerique;
                _context.PrixEtatsLivres.Update(prixDigital);
                _context.SaveChanges();
            }
            else if(form.PrixNumerique != 0)
            {
                PrixEtatLivre nouveauPrixDigital = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.DIGITAL).EtatLivreId,
                    Prix = (double)form.PrixNumerique,
                };
                _context.PrixEtatsLivres.Add(nouveauPrixDigital);
                _context.SaveChanges();
            }

            if (prixUsager != null && form.PrixUsage != 0)
            {
                prixUsager.Prix = (double)form.PrixUsage;
                prixUsager.QuantiteUsage = (int)form.QuantiteUsagee;
                _context.PrixEtatsLivres.Update(prixUsager);
                _context.SaveChanges();
            }
            else if (form.PrixUsage != 0)
            {
                PrixEtatLivre nouveauPrixUsage = new()
                {
                    PrixEtatLivreId = 0,
                    LivreBibliothequeId = LivreEtatPrix.LivreId,
                    EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.USAGE).EtatLivreId,
                    Prix = (double)form.PrixUsage,
                    QuantiteUsage = 0
                };
                if (form.QuantiteUsagee != null)
                {
                    nouveauPrixUsage.QuantiteUsage = (int)form.QuantiteUsagee;
                }
                _context.PrixEtatsLivres.Add(nouveauPrixUsage);
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