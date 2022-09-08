using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class LivreEtudiant : ILivre
    {
        [Required]
        public int Id { get; set; }

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
