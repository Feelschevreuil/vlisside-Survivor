using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace vlissides_bibliotheque.Extensions.Interface
{
    public interface IDropDownList
    {
        List<SelectListItem> ListDropDownAuteurs();
        List<SelectListItem> ListDropDownMaisonDedition();
        List<SelectListItem> ListDropDownProgrammesEtude();
        List<SelectListItem> ListDropDownEtatsLivre();
        List<SelectListItem> ListDropDownStatutCommande();
        List<SelectListItem> ListDropDownEtudiant();
    }
}
