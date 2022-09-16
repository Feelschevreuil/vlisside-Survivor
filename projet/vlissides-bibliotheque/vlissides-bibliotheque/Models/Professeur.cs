using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class Professeur
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }
    }
}
