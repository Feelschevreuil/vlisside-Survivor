using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.DTO.Ajax
{

    /// <summary>
    /// Classe <c>AchatinformationsLivraisonDTO</c> contient l'information pour la
    /// livraison de commandes.
    /// </summary>
    public class AchatInformationsLivraisonDTO
    {

        [Required]
        public int FactureEtudiantId { get; set; }

        [Required]
        [StringLength(64)]
        public string Ville { get; set; }


        [Required]
        public int NumeroCivique { get; set; }

        public string App { get; set; }

        [Required]
        [StringLength(64)]
        public string Rue { get; set; }

        [Required]
        [StringLength(6)]
        public string CodePostal { get; set; }

    }
}
