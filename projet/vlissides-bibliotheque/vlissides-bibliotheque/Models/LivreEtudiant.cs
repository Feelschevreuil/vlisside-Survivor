using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class LivreEtudiant : ILivre
    {
        [Required]
        public int Id { get; set; }

        public string Titre { get; set; }

        public string Description { get; set; }

        public string PhotoCouverture { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }
    }
}
