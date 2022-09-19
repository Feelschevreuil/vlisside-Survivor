using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>CoursLivre</c> tisse un lien entre les 
    /// cours et les livres.
    /// </summary>
    public class CoursLivre
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CoursId { get; set; }
        public Cours Cours { get; set; }

        [Required]
        public int LivreBibliothequeId { get; set; }
        public LivreBibliotheque LivreBibliotheque { get; set; }

        [Required]
        public bool Complementaire { get; set; }
    }
}
