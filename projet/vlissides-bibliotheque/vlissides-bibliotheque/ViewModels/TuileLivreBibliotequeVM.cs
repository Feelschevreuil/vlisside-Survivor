using System.ComponentModel;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    /// <summary>
    /// Combine les classes « EvaluationLivre » et la classe « Evenement » en liste distinct pour les affichers dans la page d'accueil
    /// </summary>
    public class TuileLivreBibliotequeVM
    {
        public LivreBibliothequeDto livreBibliotheque;

        public string? programmeEtudeNom;

        public List<PrixEtatLivre> prixEtatLivre;
        [DisplayName("Quantité")]
        public int quantite { get; set; }
        public List<string>? auteurs;
    }
}
