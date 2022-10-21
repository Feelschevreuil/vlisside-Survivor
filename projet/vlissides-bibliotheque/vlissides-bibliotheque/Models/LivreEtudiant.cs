using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>LivreEtudiant</c> définit la table livreEtudiant dans la base de données.
    /// </summary>
    public class LivreEtudiant : ILivre
    {
        [Key]
        [Required]
        public int LivreId { get; set; }

        public Etudiant Etudiant { get; set; }

        [Required]
        [StringLength(64)]
        public string Titre { get; set; }

        [Required]
        [Isbn]
        public string Isbn { get; set; }


        [Required]
        [StringLength(512)]
        public string Resume { get; set; }

        public string PhotoCouverture { get; set; }

        [Required]
        public DateTime DatePublication { get; set; }

        public string MaisonEdition { get; set; }

        public string Auteur { get; set; }

    }
}
