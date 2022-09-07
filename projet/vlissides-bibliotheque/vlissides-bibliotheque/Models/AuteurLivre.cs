using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class AuteurLivre
    {
        [Required]
        public int LivreBibliothequeId { get; set; }
        public LivreBibliotheque LivreBibliotheque { get; set; }

        [Required]
        public int AuteurId { get; set; }
        public Auteur Auteur { get; set; }
    }
}
