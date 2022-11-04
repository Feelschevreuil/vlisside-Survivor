using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public static class LivreEnTuile
    {
        public static TuileLivreBibliotequeVM GetTuileLivreBibliotequeVMs(this LivreBibliotheque livreBibliotheque, ApplicationDbContext _context)
        {
            List<CoursLivre> bdCoursLivre = _context.CoursLivres
                .Include(x=>x.Cours)
                .Include(x=>x.LivreBibliotheque)
                .Include(x=>x.Cours.ProgrammeEtude)
                .ToList();
            CoursLivre coursLivreAssocier = bdCoursLivre.Find(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
            
            List<PrixEtatLivre> bdPrixLivre = _context.PrixEtatsLivres
                .Include(x => x.EtatLivre)
                .ToList()
                .FindAll(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
            
            TuileLivreBibliotequeVM tuileVM = new()
            {
                livreBibliotheque = livreBibliotheque
            };

            try
            {
                if (coursLivreAssocier != null)
                {
                    tuileVM.coursLivre = bdCoursLivre.Find(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
                    
                }
                if (bdPrixLivre != null)
                {
                    tuileVM.prixEtatLivre = bdPrixLivre;
                }

                if (tuileVM.livreBibliotheque.PhotoCouverture == null || tuileVM.livreBibliotheque.PhotoCouverture == "N/A")
                {
                    tuileVM.livreBibliotheque.PhotoCouverture = "livreDemo.jpg";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Error", e);
            }

            return tuileVM;
        }


        public static List<TuileLivreBibliotequeVM> GetQuatreLivresVM(ApplicationDbContext _context)
        {
            List<TuileLivreBibliotequeVM> listTuileLivreBibliotequeVMs = new();
            List<CoursLivre> listQuatreLivre = _context.CoursLivres
                .Include(x=>x.LivreBibliotheque)
                .Include(x=>x.Cours)
                .Take(4)
                .ToList();

            foreach (CoursLivre CoursLivre in listQuatreLivre)
            {
                var livreConvertie = CoursLivre.LivreBibliotheque.GetTuileLivreBibliotequeVMs(_context);
                listTuileLivreBibliotequeVMs.Add(livreConvertie);
            };

            return listTuileLivreBibliotequeVMs;
        }

    }
}
