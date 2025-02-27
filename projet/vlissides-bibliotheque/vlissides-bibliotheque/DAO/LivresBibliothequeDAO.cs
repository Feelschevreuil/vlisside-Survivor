using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.DAO
{
    public class LivresBibliothequeDAO : IDAO<LivreBibliotheque>, ILivreTest
    {

        private ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public LivresBibliothequeDAO(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public LivreBibliotheque GetById(int id)
        {
                return _context.LivresBibliotheque
                    .Include(l => l.Auteurs)
                    .Include(l => l.Cours).ThenInclude(cl => cl.ProgrammeEtude)
                    .Include(l => l.MaisonEdition)
                    .SingleOrDefault(livre => livre.LivreId == id);
        }

        public IEnumerable<LivreBibliotheque> GetAll()
        {
            return _context.LivresBibliotheque.Include(l => l.MaisonEdition)
                                            .Include(l => l.Auteurs)
                                            .Include(l => l.Cours).ThenInclude(cl => cl.ProgrammeEtude)
                                            .Include(l => l.MaisonEdition);
        }

        public bool Insert(LivreBibliotheque livre)
        {
            _context.LivresBibliotheque.Add(livre);
            return true;
        }

        public LivreBibliotheque MiseAJour(AjoutEditLivreVM form)
        {
            var livre = _context.LivresBibliotheque
                    .Include(l => l.Auteurs)
                    .Include(l => l.Cours).ThenInclude(cl => cl.ProgrammeEtude)
                    .Include(l => l.MaisonEdition)
                    .SingleOrDefault(livre => livre.LivreId == form.Id);
            _mapper.Map(form, livre);

            livre = _context.LivresBibliotheque.Update(livre).Entity;
            _context.SaveChanges();
            return livre;
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

        public LivreBibliotheque Update(LivreBibliotheque t)
        {
            throw new System.NotImplementedException();
        }
    }
}
