using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services.Interface
{
    public interface ILivreEtudiant
    {
        List<LivreEtudiant> GetAllLivreEtudiant(string id);
        LivreEtudiant GetLivreByEtudiantId(string id);
    }
}
