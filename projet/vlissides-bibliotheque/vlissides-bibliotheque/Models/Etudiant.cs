using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    public class Etudiant : IdentityUser
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
        public int ComptePayPalId { get; set; }
        public ComptePayPal ComptePayPal { get; set; }

        [Required]
        public int CarteCreditId { get; set; }
        public CarteCredit CarteCredit { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }
    }
}
