using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class AuteurDAO : IDAO<Auteur>
    {
        private readonly ApplicationDbContext _context;
        public AuteurDAO(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            _context.Remove(GetById(id));
            return true;
        }

        public IEnumerable<Auteur> GetAll()
        {
           return _context.Auteurs;
        }

        public Auteur GetById(int id)
        {
            return _context.Auteurs.Single(a=>a.AuteurId == id);
        }

        public bool Insert(Auteur auteur)
        {
            _context.Auteurs.Add(auteur);
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }

        public Auteur Update(Auteur auteur)
        {
            _context.Update(auteur);
            return null;
        }
    }
}
