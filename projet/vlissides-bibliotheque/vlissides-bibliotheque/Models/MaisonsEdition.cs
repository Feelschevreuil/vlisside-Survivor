using Microsoft.Build.Framework;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>MaisonEdition</c> instancie un maison d'édition.
    /// </summary>
    public class MaisonsEdition
    {
        [Required]
        public int MaisonsEditionId { get; set; }

        [Required]
        public string Nom { get; set; }
    }
}
