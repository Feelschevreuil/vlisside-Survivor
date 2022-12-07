using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{
    public class RechercheService
    {
        public static List<TuileLivreBibliotequeVM> TrouverLivreCreerTuile(List<TuileLivreBibliotequeVM> livreBibliotheques, LivresBibliothequeDAO livresBibliothequeDAO, List<LivreBibliotheque> livres, List<PrixEtatLivre> prixEtatLivre, List<CoursLivre> coursLivres, List<AuteurLivre> auteursLivres, RechercheSimple rechercheSimple) {
            if (rechercheSimple.numPage >= 0)
            {
                livres = livresBibliothequeDAO.GetSelonPropriete(rechercheSimple.texteRecherche, 15, (int)rechercheSimple.numPage).ToList();
            }
            else
            {
                livres = livresBibliothequeDAO.GetSelonPropriete(rechercheSimple.texteRecherche, 15, 0).ToList();
            }

            foreach (LivreBibliotheque livre in livres)
            {
                var livreConvertie = livre.GetTuileLivreBibliotequeVMs(coursLivres, prixEtatLivre, auteursLivres);
                livreBibliotheques.Add(livreConvertie);
            }
            livreBibliotheques = TuileLivreBibliothequeVMService.TrouverAuteursLivres(auteursLivres, livreBibliotheques);

            return livreBibliotheques;
        }
    }
}
