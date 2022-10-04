using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>TypePaiement</c> définit la table typePaiement dans la base de données.
    /// </summary>
    public class TypePaiement
    {
        [Required]
        public int TypePaiementId { get; set; }

        [Required]
        public string Nom { get; set; }
    }
}
