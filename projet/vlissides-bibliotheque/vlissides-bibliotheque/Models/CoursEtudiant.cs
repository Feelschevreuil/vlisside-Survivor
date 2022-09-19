using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class CoursEtudiant
    {
        [Required]
        public int CoursId { get; set; }
        [Required]
        public Cours Cours { get; set; }

        [Required]
        public int EtudiantId { get; set; }
        [Required]
        public Etudiant Etudiant { get; set; }
    }
}
