using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>EvaluationLivre</c> définit la table de liaison evaluationLivre dans la base de données.
    /// </summary>
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
