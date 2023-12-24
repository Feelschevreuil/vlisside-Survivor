using System.Collections.Generic;
using System.Threading.Tasks;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services.Interface
{
    public interface IEvenementVM
    {
        Task<List<EvenementVM>> GetEvenementAccueil();

        Task<List<EvenementVM>> GetEvenementInventaire();
    }
}
