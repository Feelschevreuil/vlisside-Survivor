using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.Commun;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class InventaireController : Controller
    {
        private readonly ILogger<InventaireController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDAO<LivreBibliotheque> _livreDAO;
        private readonly ILivreTest _livreTest;
        private readonly IDAO<Auteur> _auteurDAO;
        private readonly IDAO<Cours> _courDAO;
        private readonly ILivreBibliotheque _livreService;
        private readonly ICheckedBox _CheckedBox;
        private readonly IDropDownList _dropDownList;
        private readonly IMapper _mapper;


        public InventaireController(ILogger<InventaireController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment,
            IDAO<LivreBibliotheque> livreDAO, ICheckedBox checkedBox, IDropDownList dropDownList, ILivreBibliotheque livreService,
            IDAO<Auteur> auteurDAO, IMapper mapper, IDAO<Cours> courDAO, ILivreTest livreTest)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _livreDAO = livreDAO;
            _auteurDAO = auteurDAO;
            _CheckedBox = checkedBox;
            _dropDownList = dropDownList;
            _livreService = livreService;
            _mapper = mapper;
            _courDAO = courDAO;
            _livreTest = livreTest;
        }

        public IActionResult Bibliotheque()
        {
            return View(_livreService.GetTuileLivreBibliotequeInventaire());
        }

        public ActionResult Detail(int id)
        {
            if (_livreDAO.GetById(id) == null)
                return Content("Ce livre n'existe pas dans la base de données.");

            return View(_livreService.GetLivreDetailVM(id));
        }

        [Authorize(Roles = Constante.Admin)]
        [HttpPost]
        public ActionResult AjoutEditLivre([FromBody] AjoutEditLivreVM form)
        {
            ModelState.Remove(nameof(AjoutEditLivreVM.MaisonsDeditions));
            ModelState.Remove(nameof(AjoutEditLivreVM.CheckBoxCours));
            ModelState.Remove(nameof(AjoutEditLivreVM.CheckBoxAuteurs));
            ModelState.Remove(nameof(AjoutEditLivreVM.DateFormater));

            if(form == null)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                // Log or return errors for inspection
                return BadRequest(errors);
            }

            if (!ModelState.IsValid)
            {
                if (form.Id == 0)
                {

                    var ModelErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                    foreach (var error in ModelErrors)
                    {
                        if (error.Exception == null)
                            ModelState.AddModelError(string.Empty, "TODO: Gérer l'erreur");
                        else
                            ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    form.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
                    form.CheckBoxCours = _CheckedBox.GetCours();
                    form.CheckBoxAuteurs = _CheckedBox.GetAuteurs();

                    return View(form);
                }
                else
                {
                    form.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
                    form.CheckBoxCours = _CheckedBox.GetCoursLivre(form.Id);
                    form.CheckBoxAuteurs = _CheckedBox.GetAuteursLivre(form.Id);
                    return View("Views/Inventaire/AjoutEditBibliotheque.cshtml", form);
                }
            }

            var livre = new LivreBibliotheque();

            if (form.Id == 0)
                livre = Creer(form);
            else
                livre = Modifier(form);

            return View("Views/Inventaire/Bibliotheque.cshtml", _livreService.GetTuileLivreBibliotequeInventaire());
        }

        [Authorize(Roles = Constante.Admin)]
        [HttpGet]
        public ActionResult Creer()
        {
            AjoutEditLivreVM nouveauLivre = new()
            {
                CheckBoxAuteurs = _CheckedBox.GetAuteurs(),
                CheckBoxCours = _CheckedBox.GetCours(),
                MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition(),
            };
            return View("Views/Inventaire/AjoutEditBibliotheque.cshtml", nouveauLivre);
        }


        private LivreBibliotheque Creer(AjoutEditLivreVM form)
        {
            LivreBibliotheque livre = new();

            _mapper.Map(form, livre);

            _livreDAO.Insert(livre);
            _livreDAO.Save();

            return livre;
        }


        [Authorize(Roles = Constante.Admin)]
        [HttpGet]
        public ActionResult Modifier(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un livre de la base de données.");
            }

            AjoutEditLivreVM modifierLivre = _mapper.Map<AjoutEditLivreVM>(_livreDAO.GetById(id.Value));
            modifierLivre.MaisonsDeditions = _dropDownList.ListDropDownMaisonDedition();
            modifierLivre.CheckBoxAuteurs = _CheckedBox.GetAuteursLivre(id.Value);
            modifierLivre.CheckBoxCours = _CheckedBox.GetCoursLivre(id.Value);

            return View("Views/Inventaire/AjoutEditBibliotheque.cshtml", modifierLivre);
        }



        private LivreBibliotheque Modifier(AjoutEditLivreVM form)
        {
            //LivreBibliotheque livre = _livreDAO.GetById(form.Id);

            //_mapper.Map(form, livre);

            var livre = _livreTest.MiseAJour(form);
            //_livreDAO.Save();

            return livre;
        }

        [Authorize(Roles = Constante.Admin)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Effacer(int? id)
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


            _livreDAO.Delete(id.Value);
            _livreDAO.Save();

            return RedirectToAction("Bibliotheque");
        }

        [HttpGet]
        public IActionResult CreerAuteurs()
        {
            AuteurVM auteurs = new();
            return PartialView("Views/Shared/_AjouteEditAuteursPartial.cshtml", auteurs);
        }

        [HttpPost]
        public IActionResult CreerAuteurs([FromBody] AuteurVM vm)
        {
            ModelState.Remove(nameof(vm.Id));

            if (!ModelState.IsValid)
                return PartialView("Views/Shared/_AjouteEditAuteursPartial.cshtml", vm);


            var nouvelleAuteurs = new Auteur();

            _mapper.Map(vm, nouvelleAuteurs);

            _auteurDAO.Insert(nouvelleAuteurs);
            _auteurDAO.Save();
            vm.Id = nouvelleAuteurs.AuteurId;

            return Json(vm);
        }

        //public LivreBibliotheque AssignerCoursLivre(LivreBibliotheque livre, List<int> courdIds)
        //{
        //    livre.Cours.Clear();
        //    foreach (var id in courdIds)
        //        livre.Cours.Add(_courDAO.GetById(id));

        //    return livre;
        //}
        
        //public LivreBibliotheque AssignerAuteursLivre(LivreBibliotheque livre, List<int> auteurIds)
        //{
        //    livre.Auteurs.Clear();
        //    foreach (var id in auteurIds)
        //        livre.Auteurs.Add(_auteurDAO.GetById(id));

        //    return livre;
        //}

    }
}
