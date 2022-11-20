using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Validation;
using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>CommandeEtudiant</c> définit la table de liaison commandeEtudiant dans la base de données.
    /// </summary>
    public class CommandeEtudiant
    {
        [Required]
        public int FactureEtudiantId { get; set; }
        public FactureEtudiant FactureEtudiant { get; set; }

        [Required]
        public int PrixEtatLivreId { get; set; }
        public PrixEtatLivre PrixEtatLivre { get; set; }

        [Required]
        [Isbn]
        public string Isbn { get; set; }

        [Required]
        [StringLength(64)]
        public string Titre;

        [Required]
        public EtatLivreEnum EtatLivre { get; set; }

        [Required]
        public double PrixUnitaireGele { get; set; }

        [Required]
        public int Quantite { get; set; }
    }
}
