using System;
using System.Collections.Generic;
using System.ComponentModel;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.DTO
{
    public class LivreBibliothequeDto
    {
        public int LivreId { get; set; }

        public int MaisonEditionId { get; set; }

        [DisplayName("Maison d'édition")]
        public string MaisonEditionNom { get; set; }

        public string Isbn { get; set; }

        public PrixEtatLivreDto Prix { get; set; } = new PrixEtatLivreDto();

        public List<CoursVM> Cours { get; set; }

        public string Titre { get; set; }
        [DisplayName("Description")]
        public string Resume { get; set; }
        [DisplayName("Photo")]
        public string PhotoCouverture { get; set; }

        public DateTime DatePublication { get; set; }
    }
}
