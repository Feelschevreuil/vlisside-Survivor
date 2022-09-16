using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class PrixEtatLivre
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EtatLivreId { get; set; }
        public EtatLivre EtatLivre { get; set; }

        [Required]
        public int LivreBibliothequeId { get; set; }
        public LivreBibliotheque LivreBibliotheque { get; set; }

        [Required]
        public double Prix { get; set; }
    }
}
