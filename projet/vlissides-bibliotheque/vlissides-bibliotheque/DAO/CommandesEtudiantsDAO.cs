using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services;

namespace vlissides_bibliotheque.DAO
{

    public class CommandesEtudiantsDAO: IDAO<CommandeEtudiant>, IDAOTableLiason<CommandeEtudiant>
    {

        private ApplicationDbContext _context;

        public CommandesEtudiantsDAO(ApplicationDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Cherche les commandeEtudiants correspondants à l'id de la factureEtudiante désirée.
        /// </summary>
        public IEnumerable<CommandeEtudiant> GetSelonPremierId(long factureEtudiantId)
        {

            IEnumerable<CommandeEtudiant> commandesEtudiants;
            
            commandesEtudiants = _context.CommandesEtudiants
                .Where
                (
                    commandeEtudiant => 
                        commandeEtudiant.FactureEtudiantId == factureEtudiantId
                );

            return commandesEtudiants;
        }

        /// <summary>
        /// Cherche les commandeEtudiants correspondants à l'id du prix état livre désiré.
        /// </summary>
        public IEnumerable<CommandeEtudiant> GetSelonDeuxiemeId(long prixEtatLivreId)
        {

            IEnumerable<CommandeEtudiant> commandesEtudiants;
            
            commandesEtudiants = _context.CommandesEtudiants
                .Where
                (
                    commandeEtudiant => 
                        commandeEtudiant.PrixEtatLivreId == prixEtatLivreId
                );

            return commandesEtudiants;
        }

        /// <summary>
        /// Cherche commandeEtudiants correspondants aux id's.
        /// </summary>
        /// <param name="factureEtudiantId">Id de la facture étudiant.</param>
        /// <param name="prixEtatLivreId">Id du prix état livre.</param>
        /// <returns>commandeEtudiant correspondant aux id's.</returns>
        public CommandeEtudiant Get(long factureEtudiantId, long prixEtatLivreId)
        {

            CommandeEtudiant commandeEtudiant;

            commandeEtudiant = _context
                .CommandesEtudiants
                    .Where
                    (
                        commandeEtudiant =>
                            commandeEtudiant.FactureEtudiantId == factureEtudiantId
                            &&
                            commandeEtudiant.PrixEtatLivreId == prixEtatLivreId
                    )
                    .FirstOrDefault();
                    
            return commandeEtudiant;
        }

        /// <summary>
        /// Cherche tous les commandeEtudiants.
        /// </summary>
        /// <returns>Les commandeEtudiants en liste.</returns>
        public IEnumerable<CommandeEtudiant> GetAll()
        {

            IEnumerable<CommandeEtudiant> commandesEtudiants;

            commandesEtudiants = _context.CommandesEtudiants;

            return commandesEtudiants;
        }

        /// <summary>
        /// Sauvegarde commandeEtudiant désiré.
        /// </summary>
        /// <param name="commandeEtudiant">commandeEtudiant à sauvegarder.</param>
        /// <returns>true si commandeEtudiant a été sauvegardé avec succès.</returns>
        public bool Save(CommandeEtudiant commandeEtudiant)
        {

            _context.CommandesEtudiants.Add(commandeEtudiant);
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Met à jour commandeEtudiant désiré.
        /// </summary>
        /// <param name="factureEtudiantId">Id de la facture étudiant.</param>
        /// <param name="prixEtatLivreId">Id du prix état livre.</param>
        /// <param name="commandeEtudiantAJour">commandeEtudiant contenant les modifications.</param>
        /// <returns>true si commandeEtudiant a été sauvegardé avec succès.</returns>
        public CommandeEtudiant Update
        (
            long factureEtudiantId, 
            long prixEtatLivreId, 
            CommandeEtudiant commandeEtudiantAJour
        )
        {

            CommandeEtudiant commandeEtudiantOriginale;

            commandeEtudiantOriginale = Get(factureEtudiantId, prixEtatLivreId);

            CommandeEtudiantService
                .MettreAJourProprietes(commandeEtudiantOriginale, commandeEtudiantAJour);

            _context.CommandesEtudiants.Update(commandeEtudiantOriginale);
            _context.SaveChanges();

            return commandeEtudiantOriginale;
        }

        /// <summary>
        /// Efface commandeEtudiant désiré.
        /// </summary>
        /// <param name="id">L'id de commandeEtudiant à effacer.</param>
        /// <returns>true si commandeEtudiant a été effacé avec succès.</returns>
        public bool Delete(long factureEtudiantId, long prixEtatLivreId)
        {

            CommandeEtudiant commandeEtudiantEffacer;

            commandeEtudiantEffacer = Get(factureEtudiantId, prixEtatLivreId);

            if(commandeEtudiantEffacer != null)
            {

                _context.CommandesEtudiants.Remove(commandeEtudiantEffacer);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
