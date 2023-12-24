using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class CommanditaireDAO : IDAO<Commanditaire>
    {
        private readonly ApplicationDbContext _context;

        public CommanditaireDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            _context.Commanditaires.Remove(GetById(id));
            return true;
        }

        public IEnumerable<Commanditaire> GetAll()
        {
           return _context.Commanditaires;
        }

        public Commanditaire GetById(int id)
        {
            return _context.Commanditaires.SingleOrDefault(c=> c.CommanditaireId == id);
        }

        public bool Insert(Commanditaire c)
        {
            _context.Commanditaires.Add(c);
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }

        public Commanditaire Update(Commanditaire c)
        {
            _context.Commanditaires.Update(c);
            return null;
        }
    }
}
