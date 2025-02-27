using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.DTO
{
    public class PrixEtatLivreDto
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Number]
        [DataType(DataType.Currency)]
        [DisplayName("Neuf")]
        public decimal PrixNeuf { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Numérique")]
        [DataType(DataType.Currency)]
        [Number]
        public decimal PrixNumerique { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Usagé")]
        [DataType(DataType.Currency)]
        [Number]
        public decimal PrixUsager { get; set; }

        [DisplayName("Quantité")]
        public int QuantiteUsage { get; set; } = 0;
    }
}
