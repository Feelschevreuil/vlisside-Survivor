using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Interface <c>ILivre</c> définit la composition des classes relatives aux livres.
    /// </summary>
    public interface ILivre
    {
        [Required]
        public int LivreId { get; set; }

        [Required]
        [StringLength(64)]
        public string Titre { get; set; }

        public string PhotoCouverture { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }
    }
}
