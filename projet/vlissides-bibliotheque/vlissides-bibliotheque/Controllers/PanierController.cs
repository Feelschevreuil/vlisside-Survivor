//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using NuGet.Protocol;
//using vlissides_bibliotheque.Constantes;
//using vlissides_bibliotheque.Data;
//using vlissides_bibliotheque.DTO;
//using vlissides_bibliotheque.Models;
//using vlissides_bibliotheque.ViewModels;
//using vlissides_bibliotheque.Enums;
//using static Humanizer.In;

//namespace vlissides_bibliotheque.Controllers
//{
//    [Authorize]
//    public class PanierController : Controller
//    {
//        private readonly ILogger<AccueilController> _logger;
//        private readonly ApplicationDbContext _context;

//        public PanierController(ILogger<AccueilController> logger, ApplicationDbContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        //[ValidateAntiForgeryToken]
//        public IActionResult GetLivres([FromBody] GetLivres livres)
//        {
//            if (livres == null)
//            {
//                return BadRequest();
//            }

//            List<CoursLivre> coursLivres = _context.CoursLivres.Include(x => x.Cours).Include(x => x.Cours.ProgrammeEtude).Include(x => x.LivreBibliotheque).ToList();
//            CoursLivre coursLivresTrouves = new CoursLivre();
//            List<TuileLivreBibliotequeVM> livresModifie = new List<TuileLivreBibliotequeVM>();
//            List<CoursLivre> cours = new List<CoursLivre>();

//            List<PrixEtatLivre> listPrixEtat = _context.PrixEtatsLivres
//                .Include(x => x.LivreBibliotheque)
//                .Include(x=>x.LivreBibliotheque.MaisonEdition)
//                .ToList();

//            if (coursLivres != null)
//            {

//                foreach (Neufs id in livres.Neufs)
//                {
//                    coursLivresTrouves = coursLivres.Find(element => element.LivreBibliotheque.LivreId == id.LivreId);

//                    if (coursLivresTrouves != null)
//                    {
//                        PrixEtatLivre prixNeuf = listPrixEtat.Find(x => x.LivreBibliotheque == coursLivresTrouves.LivreBibliotheque && x.EtatLivre == EtatLivreEnum.NEUF);

//                        livresModifie.Add(new TuileLivreBibliotequeVM
//                        {
//                            livreBibliotheque = coursLivresTrouves.LivreBibliotheque,
//                            coursLivre = _context.CoursLivres.ToList().Find(element => element.LivreBibliothequeId == coursLivresTrouves.LivreBibliotheque.LivreId)/*TODO à changer pour ce commentaire quand la tuille acceptera plusieurs cours ||| _context.CoursLivres.ToList().Where(element=>element.LivreBibliothequeId==livre.LivreId)*/,
//                            prixEtatLivre = new List<PrixEtatLivre> { prixNeuf, null, null }
//                        });
//                    }
//                }

//                foreach (Usages id in livres.Usages)
//                {
//                    coursLivresTrouves = coursLivres.Find(element => element.LivreBibliotheque.LivreId == id.LivreId);
//                    if (coursLivresTrouves != null)
//                    {
//                        PrixEtatLivre prixUsage = listPrixEtat.Find(x => x.LivreBibliotheque == coursLivresTrouves.LivreBibliotheque && x.EtatLivre == EtatLivreEnum.USAGE);

//                        livresModifie.Add(new TuileLivreBibliotequeVM
//                        {
//                            livreBibliotheque = coursLivresTrouves.LivreBibliotheque,
//                            coursLivre = _context.CoursLivres.ToList().Find(element => element.LivreBibliothequeId == coursLivresTrouves.LivreBibliotheque.LivreId)/*TODO à changer pour ce commentaire quand la tuille acceptera plusieurs cours ||| _context.CoursLivres.ToList().Where(element=>element.LivreBibliothequeId==livre.LivreId)*/,
//                            prixEtatLivre = new List<PrixEtatLivre> { null, prixUsage, null }
//                        });
//                    }
//                }
//                foreach (Numeriques id in livres.Numeriques)
//                {
//                    coursLivresTrouves = coursLivres.Find(element => element.LivreBibliotheque.LivreId == id.LivreId);
//                    if (coursLivresTrouves != null)
//                    {
//                        PrixEtatLivre prixNumerique = listPrixEtat.Find(x => x.LivreBibliotheque == coursLivresTrouves.LivreBibliotheque && x.EtatLivre == EtatLivreEnum.NUMERIQUE);

//                        livresModifie.Add(new TuileLivreBibliotequeVM
//                        {
//                            livreBibliotheque = coursLivresTrouves.LivreBibliotheque,
//                            coursLivre = _context.CoursLivres.ToList().Find(element => element.LivreBibliothequeId == coursLivresTrouves.LivreBibliotheque.LivreId)/*TODO à changer pour ce commentaire quand la tuille acceptera plusieurs cours ||| _context.CoursLivres.ToList().Where(element=>element.LivreBibliothequeId==livre.LivreId)*/,
//                            prixEtatLivre = new List<PrixEtatLivre> { null, null, prixNumerique }
//                        });
//                    }
//                }
//            }

//            List<AuteurLivre> auteursLivres = _context.AuteursLivres.Include(x => x.Auteur).ToList();
//            for (int i = 0; i < livresModifie.Count; i++)
//            {
//                List<AuteurLivre> auteursLivresTrouve = auteursLivres.FindAll(e => e.LivreBibliothequeId == livresModifie[i].livreBibliotheque.LivreId);

//                if (auteursLivresTrouve != null)
//                {
//                    if (auteursLivres.Count > 0)
//                    {
//                        List<Auteur> auteurs = new List<Auteur>();
//                        foreach (AuteurLivre auteurLivre in auteursLivresTrouve)
//                        {
//                            auteurs.Add(auteurLivre.Auteur);
//                        }
//                        livresModifie[i].auteurs = auteurs;
//                    }
//                }

//            }

//            return PartialView("_ConteneurPanier", livresModifie);
//        }

//    }
//}
