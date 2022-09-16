using Microsoft.Build.Framework;

namespace vlissides_bibliotheque.Models
{
    public class Cours
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public int AnneeParcours { get; set; }
    }
}
