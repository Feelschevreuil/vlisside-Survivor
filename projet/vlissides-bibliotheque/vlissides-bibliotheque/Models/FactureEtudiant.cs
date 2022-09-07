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

        public DateTime DateFacturation { get; set; }
    }
}
