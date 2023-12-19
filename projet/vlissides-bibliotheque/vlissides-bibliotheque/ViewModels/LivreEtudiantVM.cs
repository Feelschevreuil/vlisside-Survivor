using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class AjoutEditLivreEtudiantVM
    {
        [Required]
        public int LivreId { get; set; }

        public string EtudiantEmail { get; set; }
        public string EtudiantId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Titre { get; set; }

        [DisplayName("ISBN")]
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Isbn]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Isbn { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(512, ErrorMessage = "Le champ {0} ne peux pas dépasser 512 caractères ")]
        public string Resume { get; set; }
        [DisplayName("Photo de couverture")]
        [Image]
        [Required (ErrorMessage ="Le champ {0} est requis")]
        public string Photo { get; set; }

        [DisplayName("Date de publication")]
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DataType(DataType.Date)]
        public DateTime DatePublication { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Maison d'édition")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string MaisonEdition { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Auteur { get; set; }
        [Number]
        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        public decimal? Prix { get; set; }
    }
}
