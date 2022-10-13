﻿using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Data;

namespace vlissides_bibliotheque.Controllers
{
    public class BoutiqueController : Controller
    {
        private readonly ILogger<InventaireController> _logger;
        private readonly ApplicationDbContext _context;

        public BoutiqueController(ILogger<InventaireController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult ma_boutique()
        {
            return View();
        }
    }
}