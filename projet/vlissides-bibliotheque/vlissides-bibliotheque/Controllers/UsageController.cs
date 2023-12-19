using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using vlissides_bibliotheque.Commun;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class UsageController : Controller
    {
        private readonly ILogger<UsageController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly IDAO<LivreEtudiant> _livreEtudiantDAO;
        private readonly ILivreEtudiant _livreEtudiantService;

        public UsageController(ILogger<UsageController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IDAO<LivreEtudiant> livreEtudiantDAO, IMapper mapper, ILivreEtudiant livreEtudiantService)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _livreEtudiantDAO= livreEtudiantDAO;
            _livreEtudiantService = livreEtudiantService;
        }

        [Route("Usage/Index")]
        [Route("Usage/Index/{id?}")]
        [Route("Usage/{id?}")]
        [HttpGet]
        public IActionResult Usage()
        {
            List<LivreEtudiant> livreEtudiants = _context.LivresEtudiants
                    .Include(x => x.Etudiant)
                    .Take(8)
                    .ToList();


            return View(livreEtudiants);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            LivreEtudiant livre = _livreEtudiantDAO.GetById(id);
            if (livre != null)
                return View(_mapper.Map<LivreEtudiantDto>(livre));

            return Content("Ce livre n'existe pas dans la base de données.");
        }

        [Route("Usage/MaBoutique")]
        [HttpGet]
        public IActionResult MaBoutique()
        {
            return View(_livreEtudiantService.GetAllLivreEtudiant(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [Route("Usage/ajouter")]
        [Authorize(Roles = Constante.Etudiant)]
        public IActionResult ajouter()
        {
            AjoutEditLivreEtudiantVM livre = new();

            return View(livre);
        }

        [HttpPost]
        [Route("Usage/ajouter")]
        [Authorize(Roles = Constante.Etudiant)]
        public IActionResult ajouter(AjoutEditLivreEtudiantVM livreEtudiantVM)
        {
            ModelState.Remove(nameof(AjoutEditLivreEtudiantVM.EtudiantId));

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                LivreEtudiant livreEtudiant = new()
                {
                    LivreId = livreEtudiantVM.LivreId,
                    Etudiant = _context.Etudiants.Single(x => x.Id == userId),
                    Titre = livreEtudiantVM.Titre,
                    Isbn = livreEtudiantVM.Isbn,
                    Resume = livreEtudiantVM.Resume,
                    PhotoCouverture = livreEtudiantVM.Photo,
                    DatePublication = livreEtudiantVM.DatePublication,
                    MaisonEdition = livreEtudiantVM.MaisonEdition,
                    Auteur = livreEtudiantVM.Auteur,
                    Prix = livreEtudiantVM.Prix

                };
                _livreEtudiantDAO.Insert(livreEtudiant);
                _livreEtudiantDAO.Save();
                return RedirectToAction("MaBoutique");
            }
            var ModelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            foreach (var error in ModelErrors)
            {
                if (error.ErrorMessage.StartsWith("The value"))
                {
                    ModelState.AddModelError(string.Empty, "TODO: Gérer les messages d'erreurs");
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
            if (!id.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Etudiant etudiant = await _context.Etudiants.SingleAsync(x => x.Id == userId);
            LivreEtudiant livreEtudiantRechercher = _livreEtudiantDAO.GetById(id.Value);

            if (livreEtudiantRechercher.Etudiant.Id == userId || User.IsInRole(Constante.Admin))
            {
                AjoutEditLivreEtudiantVM livreEtudiant = new()
                {
                    LivreId = livreEtudiantRechercher.LivreId,
                    EtudiantEmail = livreEtudiantRechercher.Etudiant.Email,
                    EtudiantId = livreEtudiantRechercher.Etudiant.Id,
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
        public ActionResult Modifier(AjoutEditLivreEtudiantVM form)
        {
            ModelState.Remove(nameof(AjoutEditLivreEtudiantVM.EtudiantId));


            if (ModelState.IsValid)
            {
                LivreEtudiant livreEtudiant = _livreEtudiantService.GetLivreByEtudiantId(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (livreEtudiant != null || User.IsInRole(Constante.Admin))
                {
                    LivreEtudiant LivreEtudiantModifier = _livreEtudiantDAO.GetById(livreEtudiant.LivreId);

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
                    ModelState.AddModelError(string.Empty, "TODO gerer erreur");
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
        public ActionResult Effacer(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }
            var livreSupprimer = _livreEtudiantDAO.GetById(id.Value);

            if (livreSupprimer == null)
            {
                Response.StatusCode = 404;
                return Content("Ce livre n'existe pas dans la base de données");
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_livreEtudiantService.GetLivreByEtudiantId(userId) != null || User.IsInRole(Constante.Admin))
            {
                _livreEtudiantDAO.Delete(id.Value);
                _livreEtudiantDAO.Save();

                List<LivreEtudiant> inventaireLivreEtudiant = _livreEtudiantDAO.GetAll().ToList();

                return RedirectToAction("MaBoutique");
            }
            return Content("Ce livre ne vous appartient pas. Vous ne pouvez pas l'effacer");
        }
    }
}