using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

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
        public IActionResult Index(List<TuileLivreBibliotequeVM>? cardsPanier)
        {
            //TODO modifier/ajouter un viewmodel pour la vue
            if (cardsPanier == null)
                return View();
            return View(cardsPanier);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult GetLivres([FromBody] GetLivres ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            List<LivreBibliotheque> livres = _context.LivresBibliotheque.ToList();
            List<LivreBibliotheque> livresTrouves = new List<LivreBibliotheque>();
            List<TuileLivreBibliotequeVM> livresModifie = new List<TuileLivreBibliotequeVM>();
            List<CoursLivre> cours = new List<CoursLivre>();

            if (livres != null) { 
                
                foreach (int id in ids.Id)
                {
                    livresTrouves.Add(livres.Find(element => element.LivreId == id));
                }
            }

            if (livresTrouves != null)
            {
                foreach (LivreBibliotheque livre in livresTrouves)
                {
                    if(livre!=null)
                        livresModifie.Add(new TuileLivreBibliotequeVM { livreBibliotheque=livre, coursLivre=_context.CoursLivres.ToList().Find(element => element.LivreBibliothequeId == livre.LivreId)/*TODO à changer pour ce commentaire quand la tuille acceptera plusieurs cours ||| _context.CoursLivres.ToList().Where(element=>element.LivreBibliothequeId==livre.LivreId)*/});
                }
            }

            //TODO crééer un view model pour les vue partiels et update la liste de livre  

            return PartialView("_ConteneurPanier", livresModifie);

        }
    }
}
