using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class CoursDAO : IDAO<Cours>
    {
        private readonly ApplicationDbContext _context;
        public CoursDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            _context.Cours.Remove(GetById(id));
            return true;
        }

        public IEnumerable<Cours> GetAll()
        {
            return _context.Cours.Include(c=>c.ProgrammeEtude);
        }

        public Cours GetById(int id)
        {
            return _context.Cours.Include(c=>c.ProgrammeEtude).SingleOrDefault(c => c.CoursId == id);
        }

        public bool Insert(Cours cours)
        {
            _context.Cours.Add(cours);
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }

        public Cours Update(Cours cours)
        {
            _context.Update(cours);
            return null;
        }
    }
}
