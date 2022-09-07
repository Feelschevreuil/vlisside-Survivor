using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class FactureEtudiant
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int TypePaiementId { get; set; }
        public TypePaiement TypePaiement { get; set; }

        [Required]
        public int EtudiantId { get; set; }
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
