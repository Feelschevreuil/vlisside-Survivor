﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.ViewModels
{
    public class ModificationLivreVM
    {
        public int IdDuLivre { get; set; }

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

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Usagé")]
        [DataType(DataType.Currency)]
        [Number]

        public double? PrixUsage { get; set; } = 0;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [DisplayName("Numérique")]
        [DataType(DataType.Currency)]
        [Number]
        public double? PrixNumerique { get; set; } = 0;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Number]
        [DataType(DataType.Currency)]
        [DisplayName("Neuf")]
        public double? PrixNeuf { get; set; } = 0;

        [DisplayName("Quantité")]
        public int? QuantiteUsagee { get; set; }

        [DisplayName("Vendable")]
        public bool PossedeNumerique { get; set; } = false;

        [DisplayName("Vendable")]
        public bool PossedeNeuf { get; set; } = false;

        public bool PossedeUsagee { get; set; } = false;

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Isbn]
        [Range(1000000000, 9999999999999, ErrorMessage = "Veuillez entrer un nombre.")]
        [MaxLength(13, ErrorMessage = "Le champ {0} ne peux pas dépasser 13 caractères ")]
        public string ISBN { get; set; }

        [DisplayName("Maison d'édition")]
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int? MaisonDeditionId { get; set; }

        public List<SelectListItem> MaisonsDeditions { get; set; }
        [DisplayName("Liste des cours")]
        public List<checkBoxCours> checkBoxCours { get; set; }
        [DisplayName("Liste des auteurs")]
        public List<checkBoxAuteurs> checkBoxAuteurs { get; set; }
    }

}

