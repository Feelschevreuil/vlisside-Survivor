using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>LivreEtudiant</c> définit la table livreEtudiant dans la base de données.
    /// </summary>
    public class LivreEtudiant : ILivre
    {
        [Key]
        [Required]
        public int LivreId { get; set; }

        public Etudiant Etudiant { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(64)]
        public string Titre { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Isbn]
        public string Isbn { get; set; }


        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(512)]
        public string Resume { get; set; }

        public string PhotoCouverture { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public DateTime DatePublication { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string MaisonEdition { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        public string Auteur { get; set; }
        [Number]
        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        public double? Prix { get; set; }

    }
}
