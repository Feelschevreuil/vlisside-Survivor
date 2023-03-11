using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services.Interface
{
    public interface ILivreBibliotheque
    {
        Task<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeVMs(int livreBibliothequeId);

        Task<List<TuileLivreBibliotequeVM>> GetTuileLivreBibliotequeAccueil();

        Task<List<TuileLivreBibliotequeVM>> GetTuileLivreBibliotequeInventaire();

        Task<LivreDetailVM> GetLivreDetailVM(int livreBibliothequeId);

        string GetImageParDefaut();
    }
}
