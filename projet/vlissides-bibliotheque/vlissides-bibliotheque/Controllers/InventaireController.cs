using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.DTO.Ajax;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class InventaireController : Controller
    {
        private readonly ILogger<InventaireController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDAO<LivreBibliotheque> _livreDAO;
        private readonly IDAO<Auteur> _auteurDAO;
        private readonly ILivreBibliotheque _livreService;
        private readonly ICheckedBox _CheckedBox;
        private readonly IDropDownList _dropDownList;


        public InventaireController(ILogger<InventaireController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IDAO<LivreBibliotheque> livreDAO, ICheckedBox checkedBox, IDropDownList dropDownList, ILivreBibliotheque livreService, IDAO<Auteur> auteurDAO)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _livreDAO = livreDAO;
            _auteurDAO = auteurDAO;
            _CheckedBox = checkedBox;
            _dropDownList = dropDownList;
            _livreService = livreService;
        }

        public IActionResult Bibliotheque()
        {
            List<TuileLivreBibliotequeVM> inventaireBibliotheque = _livreService.GetTuileLivreBibliotequeInventaire().Result;
            return View(inventaireBibliotheque);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (_livreDAO.GetById(id) != null)
            {
                return View(_livreService.GetLivreDetailVM(id));
            }

            return Content("Ce livre n'existe pas dans la base de données.");
        }

        [Authorize(Roles = RolesName.Admin)]
        [HttpGet]
        public ActionResult creer()
        {
            AjoutEditLivreVM nouveauLivre = new()
            {
                CheckBoxAuteurs = _CheckedBox.GetAuteurs(),
                CheckBoxCours = _CheckedBox.GetCours(),
                MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition(),
            };
            return View(nouveauLivre);
        }

        [Authorize(Roles = RolesName.Admin)]
        [HttpPost]
        public async Task<ActionResult> creer([FromBody] AjoutEditLivreVM form)
        {
            ModelState.Remove(nameof(AjoutEditLivreVM.MaisonsDeditions));
            ModelState.Remove(nameof(AjoutEditLivreVM.CheckBoxCours));
            ModelState.Remove(nameof(AjoutEditLivreVM.CheckBoxAuteurs));
            ModelState.Remove(nameof(AjoutEditLivreVM.DateFormater));

            if (ModelState.IsValid)
            {
                LivreBibliotheque nouveauLivreBibliothèque = new()
                {
                    LivreId = 0,
                    MaisonEditionId = form.MaisonDeditionId,
                    Isbn = form.ISBN,
                    Titre = form.Titre,
                    Resume = form.Resume,
                    PhotoCouverture = form.Photo,
                    DatePublication = form.DatePublication,
                };

                _livreDAO.Insert(nouveauLivreBibliothèque);
                _livreDAO.Save();

                if (form.Cours.Any())
                {
                    List<CoursLivre> nouveauCoursLivre = form.Cours.Select(c => new CoursLivre()
                    {
                        CoursLivreId = 0,
                        CoursId = c,
                        LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                    }).ToList();

                    _context.CoursLivres.AddRange(nouveauCoursLivre);
                    _context.SaveChanges();
                }

                if (form.Auteurs.Any())
                {
                    List<AuteurLivre> nouveauAuteurLivre = form.Auteurs.Select(a => new AuteurLivre()
                    {
                        AuteurId = a,
                        LivreBibliothequeId = nouveauLivreBibliothèque.LivreId,
                    }).ToList();

                    _context.AuteursLivres.AddRange(nouveauAuteurLivre);
                    _context.SaveChanges();
                }

                _context.SaveChanges();
                return View("succesAjoutLivre", nouveauLivreBibliothèque);
            }


            form.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
            form.CheckBoxCours = _CheckedBox.GetCours();
            form.CheckBoxAuteurs = _CheckedBox.GetAuteurs();
            return View(form);
        }


        [Authorize(Roles = RolesName.Admin)]
        [HttpGet]
        public ActionResult modifier(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }

            LivreBibliotheque livreBibliothequeRechercher = _livreDAO.GetById(id.Value);

            AjoutEditLivreVM ModifierLivre = new()
            {
                IdDuLivre = livreBibliothequeRechercher.LivreId,
                MaisonDeditionId = livreBibliothequeRechercher.MaisonEditionId,
                MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition(),
                DatePublication = livreBibliothequeRechercher.DatePublication,
                ISBN = livreBibliothequeRechercher.Isbn,
                Titre = livreBibliothequeRechercher.Titre,
                Resume = livreBibliothequeRechercher.Resume,
                Photo = livreBibliothequeRechercher.PhotoCouverture,
            };

            return View(ModifierLivre);
        }


        [Authorize(Roles = RolesName.Admin)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> modifier(AjoutEditLivreVM form)
        {
            ModelState.Remove(nameof(AjoutEditLivreVM.MaisonsDeditions));
            ModelState.Remove(nameof(AjoutEditLivreVM.CheckBoxCours));
            ModelState.Remove(nameof(AjoutEditLivreVM.CheckBoxAuteurs));

            LivreBibliotheque LivreBibliothèqueModifier = _livreDAO.GetById(form.IdDuLivre);

            if (ModelState.IsValid)
            {
                LivreBibliothèqueModifier.MaisonEditionId = form.MaisonDeditionId;
                LivreBibliothèqueModifier.Isbn = form.ISBN;
                LivreBibliothèqueModifier.Titre = form.Titre;
                LivreBibliothèqueModifier.Resume = form.Resume;
                LivreBibliothèqueModifier.PhotoCouverture = form.Photo;
                LivreBibliothèqueModifier.DatePublication = form.DatePublication;

                _livreDAO.Update(LivreBibliothèqueModifier);
                _livreDAO.Save();

                return View("succesModifierLivre", LivreBibliothèqueModifier);
            }

            var ModelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            foreach (var error in ModelErrors)
            {
                if (error.Exception == null)
                {
                    ModelState.AddModelError(string.Empty, "TODO: Gérer l'erreur");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }

            form.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
            form.CheckBoxCours = _CheckedBox.GetCoursLivre(LivreBibliothèqueModifier);
            form.CheckBoxAuteurs = _CheckedBox.GetAuteursLivre(LivreBibliothèqueModifier);
            return View(form);
        }

        [Authorize(Roles = RolesName.Admin)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult supprimer(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }

            if (_livreDAO.GetById(id.Value) == null)
            {
                Response.StatusCode = 404;
                return Content("Ce livre n'existe pas dans la base de données");
            };


            _context.CoursLivres.RemoveRange(_context.CoursLivres.Where(x => x.LivreBibliothequeId == id.Value));
            _context.AuteursLivres.RemoveRange(_context.AuteursLivres.Where(x => x.LivreBibliothequeId == id.Value));
            _livreDAO.Delete(id.Value);
            _livreDAO.Save();

            return RedirectToAction("Bibliotheque");
        }

        [HttpGet]
        public IActionResult CreerAuteurs()
        {
            AuteursVM auteurs = new();
            return PartialView("Views/Shared/_AjouteEditAuteursPartial.cshtml", auteurs);
        }

        [HttpPost]
        public IActionResult CreerAuteurs([FromBody] AuteursVM vm)
        {
            ModelState.Remove(nameof(vm.AuteurId));
            ModelState.Remove(nameof(vm.Id));

            if (ModelState.IsValid)
            {
                Auteur nouvelleAuteurs = new()
                {
                    AuteurId = 0,
                    Prenom = vm.Prenom,
                    Nom = vm.Nom,
                };
                _auteurDAO.Insert(nouvelleAuteurs);
                _auteurDAO.Save();
                vm.Id = nouvelleAuteurs.AuteurId;
                return Json(vm);
            }
            return PartialView("Views/Shared/_AjouteEditAuteursPartial.cshtml", vm);
        }

        [HttpPost]
        public string? AssignerCoursLivre([FromBody] CoursAssocier coursAssocier)
        {
            List<CoursLivre> coursUpdate = new();
            List<CoursLivre> coursReset = _context.CoursLivres
                .Where(c => coursAssocier.CoursId.Any(y => y == c.CoursId) && c.LivreBibliothequeId == coursAssocier.livreId)
                .ToList();
            _context.CoursLivres.RemoveRange(coursReset);
            _context.SaveChanges();

            foreach (int cours in coursAssocier.CoursId)
            {
                CoursLivre coursLivre = new()
                {
                    CoursId = cours,
                    LivreBibliothequeId = coursAssocier.livreId
                };
                coursUpdate.Add(coursLivre);
            }
            _context.CoursLivres.AddRange(coursUpdate);
            _context.SaveChanges();


            return null;
        }
        [HttpPost]
        public string? AssignerAuteursLivre([FromBody] AuteursAssocier auteursAssocier)
        {
            List<AuteurLivre> auteurUpdate = new();
            List<AuteurLivre> auteurReset = _context.AuteursLivres
                .Where(c => auteursAssocier.AuteursId.Any(y => y == c.AuteurId) && c.LivreBibliothequeId == auteursAssocier.livreId)
                .ToList();
            _context.AuteursLivres.RemoveRange(auteurReset);
            _context.SaveChanges();

            foreach (int auteur in auteursAssocier.AuteursId)
            {
                AuteurLivre auteurLivre = new()
                {
                    AuteurId = auteur,
                    LivreBibliothequeId = auteursAssocier.livreId
                };
                auteurUpdate.Add(auteurLivre);
            }
            _context.AuteursLivres.AddRange(auteurUpdate);
            _context.SaveChanges();

            return null;
        }

    }
}
