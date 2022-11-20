using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.ViewModels;
using Stripe;

namespace vlissides_bibliotheque.Services
{

    /// <summary>
    /// Clsase <c>FactureEtudiantService</c> qui ajoute des Services pour <c>FactureEtudiant</c>.
    /// </summary>
    public class FactureEtudiantService
    {

        private ApplicationDbContext _context;
        private ConfigurationService _configurationService;

        private Etudiant _etudiant;
        private FactureEtudiant _factureEtudiant;
        private List<CommandeEtudiant> _commandesEtudiantsAjouter;
        private CommandeEtudiantService _commandeEtudiantService;
        private double _totalFacture;

        /// <summary>
        /// Constructeur de base lorsque l'on n'a pas besoin du configuration service.
        /// </summary>
        /// <param name="context">Représente la base de données.</param>
        public FactureEtudiantService(ApplicationDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Constructeur surchagé lorsque l'on abesoin du configuration service.
        /// </summary>
        /// <param name="context">Représente la base de données.</param>
        /// <param name="configurationService">ConfigurationService.</param>
        /// <param name="etudiant">Étudiant effectuant la commande.</param>
        public FactureEtudiantService
        (
            ApplicationDbContext context, 
            ConfigurationService configurationService,
            CommandeEtudiantService commandeEtudiantService,
            Etudiant etudiant
        )
        {

            _context = context;
            _configurationService = configurationService;
            _commandeEtudiantService = commandeEtudiantService;
            _etudiant = etudiant;
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
            else if
            (
                !string
                    .Equals
                    (
                        factureEtudiantReference.EtudiantId, 
                        factureEtudiantModifie.EtudiantId
                    )
            )
            {

                return true;
            }
            else if
            (
                !string.IsNullOrEmpty(factureEtudiantReference.PaymentIntentId) &&
                    !string.IsNullOrEmpty(factureEtudiantModifie.PaymentIntentId) &&
                        !string
                            .Equals
                            (
                                factureEtudiantReference.PaymentIntentId, 
                                factureEtudiantModifie.PaymentIntentId, 
                                StringComparison.OrdinalIgnoreCase
                            )
            )
            {

                return true;
            }
            else if
            (
                !string.IsNullOrEmpty(factureEtudiantReference.ClientSecret) &&
                    !string.IsNullOrEmpty(factureEtudiantModifie.ClientSecret) &&
                        !string
                            .Equals
                            (
                                factureEtudiantReference.ClientSecret, 
                                factureEtudiantModifie.ClientSecret, 
                                StringComparison.OrdinalIgnoreCase
                            )
            )
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
        public FactureEtudiant MettreAJourProprietes
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

                factureEtudiantAMettreAJour
                    .AdresseLivraison = factureEtudiantModifie.AdresseLivraison;
            }
            if
            (
                !factureEtudiantAMettreAJour
                    .DateFacturation
                    .Equals(factureEtudiantModifie.DateFacturation)
            )
            {

                factureEtudiantAMettreAJour
                    .DateFacturation = factureEtudiantModifie.DateFacturation;
            }
            if(factureEtudiantAMettreAJour.Tps != factureEtudiantModifie.Tps)
            {

                factureEtudiantAMettreAJour.Tps = factureEtudiantModifie.Tps;
            }
            if(factureEtudiantAMettreAJour.Tvq != factureEtudiantModifie.Tvq)
            {

                factureEtudiantAMettreAJour.Tvq = factureEtudiantModifie.Tvq;
            }
            if
            (
                !string
                    .Equals
                    (
                        factureEtudiantAMettreAJour.EtudiantId, 
                        factureEtudiantModifie.EtudiantId
                    )
            )
            {

                Etudiant etudiantAJour;

                // TODO: utiliser DAO
                etudiantAJour = _context
                    .Etudiants
                        .First
                        (
                            etudiant => 
                                etudiant.Id == factureEtudiantModifie.EtudiantId
                        );

                if(etudiantAJour != null)
                {

                    factureEtudiantAMettreAJour.Etudiant = etudiantAJour;
                }
            }
            // TODO: optimiser comparaison
            if
            (
                !string.IsNullOrEmpty(factureEtudiantAMettreAJour.PaymentIntentId) &&
                    !string.IsNullOrEmpty(factureEtudiantModifie.PaymentIntentId) &&
                        !string
                            .Equals
                            (
                                factureEtudiantAMettreAJour.PaymentIntentId, 
                                factureEtudiantModifie.PaymentIntentId, 
                                StringComparison.OrdinalIgnoreCase
                            )
            )
            {

                factureEtudiantAMettreAJour
                    .PaymentIntentId = factureEtudiantModifie.PaymentIntentId;
            }
            if
            (
                !string.IsNullOrEmpty(factureEtudiantAMettreAJour.ClientSecret) &&
                    !string.IsNullOrEmpty(factureEtudiantModifie.ClientSecret) &&
                        !string
                            .Equals
                            (
                                factureEtudiantAMettreAJour.ClientSecret, 
                                factureEtudiantModifie.ClientSecret, 
                                StringComparison.OrdinalIgnoreCase
                            )
            )
            {

                factureEtudiantAMettreAJour
                    .ClientSecret = factureEtudiantModifie.ClientSecret;
            }

            return factureEtudiantAMettreAJour;
        }

        /// <summary>
        /// Crée une facture et retourne le bon modèle de vue à afficher.
        /// </summary>
        /// <param name="prixEtatLivresId">Livres à ajouter à la facture.</param>
        /// <returns>
        /// Le modèle de vue <c>PaiementVM</c> avec les informations du paiement.
        /// </returns>
        public PaiementVM CreateViewModel
        (
            List<int> prixEtatLivresId
        )
        {

            if(Create(prixEtatLivresId) != null)
            {

                PaiementVM paiementVM;
                string apiKeyPublique;
                List<CommandePartielleVM> commandesPartielles;

                apiKeyPublique = _configurationService
                    .GetProprieteDeSection
                    (
                        ConstantesConfiguration.PROPRIETE_STRIPE,
                        ConstantesConfiguration.PROPRIETE_STRIPE_CLE_API_PUBLIQUE
                    );

                commandesPartielles = _commandeEtudiantService
                    .GetCommandesPartiellesFromCommandes(_commandesEtudiantsAjouter);

                paiementVM = new()
                {
                    PublicApiKey = apiKeyPublique,
                    PaymentIntentId = _factureEtudiant.PaymentIntentId,
                    AdresseLivraison = _etudiant.Adresse,
                    CommandesPartielles = commandesPartielles,
                    Tvq = _factureEtudiant.Tvq,
                    Tps = _factureEtudiant.Tps,
                    Total = _totalFacture,
                    StatutFacture = _factureEtudiant.Statut
                };

                return paiementVM;
            }

            return null;
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
        private FactureEtudiant Create
        (
            List<int> prixEtatLivresId
        )
        {

            List<CommandeEtudiant> commandesEtudiantsAjouter;
            PrixEtatLivreDAO prixEtatLivreDAO;
            FacturesEtudiantsDAO facturesEtudiantsDAO;
            long prixFactureFinal;

            prixEtatLivreDAO = new(_context);

            if(prixEtatLivreDAO.GetBulk(prixEtatLivresId).Count() > 0)
            {

                facturesEtudiantsDAO = new(_context);
                
                // TODO: outsource tax from config!!
                _factureEtudiant = new()
                {
                    Etudiant = _etudiant,
                    DateFacturation = DateTime.Now,
                    Statut = StatusFacture.ATTENTE_PAIEMENT,
                    Tvq = 0.0M,
                    Tps = 0.05M
                };
                
                facturesEtudiantsDAO.Save(_factureEtudiant);

                _commandesEtudiantsAjouter = _commandeEtudiantService
                    .CreerCommandesSelonListeIdsPrixEtatLivre
                    (
                        _factureEtudiant,
                        prixEtatLivresId
                    );

                _totalFacture = CalculerTotalCommandes();

                prixFactureFinal = AdapterPrixAStripe(_totalFacture);

                _factureEtudiant = TraiterFactureAvecStripe
                (
                    prixFactureFinal
                );

                facturesEtudiantsDAO
                    .Update
                    (
                        _factureEtudiant.FactureEtudiantId, 
                        _factureEtudiant
                    );
                
                return _factureEtudiant;
            }

            return null;
        }

        /// <summary>
        /// Calcule le total d'une liste de commandes.
        /// </summary>
        /// <param name="commandesEtudiantes">Commandes à chercher le prix.</param>
        /// <param name="factureEtudiant">Factue contenant le taux d'impôts.</param>
        /// <returns>Le total en format double des commandes.</returns>
        private double CalculerTotalCommandes()
        {

            double totalFacture;

            totalFacture = 0.0;

            foreach(CommandeEtudiant commandeEtudiant in _commandesEtudiantsAjouter)
            {

                totalFacture += commandeEtudiant.PrixUnitaireGele;
            }

            if(_factureEtudiant.Tvq > 0)
            {

                totalFacture = totalFacture * decimal.ToDouble(1 + _factureEtudiant.Tvq);
            }

            if(_factureEtudiant.Tps > 0)
            {

                totalFacture = totalFacture * decimal.ToDouble(1 + _factureEtudiant.Tps);
            }

            return totalFacture;
        }

        /// <summary>
        /// Transforme un prix de double à long, qui est le format que
        /// Stripe veut.
        /// </summary>
        /// <param name="prix">Prix à trasnformer.</param>
        /// <returns>Le prix adapté au format que Stripe veut.</returns>
        private long AdapterPrixAStripe(double prix)
        {

            long prixStripe;

            prixStripe = (long)(prix * 100);

            return prixStripe;
        }

        /// <summary>
        /// </summary>
        private FactureEtudiant TraiterFactureAvecStripe
        (
            long totalFacture
        )
        {

            PaymentIntent paymentIntent;
            string apiKeyPrivee;
            string apiKeyPublique;

            apiKeyPrivee = _configurationService
                .GetProprieteDeSection
                (
                    ConstantesConfiguration.PROPRIETE_STRIPE, 
                    ConstantesConfiguration.PROPRIETE_STRIPE_CLE_API_PRIVEE
                );

            StripeConfiguration.ApiKey = apiKeyPrivee;

            var options = new PaymentIntentCreateOptions
            {
                Amount = totalFacture,
                Currency = "cad",
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();

            paymentIntent = service.Create(options);
            
            _factureEtudiant.PaymentIntentId = paymentIntent.Id;
            _factureEtudiant.ClientSecret = paymentIntent.ClientSecret;

            return _factureEtudiant;
        }
    }
}
