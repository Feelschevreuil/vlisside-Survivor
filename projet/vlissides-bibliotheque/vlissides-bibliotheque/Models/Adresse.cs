using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Adresse</c> définit la table adresse dans la base de données.
    /// </summary>
    public class Adresse
    {
        [Required]
        public int AdresseId { get; set; }

        [Required]
        [StringLength(64)]
        public string Ville { get; set; }


        [Required]
        public int NumeroCivique { get; set; }


        public string? App { get; set; }

        [Required]
        [StringLength(64)]
        public string Rue { get; set; }

        [Required]
        [StringLength(6)]
        public string CodePostal { get; set; }

        [Required]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}
