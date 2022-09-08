﻿using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Evenement</c> définit la table evenement dans la base de données.
    /// </summary>
    public class Evenement
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CommanditaireId { get; set; }
        public Commanditaire Commanditaire { get; set; }

        [Required]
        [StringLength(64)]
        public string Nom { get; set; }

        [Required]
        public DateTime Debut { get; set; }

        [Required]
        public DateTime Fin { get; set; }

        [Required]
        [StringLength(512)]
        public string Description { get; set; }
    }
}
