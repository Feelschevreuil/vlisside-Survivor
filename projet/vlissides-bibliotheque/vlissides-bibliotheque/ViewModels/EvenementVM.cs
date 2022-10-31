using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class EvenementVM
    {
        [Required]
        public int EvenementId { get; set; }

        [Required]
        public int CommanditaireId { get; set; }
        public Commanditaire Commanditaire { get; set; }

        [Required]
        [StringLength(64)]
        public string Nom { get; set; }

        [Required]
        public DateTime Debut { get; set; }

        [Required]
        public DateTime Fin { get; set; }

        [Required]
        [StringLength(512)]
        public string Description { get; set; }

        public string Image { get; set; }
    }
}
