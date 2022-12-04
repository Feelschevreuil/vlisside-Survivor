using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class ProgrammeEtudeProfeseur
    {
        [Required]
        public int ProfesseurId { get; set; }
        public Professeur Professeur { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }
    }
}
