using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
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
            List<EvaluationLivre> listeLivres = new()
            {
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=4,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="La vie en France",LivreId=0, PhotoCouverture="https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781787550360/classic-book-cover-foiled-journal-9781787550360_xlg.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=9,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le souffle de vie",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=2,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Pourquoi moi?",LivreId=0, PhotoCouverture="https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781787550360/classic-book-cover-foiled-journal-9781787550360_xlg.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=10,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn="osidfids",DatePublication=DateTime.Today,Resume="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In et dignissim nulla. Suspendisse aliquam augue et tellus accumsan tempor. Pellentesque scelerisque purus purus, nec facilisis libero aliquam et. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",Titre="Lorem Ipsum",LivreId=1,PhotoCouverture="https://live.staticflickr.com/5567/14776555342_f8550d0eda_b.jpg" } },
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=3,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn="jshfiffdddddd",DatePublication=DateTime.MaxValue,Titre="Paramire",Resume="Un jeune mage dont le père est posséidon part à l'aventure dans un monde magique rempli de monstre et d'aventure aventureuses",LivreId=2,PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"} }
            };

            CoursProfesseur HardCodeCoursProfesseur = new CoursProfesseur() { Cours = _context.Cours.First(), Professeur = new Professeur { Nom = "HAHAHAHAHHA", Prenom = "aaaAAAAAAA̵͗̊͂̔͑̉̿̂̇͊̓̈́̂̍̍̀͐̐̀͌̈̀͂̉͘͘̕͠͝͝ͅ" } };

            Commanditaire commandit = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };

            List<Evenement> evenements = new()
            {
              new Evenement() {EvenementId=0,Commanditaire=commandit,Nom="Soccer",Debut=DateTime.Now,Fin=DateTime.MaxValue,Description="vener jouer",Image="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}
            };

            List<TuileLivreBibliotequeVM> tuileLivreBibliotequeVMs = new()
            {
                new TuileLivreBibliotequeVM(){coursProfesseurs=HardCodeCoursProfesseur,livreBibliothequesEvaluation= new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le corps humain",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}}}
            };

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = tuileLivreBibliotequeVMs, evenements = evenements };

            return View(recommendationPromotions);


        }

        public IActionResult Bibliotheque()
        {

            List<EvaluationLivre> listeLivres = new()
            {
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le corps humain",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le corps humain",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le corps humain",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}},

            };

            List<CoursProfesseur> listCoursProfesseurs = new()
            {
                new CoursProfesseur(){Cours=_context.Cours.First(),Professeur=new Professeur{ Nom="Bob",Prenom="Marley"} },
                new CoursProfesseur() { Cours = _context.Cours.First(), Professeur = new Professeur { Nom = "Mike", Prenom = "Bernard" } },
                new CoursProfesseur(){Cours=_context.Cours.First(),Professeur=new Professeur{ Nom="HAHAHAHAHHA",Prenom="aaaAAAAAAA̵͗̊͂̔͑̉̿̂̇͊̓̈́̂̍̍̀͐̐̀͌̈̀͂̉͘͘̕͠͝͝ͅ"} }

            };

            Commanditaire commandit = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };
            List<Evenement> evenements = new()
            {
              new Evenement() {EvenementId=0,Commanditaire=commandit,Nom="Soccer",Debut=DateTime.Now,Fin=DateTime.MaxValue,Description="vener jouer",Image="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}

            };

            List<TuileLivreBibliotequeVM> tuileLivreBibliotequeVMs = new()
            {
                new TuileLivreBibliotequeVM(){coursProfesseurs=listCoursProfesseurs[0],livreBibliothequesEvaluation=new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque= _context.LivresBibliotheque.ToList().First()} }
            };

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = tuileLivreBibliotequeVMs, evenements = evenements };

            return View(recommendationPromotions);

        }


        [HttpGet]
        public ActionResult creer()
        {

            CreationLivreVM nouveauLivre = new CreationLivreVM { Auteurs = ListDropDownAuteurs(), MaisonsDeditions = ListDropDownMaisonDedition(), ListeCours = ListDropDownCours() };
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
            form.ListeCours = ListDropDownCours();
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


            int idLivreNeuf = etatLivres.Find(y => y.Nom == "Neuf").EtatLivreId;
            int idLivreNumerique = etatLivres.Find(y => y.Nom == "Digital").EtatLivreId;
            int idLivreUsager = etatLivres.Find(y => y.Nom == "Usagé").EtatLivreId;

            //TODO enlever se code quand le seeder sera fini
            //Va planter une fois pour les livre qui ont besoin des if, Juste reloader la page 1 fois sa va être suffisant.
            var pasEtatAuLivre = _context.PrixEtatsLivres.ToList().FindAll(x => x.LivreBibliotheque.LivreId == livreBibliothequeRechercher.LivreId);
            var PasNumerique = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreNumerique);
            var pasUsager = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreUsager);
            var pasDeNeuf = pasEtatAuLivre.Find(x => x.EtatLivreId == idLivreNeuf).EtatLivreId;

            if (auteurLivre == null)
            {
                auteurLivre = new()
                {
                    AuteurId = _context.Auteurs.FirstOrDefault().AuteurId,
                    LivreBibliothequeId = livreBibliothequeRechercher.LivreId
                };
                _context.AuteursLivres.Add(auteurLivre);
                _context.SaveChanges();
            };

            if (coursLivre == null)
            {
                coursLivre = new()
                {
                    CoursLivreId = 0,
                    CoursId = _context.Cours.FirstOrDefault().CoursId,
                    LivreBibliothequeId = livreBibliothequeRechercher.LivreId,
                    Complementaire = false,
                };
                _context.CoursLivres.Add(coursLivre);
                _context.SaveChanges();
            };

            if (prixEtatLivre.Count() != 3)
            {
                if (pasEtatAuLivre == null || pasDeNeuf == null)
                {
                    PrixEtatLivre neufPrixLivre = new()
                    {
                        PrixEtatLivreId = 0,
                        EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Neuf").EtatLivreId,
                        LivreBibliothequeId = livreBibliothequeRechercher.LivreId,
                        Prix = 20
                    };
                    _context.PrixEtatsLivres.Add(neufPrixLivre);
                    _context.SaveChanges();
                }
                if (pasEtatAuLivre == null || PasNumerique == null)
                {
                    PrixEtatLivre DigitalPrixLivre = new()
                    {
                        PrixEtatLivreId = 0,
                        EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Digital").EtatLivreId,
                        LivreBibliothequeId = livreBibliothequeRechercher.LivreId,
                        Prix = 10
                    };
                    _context.PrixEtatsLivres.Add(DigitalPrixLivre);
                    _context.SaveChanges();
                };

                if (pasEtatAuLivre == null || pasUsager == null)
                {
                    PrixEtatLivre UsagerPrixLivre = new()
                    {
                        PrixEtatLivreId = 0,
                        EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Usagé").EtatLivreId,
                        LivreBibliothequeId = livreBibliothequeRechercher.LivreId,
                        Prix = 5
                    };
                    _context.PrixEtatsLivres.Add(UsagerPrixLivre);
                    _context.SaveChanges();
                };

            };
            //TODO arrête ici

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
                PrixNeuf = prixEtatLivre.Find(x => x.EtatLivreId == idLivreNeuf).Prix,
                PrixNumerique = prixEtatLivre.Find(x => x.EtatLivreId == idLivreNumerique).Prix,
                PrixUsage = prixEtatLivre.Find(x => x.EtatLivreId == idLivreUsager).Prix
                //TODO: décommenter la ligne en dessous quand le pull request "Ajustement de la BD" sera fait. (faire un rebase pour aller chercher la variable "NombreUsager)
                //QuantiteUsagee = prixEtatLivre.Find(x => x.LivreBibliothequeId == id).NombreUsager 
            };

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

            //TODO enlever le code ci-dessous
            Commanditaire commandit = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };
            List<Evenement> evenements = new()
            {
              new Evenement() {EvenementId=0,Commanditaire=commandit,Nom="Soccer",Debut=DateTime.Now,Fin=DateTime.MaxValue,Description="vener jouer",Image="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}

            };
            List<CoursProfesseur> listCoursProfesseurs = new()
            {
                new CoursProfesseur(){Cours=_context.Cours.First(),Professeur=new Professeur{ Nom="Bob",Prenom="Marley"} },
                new CoursProfesseur() { Cours = _context.Cours.First(), Professeur = new Professeur { Nom = "Mike", Prenom = "Bernard" } },
                new CoursProfesseur(){Cours=_context.Cours.First(),Professeur=new Professeur{ Nom="HAHAHAHAHHA",Prenom="aaaAAAAAAA̵͗̊͂̔͑̉̿̂̇͊̓̈́̂̍̍̀͐̐̀͌̈̀͂̉͘͘̕͠͝͝ͅ"} }

            };
            List<TuileLivreBibliotequeVM> tuileLivreBibliotequeVMs = new()
            {
                new TuileLivreBibliotequeVM(){coursProfesseurs=listCoursProfesseurs[0],livreBibliothequesEvaluation=new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque= _context.LivresBibliotheque.ToList().First()} }
            };

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = tuileLivreBibliotequeVMs, evenements = evenements };
            //Fin du TODO

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

            foreach (var e in _context.MaisonsEditions)
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
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Neuf").EtatLivreId,
                Prix = form.PrixNeuf,
            };
            PrixEtatLivre AssociationPrixNumérique = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Digital").EtatLivreId,
                Prix = form.PrixNumerique,
            };
            PrixEtatLivre AssociationPrixUsager = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Usagé").EtatLivreId,
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
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Neuf").EtatLivreId,
                Prix = form.PrixNeuf,
            };
            PrixEtatLivre AssociationPrixNumérique = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Digital").EtatLivreId,
                Prix = form.PrixNumerique,
            };
            PrixEtatLivre AssociationPrixUsager = new()
            {
                PrixEtatLivreId = 0,
                LivreBibliothequeId = LivreEtatPrix.LivreId,
                EtatLivreId = _context.EtatsLivres.ToList().Find(x => x.Nom == "Usagé").EtatLivreId,
                Prix = form.PrixUsage,
            };

            ListPrixEtat.Add(AssociationPrixNeuf);
            ListPrixEtat.Add(AssociationPrixNumérique);
            ListPrixEtat.Add(AssociationPrixUsager);


            return ListPrixEtat;
        }
    }
}