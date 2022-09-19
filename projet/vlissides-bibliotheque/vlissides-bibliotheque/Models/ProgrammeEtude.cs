using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>ProgrammeEtude</c> définit la table programmeEtude dans la base de données.
    /// </summary>
    public class ProgrammeEtude
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        [MaxLength(4)]
        public string Code { get; set; }
    }
}
