using System.ComponentModel;

namespace vlissides_bibliotheque.ViewModels
{
    public class CoursVM
    {
        public int Id { get; set; }

        [DisplayName("Programme d'étude")]
        public string ProgrammeEtudeNom { get; set; }

        public string Nom { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        [DisplayName("Année")]
        public int AnneeParcours { get; set; }
    }
}
