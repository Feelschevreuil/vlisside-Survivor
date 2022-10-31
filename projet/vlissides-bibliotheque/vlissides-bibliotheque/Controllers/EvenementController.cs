﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;
using System.Collections;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Exercice_Ajax.DTO;
using Newtonsoft.Json;
using vlissides_bibliotheque.Constantes;

namespace vlissides_bibliotheque.Controllers
{
    [AllowAnonymous]
    public class EvenementController : Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly ApplicationDbContext _context;

        public EvenementController(ILogger<AccueilController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Evenements()
        {

            List<EvenementVM> listEvenementsVM = new();
            List<Evenement> listEvenements = _context.Evenements.ToList();
            listEvenementsVM = GetEvenement.GetEvenements(listEvenements);


            return View(listEvenementsVM);
        }
        
        [Authorize(Roles = RolesName.Admin)]
        [HttpGet]
        public IActionResult Creer()
        {
            EvenementVM evenementVM = new();

            return View(evenementVM);
        }
        [Authorize(Roles = RolesName.Admin)]
        [HttpPost]
        public IActionResult Creer(EvenementVM evenementVM)
        {
            if (ModelState.IsValid)
            {
                Evenement newEvenement = new()
                {
                    EvenementId = evenementVM.EvenementId,
                    Commanditaire = evenementVM.Commanditaire,
                    CommanditaireId = evenementVM.CommanditaireId,
                    Debut = evenementVM.Debut,
                    Fin = evenementVM.Fin,
                    Image = evenementVM.Image,
                    Nom = evenementVM.Nom,
                    Description = evenementVM.Description,
                };
                _context.Evenements.Add(newEvenement);
                _context.SaveChanges();
                return RedirectToAction("Evenements");

            }
            return View(evenementVM);
        }

        [Authorize(Roles =RolesName.Admin)]
        [Route("Evenement/modifier/{id?}")]
        [HttpGet]
        public async Task<ActionResult> modifier(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 400;
                return Content("Cette identifiant n'est pas associer à un événement de la base de données.");
            }
            Evenement evenement  = _context.Evenements
               .Include(x => x.Commanditaire)
               .ToList()
               .Find(x => x.EvenementId == id);

            if (evenement != null)
            {
                return View(GetEvenement.GetUnEvenement(evenement));
            }
            return Content("L'événement recherche n'a pas été trouvé dans la base de données");

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> modifier (EvenementVM evenementVM)
        {
            if (ModelState.IsValid)
            {
                Evenement modifyevenement = _context.Evenements.ToList().Find(x => x.EvenementId == evenementVM.EvenementId);
                if (modifyevenement != null)
                {
                    {
                        modifyevenement.EvenementId = evenementVM.EvenementId;
                        modifyevenement.Commanditaire = evenementVM.Commanditaire;
                        modifyevenement.CommanditaireId = evenementVM.CommanditaireId;
                        modifyevenement.Debut = evenementVM.Debut;
                        modifyevenement.Fin = evenementVM.Fin;
                        modifyevenement.Image = evenementVM.Image;
                        modifyevenement.Nom = evenementVM.Nom;
                        modifyevenement.Description = evenementVM.Description;
                    };
                    _context.Evenements.Update(modifyevenement);
                    _context.SaveChanges();
                    return RedirectToAction("Evenements");
                }
                return Content("L'événement que vous tentez de modifier n'a pas été trouver dans la base de données");
            }
            return View(evenementVM);

        }
    }
}