using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class Evenement
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CommanditaireId { get; set; }
        public Commanditaire Commanditaire { get; set; }

        public string Nom { get; set; }

        public DateTime Debut { get; set; }

        public DateTime Fin { get; set; }

        public string Description { get; set; }
    }
}
