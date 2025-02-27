using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.ViewModels
{
    public class AuteurVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nom { get; set; }

        [Required]
        [StringLength(40)]
        public string Prenom { get; set; }

        public string NomComplet { get { return Prenom + " " + Nom; } }

    }
}
