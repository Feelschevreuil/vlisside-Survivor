using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class CommandeEtudiant
    {
        [Required]
        public int FactureEtudiantId { get; set; }
        public FactureEtudiant FactureEtudiant { get; set; }

        [Required]
        public int LivreBibliothequeId { get; set; }
        public LivreBibliotheque LivreBibliotheque { get; set; }
    }
}
