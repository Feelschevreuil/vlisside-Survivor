using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class FactureLivreVM
    {
        public List<LivreBibliotheque> Livres { get; set; }
        [DisplayName("Liste des livres")]
        public List<checkBoxLivre> checkBoxLivres { get; set; }

        public List<int> livreIds { get; set; }
    }
}
