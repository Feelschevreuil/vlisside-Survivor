using vlissides_bibliotheque.ViewModels;
using System.Collections.Generic;

namespace vlissides_bibliotheque.DTO.Ajax
{
    public class ModifierLivreCours : AjoutEditLivreVM
    {
        public List<int> Cours { get; set; }

        public string DateFormater { get; set; }
    }
}
