using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.DAO.Interface
{
    public interface ILivreTest
    {
        LivreBibliotheque MiseAJour(AjoutEditLivreVM form);
    }
}
