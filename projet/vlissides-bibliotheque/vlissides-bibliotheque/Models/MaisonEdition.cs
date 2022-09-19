using Microsoft.Build.Framework;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>MaisonEdition</c> instancie un maison d'édition.
    /// </summary>
    public class MaisonEdition
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
    }
}
