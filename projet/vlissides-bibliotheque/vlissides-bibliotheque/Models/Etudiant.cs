using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Etudiant</c> définit la table etudiant dans la base de données.
    /// </summary>
    public class Etudiant : Utilisateur
    {
        [Required]
        public int ProgrammeEtudeId { get; set; }
        public ProgrammeEtude ProgrammeEtude { get; set; }

        [Required]
        public int AdresseId { get; set; }
        public Adresse Adresse { get; set; }

        [Required]
        public int AnneeParcours { get; set; }
    }
}
