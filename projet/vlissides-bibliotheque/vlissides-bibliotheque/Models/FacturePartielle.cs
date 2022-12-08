using vlissides_bibliotheque.Enums;

namespace vlissides_bibliotheque.Models
{

    public class FacturePartielle
    {

        public int FactureEtudiantId { get; set; }
        public int NombreCommandes { get; set; }
        public string AdresseLivraison { get; set; }
        public StatutFactureEnum StatutFacture { get; set; }
        public string PrixTotal { get; set; }
    }
}
