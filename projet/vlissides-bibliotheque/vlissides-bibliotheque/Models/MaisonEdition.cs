using Microsoft.Build.Framework;

namespace vlissides_bibliotheque.Models
{
    public class MaisonEdition
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
    }
}
