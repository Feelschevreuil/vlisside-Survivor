using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.Models
{
    public class LivreBibliotheque : ILivre
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EtatLivreId { get; set; }
        public EtatLivre EtatLivre { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }

        [Required]
        [Isbn]
        public string Isbn { get; set; }

        [Required]
        [StringLength(64)]
        public string Titre { get; set; }

        [Required]
        [StringLength(512)]
        public string Resume { get; set; }

        [Required]
        public string PhotoCouverture { get; set; }

        [Required]
        public DateTime DateEdition { get; set; }
    }
}
