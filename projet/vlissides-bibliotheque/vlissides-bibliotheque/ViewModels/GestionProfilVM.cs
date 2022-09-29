using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using vlissides_bibliotheque.Models;
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
        [Display(Name = "Programme d'étude")]
        public int ProgrammeEtudeId { get; set; }
        public SelectList ProgrammeEtudes { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "No de téléphone")]
        [DataType(DataType.PhoneNumber)]
        public string NoTelephone { get; set; }

        // adresse de facturation
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Numéro civique")]
        [Number]
        public string NoCiviqueFacturation { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Rue")]
        public string RueFacturation { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Ville")]
        public string VilleFacturation { get; set; }

        [Display(Name = "Numéro d'appartement")]
        public int AppFacturation { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Code postal")]
        [DataType(DataType.PostalCode)]
        public string CodePostalFacturation { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int ProvinceFacturationId { get; set; }

        // adresse de livraison
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Numéro civique")]
        [Number]
        public string NoCiviqueLivraison { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Rue")]
        public string RueLivraison { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Ville")]
        public string VilleLivraison { get; set; }

        [Display(Name = "Numéro d'appartement")]
        public int AppLivraison { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Code postal")]
        [DataType(DataType.PostalCode)]
        public string CodePostalLivraison { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Province")]
        public int ProvinceLivraisonId { get; set; }

        // liste des provinces
        public SelectList Provinces { get; set; }
    }
}
