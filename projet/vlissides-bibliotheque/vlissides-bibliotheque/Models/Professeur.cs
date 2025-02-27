using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Models
{
    /// <summary>
    /// Classe <c>Professeur</c> instancie un professeur.
    /// </summary>
    public class Professeur
    {
        [Required]
        public int ProfesseurId { get; set; }

        [Required]
        public int NumeroProfesseur { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        public virtual List<Cours> Cours { get; set; }
    }
}
