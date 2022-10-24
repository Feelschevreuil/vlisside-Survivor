using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public static class LivreEnTuile
    {
        public static TuileLivreBibliotequeVM GetTuileLivreBibliotequeVMs(this LivreBibliotheque livreBibliotheque, ApplicationDbContext _context)
        {
            IEnumerable<CoursLivre> bdCoursLivre = _context.CoursLivres;
            IEnumerable<EvaluationLivre> bdEvaluationsLivre = _context.EvaluationsLivres.Include(x=>x.Evaluation);
            TuileLivreBibliotequeVM tuileVM = new()
            {
                livreBibliotheque = livreBibliotheque,
            };

            try
            {
                    if (bdCoursLivre.ToList().Find(x => x.LivreBibliothequeId == livreBibliotheque.LivreId) != null)
                    {
                        tuileVM.coursLivre = _context.CoursLivres
                            .Include(x => x.Cours)
                            .Include(x => x.Cours.ProgrammeEtude)
                            .ToList()
                            .Find(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
                        tuileVM.complementaire = bdCoursLivre.ToList().Find(x => x.LivreBibliothequeId == livreBibliotheque.LivreId).Complementaire;
                    }

                    if (tuileVM.complementaire)
                    {
                        tuileVM.livreEvaluation = bdEvaluationsLivre.ToList().FindAll(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Error", e);
            }

            return tuileVM;
        }
    }
}
