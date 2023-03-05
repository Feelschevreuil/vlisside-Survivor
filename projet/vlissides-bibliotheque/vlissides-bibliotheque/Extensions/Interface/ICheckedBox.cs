using vlissides_bibliotheque.DTO.Ajax;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Extensions.Interface
{
    public interface ICheckedBox
    {
        public List<checkBoxCours> GetCoursCheckedBox(string etudiantId);
        public List<checkBoxCours> GetCoursLivre(LivreBibliotheque livreBibliotheque);
        public List<checkBoxAuteurs> GetAuteursLivre(LivreBibliotheque livreBibliotheque);
        public List<checkBoxCours> GetCours();
        public List<checkBoxAuteurs> GetAuteurs();
    }
}
