using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class AjoutEditProgrammeEtudesVM
    {
        public int Id { get; set; }

        [Required]
        public int ProgrammeEtudeId { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(64, ErrorMessage = "Le champ {0} ne peux pas dépasser 64 caractères ")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [MaxLength(4, ErrorMessage = "Le code ne peux pas excéder 4 charactères")]
        public string Code { get; set; }
    }
}
