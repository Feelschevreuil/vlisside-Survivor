using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class AjoutEditLivreVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Titre { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(512, ErrorMessage = "Le champ {0} ne peux pas dépasser 512 caractères ")]
        public string Resume { get; set; }

        [Required]
        [Image]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Date de publication")]
        [DataType(DataType.Date)]
        public DateTime DatePublication { get; set; } = DateTime.Now;

        public PrixEtatLivreDto Prix {  get; set; }


        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Isbn]
        [Range(1000000000, 9999999999999, ErrorMessage = "Veuillez entrer un nombre.")]
        [MaxLength(13, ErrorMessage = "Le champ {0} ne peux pas dépasser 13 caractères ")]
        public string ISBN { get; set; }

        [DisplayName("Maison d'édition")]
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int MaisonEditionId { get; set; }

        public List<SelectListItem> MaisonsDeditions { get; set; }
        [DisplayName("Cours")]
        public List<checkBoxCours> CheckBoxCours { get; set; }
        [DisplayName("Auteurs")]
        public List<checkBoxAuteurs> CheckBoxAuteurs { get; set; }
        public List<CoursVM> Cours { get; set; }
        public List<AuteurVM> Auteurs { get; set; }
        public string DateFormater { get { return DatePublication.ToString("dd MMMM yyyy"); } }
    }

}

