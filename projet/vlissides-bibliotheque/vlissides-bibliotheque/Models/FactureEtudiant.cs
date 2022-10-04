using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>FactureEtudiant</c> définit la table factureEtudiant dans la base de données.
    /// </summary>
    public class FactureEtudiant
    {
        [Required]
        public int FactureEtudiantId { get; set; }

        [Required]
        public int TypePaiementId { get; set; }
        public TypePaiement TypePaiement { get; set; }

        public Etudiant Etudiant { get; set; }

        [Required]
        public int AdresseLivraisonId { get; set; }
        public Adresse AdresseLivraison { get; set; }

        [Required]
        public DateTime DateFacturation { get; set; }

        public decimal Tps { get; set; }

        public decimal Tvq { get; set; }
    }
}
