using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.DAO.Interface;
using System.Collections.Generic;
using System.Linq;

namespace vlissides_bibliotheque.DAO
{
    public class EvenementDAO : IDAO<Evenement>
    {

        private ApplicationDbContext _context;

        public EvenementDAO(ApplicationDbContext context)
        {

            _context = context;
        }

        public Evenement GetById(int id)
        {
           return _context.Evenements.Include(e=> e.Commanditaire).SingleOrDefault(evenement => evenement.EvenementId == id);
        }

        public IEnumerable<Evenement> GetAll()
        {
            return _context.Evenements.Include(e => e.Commanditaire);
        }

        public bool Insert(Evenement evenement)
        {
            _context.Evenements.Add(evenement);
            Save();
            return true;
        }

        public Evenement Update(Evenement evenement)
        {
            _context.Evenements.Update(evenement);
            return null;
        }

        public bool Delete(int id)
        {

            _context.Evenements.Remove(GetById(id));
            Save();
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }
    }
}
