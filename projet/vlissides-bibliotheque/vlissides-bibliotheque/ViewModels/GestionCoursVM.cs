using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class GestionCoursVM : Cours
    {
        public List<SelectListItem> ProgrammesEtude { get; set; }
        [DisplayName("Programmes d'étude")]
        public int ProgrammesEtudeId { get; set; }
    }
}
