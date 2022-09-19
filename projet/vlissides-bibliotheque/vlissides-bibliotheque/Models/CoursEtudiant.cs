using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>CoursEtudiant</c> tisse un lien avec 
    /// les étudiants et les cours.
    /// </summary>
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
