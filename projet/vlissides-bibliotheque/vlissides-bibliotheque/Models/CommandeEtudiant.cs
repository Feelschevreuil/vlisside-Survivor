using System.ComponentModel.DataAnnotations;

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
        public int LivreBibliothequeId { get; set; }
        public LivreBibliotheque LivreBibliotheque { get; set; }

        [Required]
        public int Quantite { get; set; }
    }
}
