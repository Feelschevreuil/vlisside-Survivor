﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    public class AccueilController : Controller
    {
        private readonly ILogger<AccueilController> _logger;

        public AccueilController(ILogger<AccueilController> logger)
        {
            _logger = logger;
        }
        [Route("")]
        public IActionResult Accueil()
        {

            Commanditaire commanditaire = new Commanditaire(){ Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };


            List<EvaluationLivre> listeLivres  = new() 
            { 
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=7,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn ="1676362s",DatePublication=DateTime.Now,Resume="bio",Titre="Le corps humain",LivreId=0, PhotoCouverture="https://www.publicdomainpictures.net/pictures/400000/velka/18th-century-persian-book-cover.jpg"}},
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=9,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn="osidfids",DatePublication=DateTime.Today,Resume="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In et dignissim nulla. Suspendisse aliquam augue et tellus accumsan tempor. Pellentesque scelerisque purus purus, nec facilisis libero aliquam et. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",Titre="Lorem Ipsum",LivreId=1,PhotoCouverture="https://live.staticflickr.com/5567/14776555342_f8550d0eda_b.jpg" } },
                new EvaluationLivre(){Evaluation=new Evaluation{Commentaire="",Etoiles=1,Date=DateTime.Now,Titre="",EvaluationId=0 },LivreBibliotheque=new LivreBibliotheque(){  Isbn="jshfiffdddddd",DatePublication=DateTime.MaxValue,Titre="Hanrry potdbeur et la mer des monstres",Resume="Un jeune mage dont le père est posséidon part à l'aventure dans un monde magique rempli de monstre et d'aventure aventureuses",LivreId=2,PhotoCouverture="data:image/jpeg;base64,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"} }
            };

            List<Evenement> listEvenements = new()
            {

               new Evenement(){EvenementId=0,Commanditaire= commanditaire,CommanditaireId=0,Debut=DateTime.Now, Fin=DateTime.MaxValue,Description=commanditaire.Message,Nom="Pomme"}
                

            };

            RecommendationPromotionsVM recommendationPromotions = new() { livreBibliothequesEvaluation=listeLivres, evenements=listEvenements};

            return View(recommendationPromotions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Actualiter()
        {
            Commanditaire commanditaire = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" }; 
            
           

            List<Evenement> listEvenements = new()
            {

               new Evenement(){EvenementId=0,Commanditaire= commanditaire,CommanditaireId=0,Debut=DateTime.Now, Fin=DateTime.MaxValue,Description=commanditaire.Message,Nom="Pomme"},
               
               new Evenement(){EvenementId=0,Commanditaire= commanditaire,CommanditaireId=0,Debut=DateTime.MinValue, Fin=DateTime.MaxValue,Description=commanditaire.Message,Nom="Banane"}


            };


            return View(listEvenements);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}