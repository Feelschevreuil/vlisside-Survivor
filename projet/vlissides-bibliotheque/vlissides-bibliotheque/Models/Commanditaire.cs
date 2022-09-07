using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class Commanditaire
    {
        [Required]
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Courriel { get; set; }

        public string Url { get; set; }

        public string Message { get; set; }
    }
}
