using System.Collections.Generic;
using System.Threading.Tasks;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services.Interface
{
    public interface IEvenementVM
    {
        Evenement GetEvenement(EvenementVM evenementRecus);
        EvenementVM GetEvenementVM(Evenement evenementRecus);
        Task<List<EvenementVM>> GetEvenementAccueil();

        Task<List<EvenementVM>> GetEvenementInventaire();
    }
}
