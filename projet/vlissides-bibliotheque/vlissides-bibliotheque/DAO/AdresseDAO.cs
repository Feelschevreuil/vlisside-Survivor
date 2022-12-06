using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services;

namespace vlissides_bibliotheque.DAO
{

    /// <summary>
    /// Clsase <c>AdresseDAO</c> qui implémente l'interface DAO.
    /// </summary>
    public class AdresseDAO: IDAO<Adresse>, IDAOCleUnique<Adresse>
    {

        private ApplicationDbContext _context;

        public AdresseDAO(ApplicationDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Cherche tous les adresses.
        /// </summary>
        /// <returns>Les adresses en liste.</returns>
        public IEnumerable<Adresse> GetAll()
        {

            IEnumerable<Adresse> adresses;

            adresses = _context.Adresses;

            return adresses;
        }

        /// <summary>
        /// Sauvegarde l'adresse désiré.
        /// </summary>
        /// <param name="t">L'adresse à sauvegarder.</param>
        /// <returns>L'adresse modifié.</returns>
        public bool Save(Adresse adresse)
        {

            _context.Adresses.Add(adresse);
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Cherche l'adresse correspondant avec l'id.
        /// </summary>
        /// <param name="id">L'id de l'adresse à chercher.</param>
        /// <returns>L'object correspondant à l'adresse.</returns>
        public Adresse Get(long id)
        {

            Adresse adresse;

            adresse = _context
                .Adresses
                    .Where
                    (
                        adresse =>
                            adresse.AdresseId == id
                    )
                    .FirstOrDefault();

            return adresse;
        }

        /// <summary>
        /// Met à jour l'adresse désiré.
        /// </summary>
        /// <param name="idAdresseOriginale">L'adresse contenant les propriétés originales</param>
        /// <param name="adresseAJour">L'adresse contenant les modifications.</param>
        /// <returns>true si l'adresse a été sauvegardé avec succès.</returns>
        public Adresse Update(long idAdresseOriginale, Adresse adresseAJour)
        {

            Adresse adresseOriginale;

            adresseOriginale = Get(idAdresseOriginale);

            adresseOriginale = AdresseService
                .MettreAJourProprietes(adresseOriginale, adresseAJour);

            _context.Adresses.Update(adresseOriginale);
            _context.SaveChanges();

            return adresseOriginale;
        }

        /// <summary>
        /// Efface l'adresse désiré.
        /// </summary>
        /// <param name="id">L'id de l'adresse à effacer.</param>
        /// <returns>true si l'adresse a été effacé avec succès.</returns>
        public bool Delete(long id)
        {

            Adresse adresseEffacer;

            adresseEffacer = Get(id);

            if(adresseEffacer != null)
            {

                _context.Adresses.Remove(adresseEffacer);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
