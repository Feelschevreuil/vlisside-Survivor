using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Models.Achat;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Extentions;
using vlissides_bibliotheque.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;

namespace vlissides_bibliotheque.DAO
{

    /// <summary>
    /// Clsase <c>PrixEtatLivreDAO</c> qui implémente l'interface DAO et DAO à clé unique.
    /// </summary>
    public class PrixEtatLivreDAO : IDAO<PrixEtatLivre>
    {

        private ApplicationDbContext _context;

        public PrixEtatLivreDAO(ApplicationDbContext context)
        {

            _context = context;
        }

        public PrixEtatLivre GetById(int id)
        {
            PrixEtatLivre prixEtatLivre = _context.PrixEtatsLivres.Single(prixEtatLivre => prixEtatLivre.PrixEtatLivreId == id);
            return prixEtatLivre;
        }

        public IEnumerable<PrixEtatLivre> GetAll()
        {

            IEnumerable<PrixEtatLivre> prixEtatsLivres = _context.PrixEtatsLivres;

            return prixEtatsLivres;
        }

        public bool Insert(PrixEtatLivre prixEtatLivre)
        {
            _context.PrixEtatsLivres.Add(prixEtatLivre);
            Save();
            return true;
        }

        public PrixEtatLivre Update(PrixEtatLivre prixEtatLivreAJour)
        {
            _context.PrixEtatsLivres.Update(prixEtatLivreAJour);
            return null;
        }

        /// <summary>
        /// Efface prixEtatLivre désiré.
        /// </summary>
        /// <param name="id">L'id de prixEtatLivre à effacer.</param>
        /// <returns>true si prixEtatLivre a été effacé avec succès.</returns>
        public bool Delete(int id)
        {

            _context.PrixEtatsLivres.Remove(GetById(id));
            Save();
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }
    }
}
