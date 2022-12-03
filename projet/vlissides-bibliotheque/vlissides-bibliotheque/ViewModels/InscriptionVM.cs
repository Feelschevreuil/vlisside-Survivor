using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class InscriptionVM
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(512, ErrorMessage = "Le champ {0} ne peux pas dépasser 512 caractères ")]
        //[RegularExpression(@"^[\w-\.]+@(cegep-connaissance-aleatoire\.qc\.ca)",
        //    ErrorMessage = "Le courriel doit correspondre au format : 123456@cegep-connaissance-aleatoire.qc.ca")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }


        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Confirmer le mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation du mot de passe ne concordent pas.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Programme d'étude")]
        public int? ProgrammeEtudeId { get; set; }
        public SelectList ProgrammeEtudes { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Numéro civique")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        [Number]
        public string NoCivique { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Rue { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Ville { get; set; }

        [Display(Name = "Numéro d'appartement")]
        public string? App { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Code postal")]
        [RegularExpression(@"[A-Z][0-9][A-Z][0-9][A-Z][0-9]",
            ErrorMessage = "Le code postal doit correspondre au format : " +
            "A0A 0A0")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Numéro de téléphone")]
        //[RegularExpression(@"[0-9]{3}\-[0-9]{3}\-[0-9]{4}",
        //    ErrorMessage = "Le numéro de téléphone doit correspondre au format : " +
        //    "123-456-7890")]
        [MinLength(10, ErrorMessage = "Le numéro de téléphone doit être composé de 10 chiffres.")]
        [MaxLength(10, ErrorMessage = "Le numéro de téléphone doit être composé de 10 chiffres.")]
        public string NoTelephone { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Province")]
        public int? ProvinceId { get; set; }
        public SelectList Provinces { get; set; }
    }
}
