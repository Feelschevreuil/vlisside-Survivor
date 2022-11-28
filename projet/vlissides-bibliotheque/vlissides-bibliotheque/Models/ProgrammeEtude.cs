using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>ProgrammeEtude</c> définit la table programmeEtude dans la base de données.
    /// </summary>
    public class ProgrammeEtude
    {
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
