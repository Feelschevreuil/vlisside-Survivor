﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    /// <summary>
    /// Classe <c>ConnexionController</c> gère les url(s) pour les pages
    /// relatives à la connexion d'un utilisateur.
    /// </summary>
    public class ConnexionController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ConnexionController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Retourne la page de connexion pour un utilisateur.
        /// </summary>
        /// <returns>Page de connexion.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Valide la connexion de l'utilisateur.
        /// </summary>
        /// <returns>
        /// Succès : Page d'accueil avec utilisateur connecté.
        /// <br></br>
        /// Erreurs : Page de connexion avec messages d'erreur.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> IndexAsync(ConnexionVM vm)
        {
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }
                if (!result.IsLockedOut) {
                    ModelState.AddModelError(string.Empty, "Tentative de connexion invalide.");
                    return View(vm);
                }
            }

            return View(vm);
        }

        /// <summary>
        /// Retourne la page de création d'un nouvel utilisateur.
        /// </summary>
        /// <returns>Page d'inscription.</returns>
        [HttpGet]
        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inscription(InscriptionVM vm)
        {
            if (ModelState.IsValid) {

                // model binding
                //Etudiant etudiant = ;

                // création
                //var result = await _userManager.CreateAsync(etudiant, vm.Password);

                //if (result.Succeeded) {

                    // ajouter rôle
                    //await _userManager.AddToRoleAsync(etudiant, "Etudiant");

                    // connecter le nouvel étudiant
                    //await _signInManager.SignInAsync(etudiant, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                //}

                //foreach (var error in result.Errors) {
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }
            return View(vm);
        }

        /// <summary>
        /// Retourne la page de déconnexion pour un utilisateur.
        /// </summary>
        /// <returns>Page d'accueil.</returns>
        public IActionResult Logout()
        {
            return View();
        }
    }
}
