using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class GestionProfilVM
    {
        public string EtudiantId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        //[RegularExpression(@"^[\w-\.]+@(cegep-connaissance-aleatoire\.qc\.ca)",
        //    ErrorMessage = "Le courriel doit correspondre au format : 123456@cegep-connaissance-aleatoire.qc.ca")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        [DisplayName("Prénom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Programme d'étude")]
        public int ProgrammeEtudeId { get; set; }
        public SelectList ProgrammeEtudes { get; set; }
        public string? NomProgrammeEtude { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Numéro de téléphone")]
        [MinLength(10, ErrorMessage = "Le numéro de téléphone doit être composé de 10 chiffres.")]
        [MaxLength(10, ErrorMessage = "Le numéro de téléphone doit être composé de 10 chiffres.")]
        public string NoTelephone { get; set; }

        // adresse de facturation
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Numéro civique")]
        [Number]
        public string NoCivique { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Rue")]
        public string Rue { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Ville")]
        public string Ville { get; set; }

        [Display(Name = "Numéro d'appartement")]
        public string? App { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Code postal")]
        [RegularExpression(@"[A-Z][0-9][A-Z][0-9][A-Z][0-9]",
            ErrorMessage = "Le code postal doit correspondre au format : " +
            "A0A 0A0")]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Province")]
        public int ProvinceId { get; set; }

        // liste des provinces
        public SelectList Provinces { get; set; }
        [DisplayName("Province")]
        public string? NomProvince { get; set; }

        public int CoursId { get; set; }
        [Display(Name = "Liste des cours")]
        public List<checkBoxCours> checkBoxCours { get; set; }
    }
}
