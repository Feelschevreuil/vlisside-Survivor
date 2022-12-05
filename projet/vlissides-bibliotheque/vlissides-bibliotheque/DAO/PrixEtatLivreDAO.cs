using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Models.Achat;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Extentions;
using vlissides_bibliotheque.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        /// Cherche les <c>PrixEtatLivre</c> selon la liste d'id's de 
        /// <c>LivreBibliotheque</c> et l'état désiré.
        /// </summary>
        /// <param name="livresDesires">Les livres désirés par le client.</param>
        /// <param name="etatLivre">L'état du livre désiré.</param>
        /// <returns><returns>
        public List<PrixEtatLivre> GetBulkByLivreDesireEtEtat
        (
            List<LivreDesire> livresDesires,
            EtatLivreEnum etatLivre
        )
        {

            List<PrixEtatLivre> prixEtatsLivres;

            prixEtatsLivres = new();

            foreach(LivreDesire livreDesire in livresDesires)
            {

                PrixEtatLivre prixEtatLivre;

                prixEtatLivre = _context
                    .PrixEtatsLivres
                    .Include(prixEtatLivre => prixEtatLivre.LivreBibliotheque)
                    .Where
                    (
                        prixEtatLivre =>
                            prixEtatLivre.LivreBibliothequeId == livreDesire.LivreId &&
                            prixEtatLivre.EtatLivre == etatLivre
                    )
                    .FirstOrDefault();

                if(prixEtatLivre != null)
                {

                    prixEtatsLivres.Add(prixEtatLivre);
                }
            }

            return prixEtatsLivres;
        }

        /// <summary>
        /// Cherche prixEtatLivre correspondant avec l'id.
        /// </summary>
        /// <param name="id">L'id de prixEtatLivre à chercher.</param>
        /// <returns>PrixEtatLivre correspondant à prixEtatLivre. ou NULL s'il n'y en a plus.</returns>
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
        /// Cherche prixEtatLivre correspondant avec l'id.
        /// </summary>
        /// <param name="id">L'id de prixEtatLivre à chercher.</param>
        /// <param name="quantiteUsageMinimum">
        /// Quantité usagée de livres minimum à avoir.
        /// </param>
        /// <returns>PrixEtatLivre correspondant à prixEtatLivre. ou NULL s'il n'y en a plus.</returns>
        public PrixEtatLivre Get(long id, int quantiteUsageMinimum)
        {

            PrixEtatLivre prixEtatLivre;

            prixEtatLivre = _context
                .PrixEtatsLivres
                    .Where
                    (
                        prixEtatLivre =>
                            prixEtatLivre.PrixEtatLivreId == id &&
                            prixEtatLivre.EtatLivre == EtatLivreEnum.USAGE ?
                                prixEtatLivre.QuantiteUsage >= quantiteUsageMinimum : 
                                true
                    )
                    .FirstOrDefault();

            return prixEtatLivre;
        }

        /// <summary>
        /// Va chercher les <c>PrixEtatLivre</c>s correspondants aux id's donnés
        /// en paramètres.
        /// </summary>
        /// <param name="prixEtatsLivresIds">
        /// Liste d'ids de <c>PrixEtatLivre</c>`à aller chercher.
        /// </param>
        /// <param name="quantiteUsageMinimum">
        /// Quantité usagée de livres minimum à avoir.
        /// </param>
        /// <returns></returns>
        public IEnumerable<PrixEtatLivre> GetBulk
        (
            List<int> prixEtatsLivresIds,
            int quantiteUsageMinimum = 1
        )
        {

            IEnumerable<PrixEtatLivre> prixEtatsLivres;

            prixEtatsLivres = _context
                .PrixEtatsLivres
                    .Where
                    (
                        prixEtatLivre =>
                            prixEtatsLivresIds.Contains(prixEtatLivre.PrixEtatLivreId) &&
                                prixEtatLivre.EtatLivre == EtatLivreEnum.USAGE ? 
                                    prixEtatLivre.QuantiteUsage >= quantiteUsageMinimum : 
                                    true
                    );

            return prixEtatsLivres;
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

        /// <summary>
        /// Enlève la quantité désirée à un prix état livre usagé.
        /// </summary>
        /// <param name="prixEtatLivre">PrixEtatLivre usagé à soustraire la quantité désirée.</param>
        /// <param name="quantite">Quantité en inventaire à soustraire.</param>
        public bool SoustraireDuStock(PrixEtatLivre prixEtatLivre, int quantite = 1)
        {

            if(prixEtatLivre != null && quantite > 0 && prixEtatLivre.QuantiteUsage >= quantite)
            {

                prixEtatLivre.QuantiteUsage -= quantite;
                
                _context.PrixEtatsLivres.Update(prixEtatLivre);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Ajoute la quantité désirée à un prix état livre usagé.
        /// </summary>
        /// <param name="prixEtatLivre">PrixEtatLivre usagé à ajouter la quantité désirée.</param>
        /// <param name="quantite">Quantité en inventaire à ajouter.</param>
        public bool AjouterDuStock(PrixEtatLivre prixEtatLivre, int quantite = 1)
        {

            if(prixEtatLivre != null && quantite > 0)
            {

                prixEtatLivre.QuantiteUsage += quantite;
                
                _context.PrixEtatsLivres.Update(prixEtatLivre);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Ajoute la quantité désirée à un prix état livre usagé.
        /// </summary>
        /// <param name="prixEtatLivreId">PrixEtatLivre usagé à ajouter la quantité désiré.</param>
        /// <param name="quantite">Quantité en inventaire à ajouter.</param>
        public bool AjouterDuStock(long prixEtatLivreId, int quantite = 1)
        {

            PrixEtatLivre prixEtatLivreAjouterDuStock;

            prixEtatLivreAjouterDuStock = Get(prixEtatLivreId);

            if(prixEtatLivreAjouterDuStock != null && quantite >= 1)
            {

                prixEtatLivreAjouterDuStock.QuantiteUsage += quantite;

                //TODO: use Update() when implemented.
                _context.PrixEtatsLivres.Update(prixEtatLivreAjouterDuStock);
                _context.SaveChanges();

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
