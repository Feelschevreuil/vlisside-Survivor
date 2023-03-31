using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class EtudiantDAO : IDAOEtudiant<Etudiant>
    {
        private readonly ApplicationDbContext _context;
        public EtudiantDAO(ApplicationDbContext context) 
        {
            _context= context;
        }
        public Etudiant GetById(string id)
        {
            return _context.Etudiants.SingleOrDefault(e=> e.Id == id);
        }

        public IEnumerable<Etudiant> GetAll()
        {
            return _context.Etudiants;
        }

        public bool Insert(Etudiant etudiant) 
        {
            _context.Etudiants.Add(etudiant);
            return true;
        }

        public Etudiant Update(Etudiant etudiant)
        {
            _context.Etudiants.Update(etudiant);
            return null;
        }

        public bool Delete(string id)
        {
            _context.Etudiants.Remove(GetById(id));
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }
    }
}
