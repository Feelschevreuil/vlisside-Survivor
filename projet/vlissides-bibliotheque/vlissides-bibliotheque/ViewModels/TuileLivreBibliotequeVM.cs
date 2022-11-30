using System.ComponentModel;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    /// <summary>
    /// Combine les classes « EvaluationLivre » et la classe « Evenement » en liste distinct pour les affichers dans la page d'accueil
    /// </summary>
    public class TuileLivreBibliotequeVM
    {
        public List<EvaluationLivre> livreEvaluation;

        public LivreBibliotheque livreBibliotheque;

        public CoursLivre coursLivre;

        public List<AuteurLivre> auteurLivre;

        public List<PrixEtatLivre> prixEtatLivre;
        [DisplayName("Quantité")]
        public int quantite { get; set; }
    }
}
