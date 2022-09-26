using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class GestionProfilVM
    {

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [EmailAddress(ErrorMessage = "Le format du courriel est invalide.")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }


        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Confirmer le mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation du mot de passe ne concorde pas.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Programme d'étude")]
        public int ProgrammeEtudeId { get; set; }
        public SelectList ProgrammeEtudes { get; set; }

        public int AdresseFacturationId { get; set; }
        public Adresse AdresseFacturation { get; set; }

        public int AdresseLivraisonId { get; set; }
        public Adresse AdresseLivraison { get; set; }


        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "No de téléphone")]
        [DataType(DataType.PhoneNumber)]
        public string NoTelephone { get; set; }
    }
}
