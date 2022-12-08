using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque.Models.Achat

{
    /// <summary>
    /// Class <c>CommandePartielle</c> définit ce que l'on veut envoyer au client lors de 
    /// la visualisation d'une facture.
    /// </summary>
    public class CommandePartielle: Commande
    {

        [Required]
        public int PrixEtatLivreId { get; set; }

        [Required]
        public StatutCommandeEnum StatutCommande { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public string Titre { get; set; } 

        [Required]
        public EtatLivreEnum EtatLivre { get; set; }

        [Required]
        public double Prix { get; set; }

        [Required]
        public int Quantite { get; set; }
    }
}
