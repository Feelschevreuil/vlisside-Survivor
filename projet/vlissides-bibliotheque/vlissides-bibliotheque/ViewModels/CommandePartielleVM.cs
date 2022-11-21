using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque.Models
{

    /// <summary>
    /// Classe <c>CommandePartielleVM</c> qui contient partiellement les informations de la
    /// classe <c>CommandeEtudiant</c> pour l'afficher lorsqu'un étudiant veut voir une
    /// facture.
    /// </summary>
    public class CommandePartielleVM
    {

        public string Isbn { get; set; }

        [Required]
        [StringLength(64)]
        public string Titre;

        [Required]
        public EtatLivreEnum EtatLivre { get; set; }

        [Required]
        public double PrixUnitaireGele { get; set; }

        [Required]
        public int Quantite { get; set; }
    }
}
