using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>PrixEtatLivre</c> tisse un lien entre 
    /// l'état des livres et leur prix.
    /// </summary>
    public class PrixEtatLivre
    {
        [Required]
        public int PrixEtatLivreId { get; set; }

        [Required]
        public int EtatLivreId { get; set; }
        public EtatLivre EtatLivre { get; set; }

        [Required]
        public int LivreBibliothequeId { get; set; }
        public LivreBibliotheque LivreBibliotheque { get; set; }

        [Required]
        public double Prix { get; set; }
    }
}
