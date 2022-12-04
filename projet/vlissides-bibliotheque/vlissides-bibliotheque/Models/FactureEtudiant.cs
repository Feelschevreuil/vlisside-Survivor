using vlissides_bibliotheque.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>FactureEtudiant</c> définit la table factureEtudiant dans la base de données.
    /// </summary>
    public class FactureEtudiant
    {
        [Required]
        public int FactureEtudiantId { get; set; }

        [StringLength(32)]
        public string? PaymentIntentId { get; set; }

        [StringLength(64)]
        public string? ClientSecret { get; set; }

        [Required]
        public string EtudiantId { get; set; }
        public Etudiant Etudiant { get; set; }

        [Required]
        [DisplayName("Adresse de livraison")]
        public int AdresseLivraisonId { get; set; }
        public Adresse AdresseLivraison { get; set; }

        [Required]
        [DisplayName("Date de facturation")]
        public DateTime DateFacturation { get; set; }

        public decimal Tps { get; set; }

        public decimal Tvq { get; set; }

        [Required]
        public StatutFactureEnum Statut { get; set; }
    }
}
