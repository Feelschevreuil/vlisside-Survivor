using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>AuteurLivre</c> définit la table de liaison auteurLivre dans la base de données.
    /// </summary>
    public class AuteurLivre
    {
        [Required]
        public int LivreBibliothequeId { get; set; }
        public LivreBibliotheque LivreBibliotheque { get; set; }

        [Required]
        public int AuteurId { get; set; }
        public Auteur Auteur { get; set; }
    }
}
