using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Auteur</c> définit la table auteur dans la base de données.
    /// </summary>
    public class Auteur
    {
        [Required]
        public int AuteurId { get; set; }

        [Required]
        [StringLength(40)]
        public string Nom { get; set; }

        [Required]
        [StringLength(40)]
        public string Prenom { get; set; }

        public virtual List<AuteurLivre> Livres { get; set; }

        public virtual string NomComplet { get { return Prenom + " " + Nom; } }
    }
}
