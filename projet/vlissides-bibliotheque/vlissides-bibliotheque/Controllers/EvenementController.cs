using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using Microsoft.AspNetCore.Authorization;
using vlissides_bibliotheque.Extensions;
using AutoMapper;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.DTO;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using vlissides_bibliotheque.Commun;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize(Roles = Constante.Admin)]
    public class EvenementController : Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly IEvenementVM _evenementService;
        private readonly IDAO<Evenement> _evenementDAO;
        private readonly IMapper _mapper;


        public EvenementController(ILogger<AccueilController> logger, IEvenementVM evenementService,
           IDAO<Evenement> evenementDAO, IMapper mapper)
        {
            _logger = logger;
            _evenementService = evenementService;
            _evenementDAO = evenementDAO;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [Route("Evenement/Index")]
        public async Task<IActionResult> Evenements()
        {
            List<EvenementVM> listEvenementsVM = await _evenementService.GetEvenementInventaire();
            return View(listEvenementsVM);
        }
        [AllowAnonymous]
        public IActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Aucun identifiant n'a été trouvée.");
            }
            var Evenement = _evenementDAO.GetById(id.Value);
            if (Evenement == null)
            {
                Response.StatusCode = 404;
                return Content("Cette événement n'existe pas dans la base de données");
            };
            return View(_mapper.Map<EvenementDto>(Evenement));
        }

        [HttpGet]
        public IActionResult Creer()
        {
            EvenementVM evenementVM = new();

            return View(evenementVM);
        }

        [HttpPost]
        public ActionResult Creer(EvenementVM evenementVM)
        {
            if (evenementVM.Fin < evenementVM.Debut)
                ModelState.AddModelError(string.Empty, "La date de début doit être avant ou égale la date de fin");


            if (ModelState.IsValid)
            {
                var newEvenement = new Evenement();

                _mapper.Map(evenementVM, newEvenement);

                _evenementDAO.Insert(newEvenement);
                _evenementDAO.Save();

                return RedirectToAction("Evenements");
            }
            return View(evenementVM);
        }

        [Route("Evenement/modifier/{id?}")]
        [HttpGet]
        public ActionResult Modifier(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un événement de la base de données.");
            }

            Evenement evenement = _evenementDAO.GetById(id.Value);

            if (evenement == null)
                return Content("L'événement recherche n'a pas été trouvé dans la base de données");


            return View(_mapper.Map<EvenementVM>(evenement));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Modifier(EvenementVM evenementVM)
        {
            if (evenementVM.Fin < evenementVM.Debut)
                ModelState.AddModelError(string.Empty, "La date de début doit être avant la date de fin");


            if (ModelState.IsValid)
            {
                Evenement evenementModifier = _evenementDAO.GetById(evenementVM.EvenementId);

                if (evenementModifier == null)
                    Content("L'événement que vous tentez de modifier n'a pas été trouver dans la base de données");


                _mapper.Map(evenementVM, evenementModifier);

                _evenementDAO.Update(evenementModifier);
                _evenementDAO.Save();
                return RedirectToAction("Evenements");
            }

            return View(evenementVM);

        }
        [Route("Evenement/effacer/{id?}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Effacer(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un événement de la base de données.");
            }
            var EvenementSupprimer = _evenementDAO.GetById(id.Value);
            if (EvenementSupprimer == null)
            {
                Response.StatusCode = 404;
                return Content("Cette événement n'existe pas dans la base de données");
            };

            _evenementDAO.Delete(EvenementSupprimer.EvenementId);
            _evenementDAO.Save();

            return RedirectToAction("Evenements");
        }
    }
}