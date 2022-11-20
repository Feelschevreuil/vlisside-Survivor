using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Enums;

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
        /// Crée les commandes selon une liste d'ids de PrixEtatLivres.
        /// </summary>
        /// <param name="factureEtudiant">Facture à associer les commandes.</param>
        /// <param name="prixEtatLivresId">Liste des Id's PrixÉtatsLivres désirés.</param>
        public List<CommandeEtudiant> CreerCommandesSelonListeIdsPrixEtatLivre
        (
            FactureEtudiant factureEtudiant,
            List<int> prixEtatLivresId
        )
        {

            List<CommandeEtudiant> commandesEtudiant;
            PrixEtatLivreDAO prixEtatLivreDAO;
            PrixEtatLivre prixEtatLivre;
            CommandeEtudiant commandeEtudiant;

            prixEtatLivreDAO = new(_context);

            commandesEtudiant = new();

            foreach(int idPrixEtatLivre in prixEtatLivresId)
            {

                prixEtatLivre = prixEtatLivreDAO.Get(idPrixEtatLivre);

                if(prixEtatLivre != null)
                {

                    if(prixEtatLivre.EtatLivre == EtatLivreEnum.USAGE)
                    {

                        //TODO: something more effcient than re-finding the object
                        if(prixEtatLivreDAO.SoustraireDuStock(prixEtatLivre.PrixEtatLivreId))
                        {

                            commandeEtudiant = new()
                            {
                                FactureEtudiant = factureEtudiant,
                                PrixEtatLivre = prixEtatLivre,
                                Quantite = 1
                            };

                            commandesEtudiant.Add(commandeEtudiant);
                        }
                    }
                }
            }

            return commandesEtudiant;
        }
    }
}
