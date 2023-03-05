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

            Adresse adresse;

            adresse = _context
                .Adresses
                    .Where
                    (
                        adresse =>
                            adresse.AdresseId == id
                    )
                    .SingleOrDefault();

            return adresse;
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
            _context.SaveChanges();

            return adresseOriginale;
        }

        public bool Delete(int id)
        {

            Adresse adresseEffacer;

            adresseEffacer = GetById(id);

            if (adresseEffacer != null)
            {

                _context.Adresses.Remove(adresseEffacer);
                _context.SaveChanges();

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
