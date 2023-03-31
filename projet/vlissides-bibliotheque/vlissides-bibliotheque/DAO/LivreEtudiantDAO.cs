using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class LivreEtudiantDAO : IDAO<LivreEtudiant>
    {
        private ApplicationDbContext _context;

        public LivreEtudiantDAO(ApplicationDbContext context) 
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            _context.LivresEtudiants.Remove(GetById(id));
            Save();
            return true;
        }

        public IEnumerable<LivreEtudiant> GetAll()
        {
            return _context.LivresEtudiants.Include(l=> l.Etudiant);
        }

        public LivreEtudiant GetById(int id)
        {
            return _context.LivresEtudiants.Include(l => l.Etudiant).SingleOrDefault(le=> le.LivreId == id);
        }

        public bool Insert(LivreEtudiant t)
        {
            _context.LivresEtudiants.Add(t); 
            Save(); 
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }

        public LivreEtudiant Update(LivreEtudiant t)
        {
            _context.LivresEtudiants.Update(t);
            return null;
        }
    }
}
