using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services;

namespace vlissides_bibliotheque.DAO
{

    /// <summary>
    /// Clsase <c>AdresseDAO</c> qui implémente l'interface DAO.
    /// </summary>
    public class AdresseDAO : IDAO<Adresse>
    {

        private ApplicationDbContext _context;

        public AdresseDAO(ApplicationDbContext context)
        {

            _context = context;
        }

        public Adresse GetById(int id)
        {
            return _context.Adresses.SingleOrDefault(a =>a.AdresseId == id);
        }

        public IEnumerable<Adresse> GetAll()
        {

            IEnumerable<Adresse> adresses;

            adresses = _context.Adresses;

            return adresses;
        }

        public bool Insert(Adresse adresse)
        {
            _context.Adresses.Add(adresse);
            Save();
            return true;
        }

        public Adresse Update(Adresse adresseAJour)
        {

            Adresse adresseOriginale = GetById(adresseAJour.AdresseId);

            adresseOriginale = AdresseService
                .MettreAJourProprietes(adresseOriginale, adresseAJour);

            _context.Adresses.Update(adresseOriginale);
            Save();

            return adresseOriginale;
        }

        public bool Delete(int id)
        {
            Adresse adresseEffacer = GetById(id);

            if (adresseEffacer != null)
            {

                _context.Adresses.Remove(adresseEffacer);
                Save();

                return true;
            }

            return false;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }
    }
}
