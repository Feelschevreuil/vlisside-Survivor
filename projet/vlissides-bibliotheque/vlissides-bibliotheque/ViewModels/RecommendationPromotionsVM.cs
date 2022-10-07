using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    /// <summary>
    /// Combine les classes « EvaluationLivre » et la classe « Evenement » en liste distinct pour les affichers dans la page d'accueil
    /// </summary>
    public class RecommendationPromotionsVM
    {
        public List<EvaluationLivre> livreBibliothequesEvaluation;

        public List<Evenement> evenements;

        public List<CoursProfesseur> coursProfesseurs;
    }
}
