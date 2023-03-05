using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Validation;

namespace vlissides_bibliotheque.DTO
{
    public class LivreBibliothequeDto
    {
        public int LivreId;

        public int MaisonEditionId;

        public string MaisonEditionNom;

        public string Isbn;

        public string Titre;

        public string Resume;

        public string PhotoCouverture;

        public int AnneePublication;
    }
}
