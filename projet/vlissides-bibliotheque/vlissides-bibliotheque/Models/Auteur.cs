using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Auteur</c> définit la table auteur dans la base de données.
    /// </summary>
    public class Auteur
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nom { get; set; }

        [Required]
        [StringLength(40)]
        public string Prenom { get; set; }

    }
}
