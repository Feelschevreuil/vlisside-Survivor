using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.Extensions.Interface;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;
using static Humanizer.In;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class GestionProfilController : Controller
    {
        private readonly SignInManager<Etudiant> _signInManager;
        private readonly UserManager<Etudiant> _userManagerEtudiant;
        private readonly UserManager<Utilisateur> _userManagerAdmin;
        private readonly ApplicationDbContext _context;
        private readonly ICheckedBox _checkedBox;
        private readonly IUtilisateur _utilisateur;
        private readonly IDAO<ProgrammeEtude> _programmeEtudeDAO;
        private readonly IDAOEtudiant<Etudiant> _EtudiantDAO;
        private readonly IDAO<Cours> _coursDAO;

        public GestionProfilController(
            SignInManager<Etudiant> signInManager,
            UserManager<Etudiant> userManagerEtudiant,
            UserManager<Utilisateur> userManagerAdmin,
            ApplicationDbContext context,
            ICheckedBox checkedBox,
            IUtilisateur utilisateur,
            IDAO<ProgrammeEtude> programmeEtudeDAO,
            IDAOEtudiant<Etudiant> EtudiantDAO,
            IDAO<Cours> coursDAO
            )
        {
            _signInManager = signInManager;
            _userManagerEtudiant = userManagerEtudiant;
            _userManagerAdmin = userManagerAdmin;
            _context = context;
            _checkedBox = checkedBox;
            _utilisateur = utilisateur;
            _programmeEtudeDAO = programmeEtudeDAO;
            _EtudiantDAO = EtudiantDAO;
            _coursDAO = coursDAO;

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
                return View(_utilisateur.GetEtudiantProfilVM(await GetEtudiantCourantAsync()));
            }

            if (User.IsInRole(RolesName.Admin))
            {
                return View(_utilisateur.GetAdminProfilVM(await GetAdminCourantAsync()));
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
                ModelState.Remove(nameof(vm.checkBoxCours));
                ModelState.Remove(nameof(vm.EtudiantId));

                vm = SetEtudiantProfilVM(vm);

                if (ModelState.IsValid)
                {
                    Etudiant etudiant = await GetEtudiantCourantAsync();
                    Adresse adresse = _utilisateur.GetAdresse(etudiant);

                    _utilisateur.ModelBinding(etudiant, adresse, vm);
                    _context.SaveChanges();
                }
                return View(vm);
            }

            if (User.IsInRole(RolesName.Admin))
            {
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

                if (ModelState.IsValid)
                {

                    _utilisateur.ModelBinding(await GetAdminCourantAsync(), vm);
                    _context.SaveChanges();
                }
                return View(vm);
            }
            return Content("Action interdite");
        }

        private GestionProfilVM SetEtudiantProfilVM(GestionProfilVM vm)
        {
            if (vm.CodePostal != null)
            {
                vm.CodePostal = vm.CodePostal.ToUpper();
            }

            vm.ProgrammeEtudes = new SelectList(_programmeEtudeDAO.GetAll(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
            vm.checkBoxCours = _checkedBox.GetCoursCheckedBox(vm.EtudiantId);
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
        [HttpPost]
        public string AssignerCoursEtudiant([FromBody] CoursEtudiantDTO coursAssocier)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<CoursEtudiant> resetCoursAssocier = _context.CoursEtudiants.Where(x => x.EtudiantId == id).ToList();
            _context.CoursEtudiants.RemoveRange(resetCoursAssocier);
            _context.SaveChanges();

            List<CoursEtudiant> updateCoursAssocier = new(); 
            coursAssocier.CoursId.ForEach(c=> updateCoursAssocier.Add( new CoursEtudiant { CoursId = c, EtudiantId = id}));
            _context.CoursEtudiants.AddRange(updateCoursAssocier);
            _context.SaveChanges();

            return null;
        }
    }
}
