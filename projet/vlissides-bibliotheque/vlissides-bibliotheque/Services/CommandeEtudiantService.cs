using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Enums;
using vlissides_bibliotheque.Utils;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Models.Achat;

namespace vlissides_bibliotheque.Services
{

    /// <summary>
    /// Clsase <c>CommandeEtudiantService</c> qui ajoute des Services pour <c>CommandeEtudiant</c>.
    /// </summary>
    public class CommandeEtudiantService
    {

        private ApplicationDbContext _context;

        public CommandeEtudiantService(ApplicationDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Compare deux commandeEtudiants et regarde si les propriétés diffèrent.
        /// </summary>
        /// <param name="commandeEtudiantReference">CommandeEtudiant de référence.</param>
        /// <param name="commandeEtudiantModifie">CommandeEtudiant modifié à comparer.</param>
        public static bool EstDifferentDe
        (
            CommandeEtudiant commandeEtudiantDeReference,
            CommandeEtudiant commandeEtudiantModifie
        )
        {

            if(commandeEtudiantDeReference.FactureEtudiantId != commandeEtudiantModifie.FactureEtudiantId)
            {

                return true;
            }
            else if(commandeEtudiantDeReference.PrixEtatLivreId != commandeEtudiantModifie.PrixEtatLivreId)
            {

                return true;
            }
            else if(commandeEtudiantDeReference.Quantite != commandeEtudiantModifie.Quantite)
            {

                return true;
            }

            return false;
        }

        /// <summary>
        /// Applique la ou les différences d'un commandeEtudiant à un autre.
        /// </summary>
        /// <param name="commandeEtudiantAMettreAJour">CommandeEtudiant à mettre à jour.</param>
        /// <param name="commandeEtudiantModifie">CommandeEtudiant avec les modifications.</param>
        public static CommandeEtudiant MettreAJourProprietes
        (
            CommandeEtudiant commandeEtudiantAMettreAJour, 
            CommandeEtudiant commandeEtudiantModifie
        )
        {

            if(commandeEtudiantAMettreAJour.FactureEtudiantId != commandeEtudiantModifie.FactureEtudiantId)
            {

                commandeEtudiantAMettreAJour.FactureEtudiantId = commandeEtudiantModifie.FactureEtudiantId;
            }
            else if(commandeEtudiantAMettreAJour.PrixEtatLivreId != commandeEtudiantModifie.PrixEtatLivreId)
            {

                commandeEtudiantAMettreAJour.PrixEtatLivreId = commandeEtudiantModifie.PrixEtatLivreId;
            }
            else if(commandeEtudiantAMettreAJour.Quantite != commandeEtudiantModifie.Quantite)
            {

                commandeEtudiantAMettreAJour.Quantite = commandeEtudiantModifie.Quantite;
            }

            return commandeEtudiantAMettreAJour;
        }

        /// <summary>
        /// Construit les commandes partielles à partir de <c>LivreDesire</c>
        /// </summary>
        public List<CommandeEtudiant> GetCommandesEtudiantByLivreDesire
        (
            List<LivreDesire> livresDesires,
            EtatLivreEnum etatLivre
        )
        {

            List<CommandeEtudiant> commandesEtudiants;
            List<PrixEtatLivre> prixEtatsLivres;
            PrixEtatLivreDAO prixEtatLivreDAO;
            PrixEtatLivre prixEtatLivre;
            CommandeEtudiant commandeEtudiant;

            prixEtatLivreDAO = new(_context);
            commandesEtudiants = new();

            prixEtatsLivres = prixEtatLivreDAO
                .GetBulkByLivreDesireEtEtat
                (
                    livresDesires,
                    etatLivre
                );

            if(!CollectionUtils.CollectionNulleOuVide(prixEtatsLivres))
            {

                foreach(LivreDesire livreDesire in livresDesires)
                {

                    commandeEtudiant = new();

                    // TODO: valider s'il y a duplication (Ne pas assumer que Find va work)
                    prixEtatLivre = prixEtatsLivres
                        .Find
                        (
                            prixEtatLivre =>
                                prixEtatLivre.LivreBibliothequeId == livreDesire.LivreId
                        );

                    if(prixEtatLivre != null)
                    {

                        commandeEtudiant.PrixEtatLivreId = prixEtatLivre.PrixEtatLivreId;

                        commandeEtudiant.Isbn = prixEtatLivre
                            .LivreBibliotheque
                                .Isbn;

                        commandeEtudiant.Titre = prixEtatLivre
                            .LivreBibliotheque
                                .Titre;

                        commandeEtudiant.PrixEtatLivre = prixEtatLivre;

                        commandeEtudiant.EtatLivre = prixEtatLivre.EtatLivre;

                        commandeEtudiant.PrixUnitaireGele = prixEtatLivre.Prix;

                        if(etatLivre == EtatLivreEnum.USAGE)
                        {
                            
                            if(prixEtatLivre.QuantiteUsage >= livreDesire.Quantite)
                            {

                                commandeEtudiant
                                    .StatutCommande = StatutCommandeEnum.CORRECT;

                                commandeEtudiant.Quantite = livreDesire.Quantite;

                                prixEtatLivreDAO
                                    .SoustraireDuStock
                                    (
                                        prixEtatLivre,
                                        commandeEtudiant.Quantite
                                    );
                            }
                            else if(prixEtatLivre.QuantiteUsage == 0)
                            {

                                commandeEtudiant
                                    .StatutCommande = StatutCommandeEnum.MANQUE_INVENTAIRE;
                            }
                            else if
                            (
                                prixEtatLivre.QuantiteUsage < commandeEtudiant.Quantite
                            )
                            {

                                commandeEtudiant
                                    .StatutCommande = StatutCommandeEnum
                                        .QUANTITEE_CORRIGE_SELON_DISPONIBILITE;
                                commandeEtudiant.Quantite = prixEtatLivre.QuantiteUsage;

                                prixEtatLivreDAO
                                    .SoustraireDuStock
                                    (
                                        prixEtatLivre,
                                        commandeEtudiant.Quantite
                                    );
                            }
                        }
                        else
                        {

                            commandeEtudiant.StatutCommande = StatutCommandeEnum.CORRECT;
                            commandeEtudiant.Quantite = livreDesire.Quantite;
                        }
                    }
                    else
                    {

                        commandeEtudiant.StatutCommande = StatutCommandeEnum.INEXISTANT;
                    }

                    commandesEtudiants.Add(commandeEtudiant);
                }
            }
            else
            {

                foreach(LivreDesire livreDesire in livresDesires)
                {

                    commandeEtudiant = new()
                    {
                        StatutCommande = StatutCommandeEnum.INEXISTANT
                    };

                    commandesEtudiants.Add(commandeEtudiant);
                }
            }

            return commandesEtudiants;
        }

        /// <summary>
        /// Valide si au moins une commande est valide dans une liste de commandes.
        /// </summary>
        /// <param name="commandesEtudiants">Commandes étudiants à valider.</param>
        /// <returns>
        /// true si au moins une commande est valide ou false si aucune des commandes est valide.
        /// </returns>
        public bool CommandesValides(List<CommandeEtudiant> commandesEtudiants)
        {

            int countCommandesEtudiants;
            int countCommandesEtudiantsInvalides;

            countCommandesEtudiants = commandesEtudiants.Count();

            countCommandesEtudiantsInvalides = commandesEtudiants
                .Where
                (
                    commandeEtudiant => 
                        commandeEtudiant.StatutCommande == StatutCommandeEnum.INEXISTANT ||
                        commandeEtudiant.StatutCommande == StatutCommandeEnum.MANQUE_INVENTAIRE
                )
                .Count();

            if(countCommandesEtudiants == countCommandesEtudiantsInvalides)
            {

                return false;
            }

            return true;
        }

        /// <summary>
        /// Crée une liste de <c>CommandePartielleVM</c> affin de les afficher dans une
        /// vue.
        /// </summary>
        /// <param name"commandesEtudiantes">
        /// Commandes contenant les informations désirés
        /// </param>
        /// <returns>Une liste de <c>CommandePartielleVM</c>.</returns>
        public List<CommandePartielle> GetCommandesPartiellesFromCommandes
        (
            List<CommandeEtudiant> commandesEtudiantes
        )
        {

            List<CommandePartielle> commandesPartielles;
            CommandePartielle commandePartielle;

            commandesPartielles = new();

            foreach(CommandeEtudiant commandeEtudiant in commandesEtudiantes)
            {
                
                commandePartielle = new()
                {
                    Isbn = commandeEtudiant.Isbn,
                    Titre = commandeEtudiant.Titre,
                    EtatLivre = commandeEtudiant.EtatLivre,
                    Prix = commandeEtudiant.PrixUnitaireGele,
                    Quantite = commandeEtudiant.Quantite,
                    StatutCommande = commandeEtudiant.StatutCommande
                };

                commandesPartielles.Add(commandePartielle);
            }

            return commandesPartielles;
        }

        /// <summary>
        /// Calcule le total d'une liste de <c>CommandePartielle</c>
        /// </summary>
        /// <param name="commandesPartielles">Liste de <c>CommandePartielle</c></param>
        /// <returns>Le total des <c></c></returns>
        public static double GetTotalCommandesPartielles
        (
            List<CommandePartielle> commandesPartielles
        )
        {

            double totalFacture;

            totalFacture = 0.0;

            foreach(CommandePartielle commandePartielle in commandesPartielles)
            {

                totalFacture += commandePartielle.Prix * commandePartielle.Quantite;
            }

            return totalFacture;
        }
    }
}
