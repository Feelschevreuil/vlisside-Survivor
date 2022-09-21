 using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Evaluation</c> définit la table evaluation dans la base de données.
    /// </summary>
    public class Evaluation
    {
        
        [Required]
        public int EvaluationId { get; set; }

        public Etudiant Etudiant { get; set; }

        [Required]
        [Range(0, 10)]
        public int Etoile { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(64)]
        public string? Titre { get; set; }

        [Required]
        [StringLength(512)]
        public string Commentaire { get; set; }
    }
}
