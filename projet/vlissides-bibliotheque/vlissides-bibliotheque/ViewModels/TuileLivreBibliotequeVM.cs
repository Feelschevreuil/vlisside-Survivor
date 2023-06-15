using System.ComponentModel;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class TuileLivreBibliotequeVM
    {
        public LivreBibliothequeDto? livreBibliotheque;

        public string? programmeEtudeNom;
        
        [DisplayName("Quantité")]
        public int Quantite { get; set; }

        public List<string>? auteurs;
    }
}
