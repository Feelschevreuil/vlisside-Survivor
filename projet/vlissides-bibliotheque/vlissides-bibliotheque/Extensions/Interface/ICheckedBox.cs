using System.Collections.Generic;
using vlissides_bibliotheque.DTO.Ajax;

namespace vlissides_bibliotheque.Extensions.Interface
{
    public interface ICheckedBox
    {
        public List<checkBoxCours> GetCoursCheckedBox(string etudiantId);
        public List<checkBoxCours> GetCoursLivre(int livreId);
        public List<checkBoxAuteurs> GetAuteursLivre(int livreId);
        public List<checkBoxCours> GetCours();
        public List<checkBoxAuteurs> GetAuteurs();
    }
}
