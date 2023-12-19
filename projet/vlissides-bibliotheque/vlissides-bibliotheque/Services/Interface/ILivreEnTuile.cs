using System.Collections.Generic;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services.Interface
{
    public interface ILivreBibliotheque
    {
        TuileLivreBibliotequeVM GetTuileLivreBibliotequeVMs(int livreBibliothequeId);

        List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeAccueil();

        List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeInventaire();

        LivreDetailVM GetLivreDetailVM(int livreBibliothequeId);

        string GetImageParDefaut();
    }
}
