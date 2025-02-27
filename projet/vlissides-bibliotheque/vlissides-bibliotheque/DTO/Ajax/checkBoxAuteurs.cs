using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.DTO.Ajax
{
    public class checkBoxAuteurs
    {
        public AuteurVM Auteur { get; set; }

        public bool Cocher { get; set; }
    }
}
