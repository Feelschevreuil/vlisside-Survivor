using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Interface
{
    public interface IEvenementVM
    {
        EvenementVM GetEvenementVM(Evenement evenementRecus);

        Task<List<EvenementVM>> GetEvenementAccueil();

        List<EvenementVM> GetEvenementInventaire();
    }
}
