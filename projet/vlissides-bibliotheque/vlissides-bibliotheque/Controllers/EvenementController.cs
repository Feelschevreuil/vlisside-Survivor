using Microsoft.AspNetCore.Mvc;
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

        }
    }
}