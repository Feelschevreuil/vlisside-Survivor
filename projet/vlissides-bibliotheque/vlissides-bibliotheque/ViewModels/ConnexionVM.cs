using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.ViewModels
{
    public class ConnexionVM
    {
        [Required]
        [Display(Name = "Courriel")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi?")]
        public bool RememberMe { get; set; }
    }
}
