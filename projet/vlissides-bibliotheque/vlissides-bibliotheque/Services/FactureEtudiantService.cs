using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Models.Achat;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.ViewModels;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Utils;
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

        // TODO: clean up le code
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

        // TODO: fix double constructor (Will break)
        /// <summary>
        /// Constructeur surchagé lorsque l'on abesoin du configuration service.
        /// </summary>
        /// <param name="context">Représente la base de données.</param>
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
        /// Crée la facture de l'étudiant selon l'étudiant et ses commandes.
        /// </summary>
        /// <param name="etudiant">Étudiant désirant les livres.</param>
        /// <param name="commandesEtudiants">Commandes que l'étudiant demande.</param>
        /// <returns></returns>
        public FactureEtudiant Creer
        (
            List<CommandeEtudiant> commandesEtudiants,
            // TODO: sortir d'ici!!
            FacturesEtudiantsDAO facturesEtudiantsDAO
        )
        {

            double totalFacture;
            long totalFactureAdapte;

            _commandesEtudiantsAjouter = commandesEtudiants;

            // TODO: fix this garbage
            _factureEtudiant = new()
            {
                Tvq = 0.0M,
                Tps = 0.05M
            };

            totalFacture = CalculerTotalCommandes();

            if(totalFacture != 0)
            {


                totalFactureAdapte = AdapterPrixAStripe(totalFacture);

                //TODO: fix this garbage
                _factureEtudiant.Etudiant = _etudiant;
                _factureEtudiant.AdresseLivraison = _etudiant.Adresse.ToString();
                _factureEtudiant.DateFacturation = DateTime.Now;
                _factureEtudiant.Statut = StatutFactureEnum.ATTENTE_PAIEMENT;
                
                /*
                _factureEtudiant = new()
                {
                    Etudiant = _etudiant,
                    // TODO: merge develop to get formated ToString()
                    AdresseLivraison = _etudiant.Adresse.ToString(),
                    DateFacturation = DateTime.Now,
                    Tvq = 0.0M,
                    Tps = 0.05M,
                    Statut = StatutFactureEnum.ATTENTE_PAIEMENT
                };
                */

                _factureEtudiant = TraiterFactureAvecStripe
                (
                    totalFactureAdapte
                );

                facturesEtudiantsDAO.Save(_factureEtudiant);
                
                return _factureEtudiant;
            }

            return null;
        }

        /// <summary>
        /// Crée le modèle de vue pour afficher les informations d'une commande étudiante.
        /// </summary>
        /// <param name="commandesEtudiants"></param>
        /// <param name="facture"></param>
        /// <returns></returns>
        public AchatVM CreerAchatVM
        (
            FactureEtudiant factureEtudiant, 
            List<CommandeEtudiant> commandesEtudiants
        )
        {

            AchatVM achatVM;
            AchatInformationsLivraisonVM achatInformationsLivraison;
            List<CommandePartielle> commandesPartielles;
            AchatInformationsLivraisonVM achat;

            achatInformationsLivraison = new()
            {
                AdresseLivraison = factureEtudiant.Etudiant.Adresse,
                NumeroTelephone = factureEtudiant.Etudiant.PhoneNumber
            };

            commandesPartielles = _commandeEtudiantService
                .GetCommandesPartiellesFromCommandes(commandesEtudiants);

            // TODO: fix this redneck shit
            _commandesEtudiantsAjouter = commandesEtudiants;

            achatVM = new()
            {
                AchatInformationsLivraison = achatInformationsLivraison,
                CommandesPartielles = commandesPartielles,
                Tvq = factureEtudiant.Tvq,
                Tps = factureEtudiant.Tps,
                Total = CalculerTotalCommandes(),
                StatutFacture = factureEtudiant.Statut
            };

            if(factureEtudiant.Statut == StatutFactureEnum.ATTENTE_PAIEMENT)
            {

                string apiKeyPublique;

                apiKeyPublique = _configurationService.GetProprieteDeSection
                (
                    ConstantesConfiguration.PROPRIETE_STRIPE,
                    ConstantesConfiguration.PROPRIETE_STRIPE_CLE_API_PUBLIQUE
                );

                achatVM.PublicApiKey = apiKeyPublique;
                achatVM.ClientSecret = factureEtudiant.ClientSecret;
            }

            return achatVM;
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

                if
                (
                    commandeEtudiant.StatutCommande != StatutCommandeEnum.INEXISTANT &&
                    commandeEtudiant.StatutCommande != StatutCommandeEnum.MANQUE_INVENTAIRE
                )
                {

                    totalFacture += 
                        (commandeEtudiant.PrixUnitaireGele * commandeEtudiant.Quantite);
                }
            }

            totalFacture = CashUtils
                .CalculerTaxes(totalFacture, _factureEtudiant.Tps, _factureEtudiant.Tvq);

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
        /// Traite la facture avec Stripe.
        /// </summary>
        /// <param name="totalFacture">Le total de la facture à facturer au client.</param>
        /// <returns>
        /// La facture mis à jour avec les informations de Stripe.
        /// </returns>
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

            // Task.Run(async () => await ReserverLivres(_factureEtudiant));

            return _factureEtudiant;
        }

        /// <summary>
        /// Réserver les livres d'une facture pendant 15 minutes. Le paiement
        /// est annulé si la facture n'est pas payée après 15 minutes.
        /// </summary>
        /// <param name="commandesEtudiants"></param>
        /// <param name="factureEtudiant"></param>
        /// <returns></returns>
        private async Task ReserverLivres
        (
            FactureEtudiant factureEtudiant,
            int minutes = 15
        )
        {

            Thread.Sleep(TimeUtils.MinutesEnMilisecondes(minutes));

            if(factureEtudiant.Statut == StatutFactureEnum.ATTENTE_PAIEMENT)
            {

                AnnulerFacture(factureEtudiant);
                factureEtudiant.Statut = StatutFactureEnum.ANNULEE_NON_PAYE_DELAIS;

                // TODO: utiliser le DAO des factures.
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Annule une facture et remet les lives en inventiare.
        /// </summary>
        /// <param name="factureEtudiant"><c>FactureEtudiant</c> à annuler.</param>
        private void AnnulerFacture(FactureEtudiant factureEtudiant)
        {

            // TODO: implement
        }
    }
}
