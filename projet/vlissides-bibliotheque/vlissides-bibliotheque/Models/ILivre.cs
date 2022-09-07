using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public interface ILivre
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Titre { get; set; }

        public string PhotoCouverture { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }
    }
}
