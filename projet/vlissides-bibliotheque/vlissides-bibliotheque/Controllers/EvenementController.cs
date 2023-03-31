﻿using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using Microsoft.AspNetCore.Authorization;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Extensions;
using AutoMapper;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.DTO;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize(Roles = RolesName.Admin)]
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
        public async Task<IActionResult> Creer(EvenementVM evenementVM)
        {
            if (!DateEvenement.CompareDate(evenementVM.Debut, evenementVM.Fin))
            {
                ModelState.AddModelError(string.Empty, "La date de début doit être avant ou égale la date de fin");
            }

            if (ModelState.IsValid)
            {
                Evenement newEvenement = _evenementService.GetEvenement(evenementVM);
                _evenementDAO.Insert(newEvenement);
                _evenementDAO.Save();

                return RedirectToAction("Evenements");
            }
            return View(evenementVM);
        }

        [Route("Evenement/modifier/{id?}")]
        [HttpGet]
        public async Task<ActionResult> modifier(int? id)
        {
            if (!id.HasValue)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un événement de la base de données.");
            }

            Evenement evenement = _evenementDAO.GetById(id.Value);

            if (evenement != null)
            {
                return View(_evenementService.GetEvenementVM(evenement));
            }
            return Content("L'événement recherche n'a pas été trouvé dans la base de données");

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult modifier(EvenementVM evenementVM)
        {
            if (!DateEvenement.CompareDate(evenementVM.Debut, evenementVM.Fin))
            {
                ModelState.AddModelError(string.Empty, "La date de début doit être avant la date de fin");
            }

            if (ModelState.IsValid)
            {
                Evenement evenementModifier = _evenementDAO.GetById(evenementVM.EvenementId);

                if (evenementModifier != null)
                {
                    {
                        evenementModifier.EvenementId = evenementVM.EvenementId;
                        evenementModifier.Debut = evenementVM.Debut;
                        evenementModifier.Fin = evenementVM.Fin;
                        evenementModifier.Commanditaire = evenementVM.Commanditaire;
                        evenementModifier.CommanditaireId = evenementVM.CommanditaireId;
                        evenementModifier.Nom = evenementVM.Nom;
                        evenementModifier.Description = evenementVM.Description;
                        evenementModifier.Image = evenementVM.Image;
                    }
                    _evenementDAO.Update(evenementModifier);
                    _evenementDAO.Save();
                    return RedirectToAction("Evenements");
                }
                return Content("L'événement que vous tentez de modifier n'a pas été trouver dans la base de données");
            }
            return View(evenementVM);

        }
        [Route("Evenement/effacer/{id?}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult effacer(int? id)
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