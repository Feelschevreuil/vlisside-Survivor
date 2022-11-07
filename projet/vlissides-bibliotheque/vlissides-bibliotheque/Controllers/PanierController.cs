using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class PanierController : Controller
    {
        private readonly ILogger<AccueilController> _logger;
        private readonly ApplicationDbContext _context;

        public PanierController(ILogger<AccueilController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult GetLivres([FromBody] GetLivres ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            List<LivreBibliotheque> livres = _context.LivresBibliotheque.ToList();

            foreach(int id in ids.Id)
            {
                livres.Find(element => element.LivreId == id);
            }


            return Ok();
        }
    }
}
