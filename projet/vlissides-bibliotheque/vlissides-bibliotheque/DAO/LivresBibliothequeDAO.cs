using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.DAO.Interface;
using System.Collections.Generic;

namespace vlissides_bibliotheque.DAO
{
    public class LivresBibliothequeDAO : IDAO<LivreBibliotheque>
    {

        private ApplicationDbContext _context;

        public LivresBibliothequeDAO(ApplicationDbContext context)
        {

            _context = context;
        }

        public LivreBibliotheque GetById(int id)
        {
            return _context.LivresBibliotheque
                .Include(l => l.Auteurs.Where(al=> al.LivreBibliothequeId == id)).ThenInclude(al=> al.Auteur)
                .Include(l => l.Cours.Where(al=> al.LivreBibliothequeId == id)).ThenInclude(cl=> cl.Cours.ProgrammeEtude)
                .Include(l => l.MaisonEdition)
                .SingleOrDefault(livre => livre.LivreId == id);
        }

        public IEnumerable<LivreBibliotheque> GetAll()
        {
            return _context.LivresBibliotheque.Include(l => l.MaisonEdition);
        }

        public bool Insert(LivreBibliotheque livre)
        {
            _context.LivresBibliotheque.Add(livre);
            return true;
        }

        public LivreBibliotheque Update(LivreBibliotheque livre)
        {
            _context.LivresBibliotheque.Update(livre);
            return null;
        }

        public bool Delete(int id)
        {
            _context.Remove(GetById(id));
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
