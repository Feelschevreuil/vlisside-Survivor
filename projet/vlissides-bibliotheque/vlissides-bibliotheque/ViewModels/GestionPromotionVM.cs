using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.ViewModels
{
    public class GestionPromotionVM
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int EvenementId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(64, ErrorMessage = "Votre {0} est trop longue")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DataType(DataType.Date)]
        public DateTime Debut { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DataType(DataType.Date)]
        public DateTime Fin { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(512, ErrorMessage = "Votre {0} est trop longue")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int CommanditaireId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(40)]
        [DisplayName("Nom du commanditaire")]
        public string CommanditaireNom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse courriel valide")]
        [DisplayName("Courriel du commanditaire")]
        public string CommanditaireCourriel { get; set; }

        [Url]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(512)]
        [DisplayName("Message du commanditaire")]
        public string CommanditaireMessage { get; set; }
    }
}
