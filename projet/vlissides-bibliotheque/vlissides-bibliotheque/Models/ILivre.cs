using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Interface <c>ILivre</c> définit la composition des classes relatives aux livres.
    /// </summary>
    public interface ILivre
    {
        [Required]
        public int LivreId { get; set; }

        [Required]
        [StringLength(64)]
        public string Titre { get; set; }

        public string PhotoCouverture { get; set; }

        [Required]
        [Isbn]
        public string Isbn { get; set; }


        [Required]
        [StringLength(512)]
        public string Resume { get; set; }

        [Required]
        public DateTime DatePublication { get; set; }
    }
}
