﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class GestionProfilController : Controller
    {
        private readonly SignInManager<Etudiant> _signInManager;
        private readonly UserManager<Etudiant> _userManager;
        private readonly ApplicationDbContext _context;

        public GestionProfilController(
            SignInManager<Etudiant> signInManager,
            UserManager<Etudiant> userManager,
            ApplicationDbContext context
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Retourne la page de modification de l'étudiant courant.
        /// </summary>
        /// <returns>¨Page de modification d'étudiant.</returns>
        [Authorize (Roles = RolesName.Etudiant)]
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            if (User.IsInRole(RolesName.Etudiant))
            {
                Etudiant utilisateurCourant = await GetEtudiantCourantAsync();

                Adresse adresse = utilisateurCourant.GetAdresse(_context);

                GestionProfilVM vm = new()
                {
                    Courriel = utilisateurCourant.Email,
                    Nom = utilisateurCourant.Nom,
                    Prenom = utilisateurCourant.Prenom,
                    NoTelephone = utilisateurCourant.PhoneNumber,
                    ProgrammeEtudeId = utilisateurCourant.ProgrammeEtudeId,
                    ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom)),

                    NoCivique = adresse.NumeroCivique.ToString(),
                    Rue = adresse.Rue,
                    Ville = adresse.Ville,
                    App = adresse.App,
                    CodePostal = adresse.CodePostal,
                    ProvinceId = adresse.Province.ProvinceId,

                    Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom)),
                };

                return View(vm);
            }
            return Content("Accès interdit");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> IndexAsync(GestionProfilVM vm)
        {
            if (User.IsInRole(RolesName.Etudiant))
            {

                ModelState.Remove(nameof(vm.ProgrammeEtudes));
                ModelState.Remove(nameof(vm.Provinces));

                if (vm.CodePostal != null)
                {
                    vm.CodePostal = vm.CodePostal.ToUpper();
                }

                if (ModelState.IsValid)
                {

                    Etudiant utilisateurCourant = await GetEtudiantCourantAsync();

                    Adresse adresse = utilisateurCourant.GetAdresse(_context);

                    adresse.App = vm.App;
                    adresse.CodePostal = vm.CodePostal;
                    adresse.NumeroCivique = Convert.ToInt32(vm.NoCivique);
                    adresse.Rue = vm.Rue;
                    adresse.Ville = vm.Ville;
                    adresse.ProvinceId = vm.ProvinceId;

                    utilisateurCourant.Email = vm.Courriel;
                    utilisateurCourant.UserName = vm.Courriel;
                    utilisateurCourant.Nom = vm.Nom;
                    utilisateurCourant.Prenom = vm.Prenom;
                    utilisateurCourant.PhoneNumber = vm.NoTelephone;
                    utilisateurCourant.ProgrammeEtudeId = vm.ProgrammeEtudeId;

                    _context.SaveChanges();
                }

                vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
                vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));

                return View(vm);
            }
            return Content("Action interdite");
        }

        [Authorize(Roles = RolesName.Etudiant)]
        private async Task<Etudiant> GetEtudiantCourantAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
