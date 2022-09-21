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
        public int AdresseLivraisonId { get; set; }
        public Adresse AdresseLivraison { get; set; }

        [Required]
        public int AdresseFacturationId { get; set; }
        public Adresse AdresseFacturation { get; set; }

        [Required]
        public int AnneeParcours { get; set; }
    }
}
