using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    public class BoutiqueController : Controller
    {
        private readonly ILogger<InventaireController> _logger;
        private readonly ApplicationDbContext _context;

        public BoutiqueController(ILogger<InventaireController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult ma_boutique()
        {
            Commanditaire commanditaire = new Commanditaire() { Courriel = "aaaaaaa@gmail.cum", CommanditaireId = 0, Message = "VENEZ ACHETER NOS DÉLICIEUX BISCUITS", Nom = "BakeryChezMarki's", Url = "http//BiscuitsChezMary's.cum" };


            List<Evenement> listEvenements = new()
            {
               new Evenement(){EvenementId=0,Commanditaire= commanditaire,CommanditaireId=0,Debut=DateTime.Now, Fin=DateTime.MaxValue,Description=commanditaire.Message,Nom="Pomme"}

            };
            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = GetTuileLivreBibliotequeVMs(), evenements = listEvenements };

            return View(recommendationPromotions);

        }

        public List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeVMs()
        {
            List<TuileLivreBibliotequeVM> listTuileLivreBibliotequeVMs = new();
            List<LivreBibliotheque> listLivreBibliotheque = _context.LivresBibliotheque.ToList();
            Random random = new Random();


            ProgrammeEtude programme = new()
            {
                Nom = "Fleur",
                ProgrammeEtudeId = 0,
                Code = "hfde",
            };
            _context.ProgrammesEtudes.Add(programme);
            _context.SaveChanges();

            Cours cours = new()
            {
                AnneeParcours = 1,
                Nom = "Sicen",
                Code = "45ksup",
                CoursId = 0,
                Description = "cours sur la modialisation",
                ProgrammeEtudeId = _context.ProgrammesEtudes.First().ProgrammeEtudeId,
            };
            _context.Cours.Add(cours);
            _context.SaveChanges();



            MaisonEdition maison = new()
            {
                MaisonEditionId = 0,
                Nom = "Des Noix"
            };
            _context.MaisonsEditions.Add(maison);
            _context.SaveChanges();

            for (int i = 0; i < 5; i++)
            {

                LivreBibliotheque livre = new()
                {
                    LivreId = 0,
                    Isbn = "1478523698",
                    DatePublication = DateTime.Now,
                    MaisonEditionId = _context.MaisonsEditions.First().MaisonEditionId,
                    PhotoCouverture = "horloge.svg",
                    Resume = "Pomme",
                    Titre = "La mort"
                };
                _context.LivresBibliotheque.Add(livre);
                _context.SaveChanges();



            }

            Province province = new()
            {
                ProvinceId = 0,
                Nom = "Quebec"
            };
            _context.Provinces.Add(province);
            _context.SaveChanges();

            Adresse adresse = new()
            {
                AdresseId = 0,
                Ville = "Granby",
                NumeroCivique = 45,
                CodePostal = "kqofue",
                ProvinceId = _context.Provinces.First().ProvinceId,
                Rue = "baker"
            };
            _context.Adresses.Add(adresse);
            _context.SaveChanges();


            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(listLivreBibliotheque.Count());
                LivreBibliotheque livreBibliotheque = listLivreBibliotheque[index];
                CoursLivre CoursLivre = _context.CoursLivres.ToList().Find(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
                EvaluationLivre evaluationLivre = _context.EvaluationsLivres.ToList().Find(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
                CoursProfesseur coursProfesseur = _context.CoursProfesseurs.ToList().Find(x => x.CoursId == CoursLivre.CoursId);

                Evaluation evaluation = new Evaluation { Commentaire = "", Etoiles = 3, Date = DateTime.Now, Titre = "", EvaluationId = 0, Etudiant = _context.Etudiants.FirstOrDefault(), EtudiantId = _context.Etudiants.FirstOrDefault().Id };
                Professeur professeur = new Professeur { ProfesseurId = 0, Nom = "Jeremy", Prenom = "Barnet" };

                //TODO
                //Modifer le code ci-dessus quand le seeder remplira toutes la BD
                //Un associer doit être fait ici avec le Id.
                //Ex: Cherche coursId pour avoir la variable cours remplie avec un object "cours" avant de l'envoyer à la vue 


                if (CoursLivre == null)
                {
                    CoursLivre = new();
                    CoursLivre.CoursLivreId = 0;
                    CoursLivre.CoursId = _context.Cours.First().CoursId;
                    CoursLivre.LivreBibliothequeId = livreBibliotheque.LivreId;
                    CoursLivre.Complementaire = false;
                    _context.CoursLivres.Add(CoursLivre);
                    _context.SaveChanges();
                };

                if (evaluationLivre == null)
                {
                    _context.Evaluations.Add(evaluation);
                    _context.SaveChanges();
                    evaluationLivre = new();
                    evaluationLivre.EvaluationId = _context.Evaluations.ToList().Find(x => x.EvaluationId == evaluation.EvaluationId).EvaluationId;
                    evaluationLivre.LivreBibliothequeId = livreBibliotheque.LivreId;
                    evaluationLivre.LivreBibliotheque = livreBibliotheque;
                    evaluationLivre.Evaluation = _context.Evaluations.ToList().Find(x => x.EvaluationId == evaluation.EvaluationId);

                };
                if (coursProfesseur == null)
                {
                    _context.Professeurs.Add(professeur);
                    _context.SaveChanges();
                    coursProfesseur = new();
                    coursProfesseur.ProfesseurId = _context.Professeurs.ToList().Find(x => x.ProfesseurId == 1).ProfesseurId;
                    coursProfesseur.CoursId = _context.Cours.ToList().First().CoursId;
                    coursProfesseur.Professeur = _context.Professeurs.ToList().Find(x => x.ProfesseurId == 1);
                    coursProfesseur.Cours = _context.Cours.ToList().First();

                };



                TuileLivreBibliotequeVM tuileLivreBibliotequeVM = new();
                tuileLivreBibliotequeVM.livreBibliothequesEvaluation = evaluationLivre;
                tuileLivreBibliotequeVM.coursProfesseurs = coursProfesseur;
                tuileLivreBibliotequeVM.complementaire = CoursLivre.Complementaire;

                listTuileLivreBibliotequeVMs.Add(tuileLivreBibliotequeVM);
            }

            return listTuileLivreBibliotequeVMs;

        }
    }
}
