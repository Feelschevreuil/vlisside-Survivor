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

		// GET: TableauDeBordController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: TableauDeBordController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: TableauDeBordController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: TableauDeBordController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: TableauDeBordController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: TableauDeBordController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: TableauDeBordController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
