using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class InscriptionVM
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

        //[Required(ErrorMessage = "Le champ {0} est requis.")]
        //[Display(Name = "Programme d'étude")]
        //public ProgrammeEtude ProgrammeEtude { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Numéro civique")]
        [Range(0, int.MaxValue, ErrorMessage = "Entrer un nombre valide.")]
        [Number]
        public int NoCivique { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Rue { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Ville { get; set; }

        [Display(Name = "Numéro d'appartement")]
        public int App { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Code postal")]
        [DataType(DataType.PostalCode)]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "No de téléphone")]
        [DataType(DataType.PhoneNumber)]
        public string NoTelephone { get; set; }
    }
}
