using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Interface;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{

    public class RechercheService
    {
        private readonly ILivreEnTuile _livre;

        public List<TuileLivreBibliotequeVM> TrouverLivreCreerTuile(List<TuileLivreBibliotequeVM> livreBibliotheques, LivresBibliothequeDAO livresBibliothequeDAO,  RechercheSimple rechercheSimple) {

            List<LivreBibliotheque> livres = new();

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
                var livreConvertie = _livre.GetTuileLivreBibliotequeVMs(livre);
                livreBibliotheques.Add(livreConvertie);
            }

            return livreBibliotheques;
        }
    }
}
