using System.ComponentModel;
using vlissides_bibliotheque.DTO;

namespace vlissides_bibliotheque.ViewModels
{
    public class LivreDetailVM
    {
        public LivreBibliothequeDto? livreBibliotheque;

        public string? programmeEtudeNom;

        [DisplayName("Quantité")]
        public int Quantite { get; set; }

        public List<string>? auteurs { get; set; }

        public int quantite { get; set; }
    }
}
