using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{
    public class TuileLivreBibliothequeVMService
    {
        public static List<TuileLivreBibliotequeVM> TrouverAuteursLivres(List<AuteurLivre> auteursLivres, List<TuileLivreBibliotequeVM> livreBibliotheques)
        {

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
            return livreBibliotheques;
        }

        public static InventaireLivreBibliotheque TrouverAuteursLivres(List<AuteurLivre> auteursLivres, InventaireLivreBibliotheque livreBibliotheques)
        {

            for (int i = 0; i < livreBibliotheques.tuileLivreBiblioteques.Count; i++)
            {
                List<AuteurLivre> auteursLivresTrouve = auteursLivres.FindAll(e => e.LivreBibliothequeId == livreBibliotheques.tuileLivreBiblioteques[i].livreBibliotheque.LivreId);

                if (auteursLivresTrouve != null)
                {
                    if (auteursLivres.Count > 0)
                    {
                        List<Auteur> auteurs = new List<Auteur>();
                        foreach (AuteurLivre auteurLivre in auteursLivresTrouve)
                        {
                            auteurs.Add(auteurLivre.Auteur);
                        }
                        livreBibliotheques.tuileLivreBiblioteques[i].auteurs = auteurs;
                    }
                }

            }
            return livreBibliotheques;
        }

        public static RecommendationPromotionsVM TrouverAuteursLivres(List<AuteurLivre> auteursLivres, RecommendationPromotionsVM livreBibliotheques)
        {

            for (int i = 0; i < livreBibliotheques.tuileLivreBibliotequeVMs.Count; i++)
            {
                List<AuteurLivre> auteursLivresTrouve = auteursLivres.FindAll(e => e.LivreBibliothequeId == livreBibliotheques.tuileLivreBibliotequeVMs[i].livreBibliotheque.LivreId);

                if (auteursLivresTrouve != null)
                {
                    if (auteursLivres.Count > 0)
                    {
                        List<Auteur> auteurs = new List<Auteur>();
                        foreach (AuteurLivre auteurLivre in auteursLivresTrouve)
                        {
                            auteurs.Add(auteurLivre.Auteur);
                        }
                        livreBibliotheques.tuileLivreBibliotequeVMs[i].auteurs = auteurs;
                    }
                }

            }
            return livreBibliotheques;
        }

        
    }
}
