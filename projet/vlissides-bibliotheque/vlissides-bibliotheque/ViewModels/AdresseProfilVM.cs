using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class AdresseProfilVM
    {
        public int AdresseId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Numéro civique")]
        [Number]
        public string NoCivique { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Rue { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Ville { get; set; }

        [Display(Name = "Numéro d'appartement")]
        public int App { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Code postal")]
        [DataType(DataType.PostalCode)]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Province { get; set; }
    }
}
