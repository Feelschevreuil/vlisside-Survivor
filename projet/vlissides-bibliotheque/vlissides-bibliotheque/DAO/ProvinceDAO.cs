using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class ProvinceDAO : IDAO<Province>
    {
        private readonly ApplicationDbContext _context;

        public ProvinceDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            _context.Provinces.Remove(GetById(id));
            return true;
        }

        public IEnumerable<Province> GetAll()
        {
            return _context.Provinces;
        }

        public Province GetById(int id)
        {
            return _context.Provinces.SingleOrDefault(p=> p.ProvinceId == id);
        }

        public bool Insert(Province p)
        {
            _context.Provinces.Add(p);
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }

        public Province Update(Province p)
        {
            _context.Provinces.Update(p);
            return null;
        }
    }
}
