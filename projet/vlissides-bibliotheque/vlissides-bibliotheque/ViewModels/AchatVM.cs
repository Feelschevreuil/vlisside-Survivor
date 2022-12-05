using System.ComponentModel.DataAnnotations;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Models.Achat;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.ViewModels
{
    public class AchatVM
    {

        public int FactureEtudiantId { get; set; }
        public string NomEtudiant { get; set; }
        public string PublicApiKey { get; set; }
        public string ClientSecret { get; set; }
        public AchatInformationsLivraisonVM AchatInformationsLivraison { get; set; }
        public List<CommandePartielle> CommandesPartielles { get; set; }
        public decimal Tvq { get; set; }
        public decimal Tps { get; set; }
        public double Total { get; set; }
        public StatutFactureEnum StatutFacture { get; set; }

        /// <summary>
        /// Calcule le nombre total de commandes.
        /// </summary>
        public int GetNombreCommandesPartielles()
        {

            int nombreCommandesPartielles;

            nombreCommandesPartielles = 0;

            foreach(CommandePartielle commandePartielle in CommandesPartielles)
            {

                nombreCommandesPartielles = nombreCommandesPartielles + commandePartielle.Quantite;
            }

            return nombreCommandesPartielles;
        }
    }
}
