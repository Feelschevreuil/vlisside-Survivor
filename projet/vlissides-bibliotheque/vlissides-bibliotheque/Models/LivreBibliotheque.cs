using System.ComponentModel.DataAnnotations;

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
        public string Isbn { get; set; }

        [Required]
        public string Titre { get; set; }

        [Required]
        public string Resume { get; set; }

        [Required]
        public string PhotoCouverture { get; set; }

        [Required]
        public DateTime DateEdition { get; set; }
    }
}
