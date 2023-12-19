

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Commanditaire</c> définit la table commanditaire dans la base de données.
    /// </summary>
    public class Commanditaire
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int CommanditaireId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(40)]
        [DisplayName("Nom du commanditaire")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [EmailAddress]
        [DisplayName("Courriel du commanditaire")]
        public string Courriel { get; set; }

        [Url]
        public string Url { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(512)]
        [DisplayName("Message du commanditaire")]
        public string Message { get; set; }
    }
}
