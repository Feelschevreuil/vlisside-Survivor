using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using static Humanizer.In;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class PanierController : Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly ApplicationDbContext _context;

        public PanierController(ILogger<AccueilController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //TODO modifier/ajouter un viewmodel pour la vue

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult GetLivres([FromBody] GetLivres livres)
        {
            if (livres == null)
            {
                return BadRequest();
            }

            List<CoursLivre> coursLivres = _context.CoursLivres.Include(x => x.Cours).Include(x => x.Cours.ProgrammeEtude).Include(x => x.LivreBibliotheque).ToList();
            CoursLivre coursLivresTrouves = new CoursLivre();
            List<TuileLivreBibliotequeVM> livresModifie = new List<TuileLivreBibliotequeVM>();
            List<CoursLivre> cours = new List<CoursLivre>();

            List<PrixEtatLivre> listPrixEtat = _context.PrixEtatsLivres
                .Include(x => x.LivreBibliotheque)
                .Include(x => x.EtatLivre)
                .ToList();

            if (coursLivres != null)
            {

                foreach (int id in livres.Neuf)
                {
                    coursLivresTrouves = coursLivres.Find(element => element.LivreBibliotheque.LivreId == id);

                    if (coursLivresTrouves != null)
                    {
                        PrixEtatLivre prixNeuf = listPrixEtat.Find(x => x.LivreBibliotheque == coursLivresTrouves.LivreBibliotheque && x.EtatLivre.Nom == NomEtatLivre.NEUF);

                        livresModifie.Add(new TuileLivreBibliotequeVM
                        {
                            livreBibliotheque = coursLivresTrouves.LivreBibliotheque,
                            coursLivre = _context.CoursLivres.ToList().Find(element => element.LivreBibliothequeId == coursLivresTrouves.LivreBibliotheque.LivreId)/*TODO à changer pour ce commentaire quand la tuille acceptera plusieurs cours ||| _context.CoursLivres.ToList().Where(element=>element.LivreBibliothequeId==livre.LivreId)*/,
                            prixEtatLivre = new List<PrixEtatLivre> { prixNeuf, null, null }
                        });
                    }
                }

                foreach (int id in livres.Usage)
                {
                    coursLivresTrouves = coursLivres.Find(element => element.LivreBibliotheque.LivreId == id);
                    if (coursLivresTrouves != null)
                    {
                        PrixEtatLivre prixUsage = listPrixEtat.Find(x => x.LivreBibliotheque == coursLivresTrouves.LivreBibliotheque && x.EtatLivre.Nom == NomEtatLivre.USAGE);

                        livresModifie.Add(new TuileLivreBibliotequeVM
                        {
                            livreBibliotheque = coursLivresTrouves.LivreBibliotheque,
                            coursLivre = _context.CoursLivres.ToList().Find(element => element.LivreBibliothequeId == coursLivresTrouves.LivreBibliotheque.LivreId)/*TODO à changer pour ce commentaire quand la tuille acceptera plusieurs cours ||| _context.CoursLivres.ToList().Where(element=>element.LivreBibliothequeId==livre.LivreId)*/,
                            prixEtatLivre = new List<PrixEtatLivre> { null, prixUsage, null }
                        });
                    }
                }
                foreach (int id in livres.Numerique)
                {
                    coursLivresTrouves = coursLivres.Find(element => element.LivreBibliotheque.LivreId == id);
                    if (coursLivresTrouves != null)
                    {
                        PrixEtatLivre prixNumerique = listPrixEtat.Find(x => x.LivreBibliotheque == coursLivresTrouves.LivreBibliotheque && x.EtatLivre.Nom == NomEtatLivre.DIGITAL);

                        livresModifie.Add(new TuileLivreBibliotequeVM
                        {
                            livreBibliotheque = coursLivresTrouves.LivreBibliotheque,
                            coursLivre = _context.CoursLivres.ToList().Find(element => element.LivreBibliothequeId == coursLivresTrouves.LivreBibliotheque.LivreId)/*TODO à changer pour ce commentaire quand la tuille acceptera plusieurs cours ||| _context.CoursLivres.ToList().Where(element=>element.LivreBibliothequeId==livre.LivreId)*/,
                            prixEtatLivre = new List<PrixEtatLivre> { null, null, prixNumerique }
                        });
                    }
                }
            }


            //TODO crééer un view model pour les vue partiels et update la liste de livre  

            return PartialView("_ConteneurPanier", livresModifie);
        }

    }
}
