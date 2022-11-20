using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using Stripe;

namespace vlissides_bibliotheque.Services
{

    /// <summary>
    /// Clsase <c>FactureEtudiantService</c> qui ajoute des Services pour <c>FactureEtudiant</c>.
    /// </summary>
    public class FactureEtudiantService
    {

        private ApplicationDbContext _context;

        public FactureEtudiantService(ApplicationDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Compare deux factures etudiants et regarde si les propriétés diffèrent.
        /// Tenir en compte que ni l'id ni l'étudiant sont comparés.
        /// </summary>
        /// <param name="factureEtudiantReference">Objet de référence.</param>
        /// <param name="factureEtudiantModifie">Objet modifié à comparer.</param>
        public static bool EstDifferentDe
        (
            FactureEtudiant factureEtudiantReference, 
            FactureEtudiant factureEtudiantModifie
        )
        {
            
            if
            (
                !string
                    .Equals
                    (
                        factureEtudiantReference.AdresseLivraison, 
                        factureEtudiantModifie.AdresseLivraison, 
                        StringComparison.OrdinalIgnoreCase
                    )
            )
            {

                return true;
            }
            else if
            (
                !factureEtudiantReference
                    .DateFacturation
                    .Equals(factureEtudiantModifie.DateFacturation)
            )
            {

                return true;
            }
            else if(factureEtudiantReference.Tps != factureEtudiantModifie.Tps)
            {

                return true;
            }
            else if(factureEtudiantReference.Tvq != factureEtudiantModifie.Tvq)
            {

                return true;
            }

            return false;
        }

        /// <summary>
        /// Applique la ou les différences d'un objet à un autre.
        /// </summary>
        /// <param name="factureEtudiantAMettreAJour">Objet à mettre à jour.</param>
        /// <param name="factureEtudiantModifie">Objet avec les modifications.</param>
        public static FactureEtudiant MettreAJourProprietes
        (
            FactureEtudiant factureEtudiantAMettreAJour, 
            FactureEtudiant factureEtudiantModifie
        )
        {
            
            if
            (
                !string
                    .Equals
                    (
                        factureEtudiantAMettreAJour.AdresseLivraison, 
                        factureEtudiantModifie.AdresseLivraison, 
                        StringComparison.OrdinalIgnoreCase
                    )
            )
            {

                factureEtudiantAMettreAJour.AdresseLivraison = factureEtudiantModifie.AdresseLivraison;
            }
            if(!factureEtudiantAMettreAJour.DateFacturation.Equals(factureEtudiantModifie.DateFacturation))
            {

                factureEtudiantAMettreAJour.DateFacturation = factureEtudiantModifie.DateFacturation;
            }
            if(factureEtudiantAMettreAJour.Tps != factureEtudiantModifie.Tps)
            {

                factureEtudiantAMettreAJour.Tps = factureEtudiantModifie.Tps;
            }
            if(factureEtudiantAMettreAJour.Tvq != factureEtudiantModifie.Tvq)
            {

                factureEtudiantAMettreAJour.Tvq = factureEtudiantModifie.Tvq;
            }

            return factureEtudiantAMettreAJour;
        }

        /// <summary>
        /// Crée une <c>FactureEtudiant</c> avec les commandes désirées.
        /// </summary>
        /// <param name="etudiantId">
        /// Id de l'étudiant à associer à la commande.
        /// </param>
        /// <param name="prixEtatLivresId">
        /// Liste de <c>PrixEtatLivre</c> à ajouter à la facture.
        /// </param>
        /// <returns>Le <c>PrixEtatLivre</c> avec les commandes désirés ou null si les
        /// livres à commander ne sont pas valides.
        /// </returns>
        public FactureEtudiant Create
        (
            string etudiantId,
            List<int> prixEtatLivresId
        )
        {

            List<CommandeEtudiant> commandesEtudiantsAjouter;
            FactureEtudiant factureEtudiant;
            PrixEtatLivreDAO prixEtatLivreDAO;
            FacturesEtudiantsDAO facturesEtudiantsDAO;
            CommandeEtudiantService commandeEtudiantService;
            double totalFacture;

            prixEtatLivreDAO = new(_context);

            if(prixEtatLivreDAO.GetBulk(prixEtatLivresId).Count() > 0)
            {

                facturesEtudiantsDAO = new(_context);

                // TODO: outsource tax from config!!
                factureEtudiant = new()
                {
                    EtudiantId = etudiantId,
                    DateFacturation = DateTime.Now,
                    Statut = StatusFacture.ATTENTE_PAIEMENT,
                    Tvq = 0.0M,
                    Tps = 0.05M
                };

                facturesEtudiantsDAO.Save(factureEtudiant);

                commandeEtudiantService = new(_context);

                commandesEtudiantsAjouter = commandeEtudiantService
                    .CreerCommandesSelonListeIdsPrixEtatLivre
                    (
                        factureEtudiant,
                        prixEtatLivresId
                    );

                totalFacture = 0.0;

                foreach(CommandeEtudiant commandeEtudiant in commandesEtudiantsAjouter)
                {

                    totalFacture += commandeEtudiant.PrixUnitaireGele;
                }

                if(factureEtudiant.Tvq > 0)
                {

                    totalFacture = totalFacture * decimal.ToDouble(1 + factureEtudiant.Tvq);
                }

                if(factureEtudiant.Tps > 0)
                {

                    totalFacture = totalFacture * decimal.ToDouble(1 + factureEtudiant.Tps);
                }

                // TODO: créer payment intent avec API de stripe
                // totalFacture, price and so on.
                PaymentIntent paymentIntent;

                var options = new PaymentIntentCreateOptions
                {
                    Amount = 999,
                    Currency = "cad",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var service = new PaymentIntentService();

                paymentIntent = service.Create(options);
                
                factureEtudiant.PaymentIntentId = paymentIntent.Id;
                
                return factureEtudiant;
            }

            return null;
        }
    }
}
