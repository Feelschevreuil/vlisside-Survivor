using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace vlissides_bibliotheque.ViewModels
{

    /// <summary>
    /// Classe <c>AchatinformationsLivraisonVM</c> contient l'information pour la
    /// livraison de commandes.
    /// </summary>
    public class AchatInformationsLivraisonVM
    {

        public bool AdresseModifiee = false;

        [Required]
        [StringLength(64)]
        [DisplayName("Nom de ville")]
        public string Ville { get; set; }


        [Required]
        [DisplayName("Numéro civique")]
        public int NumeroCivique { get; set; }


        [DisplayName("Numéro d'appartement")]
        public string? App { get; set; }

        [Required]
        [StringLength(64)]
        [DisplayName("Nom de rue")]
        public string Rue { get; set; }

        [Required]
        [StringLength(6)]
        [DisplayName("Code postal")]
        public string CodePostal { get; set; }
    }
}
