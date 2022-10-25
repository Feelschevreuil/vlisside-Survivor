using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
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

        public IActionResult La_blun()
        {
           InventaireLaBlunVM inventaireLivreEtudiant = new() { inventaireLivreEtudiantVMs = _context.LivresEtudiants.ToList() };      
           return View(inventaireLivreEtudiant);
        }

        public IActionResult Bibliotheque()
        {
            List<TuileLivreBibliotequeVM> inventaireBibliotheque = new();
            foreach(LivreBibliotheque livre in _context.LivresBibliotheque)
            {
                var livreConvertie = livre.GetTuileLivreBibliotequeVMs(_context);
                inventaireBibliotheque.Add(livreConvertie);
            };



            InventaireLivreBibliotheque inventaireLivreBibliotheque = new() { tuileLivreBiblioteques = inventaireBibliotheque};

            return View(inventaireLivreBibliotheque);

        }


        [HttpGet]
        public ActionResult creer()
        {

            CreationLivreVM nouveauLivre = new CreationLivreVM { Auteurs = ListDropDownAuteurs(), MaisonsDeditions = ListDropDownMaisonDedition(), ListeCoursComplete = ListDropDownCours() };
            return View(nouveauLivre);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> creer(CreationLivreVM form)
        {
            ModelState.Remove("Auteurs");
            ModelState.Remove("MaisonsDeditions");
            ModelState.Remove("ListeCours");
            ModelState.Remove("Photo");


            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string nomFicherImage = Path.GetFileNameWithoutExtension(form.fichierImage.FileName);
                string extentionFicherImage = Path.GetExtension(form.fichierImage.FileName);
                form.Photo = nomFicherImage = nomFicherImage + DateTime.Now.ToString("yymmssff") + extentionFicherImage;
                string chemin = Path.Combine(wwwRootPath + "/img", nomFicherImage);
                using (var fileStream = new FileStream(chemin, FileMode.Create))
                {
                    await form.fichierImage.CopyToAsync(fileStream);
                }

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

                CoursLivre nouvelleAssociation = new()
                {
                    CoursLivreId = 0,
                    CoursId = (int)form.CoursId,
                    LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                    Complementaire = form.Obligatoire
                };

                _context.CoursLivres.Add(nouvelleAssociation);
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


            int idLivreNeuf = etatLivres.Find(y => y.Nom == NomEtatLivre.Neuf).EtatLivreId;
            int idLivreNumerique = etatLivres.Find(y => y.Nom == NomEtatLivre.Numerique).EtatLivreId;
            int idLivreUsager = etatLivres.Find(y => y.Nom == NomEtatLivre.Usagee).EtatLivreId;

          
            var pasEtatAuLivre = _context.PrixEtatsLivres.ToList().FindAll(x => x.LivreBibliotheque.LivreId == livreBibliothequeRechercher.LivreId);
            var PasNumerique = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreNumerique);
            var pasUsager = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreUsager);
            var pasDeNeuf = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreNeuf);



            ModificationLivreVM ModifierLivre = new()
            {
                IdDuLivre = livreBibliothequeRechercher.LivreId,
                AuteurId = auteurLivre.AuteurId,
                CoursId = coursLivre.CoursId,
                MaisonDeditionId = livreBibliothequeRechercher.MaisonEditionId,
                Auteurs = ListDropDownAuteurs(),
                ListeCours = ListDropDownCours(),
                MaisonsDeditions = ListDropDownMaisonDedition(),
                DatePublication = livreBibliothequeRechercher.DatePublication,
                ISBN = livreBibliothequeRechercher.Isbn,
                Titre = livreBibliothequeRechercher.Titre,
                Resume = livreBibliothequeRechercher.Resume,
                Obligatoire = coursLivre.Complementaire,
                Photo = livreBibliothequeRechercher.PhotoCouverture,
                PossedeNeuf = true,
                PossedeNumerique = true,
       
            };

            var prixNeuf = prixEtatLivre.Find(x => x.EtatLivreId == idLivreNeuf);
            var prixDigital = prixEtatLivre.Find(x => x.EtatLivreId == idLivreNumerique);
            var prixUsage = prixEtatLivre.Find(x => x.EtatLivreId == idLivreUsager);

            if (prixNeuf != null) { ModifierLivre.PrixNeuf = prixNeuf.Prix; } else {ModifierLivre.PrixNeuf = 0; };
            if (prixDigital != null) { ModifierLivre.PrixNumerique = prixDigital.Prix; } else { ModifierLivre.PrixNumerique = 0; };
            if (prixUsage != null) { ModifierLivre.PrixUsage = prixUsage.Prix; } else { ModifierLivre.PrixUsage = 0; };
            

            return View(ModifierLivre);

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> modifier(ModificationLivreVM form)
        {
            bool ImageIsNull = form.fichierImage == null;
            ModelState.Remove("Auteurs");
            ModelState.Remove("MaisonsDeditions");
            ModelState.Remove("ListeCours");
            ModelState.Remove("Photo");

            if (ImageIsNull)
            {
                ModelState.Remove("fichierImage");
            }

            if (ModelState.IsValid)
            {
                if (!ImageIsNull)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string nomFicherImage = Path.GetFileNameWithoutExtension(form.fichierImage.FileName);
                    string extentionFicherImage = Path.GetExtension(form.fichierImage.FileName);
                    form.Photo = nomFicherImage = nomFicherImage + DateTime.Now.ToString("yymmssff") + extentionFicherImage;
                    string chemin = Path.Combine(wwwRootPath + "/img", nomFicherImage);
                    using (var fileStream = new FileStream(chemin, FileMode.Create))
                    {
                        await form.fichierImage.CopyToAsync(fileStream);
                    }
                }


                LivreBibliotheque LivreBibliothèqueModifier = _context.LivresBibliotheque.ToList().Find(x => x.LivreId == form.IdDuLivre);
                LivreBibliothèqueModifier.MaisonEditionId = (int)form.MaisonDeditionId;
                LivreBibliothèqueModifier.Isbn = form.ISBN;
                LivreBibliothèqueModifier.Titre = form.Titre;
                LivreBibliothèqueModifier.Resume = form.Resume;
                LivreBibliothèqueModifier.PhotoCouverture = form.Photo;
                LivreBibliothèqueModifier.DatePublication = form.DatePublication;

                _context.LivresBibliotheque.Update(LivreBibliothèqueModifier);
                _context.SaveChanges();

                CoursLivre nouvelleAssociation = new()
                {
                    CoursLivreId = 0,
                    CoursId = (int)form.CoursId,
                    LivreBibliothequeId = LivreBibliothèqueModifier.LivreId,
                    Complementaire = form.Obligatoire
                };

                _context.CoursLivres.Add(nouvelleAssociation);
                _context.SaveChanges();

                _context.PrixEtatsLivres.AddRange(AssocierPrixEtat(LivreBibliothèqueModifier, form));
                _context.SaveChanges();


                return View("succesModifierLivre", LivreBibliothèqueModifier);
            }

            form.Auteurs = ListDropDownAuteurs();
            form.ListeCours = ListDropDownCours();
            form.MaisonsDeditions = ListDropDownMaisonDedition();
            return View(form);
        }

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


            List<Evenement> listEvenements = _context.Evenements.OrderBy(i => i.Debut).Take(4).ToList();

            InventaireLivreBibliotheque recommendationPromotions = new() { tuileLivreBiblioteques = GetQuatreLivres.GetInventaireBibliotequeVMs(_context)};

            return View("Bibliotheque", recommendationPromotions);
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

            PrixEtatLivre AssociationPrixNeuf = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.Neuf).EtatLivreId,
                Prix = form.PrixNeuf,
            };
            PrixEtatLivre AssociationPrixNumérique = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.Numerique).EtatLivreId,
                Prix = form.PrixNumerique,
            };
            PrixEtatLivre AssociationPrixUsager = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.Usagee).EtatLivreId,
                Prix = form.PrixUsage,
            };

            ListPrixEtat.Add(AssociationPrixNeuf);
            ListPrixEtat.Add(AssociationPrixNumérique);
            ListPrixEtat.Add(AssociationPrixUsager);


            return ListPrixEtat;
        }
        public List<PrixEtatLivre> AssocierPrixEtat(LivreBibliotheque LivreEtatPrix, ModificationLivreVM form)
        {
            List<PrixEtatLivre> ListPrixEtat = new();

            PrixEtatLivre AssociationPrixNeuf = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.Neuf).EtatLivreId,
                Prix = form.PrixNeuf,
            };
            PrixEtatLivre AssociationPrixNumérique = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.Numerique).EtatLivreId,
                Prix = form.PrixNumerique,
            };
            PrixEtatLivre AssociationPrixUsager = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.Usagee).EtatLivreId,
                Prix = form.PrixUsage,
            };

            ListPrixEtat.Add(AssociationPrixNeuf);
            ListPrixEtat.Add(AssociationPrixNumérique);
            ListPrixEtat.Add(AssociationPrixUsager);


            return ListPrixEtat;
        }
    }
}