using System.ComponentModel;

namespace vlissides_bibliotheque.DTO
{
    public class LivreBibliothequeDto
    {
        public int LivreId { get; set; }

        public int MaisonEditionId { get; set; }

        [DisplayName("Maison d'édition")]
        public string MaisonEditionNom { get; set; }

        public string Isbn { get; set; }

        public PrixEtatLivreDto prix { get; set; }

        public string Titre { get; set; }
        [DisplayName("Description")]
        public string Resume { get; set; }
        [DisplayName("Photo")]
        public string PhotoCouverture { get; set; }

        public DateTime DatePublication { get; set; }
    }
}
