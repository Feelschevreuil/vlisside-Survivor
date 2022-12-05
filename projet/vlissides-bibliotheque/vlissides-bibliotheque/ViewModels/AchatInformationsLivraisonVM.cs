using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

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
        public Adresse AdresseLivraison { get; set; }
    }
}
