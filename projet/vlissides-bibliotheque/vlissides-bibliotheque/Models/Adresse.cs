﻿

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Adresse</c> définit la table adresse dans la base de données.
    /// </summary>
    public class Adresse
    {
        [Required]
        public int AdresseId { get; set; }

        [Required]
        [StringLength(64)]
        [DisplayName("Nom de ville")]
        public string Ville { get; set; }


        [Required]
        [DisplayName("Numéro civique")]
        public int NumeroCivique { get; set; }


        public string App { get; set; }

        [Required]
        [StringLength(64)]
        [DisplayName("Nom de rue")]
        public string Rue { get; set; }

        [Required]
        [StringLength(6)]
        [DisplayName("Code postal")]
        public string CodePostal { get; set; }

        [Required]
        [DisplayName("Province")]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public virtual Etudiant Etudiant { get; set; }

    }
}
