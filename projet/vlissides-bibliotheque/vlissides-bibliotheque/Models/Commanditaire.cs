using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Commanditaire</c> définit la table commanditaire dans la base de données.
    /// </summary>
    public class Commanditaire
    {
        [Required]
        public int CommanditaireId { get; set; }

        [Required]
        [StringLength(40)]
        public string Nom { get; set; }

        [Required]
        [EmailAddress]
        public string Courriel { get; set; }

        [Url]
        public string? Url { get; set; }

        [Required]
        [StringLength(512)]
        public string Message { get; set; }
    }
}
