using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class GestionCommandeVM
    {
        public List<SelectListItem> listStatut { get; set; }
        public List<SelectListItem> listEtudiant { get; set; }

        public int FactureEtudiantId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis")]
        [MaxLength(32, ErrorMessage = "Le champ {0} ne peux pas dépasser 32 caractères")]

        public string? PaymentIntentId { get; set; } //= "TODO";

        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Étudiants")]
        public string EtudiantId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis")]
        public StatutFactureEnum Statut { get; set; }

        public string NomStatut { get; set; }


        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Statut")]
        public EtatLivreEnum EtatLivre { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Statut")]
        public int ValeurEnumStatut { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères")]
        public string Titre { get; set; } //= "Test";

        [Required(ErrorMessage = "Le champ {0} est requis")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères")]
        [EmailAddress(ErrorMessage = "Veiller enter un courriel valide")]
        public string Courriel { get; set; } //= "a@a";

        [Required(ErrorMessage = "Le champ {0} est requis")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères")]
        [DisplayName("Adresse de livraison")]
        public string AdresseLivraison { get; set; } //="123 barker";

        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DataType(DataType.Date)]
        [DisplayName("Date de facturation")]
        public DateTime DateFacturation { get; set; } //= DateTime.Now;

        public string formaterDateFacturation { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis")]
        public double Total { get; set; }
    }
}
