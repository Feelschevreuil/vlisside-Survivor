using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.DTO
{

    /// <summary>
    /// DTO <c>FactureEtudiantDTO</c> qui définit ce que la route pour créer une facture
    /// doit recevoir afin de créer la facture avec les informations appropriées.
    /// </summary>
    public class FactureEtudiantDTO
    {
        
        [Required]
        public List<int> PrixEtatsLivres { get; set; }
    }
}
