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
            List<TuileLivreBibliotequeVM> livreBibliotheques=new();
            LivresBibliothequeDAO livresBibliothequeDAO = new LivresBibliothequeDAO(_context);
            if (rechercheSimple.numPage != null)
            {
                List<LivreBibliotheque> livres = new();
                if (rechercheSimple.numPage>=0){
                    livres = livresBibliothequeDAO.GetSelonPropriete(rechercheSimple.texteRecherche, 20,(int)rechercheSimple.numPage).ToList();
                }

                List<PrixEtatLivre> prixEtatLivre = _context.PrixEtatsLivres.ToList();
                List<CoursLivre> coursLivres = _context.CoursLivres.Include(x => x.Cours).Include(x => x.Cours.ProgrammeEtude).Include(x => x.LivreBibliotheque).ToList();
                foreach (LivreBibliotheque livre in livres)
                {
                    prixEtatLivre.FindAll(x => x.LivreBibliothequeId == livre.LivreId);
                    coursLivres.Find(element => element.LivreBibliothequeId== livre.LivreId);

                    livreBibliotheques.Add(new TuileLivreBibliotequeVM { livreBibliotheque=new LivreBibliotheque { DatePublication=livre.DatePublication,LivreId=livre.LivreId,MaisonEdition=livre.MaisonEdition,PhotoCouverture=livre.PhotoCouverture,Titre=livre.Titre },prixEtatLivre= prixEtatLivre , coursLivre= coursLivres[0] });
                }

                List<AuteurLivre> auteursLivres = _context.AuteursLivres.Include(x => x.Auteur).ToList();
                for (int i = 0; i < livreBibliotheques.Count; i++)
                {
                    List<AuteurLivre> auteursLivresTrouve = auteursLivres.FindAll(e => e.LivreBibliothequeId == livreBibliotheques[i].livreBibliotheque.LivreId);

                    if (auteursLivresTrouve != null)
                    {
                        if (auteursLivres.Count > 0)
                        {
                            List<Auteur> auteurs = new List<Auteur>();
                            foreach (AuteurLivre auteurLivre in auteursLivresTrouve)
                            {
                                auteurs.Add(auteurLivre.Auteur);
                            }
                            livreBibliotheques[i].auteurs = auteurs;
                        }
                    }

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
                List<CoursLivre> bdCoursLivre = _context.CoursLivres.ToList();
                List< PrixEtatLivre > bdPrixLivre=_context.PrixEtatsLivres.ToList();
                List<AuteurLivre> auteurLivres = _context.AuteursLivres.ToList();
                foreach (LivreBibliotheque livre in BDlivreBibliotheques)
                {
                    var livreConvertie = livre.GetTuileLivreBibliotequeVMs(bdCoursLivre,bdPrixLivre, auteurLivres);
                    inventaireBibliotheque.Add(livreConvertie);
                };
;
                return PartialView("_ConteneurAffichageLivresRecherche", inventaireBibliotheque);
            }

            return PartialView("_ConteneurAffichageLivresRecherche", (livreBibliotheques));
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
