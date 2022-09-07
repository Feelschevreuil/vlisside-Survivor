using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class EvaluationLivre
    {
        [Required]
        public int LivreBibliothequeId { get; set; }
        public LivreBibliotheque LivreBibliotheque { get; set; }

        [Required]
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }
    }
}
