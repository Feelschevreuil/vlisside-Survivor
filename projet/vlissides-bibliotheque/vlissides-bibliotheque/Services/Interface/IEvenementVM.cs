using System.Collections.Generic;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services.Interface
{
    public interface IEvenementVM
    {
        List<EvenementVM> GetEvenementAccueil();

        List<EvenementVM> GetEvenementInventaire();
    }
}
