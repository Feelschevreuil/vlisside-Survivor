using Microsoft.Build.Framework;

namespace vlissides_bibliotheque.Models
{
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
