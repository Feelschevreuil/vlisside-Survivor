using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class ProfesseurDAO : IDAO<Professeur>
    {
        private readonly ApplicationDbContext _context;
        public ProfesseurDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
           _context.Professeurs.Remove(GetById(id));
            return true;
        }

        public IEnumerable<Professeur> GetAll()
        {
            return _context.Professeurs;
        }

        public Professeur GetById(int id)
        {
            return _context.Professeurs.SingleOrDefault(p=> p.ProfesseurId == id);
        }

        public bool Insert(Professeur p)
        {
            _context.Professeurs.Add(p);
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }

        public Professeur Update(Professeur p)
        {
            _context.Professeurs.Update(p);
            return null;
        }
    }
}
