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
        public static TuileLivreBibliotequeVM GetTuileLivreBibliotequeVMs(this LivreBibliotheque livreBibliotheque, List<CoursLivre> bdCoursLivre, List<PrixEtatLivre> bdPrixLivre, List<AuteurLivre> auteurLivres)
        {
            auteurLivres = auteurLivres
                .FindAll(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
            bdPrixLivre = bdPrixLivre
                .FindAll(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
            CoursLivre coursLivreAssocier = bdCoursLivre.Find(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);

            TuileLivreBibliotequeVM tuileVM = new()
            {
                livreBibliotheque = livreBibliotheque
            };

            if (coursLivreAssocier != null)
            {
                tuileVM.coursLivre = coursLivreAssocier;

            }
            if (bdPrixLivre != null)
            {
                tuileVM.prixEtatLivre = bdPrixLivre;
            }

            if (tuileVM.livreBibliotheque.PhotoCouverture == null || tuileVM.livreBibliotheque.PhotoCouverture == "")
            {
                tuileVM.livreBibliotheque.PhotoCouverture = GetImageParDefaut();
            }
            if (auteurLivres.Count() > 0 && auteurLivres != null)
            {
                tuileVM.auteurLivre = auteurLivres;
            }


            return tuileVM;
        }


        public static List<TuileLivreBibliotequeVM> GetQuatreLivresVM(ApplicationDbContext _context)
        {
            List<TuileLivreBibliotequeVM> listTuileLivreBibliotequeVMs = new();
            List<CoursLivre> listQuatreLivre = _context.CoursLivres
                .Include(x => x.LivreBibliotheque)
                .Include(x => x.Cours)
                .Include(x => x.LivreBibliotheque.MaisonEdition)
                .Take(4)
                .ToList();
            List<CoursLivre> bdCoursLivre = _context.CoursLivres
               .Include(x => x.Cours)
               .Include(x => x.LivreBibliotheque)
               .Include(x => x.Cours.ProgrammeEtude)
               .ToList();
            List<AuteurLivre> bdAuteurLivres = _context.AuteursLivres
                .Include(x => x.Auteur)
                .Include(x => x.LivreBibliotheque)
                .ToList();
            List<PrixEtatLivre> bdPrixLivre = _context.PrixEtatsLivres
                .ToList();

            foreach (CoursLivre CoursLivre in listQuatreLivre)
            {
                var livreConvertie = CoursLivre.LivreBibliotheque.GetTuileLivreBibliotequeVMs(bdCoursLivre, bdPrixLivre, bdAuteurLivres);
                listTuileLivreBibliotequeVMs.Add(livreConvertie);
            };

            return listTuileLivreBibliotequeVMs;
        }

        public static string GetImageParDefaut()
        {
            return "~/img/photo-livre.jpg";
        }

    }
}
