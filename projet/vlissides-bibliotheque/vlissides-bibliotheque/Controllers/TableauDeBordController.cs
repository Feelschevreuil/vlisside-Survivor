using Microsoft.AspNetCore.Authorization;
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
			UserManager<Etudiant> userManagerEtudiant,
            ApplicationDbContext context
		)
		{
			_userManager = userManager;
            _userManagerEtudiant = userManagerEtudiant;
            _context = context;
		}

		public ActionResult Index()
		{
			return View();
		}

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

        public IActionResult Cours()
        {
            List<Cours> cours = _context.Cours
                .Include(cours => cours.ProgrammeEtude)
                .ToList();

            return View(cours);
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

        public IActionResult Livres()
        {
            List<LivreBibliotheque> livres = _context.LivresBibliotheque
                .Include(livre => livre.MaisonEdition)
                .ToList();

            return View(livres);
        }

        public IActionResult ProgrammesEtudes()
        {
            List<ProgrammeEtude> programmesEtudes = _context.ProgrammesEtudes.ToList();

            return View(programmesEtudes);
        }

        public IActionResult Promotions()
        {
            List<Evenement> evenements = _context.Evenements
                .Include(commande => commande.Commanditaire)
                .ToList();

            return View(evenements);
        }
    }
}
