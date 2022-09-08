using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vlissides_bibliotheque.Models
{
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

    }
}
