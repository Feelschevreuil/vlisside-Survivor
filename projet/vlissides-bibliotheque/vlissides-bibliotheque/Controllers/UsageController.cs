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
        [Route("Usage/{id?}")]
        public IActionResult Usage(int? id)
        {
            InventaireLaBlunVM inventaireLivreEtudiant = new()
            {
                inventaireLivreEtudiantVMs = new()
            };
            List<LivreEtudiant> livreEtudiants = _context.LivresEtudiants
                    .Include(x => x.Etudiant)
                    .ToList();

            if (id == null)
            {
                inventaireLivreEtudiant.inventaireLivreEtudiantVMs = livreEtudiants;
                return View(inventaireLivreEtudiant);
            }
            var livreRecherche = livreEtudiants.Find(x => x.LivreId == id);
            if (livreRecherche == null)
            {
                return Content("Cette identifiant n'appartient à aucun livre");
            }
            inventaireLivreEtudiant.inventaireLivreEtudiantVMs.Add(livreRecherche);
            return View(inventaireLivreEtudiant);

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

            LivreEtudiant livreEtudiantRechercher = _context.LivresEtudiants
                .Include(x => x.Etudiant)
                .ToList()
                .Find(x => x.LivreId == id);

            return View(livreEtudiantRechercher);

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> modifier(LivreEtudiant form)
        {
            ModelState.Remove("Etudiant.Nom");
            ModelState.Remove("Etudiant.Prenom");
            ModelState.Remove("Etudiant.Adresse");
            ModelState.Remove("Etudiant.ProgrammeEtude");


            if(ModelState.IsValid)
            {
                LivreEtudiant LivreEtudiantModifier = _context.LivresEtudiants.ToList().Find(x => x.LivreId == form.LivreId);
                LivreEtudiantModifier.MaisonEdition = form.MaisonEdition;
                LivreEtudiantModifier.Isbn = form.Isbn;
                LivreEtudiantModifier.Titre = form.Titre;
                LivreEtudiantModifier.Resume = form.Resume;
                LivreEtudiantModifier.Auteur = form.Auteur;
                LivreEtudiantModifier.PhotoCouverture = form.PhotoCouverture;
                LivreEtudiantModifier.DatePublication = form.DatePublication;
                LivreEtudiantModifier.Prix = form.Prix;

                _context.LivresEtudiants.Update(LivreEtudiantModifier);
                _context.SaveChanges();


                return View("succesModifierUsage", LivreEtudiantModifier);
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

            _context.LivresEtudiants.Remove(livreSupprimer);
            _context.SaveChanges();

            InventaireLaBlunVM inventaireLivreEtudiant = new()
            {
                inventaireLivreEtudiantVMs = _context.LivresEtudiants
              .Include(x => x.Etudiant)
              .ToList()
            };
            return RedirectToAction("Usage", inventaireLivreEtudiant);
        }

    }
}