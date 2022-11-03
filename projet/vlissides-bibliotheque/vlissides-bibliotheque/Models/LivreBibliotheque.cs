using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>LivreBibliotheque</c> définit la table livreBibliotheque dans la base de données.
    /// </summary>
    public class LivreBibliotheque : ILivre
    {
        [Key]
        [Required]
        public int LivreId { get; set; }

        [Required]
        public int MaisonEditionId { get; set; }
        public MaisonEdition MaisonEdition { get; set; }

        [Required]
        [Isbn]
        public string Isbn { get; set; }

        [Required]
        [StringLength(64)]
        public string Titre { get; set; }

        [Required]
        [StringLength(512)]
        public string Resume { get; set; }

        [Required]
        public string PhotoCouverture { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePublication { get; set; }
    }
}
