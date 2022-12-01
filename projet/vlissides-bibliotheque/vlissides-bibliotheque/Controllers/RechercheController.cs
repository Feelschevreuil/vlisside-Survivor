using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Controllers
{
    [Authorize]
    public class RechercheController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RechercheController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult index()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult RechercheRapide([FromBody] RechercheSimple rechercheSimple)
        {
            List<LivreBibliotheque> livreBibliotheques=new();
            LivresBibliothequeDAO livresBibliothequeDAO = new LivresBibliothequeDAO(_context);
            if (rechercheSimple.numPage != null && rechercheSimple.texteRecherche!="")
            {
                if(rechercheSimple.numPage>=0){ 
                    livreBibliotheques= livresBibliothequeDAO.GetSelonPropriete(rechercheSimple.texteRecherche, 20,(int)rechercheSimple.numPage).ToList();
                }
            }
            else
            {
                List<TuileLivreBibliotequeVM> inventaireBibliotheque = new();
                List<LivreBibliotheque> BDlivreBibliotheques = _context.LivresBibliotheque
                    .Include(x => x.MaisonEdition)
                    .OrderByDescending(i => i.DatePublication)
                    .Skip(15*rechercheSimple.numPage).Take(15)
                    .ToList();
                foreach (LivreBibliotheque livre in BDlivreBibliotheques)
                {
                    var livreConvertie = livre.GetTuileLivreBibliotequeVMs(_context);
                    inventaireBibliotheque.Add(livreConvertie);
                };
;
                return PartialView("_ConteneurAffichageLivresRecherche", inventaireBibliotheque);
            }

            return PartialView("_ConteneurAffichageLivresRecherche", FaireLivresPourVue(livreBibliotheques));
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult RechercheAvance([FromBody] LivreChampsRecherche recherche, int? page)
        {
            List<LivreBibliotheque> livreBibliotheques;
            LivresBibliothequeDAO livresBibliothequeDAO = new LivresBibliothequeDAO(_context);
            if (page != null)
            {
                livreBibliotheques = livresBibliothequeDAO.GetSelonProprietes(recherche, 20, (int)page).ToList();
            }
            else
            {
                livreBibliotheques = livresBibliothequeDAO.GetSelonProprietes(recherche, 20, 0).ToList();
            }

            return PartialView("_ConteneurAffichageLivresRecherche", FaireLivresPourVue(livreBibliotheques));
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Sugestion([FromBody] string recherche)
        {
            LivresBibliothequeDAO livresBibliothequeDAO = new LivresBibliothequeDAO(_context);
            return PartialView("_", livresBibliothequeDAO.GetSuggestions(recherche));
        }

        public List<TuileLivreBibliotequeVM> FaireLivresPourVue(List<LivreBibliotheque> listeLivres)
        {
            List<TuileLivreBibliotequeVM> TuileLivreBibliotequeVM = new();
            List<PrixEtatLivre> prixEtatLivre = _context.PrixEtatsLivres.ToList();

            foreach (LivreBibliotheque livre in listeLivres)
            {
                List<PrixEtatLivre> prixEtatLivres = new();
                List<PrixEtatLivre> vide = new();

                prixEtatLivre.FindAll(x => x.LivreBibliothequeId == livre.LivreId);
                var prixNeuf = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.NEUF);
                var prixDigital = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.NUMERIQUE);
                var prixUsage = prixEtatLivre.Find(x => x.EtatLivre == EtatLivreEnum.USAGE);

                if (prixNeuf != null) { prixEtatLivres.Add(prixNeuf); } else { prixEtatLivres.Add(new PrixEtatLivre()); };
                if (prixDigital != null) { prixEtatLivres.Add(prixDigital); } else { prixEtatLivres.Add(new PrixEtatLivre()); };
                if (prixUsage != null) { prixEtatLivres.Add(prixUsage); } else { prixEtatLivres.Add(new PrixEtatLivre()); };

                CoursLivre coursLivres = _context.CoursLivres.ToList().Find(e => e.LivreBibliothequeId == livre.LivreId);

                TuileLivreBibliotequeVM.Add(new TuileLivreBibliotequeVM { livreBibliotheque = livre, prixEtatLivre = prixEtatLivres, coursLivre = coursLivres });
            }
            return TuileLivreBibliotequeVM;
        }
    }
}
