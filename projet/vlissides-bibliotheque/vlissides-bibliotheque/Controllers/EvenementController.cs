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

            List<Evenement> listEvenements = new();
            listEvenements = _context.Evenements.ToList();
            return View(listEvenements);
        }
    }
}