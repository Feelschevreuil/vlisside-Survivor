using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class MaisonEditionDAO : IDAO<MaisonEdition>
    {
        private readonly ApplicationDbContext _context;
        public MaisonEditionDAO(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            _context.MaisonsEdition.Remove(GetById(id));
            return true;
        }

        public IEnumerable<MaisonEdition> GetAll()
        {
            return _context.MaisonsEdition;
        }

        public MaisonEdition GetById(int id)
        {
            return _context.MaisonsEdition.SingleOrDefault(m=> m.MaisonEditionId == id);
        }

        public bool Insert(MaisonEdition m)
        {
            _context.MaisonsEdition.Add(m);
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }

        public MaisonEdition Update(MaisonEdition m)
        {
            _context.MaisonsEdition.Update(m);
            return null;
        }
    }
}
