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
                    var tousLesPrix = _context.PrixEtatsLivres.ToList().FindAll(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
                    tuileVM.prixEtatLivre = tousLesPrix.Find(x => x.EtatLivreId == _context.EtatsLivres.ToList().Find(x => x.Nom == NomEtatLivre.Neuf).EtatLivreId);
                    }

                    if (tuileVM.complementaire)
                    {
                        tuileVM.livreEvaluation = bdEvaluationsLivre.ToList().FindAll(x => x.LivreBibliothequeId == livreBibliotheque.LivreId);
                    }

                    if(tuileVM.livreBibliotheque.PhotoCouverture == null || tuileVM.livreBibliotheque.PhotoCouverture == "N/A")
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
    }
}
