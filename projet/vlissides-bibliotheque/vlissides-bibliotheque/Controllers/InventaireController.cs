﻿using Humanizer;
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
                new TuileLivreBibliotequeVM(){coursProfesseurs=listCoursProfesseurs[0],livreBibliothequesEvaluation=new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque= _context.LivresBibliotheque.ToList().Find(x=>x.LivreId == 21)} }
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
                    CoursLivreId =0,
                    CoursId = (int)form.CoursId,
                    LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                    Complementaire = form.Obligatoire
                };

                _context.CoursLivres.Add(nouvelleAssociation);
                _context.SaveChanges();

                _context.PrixEtatsLivres.AddRange(AssocierPrixEtat(nouveauLivreBibliothèque, form));
                _context.SaveChanges();


                return View("succesAjoutLivre",nouveauLivreBibliothèque);
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

            return View();

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> modifier(CreationLivreVM livreBibliothequeModifier)
        {
            return View();
        }



        public List<SelectListItem> ListDropDownAuteurs()
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un auteur" });

            foreach (var e in _context.Auteurs)
                Liste.Add(new SelectListItem { Value = e.AuteurId.ToString(), Text = e.Nom + ", " + e.Prenom });

            return Liste;
        }

        /*public List<SelectListItem> ListDropDownEtats()
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un type de livre" });

            foreach (var e in _context.EtatsLivres)
                Liste.Add(new SelectListItem { Value = e.EtatLivreId.ToString(), Text = e.Nom });

            return Liste;
        }*/

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
                Prix = form.PrixNumerqiue,
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