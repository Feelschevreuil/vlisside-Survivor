using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Interface
{
    public interface ILivreEnTuile
    {
        Task<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeVMs(LivreBibliotheque livreBibliotheque);

        Task<List<TuileLivreBibliotequeVM>> GetTuileLivreBibliotequeAccueil();

        string GetImageParDefaut();
    }
}
