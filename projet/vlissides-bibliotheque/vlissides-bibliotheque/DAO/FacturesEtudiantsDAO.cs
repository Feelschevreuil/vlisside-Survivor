using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services;
using Microsoft.EntityFrameworkCore;

namespace vlissides_bibliotheque.DAO
{

    /// <summary>
    /// Clsase <c>FacturesEtudiantsDAO</c> qui implémente l'interface DAO.
    /// </summary>
    public class FacturesEtudiantsDAO : IDAO<FactureEtudiant>, IDAOCleUnique<FactureEtudiant>
    {

        private ApplicationDbContext _context;

        public FacturesEtudiantsDAO(ApplicationDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Cherche l'objet correspondant avec l'id.
        /// </summary>
        /// <param name="id">L'id de l'objet à chercher.</param>
        /// <returns>L'object correspondant à l'objet.</returns>
        public FactureEtudiant Get(long id)
        {

            FactureEtudiant factureEtudiant;

            factureEtudiant = _context
                .FacturesEtudiants
                    .Where
                    (
                        factureEtudiant => 
                            factureEtudiant.FactureEtudiantId == id
                    )
                    .Include
                    (
                        factureEtudiant => factureEtudiant.AdresseLivraison
                    )
                    .FirstOrDefault();

            return factureEtudiant;
        }

        /// <summary>
        /// Cherche tous les objets.
        /// </summary>
        /// <returns>Les objets en liste.</returns>
        public IEnumerable<FactureEtudiant> GetAll()
        {

            IEnumerable<FactureEtudiant> facturesEtudiants;

            facturesEtudiants = _context.FacturesEtudiants;

            return facturesEtudiants;
        }

        /// <summary>
        /// Sauvegarde l'objet désiré.
        /// </summary>
        /// <param name="factureEtudiant">L'objet à sauvegarder.</param>
        /// <returns>true si l'objet a été sauvegardé avec succès.</returns>
        public bool Save(FactureEtudiant factureEtudiant)
        {

            _context.FacturesEtudiants.Add(factureEtudiant);
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Met à jour l'objet désiré.
        /// </summary>
        /// <param name="idObjetOriginal">L'objet contenant les propriétés originales</param>
        /// <param name="objetAJour">L'objet contenant les modifications.</param>
        /// <returns>true si l'objet a été sauvegardé avec succès.</returns>
        public FactureEtudiant Update(long idObjetOriginal, FactureEtudiant objetAJour)
        {

            FactureEtudiant factureEtudiantOriginale;
            FactureEtudiantService factureEtudiantService;

            factureEtudiantOriginale = Get(idObjetOriginal);
            factureEtudiantService = new(_context);

            factureEtudiantService
                .MettreAJourProprietes(factureEtudiantOriginale, objetAJour);

            _context.FacturesEtudiants.Update(factureEtudiantOriginale);
            _context.SaveChanges();
            
            return factureEtudiantOriginale;
        }

        /// <summary>
        /// Efface l'objet désiré.
        /// </summary>
        /// <param name="id">L'id de l'objet à effacer.</param>
        /// <returns>true si l'objet a été effacé avec succès.</returns>
        public bool Delete(long id)
        {

            FactureEtudiant factureEtudiantEffacer;

            factureEtudiantEffacer = Get(id);

            if(factureEtudiantEffacer != null)
            {

                _context.FacturesEtudiants.Remove(factureEtudiantEffacer);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
