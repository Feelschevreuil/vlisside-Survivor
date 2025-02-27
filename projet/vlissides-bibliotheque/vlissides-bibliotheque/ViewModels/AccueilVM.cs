using System.Collections.Generic;

namespace vlissides_bibliotheque.ViewModels
{
    /// <summary>
    /// Combine les classes « EvaluationLivre » et la classe « Evenement » en liste distinct pour les affichers dans la page d'accueil
    /// </summary>
    public class AccueilVM
    {
        public List<TuileLivreBibliotequeVM> tuileLivreBibliotequeVMs;

        public List<EvenementVM> evenements;

    }
}
