using CsvHelper.Configuration.Attributes;

namespace vlissides_bibliotheque.ViewModels
{
    [Delimiter(";")]
    public class CsvEtudiantVM
    {

        public string Matricule { get; set; }
        public string Courriel { get; set; }
        public string MotDePasse { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string ProgrammeEtude { get; set; }

    }
}
