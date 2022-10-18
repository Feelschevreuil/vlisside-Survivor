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
        [RegularExpression(@"^[\w-\.]+@(cegep-connaissance-aleatoire\.qc\.ca)",
            ErrorMessage = "Le courriel doit correspondre au format : 123456@cegep-connaissance-aleatoire.qc.ca")]
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
        public string NoCivique { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Rue")]
        public string Rue { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Ville")]
        public string Ville { get; set; }

        [Display(Name = "Numéro d'appartement")]
        public int App { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Code postal")]
        [DataType(DataType.PostalCode)]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int ProvinceId { get; set; }

        // liste des provinces
        public SelectList Provinces { get; set; }
    }
}
