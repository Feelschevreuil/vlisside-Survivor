using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class LivreEtudiantVM
    {
        [Required]
        public int LivreId { get; set; }

        public Etudiant Etudiant { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(64)]
        public string Titre { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Isbn]
        public string Isbn { get; set; }


        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(512)]
        public string Resume { get; set; }

        public string PhotoCouverture { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DataType(DataType.Date)]
        public DateTime DatePublication { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string MaisonEdition { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Auteur { get; set; }
        [Number]
        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        public double? Prix { get; set; }


    }
}
