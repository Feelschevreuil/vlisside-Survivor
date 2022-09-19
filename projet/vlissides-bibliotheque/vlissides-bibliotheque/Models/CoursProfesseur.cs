using Microsoft.Build.Framework;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>CoursProfesseur</c> tisse un lien entre 
    /// les professeurs et les cours
    /// </summary>
    public class CoursProfesseur
    {
        [Required]
        public int ProfesseurId { get; set; }
        public Professeur Professeur { get; set; }

        [Required]
        public int CoursId { get; set; }
        public Cours Cours { get; set; }
    }
}
