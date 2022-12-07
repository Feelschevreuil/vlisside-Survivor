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
                prixEtatLivre.FindAll(x => x.LivreBibliothequeId == livre.LivreId);
                coursLivres.FindAll(element => element.LivreBibliothequeId == livre.LivreId);
                livreBibliotheques.Add(new TuileLivreBibliotequeVM { livreBibliotheque = new LivreBibliotheque { DatePublication = livre.DatePublication, LivreId = livre.LivreId, MaisonEdition = livre.MaisonEdition, PhotoCouverture = livre.PhotoCouverture, Titre = livre.Titre }, prixEtatLivre = prixEtatLivre, coursLivre = coursLivres[0] });
            }
            livreBibliotheques = TuileLivreBibliothequeVMService.TrouverAuteursLivres(auteursLivres, livreBibliotheques);

            return livreBibliotheques;
        }
    }
}
