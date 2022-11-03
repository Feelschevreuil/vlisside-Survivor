using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Extensions;
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
			UserManager<Etudiant> userManagerEtudiant,
            ApplicationDbContext context
		)
		{
			_userManager = userManager;
            _userManagerEtudiant = userManagerEtudiant;
            _context = context;
		}

        [HttpGet]
		public ActionResult Index()
		{
			return View();
		}

        [HttpGet]
        public IActionResult Commandes()
		{
			List<CommandeEtudiant> commandes = _context.CommandesEtudiants
				.Include(commande => commande.PrixEtatLivre)
				.Include(commande => commande.PrixEtatLivre.EtatLivre)
				.Include(commande => commande.PrixEtatLivre.LivreBibliotheque)
                .Include(commande => commande.FactureEtudiant)
                .Include(commande => commande.FactureEtudiant.Etudiant)
                .Include(commande => commande.FactureEtudiant.TypePaiement)
                .ToList();

            return View(commandes);
		}

        [HttpGet]
        public IActionResult Cours()
        {
            List<Cours> cours = _context.Cours
                .Include(cours => cours.ProgrammeEtude)
                .ToList();

            return View(cours);
        }

        [HttpGet]
        public IActionResult Etudiants()
        {
            List<Etudiant> etudiants = _userManagerEtudiant.Users
                .Include(etudiant => etudiant.ProgrammeEtude)
                .Include(etudiant => etudiant.Adresse)
                .Include(etudiant => etudiant.Adresse.Province)
                .ToList();

            return View(etudiants);
        }

        [HttpGet]
        public IActionResult ModifierEtudiant(string Id)
        {
            Etudiant? etudiant = _userManagerEtudiant.Users
                .Where(etudiant => etudiant.Id == Id)
                .FirstOrDefault();

            // retourner un erreur si l'étudiant n'existe pas
            if(etudiant == null) return NotFound();

            GestionProfilVM vm = etudiant.GetEtudiantProfilVM(_context);

            return PartialView("Views/Shared/_EtudiantPartial.cshtml", vm);
        }

        [HttpPost]
        public IActionResult ModifierEtudiant([FromBody] GestionProfilVM vm)
        {
            ModelState.Remove(nameof(vm.ProgrammeEtudes));
            ModelState.Remove(nameof(vm.Provinces));

            if (vm.CodePostal != null) {
                vm.CodePostal = vm.CodePostal.ToUpper();
            }

            vm.ProgrammeEtudes = new SelectList(_context.ProgrammesEtudes.ToList(), nameof(ProgrammeEtude.ProgrammeEtudeId), nameof(ProgrammeEtude.Nom));
            vm.Provinces = new SelectList(_context.Provinces.ToList(), nameof(Province.ProvinceId), nameof(Province.Nom));

            if (ModelState.IsValid) {
                Etudiant? etudiant = _userManagerEtudiant.Users
                    .Where(etudiant => etudiant.Id == vm.EtudiantId)
                    .FirstOrDefault();

                // retourner un erreur si l'étudiant n'existe pas
                if (etudiant == null) return NotFound();

                Adresse adresse = etudiant.GetAdresse(_context);

                etudiant.ModelBinding(adresse, vm);

                _context.SaveChanges();

                return Ok();
            } else {

                return PartialView("/Views/Shared/_EtudiantPartial.cshtml", vm);
            }
        }

        [HttpGet]
        public IActionResult Livres()
        {
            List<LivreBibliotheque> livres = _context.LivresBibliotheque
                .Include(livre => livre.MaisonEdition)
                .ToList();

            return View(livres);
        }

        [HttpGet]
        public IActionResult ProgrammesEtudes()
        {
            List<ProgrammeEtude> programmesEtudes = _context.ProgrammesEtudes.ToList();

            return View(programmesEtudes);
        }

        [HttpGet]
        public IActionResult Promotions()
        {
            List<Evenement> evenements = _context.Evenements
                .Include(commande => commande.Commanditaire)
                .ToList();

            return View(evenements);
        }
    }
}
