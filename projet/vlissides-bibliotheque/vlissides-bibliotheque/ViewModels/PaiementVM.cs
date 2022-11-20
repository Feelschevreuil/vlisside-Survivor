using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.ViewModels
{
    public class PaiementVM
    {

        public string PublicApiKey { get; set; }
        public string PaymentIntentId { get; set; }
        public Adresse AdresseLivraison { get; set; }
        public List<CommandePartielleVM> CommandesPartielles { get; set; }
        public decimal Tvq { get; set; }
        public decimal Tps { get; set; }
        public double Total { get; set; }
        public StatusFacture StatutFacture { get; set; }
    }
}
