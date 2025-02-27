using System.Collections.Generic;
using System.ComponentModel;
using vlissides_bibliotheque.DTO;

namespace vlissides_bibliotheque.ViewModels
{
    public class TuileLivreBibliotequeVM
    {
        public LivreBibliothequeDto LivreBibliotheque;

        public string ProgrammeEtudeNom;
        
        [DisplayName("Quantité")]
        public int Quantite { get; set; }

        public List<string> Auteurs;
    }
}
