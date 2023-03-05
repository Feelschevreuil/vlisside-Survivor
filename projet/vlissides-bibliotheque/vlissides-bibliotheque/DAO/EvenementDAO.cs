using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Models.Achat;
using vlissides_bibliotheque.Services;
using vlissides_bibliotheque.Extentions;
using vlissides_bibliotheque.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;

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

            Evenement evenement = _context.Evenements.Single(evenement => evenement.EvenementId == id);
            return evenement;
        }

        public IEnumerable<Evenement> GetAll()
        {

            IEnumerable<Evenement> evenements = _context.Evenements;
            return evenements;
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
