using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Cours</c> définit les cours d'un programme d'étude.
    /// </summary>
    public class Cours
    {
        [Required]
        public int CoursId { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(8)]
        public string Code { get; set; }

        [Required]
        public int AnneeParcours { get; set; }
    }
}
