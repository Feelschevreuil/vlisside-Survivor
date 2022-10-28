﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Route("Usage/MaBoutique")]
        public IActionResult MaBoutique()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            InventaireLaBlunVM inventaireUnEtudiant = new()
            {
                inventaireLivreEtudiantVMs = new()
            };
            List<LivreEtudiant> livreEtudiants = _context.LivresEtudiants
                    .Include(x => x.Etudiant)
                    .ToList();

            List<LivreEtudiant> livres = livreEtudiants.FindAll(x => x.Etudiant.Id == userId);
            if (livres.Count() == 0)
            {
                return View(inventaireUnEtudiant);
            }
            inventaireUnEtudiant.inventaireLivreEtudiantVMs.AddRange(livres);
            return View(inventaireUnEtudiant);

        }

        [Route("Usage/ajouter")]
        [Authorize (Roles =RolesName.Etudiant)]
        public IActionResult ajouter()
        {
            LivreEtudiant livre = new();

            return View(livre);
        }

        [HttpPost]
        [Route("Usage/ajouter")]
        [Authorize(Roles = RolesName.Etudiant)]
        public IActionResult ajouter(LivreEtudiant livreEtudiant)
        {
            ModelState.Remove("Etudiant.Nom");
            ModelState.Remove("Etudiant.Prenom");
            ModelState.Remove("Etudiant.Adresse");
            ModelState.Remove("Etudiant.ProgrammeEtude");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            livreEtudiant.Etudiant = _context.Etudiants.ToList().Find(x => x.Id == userId);


            if (ModelState.IsValid)
            {

                _context.LivresEtudiants.Add(livreEtudiant);
                _context.SaveChanges();
                return RedirectToAction("MaBoutique");
            }
            return View(livreEtudiant);
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
                return View(livreEtudiantRechercher);
            }
            return Content("Ce livre ne vous appartient pas. Vous ne pouvez pas le modifier");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> modifier(LivreEtudiant form)
        {
            ModelState.Remove("Etudiant.Nom");
            ModelState.Remove("Etudiant.Prenom");
            ModelState.Remove("Etudiant.Adresse");
            ModelState.Remove("Etudiant.ProgrammeEtude");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Etudiant etudiant = _context.Etudiants.ToList().Find(x => x.Id == userId);
            if (form.Etudiant.Id == userId || User.IsInRole(RolesName.Admin))
            {
                if (ModelState.IsValid)
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
            return Content("Ce livre ne vous appartient pas. Vous ne pouvez pas le modifier");
        }

        [Route("Usage/effacer/{id?}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> effacer(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Etudiant etudiant = _context.Etudiants.ToList().Find(x => x.Id == userId);
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
            var livreAssocierEtudient = _context.LivresEtudiants
                .ToList()
                .Find(x => x.Etudiant.Id == etudiant.Id && x.LivreId == id);
            if (livreAssocierEtudient  != null || User.IsInRole(RolesName.Admin))
            {
                _context.LivresEtudiants.Remove(livreSupprimer);
                _context.SaveChanges();

                InventaireLaBlunVM inventaireLivreEtudiant = new()
                {
                    inventaireLivreEtudiantVMs = _context.LivresEtudiants
                  .Include(x => x.Etudiant)
                  .ToList()
                };
                return RedirectToAction("MaBoutique");
            }
            return Content("Ce livre ne vous appartient pas. Vous ne pouvez pas l'effacer");
        }

    }
}