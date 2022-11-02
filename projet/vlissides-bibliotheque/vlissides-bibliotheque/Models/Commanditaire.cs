﻿using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Commanditaire</c> définit la table commanditaire dans la base de données.
    /// </summary>
    public class Commanditaire
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public int CommanditaireId { get; set; }

        [Required(ErrorMessage = "Le champ {0} du commanditaire est requis.")]
        [StringLength(40)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} du commanditaire est requis.")]
        [EmailAddress]
        public string Courriel { get; set; }

        [Url]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Le champ {0} du commanditaire est requis.")]
        [StringLength(512)]
        public string Message { get; set; }
    }
}
