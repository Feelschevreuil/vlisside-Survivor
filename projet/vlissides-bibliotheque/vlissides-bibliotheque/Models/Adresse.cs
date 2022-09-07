using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class Adresse
    {
        [Required]
        public int Id { get; set; }

        public string Ville { get; set; }

        public int NumeroCivique { get; set; }

        public int App { get; set; }

        public string Rue { get; set; }

        public string CodePostale { get; set; }
    }
}
