using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class Adresse
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Ville { get; set; }


        [Required]
        public int NumeroCivique { get; set; }


        public int App { get; set; }

        [Required]
        [StringLength(64)]
        public string Rue { get; set; }

        [Required]
        [StringLength(6)]
        public string CodePostale { get; set; }
    }
}
