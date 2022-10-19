using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.ViewModels
{
    public class ConnexionVM
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Courriel")]
        [RegularExpression(@"^[\w-\.]+@(cegep-connaissance-aleatoire\.qc\.ca)", 
            ErrorMessage = "Le courriel doit correspondre au format : 123456@cegep-connaissance-aleatoire.qc.ca")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi?")]
        public bool RememberMe { get; set; }
    }
}
