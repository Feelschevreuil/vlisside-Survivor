using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class CarteCredit
    {
        [Required]
        public int Id { get; set; }

        public int NumeroCarte { get; set; }

        public DateTime Expiration { get; set; }

        public int Cvv { get; set; }
    }
}
