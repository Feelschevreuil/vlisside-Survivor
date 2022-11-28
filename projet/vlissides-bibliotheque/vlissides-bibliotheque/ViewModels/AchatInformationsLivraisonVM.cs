using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{

    /// <summary>
    /// Classe <c>AchatinformationsLivraisonVM</c> contient l'information pour la
    /// livraison de commandes.
    /// </summary>
    public class AchatInformationsLivraisonVM
    {

        [Required]
        public Adresse AdresseLivraison { get; set; }

        [Required]
        public string NumeroTelephone { get; set; }
    }
}
