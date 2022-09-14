using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace vlissides_bibliotheque.ViewModels
{
    public class InscriptionVM
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [EmailAddress]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }


        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Confirmer le mot de passe")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Le champ {0} est requis.")]
        //[Display(Name = "Programme d'étude")]
        //public ProgrammeEtude ProgrammeEtude { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Numéro civique")]
        public string NoCivique { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Rue { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Ville { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Code postal")]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "No de téléphone")]
        public string NoTelephone { get; set; }
    }
}
