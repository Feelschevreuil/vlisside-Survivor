using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services;

namespace vlissides_bibliotheque.DAO
{

    /// <summary>
    /// Clsase <c>PrixEtatLivreDAO</c> qui implémente l'interface DAO et DAO à clé unique.
    /// </summary>
    public class PrixEtatLivreDAO : IDAO<PrixEtatLivre>, IDAOCleUnique<PrixEtatLivre>
    {

        private ApplicationDbContext _context;

        public PrixEtatLivreDAO(ApplicationDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Cherche prixEtatLivre correspondant avec l'id.
        /// </summary>
        /// <param name="id">L'id de prixEtatLivre à chercher.</param>
        /// <returns>PrixEtatLivre correspondant à prixEtatLivre.</returns>
        public PrixEtatLivre Get(long id)
        {

            PrixEtatLivre prixEtatLivre;

            prixEtatLivre = _context
                .PrixEtatsLivres
                    .Where
                    (
                        prixEtatLivre =>
                            prixEtatLivre.PrixEtatLivreId == id
                    )
                    .FirstOrDefault();

            return prixEtatLivre;
        }

        /// <summary>
        /// Cherche tous les prixEtatLivres.
        /// </summary>
        /// <returns>Les prixEtatLivres en liste.</returns>
        public IEnumerable<PrixEtatLivre> GetAll()
        {

            IEnumerable<PrixEtatLivre> prixEtatsLivres;

            prixEtatsLivres = _context.PrixEtatsLivres;

            return prixEtatsLivres;
        }

        /// <summary>
        /// Sauvegarde prixEtatLivre désiré.
        /// </summary>
        /// <param name="t">PrixEtatLivre à sauvegarder.</param>
        /// <returns>PrixEtatLivre modifié.</returns>
        public bool Save(PrixEtatLivre prixEtatLivre)
        {

            _context.PrixEtatsLivres.Add(prixEtatLivre);
            _context.SaveChanges();

            return true;
        }

        // TODO:
        /// <summary>
        /// Met à jour prixEtatLivre désiré.
        /// </summary>
        /// <param name="idPrixEtatLivreOriginal">PrixEtatLivre contenant les propriétés originales</param>
        /// <param name="prixEtatLivreAJour">PrixEtatLivre contenant les modifications.</param>
        /// <returns>true si prixEtatLivre a été sauvegardé avec succès.</returns>
        public PrixEtatLivre Update(long idPrixEtatLivreOriginal, PrixEtatLivre prixEtatLivreAJour)
        {

            return null;
        }

        // TODO: commenter! --> DANS SERVICE!
        public bool SoustraireDuStock(PrixEtatLivre prixEtatLivre, int quantite = 1)
        {

            if(prixEtatLivre.QuantiteUsage > 0)
            {

                prixEtatLivre.QuantiteUsage--;
                
                Update(prixEtatLivre.PrixEtatLivreId, prixEtatLivre);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Efface prixEtatLivre désiré.
        /// </summary>
        /// <param name="id">L'id de prixEtatLivre à effacer.</param>
        /// <returns>true si prixEtatLivre a été effacé avec succès.</returns>
        public bool Delete(long id)
        {

            PrixEtatLivre prixEtatLivreAEffacer;

            prixEtatLivreAEffacer = Get(id);

            if(prixEtatLivreAEffacer != null)
            {

                _context.PrixEtatsLivres.Remove(prixEtatLivreAEffacer);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
