using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    public class InventaireController : Controller
    {
        private readonly ILogger<InventaireController> _logger;
        private readonly ApplicationDbContext _context;

        public InventaireController(ILogger<InventaireController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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

            Commanditaire commandit = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };
            List<Evenement> evenements = new()
            {
              new Evenement() {EvenementId=0,Commanditaire=commandit,Nom="Soccer",Debut=DateTime.Now,Fin=DateTime.MaxValue,Description="vener jouer",Image="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}

            };

            RecommendationPromotionsVM recommendationPromotions = new() { livreBibliothequesEvaluation = listeLivres, evenements = evenements };

            return View(recommendationPromotions);


        }

        public IActionResult Bibliotheque()
        {

            List<EvaluationLivre> listeLivres = new()
            { 
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le corps humain",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le corps humain",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le corps humain",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=9,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn="osidfids",DatePublication=DateTime.Today,Resume="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In et dignissim nulla. Suspendisse aliquam augue et tellus accumsan tempor. Pellentesque scelerisque purus purus, nec facilisis libero aliquam et. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",Titre="Lorem Ipsum",LivreId=1,PhotoCouverture="https://live.staticflickr.com/5567/14776555342_f8550d0eda_b.jpg" } },
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=1,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn="jshfiffdddddd",DatePublication=DateTime.MaxValue,Titre="Hanrry potdbeur et la mer des monstres",Resume="Un jeune mage dont le père est posséidon part à l'aventure dans un monde magique rempli de monstre et d'aventure aventureuses",LivreId=2,PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"} }
            };

            Commanditaire commandit = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };
            List<Evenement> evenements = new()
            {
              new Evenement() {EvenementId=0,Commanditaire=commandit,Nom="Soccer",Debut=DateTime.Now,Fin=DateTime.MaxValue,Description="vener jouer",Image="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}

            };

            RecommendationPromotionsVM recommendationPromotions = new() { livreBibliothequesEvaluation = listeLivres, evenements = evenements };

            return View(recommendationPromotions);

        }

        public ActionResult creer()
        {

            CreationLivreVM nouveauLivre = new CreationLivreVM { Auteurs = ListDropDownAuteurs(), Etats = ListDropDownEtats(), MaisonsDeditions = ListDropDownMaisonDedition(), ListeCours = ListDropDownCours() };
            return View(nouveauLivre);
        }

        public List<SelectListItem> ListDropDownAuteurs()
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un auteur" });

            foreach (var e in _context.Auteurs)
                Liste.Add(new SelectListItem { Value = e.AuteurId.ToString(), Text = e.Nom + ", " + e.Prenom });

            return Liste;
        }

        public List<SelectListItem> ListDropDownEtats()
        {

            List<SelectListItem> Liste = new List<SelectListItem>();

            Liste.Add(new SelectListItem { Value = "", Text = "Choisissez un type de livre" });

            foreach (var e in _context.EtatsLivres)
                Liste.Add(new SelectListItem { Value = e.EtatLivreId.ToString(), Text = e.Nom });

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
    }
}