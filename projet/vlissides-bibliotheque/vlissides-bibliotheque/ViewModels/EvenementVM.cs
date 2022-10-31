using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class EvenementVM
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int EvenementId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int CommanditaireId { get; set; }
        [Display(Name = "Commanditaire")]
        public Commanditaire Commanditaire { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(64, ErrorMessage ="Votre {0} est trop longue")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DataType(DataType.Date)]
        public DateTime Debut { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DataType(DataType.Date)]
        public DateTime Fin { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(512, ErrorMessage ="Votre {0} est trop longue")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Image { get; set; }
    }
}
