using System.ComponentModel.DataAnnotations;

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
        [StringLength(512)]
        public string Description { get; set; }

        public string PhotoCouverture { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }
    }
}
