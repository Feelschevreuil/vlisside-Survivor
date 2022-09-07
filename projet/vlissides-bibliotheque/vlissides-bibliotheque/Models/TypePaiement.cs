using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class TypePaiement
    {
        [Required]
        public int Id { get; set; }
        public string Nom { get; set; }
    }
}
