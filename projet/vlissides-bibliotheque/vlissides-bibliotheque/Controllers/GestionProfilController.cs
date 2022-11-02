using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<Etudiant> _userManagerEtudiant;
        private readonly UserManager<Utilisateur> _userManagerAdmin;
        private readonly ApplicationDbContext _context;

        public GestionProfilController(
            SignInManager<Etudiant> signInManager,
            UserManager<Etudiant> userManagerEtudiant,
            UserManager<Utilisateur> userManagerAdmin,
            ApplicationDbContext context
            )
        {
            _signInManager = signInManager;
            _userManagerEtudiant = userManagerEtudiant;
            _userManagerAdmin = userManagerAdmin;
            _context = context;
        }

        /// <summary>
        /// Retourne la page de modification de l'étudiant courant.
        /// </summary>
        /// <returns>¨Page de modification d'étudiant.</returns>
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            if (User.IsInRole(RolesName.Etudiant))
            {
                Etudiant etudiant = await GetEtudiantCourantAsync();

                return View(etudiant.GetEtudiantProfilVM(_context));
            } 
            
            if (User.IsInRole(RolesName.Admin)) 
            {
                Utilisateur admin = await GetAdminCourantAsync();

                return View(admin.GetAdminProfilVM());
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

                vm = SetEtudiantProfilVM(vm);

                if (ModelState.IsValid)
                {
                    Etudiant etudiant = await GetEtudiantCourantAsync();

                    Adresse adresse = etudiant.GetAdresse(_context);

                    etudiant.ModelBinding(adresse, vm);

                    _context.SaveChanges();
                }

                return View(vm);
            }
            
            if (User.IsInRole(RolesName.Admin)) {
                ModelState.Remove(nameof(vm.ProgrammeEtudeId));
                ModelState.Remove(nameof(vm.ProgrammeEtudes));
                ModelState.Remove(nameof(vm.ProvinceId));
                ModelState.Remove(nameof(vm.Provinces));
                ModelState.Remove(nameof(vm.Nom));
                ModelState.Remove(nameof(vm.Prenom));
                ModelState.Remove(nameof(vm.NoCivique));
                ModelState.Remove(nameof(vm.App));
                ModelState.Remove(nameof(vm.Rue));
                ModelState.Remove(nameof(vm.Ville));
                ModelState.Remove(nameof(vm.CodePostal));

                if (ModelState.IsValid) {
                    Utilisateur admin = await GetAdminCourantAsync();

                    admin.ModelBinding(vm);

                    _context.SaveChanges();
                }

                return View(vm);
            }

            return Content("Action interdite");
        }

        private GestionProfilVM SetEtudiantProfilVM(GestionProfilVM vm)
        {
            if (vm.CodePostal != null) {
                vm.CodePostal = vm.CodePostal.ToUpper();
            }

            vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
            vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));

            return vm;
        }

        [Authorize(Roles = RolesName.Etudiant)]
        private async Task<Etudiant> GetEtudiantCourantAsync()
        {
            return await _userManagerEtudiant.GetUserAsync(HttpContext.User);
        }

        [Authorize(Roles = RolesName.Admin)]
        private async Task<Utilisateur> GetAdminCourantAsync()
        {
            return await _userManagerAdmin.GetUserAsync(HttpContext.User);
        }
    }
}
