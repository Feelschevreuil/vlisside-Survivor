using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DTO
{
    public class CoursDto
    {
        public int CoursId { get; set; }

        [DisplayName("Programme d'étude")]
        public string ProgrammeEtudeNom { get; set; }

        public string Nom { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        [DisplayName("Année")]
        public int AnneeParcours { get; set; }
    }
}
