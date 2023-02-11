using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class UsageController : Controller
    {
        private readonly ILogger<UsageController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsageController(ILogger<UsageController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Usage/Index")]
        [Route("Usage/Index/{id?}")]
        [Route("Usage/{id?}")]
        [HttpGet]
        public IActionResult Usage(int? id)
        {
            List<LivreEtudiant> livreEtudiants = _context.LivresEtudiants
                    .Include(x => x.Etudiant)
                    .Take(12)
                    .ToList();

            if (id == null)
            {
                return View(livreEtudiants);
            }
            var livreRecherche = livreEtudiants.Find(x => x.LivreId == id);
            if (livreRecherche == null)
            {
                return Content("Cette identifiant n'appartient à aucun livre");
            }
            return View(livreRecherche);

        }
        [HttpGet]
        public IActionResult Detail(int id)
        {

            LivreEtudiant livre = _context.LivresEtudiants
                .Include(x=>x.Etudiant)
                .ToList()
                .Find(x => x.LivreId == id);
            if (livre != null)
            {
                LivreEtudiantVM livreEtudiant = new()
                {
                    LivreId = livre.LivreId,
                    Etudiant = livre.Etudiant,
                    Titre = livre.Titre,
                    Isbn = livre.Isbn,
                    Resume = livre.Resume,
                    Photo = livre.PhotoCouverture,
                    DatePublication = livre.DatePublication,
                    MaisonEdition = livre.MaisonEdition,
                    Auteur = livre.Auteur,
                    Prix = livre.Prix

                };
                return View(livreEtudiant);
            }


            return Content("Ce livre n'existe pas dans la base de données.");
        }

        [Route("Usage/MaBoutique")]
        [HttpGet]
        public IActionResult MaBoutique()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<LivreEtudiant> inventaireUnEtudiant = new();


            List<LivreEtudiant> livreEtudiants = _context.LivresEtudiants
                    .Include(x => x.Etudiant)
                    .ToList();

            List<LivreEtudiant> livres = livreEtudiants.FindAll(x => x.Etudiant.Id == userId);
            if (livres.Count() == 0)
            {
                return View(inventaireUnEtudiant);
            }
            inventaireUnEtudiant.AddRange(livres);
            return View(inventaireUnEtudiant);

        }

        [Route("Usage/ajouter")]
        [Authorize(Roles = RolesName.Etudiant)]
        public IActionResult ajouter()
        {
            LivreEtudiantVM livre = new();

            return View(livre);
        }

        [HttpPost]
        [Route("Usage/ajouter")]
        [Authorize(Roles = RolesName.Etudiant)]
        public IActionResult ajouter(LivreEtudiantVM livreEtudiantVM)
        {
            ModelState.Remove("Etudiant.Nom");
            ModelState.Remove("Etudiant.Prenom");
            ModelState.Remove("Etudiant.Adresse");
            ModelState.Remove("Etudiant.ProgrammeEtude");

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                livreEtudiantVM.Etudiant = _context.Etudiants.ToList().Find(x => x.Id == userId);

                LivreEtudiant livreEtudiant = new()
                {
                    LivreId = livreEtudiantVM.LivreId,
                    Etudiant = livreEtudiantVM.Etudiant,
                    Titre = livreEtudiantVM.Titre,
                    Isbn = livreEtudiantVM.Isbn,
                    Resume = livreEtudiantVM.Resume,
                    PhotoCouverture = livreEtudiantVM.Photo,
                    DatePublication = livreEtudiantVM.DatePublication,
                    MaisonEdition = livreEtudiantVM.MaisonEdition,
                    Auteur = livreEtudiantVM.Auteur,
                    Prix = livreEtudiantVM.Prix

                };
                _context.LivresEtudiants.Add(livreEtudiant);
                _context.SaveChanges();
                return RedirectToAction("MaBoutique");
            }
            var ModelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            foreach (var error in ModelErrors)
            {
                if (error.ErrorMessage.StartsWith("The value"))
                {
                    ModelState.AddModelError(string.Empty, "Le format d'un prix est incorrect. Voici un exemple du bon format: 10,45");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }
            return View(livreEtudiantVM);
        }

        [Route("Usage/modifier/{id?}")]
        [HttpGet]
        public async Task<ActionResult> modifier(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Etudiant etudiant = _context.Etudiants.ToList().Find(x => x.Id == userId);
            LivreEtudiant livreEtudiantRechercher = _context.LivresEtudiants
                .Include(x => x.Etudiant)
                .ToList()
                .Find(x => x.LivreId == id);

            if (livreEtudiantRechercher.Etudiant.Id == userId || User.IsInRole(RolesName.Admin))
            {
                LivreEtudiantVM livreEtudiant = new()
                {
                    LivreId = livreEtudiantRechercher.LivreId,
                    Etudiant = livreEtudiantRechercher.Etudiant,
                    Titre = livreEtudiantRechercher.Titre,
                    Isbn = livreEtudiantRechercher.Isbn,
                    Resume = livreEtudiantRechercher.Resume,
                    Photo = livreEtudiantRechercher.PhotoCouverture,
                    DatePublication = livreEtudiantRechercher.DatePublication,
                    MaisonEdition = livreEtudiantRechercher.MaisonEdition,
                    Auteur = livreEtudiantRechercher.Auteur,
                    Prix = livreEtudiantRechercher.Prix

                };
                return View(livreEtudiant);
            }
            return Content("Ce livre ne vous appartient pas. Vous ne pouvez pas le modifier");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> modifier(LivreEtudiantVM form)
        {
            ModelState.Remove("Etudiant.Nom");
            ModelState.Remove("Etudiant.Prenom");
            ModelState.Remove("Etudiant.Adresse");
            ModelState.Remove("Etudiant.ProgrammeEtude");

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                LivreEtudiant livreEtudiant = _context.LivresEtudiants
                .Include(x => x.Etudiant)
                .ToList()
                .Find(x => x.Etudiant.Id == userId);
                if (livreEtudiant != null || User.IsInRole(RolesName.Admin))
                {

                    LivreEtudiant LivreEtudiantModifier = _context.LivresEtudiants.ToList().Find(x => x.LivreId == form.LivreId);
                    LivreEtudiantModifier.MaisonEdition = form.MaisonEdition;
                    LivreEtudiantModifier.Isbn = form.Isbn;
                    LivreEtudiantModifier.Titre = form.Titre;
                    LivreEtudiantModifier.Resume = form.Resume;
                    LivreEtudiantModifier.Auteur = form.Auteur;
                    LivreEtudiantModifier.PhotoCouverture = form.Photo;
                    LivreEtudiantModifier.DatePublication = form.DatePublication;
                    LivreEtudiantModifier.Prix = form.Prix;

                    _context.LivresEtudiants.Update(LivreEtudiantModifier);
                    _context.SaveChanges();
                    return View("succesModifierUsage", LivreEtudiantModifier);
                }
                return Content("Ce livre ne vous appartient pas. Vous ne pouvez pas le modifier");
            }
            var ModelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            foreach (var error in ModelErrors)
            {
                if (error.ErrorMessage.StartsWith("The value"))
                {
                    ModelState.AddModelError(string.Empty, "Le format d'un prix est incorrect. Voici un exemple du bon format: 10,45");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }
            return View(form);
        }

        [Route("Usage/effacer/{id?}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> effacer(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }
            var livreSupprimer = _context.LivresEtudiants.ToList().Find(x => x.LivreId == id);
            
            if (livreSupprimer == null)
            {
                Response.StatusCode = 404;
                return Content("Ce livre n'existe pas dans la base de données");
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Etudiant etudiant = _context.Etudiants.ToList().Find(x => x.Id == userId);
            var livreAssocierEtudient = _context.LivresEtudiants
                .ToList()
                .Find(x => x.Etudiant.Id == etudiant.Id && x.LivreId == id);
            if (livreAssocierEtudient != null || User.IsInRole(RolesName.Admin))
            {
                _context.LivresEtudiants.Remove(livreSupprimer);
                _context.SaveChanges();

                List<LivreEtudiant> inventaireLivreEtudiant = _context.LivresEtudiants
                  .Include(x => x.Etudiant)
                  .ToList();

                return RedirectToAction("MaBoutique");
            }
            return Content("Ce livre ne vous appartient pas. Vous ne pouvez pas l'effacer");
        }

    }
}