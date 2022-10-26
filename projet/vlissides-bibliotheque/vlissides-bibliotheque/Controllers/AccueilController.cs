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

namespace vlissides_bibliotheque.Controllers
{
    [AllowAnonymous]
    public class AccueilController : Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly ApplicationDbContext _context;

        public AccueilController(ILogger<AccueilController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Route("")]
        public IActionResult Accueil()
        {
            List<Evenement> listEvenements = _context.Evenements.OrderBy(i => i.Debut).Take(4).ToList();

            RecommendationPromotionsVM recommendationPromotions = new() { tuileLivreBibliotequeVMs = GetQuatreLivres.GetInventaireBibliotequeVMs(_context), evenements = listEvenements };

            return View(recommendationPromotions);
        }

        public IActionResult Actualiter()
        {

            List<Evenement> listEvenements = new();
            listEvenements = _context.Evenements.ToList();
            return View(listEvenements);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}