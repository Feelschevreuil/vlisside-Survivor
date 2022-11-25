using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.ViewModels
{
    public class GestionPromotionVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int EvenementId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DataType(DataType.Date)]
        public DateTime Debut { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DataType(DataType.Date)]
        public DateTime Fin { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(512, ErrorMessage = "Le champ {0} ne peux pas dépasser 512 caractères ")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int CommanditaireId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        [DisplayName("Nom du commanditaire")]
        public string CommanditaireNom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse courriel valide")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        [DisplayName("Courriel du commanditaire")]
        public string CommanditaireCourriel { get; set; }

        [Url]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(512, ErrorMessage = "Le champ {0} ne peux pas dépasser 512 caractères ")]
        [DisplayName("Message du commanditaire")]
        public string CommanditaireMessage { get; set; }
    }
}
