using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.DTO
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
        public Adresse AdresseLivraison { get; set; }
    }
}
