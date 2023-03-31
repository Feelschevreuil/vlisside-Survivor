using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services.Interface
{
    public interface IPrix
    {
        Task<bool> UpdateLesPrix(LivreBibliotheque LivreEtatPrix, AjoutEditLivreVM form);
        List<PrixEtatLivre> AssocierPrixEtat(LivreBibliotheque LivreEtatPrix, AjoutEditLivreVM form);
        Task<List<PrixEtatLivre>> GetPrixLivre(int id);
    }
}
