using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Controllers;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Interface;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{
    public class TuileLivreBibliothequeService : ILivreEnTuile
    {
        private readonly ApplicationDbContext _context;

        public TuileLivreBibliothequeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public TuileLivreBibliotequeVM GetTuileLivreBibliotequeVMs(LivreBibliotheque livreBibliotheque)
        {
            var auteurLivres = _context.AuteursLivres.Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);

            TuileLivreBibliotequeVM tuileVM = new()
            {
                livreBibliotheque = livreBibliotheque,
                coursLivre = _context.CoursLivres
                .Include(c => c.Cours).ThenInclude(c => c.ProgrammeEtude)
                .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId)
                .FirstOrDefault(),
                prixEtatLivre = _context.PrixEtatsLivres.Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId).ToList(),
                auteurs = _context.Auteurs.Where(a => auteurLivres.Any(al => al.AuteurId == a.AuteurId)).ToList(),
            };

            if (tuileVM.livreBibliotheque.PhotoCouverture == null || tuileVM.livreBibliotheque.PhotoCouverture == "")
            {
                tuileVM.livreBibliotheque.PhotoCouverture = GetImageParDefaut();
            }

            return tuileVM;
        }

        public List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeAccueil()
        {
            List<TuileLivreBibliotequeVM> tuileLivreBiblioteques = new();
            List<LivreBibliotheque> list = _context.LivresBibliotheque
                .Include(l => l.MaisonEdition)
                .Take(4)
                .ToList();

            foreach (LivreBibliotheque livre in list)
            {
                var auteurLivres = _context.AuteursLivres.Where(x => x.LivreBibliothequeId == livre.LivreId);

                TuileLivreBibliotequeVM tuileVM = new()
                {
                    livreBibliotheque = livre,
                    coursLivre = _context.CoursLivres.Include(c => c.Cours).ThenInclude(c => c.ProgrammeEtude).Where(x => x.LivreBibliothequeId == livre.LivreId).First(),
                    prixEtatLivre = _context.PrixEtatsLivres.Where(x => x.LivreBibliothequeId == livre.LivreId).ToList(),
                    auteurs = _context.Auteurs.Where(a => auteurLivres.Any(al => al.AuteurId == a.AuteurId)).ToList(),
                };

                if (tuileVM.livreBibliotheque.PhotoCouverture == null || tuileVM.livreBibliotheque.PhotoCouverture == "")
                {
                    tuileVM.livreBibliotheque.PhotoCouverture = GetImageParDefaut();
                }

                tuileLivreBiblioteques.Add(tuileVM);
            }
            return tuileLivreBiblioteques;
        }

        public string GetImageParDefaut()
        {
            return "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-livre.jpg";
        }
    }
}
