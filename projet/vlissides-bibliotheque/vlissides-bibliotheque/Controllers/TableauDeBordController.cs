﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
	[Authorize(Roles =RolesName.Admin)]
	public class TableauDeBordController : Controller
	{
		private readonly UserManager<Utilisateur> _userManager;
		private readonly UserManager<Etudiant> _userManagerEtudiant;
        private readonly ApplicationDbContext _context;

		public TableauDeBordController(
			UserManager<Utilisateur> userManager,
			ApplicationDbContext context
		)
		{
			_userManager = userManager;
			_context = context;
		}

		public ActionResult Index()
		{
			return View();
		}

		public IActionResult Etudiants()
		{
			List<Etudiant> etudiants = _userManagerEtudiant.Users
				.Include(etudiant => etudiant.ProgrammeEtude)
				.Include(etudiant => etudiant.Adresse)
				.Include(etudiant => etudiant.Adresse.Province)
                .ToList();

            return View(etudiants);
		}
	}
}
