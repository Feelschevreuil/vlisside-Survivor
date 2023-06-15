using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Cours</c> définit les cours d'un programme d'étude.
    /// </summary>
    public class Cours
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")] 
        public int CoursId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")] 
        public int ProgrammeEtudeId { get; set; }
        [DisplayName("Programme d'étude")]
        public ProgrammeEtude ProgrammeEtude { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(8, ErrorMessage ="Le code ne peux pas dépasser 8 caractères")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Range(1,3, ErrorMessage ="L'année du parcours doit se situer entre 1 et 3")]
        [DisplayName("Année")]
        public int AnneeParcours { get; set; }

        public virtual List<CoursLivre> Livres { get; set; }
    }
}
