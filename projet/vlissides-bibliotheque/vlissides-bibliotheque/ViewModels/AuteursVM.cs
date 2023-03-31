using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.ViewModels
{
    public class AuteursVM
    {
        public int Id { get; set; }

        [Required]
        public int AuteurId { get; set; }

        [Required]
        [StringLength(40)]
        public string Nom { get; set; }

        [Required]
        [StringLength(40)]
        public string Prenom { get; set; }

    }
}
