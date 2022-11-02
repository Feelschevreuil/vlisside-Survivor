using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
	}
}
