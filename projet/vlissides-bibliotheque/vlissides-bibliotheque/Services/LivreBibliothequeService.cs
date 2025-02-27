using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{
    public class LivreBibliothequeService : ILivreBibliotheque
    {
        private readonly ApplicationDbContext _context;
        private readonly IDAO<LivreBibliotheque> _livreDAO;
        private readonly IMapper _mapper;

        public LivreBibliothequeService(ApplicationDbContext context, IDAO<LivreBibliotheque> livreDAO, IMapper mapper)
        {
            _context = context;
            _livreDAO = livreDAO;
            _mapper = mapper;
        }
        

        public List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeInventaire()
        {
            List<TuileLivreBibliotequeVM> tuileInventaire = new();

            foreach (var tuile in _mapper.Map<List<TuileLivreBibliotequeVM>>(_livreDAO.GetAll().ToList()))
                tuileInventaire.Add(tuile);
            
            return tuileInventaire;
        }

        public List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeAccueil()
        {
            List<TuileLivreBibliotequeVM> tuileLivreBiblioteques = new();

            foreach (var tuile in _mapper.Map<List<TuileLivreBibliotequeVM>>(_livreDAO.GetAll().Take(4).ToList()))
                tuileLivreBiblioteques.Add(tuile);
            

            return tuileLivreBiblioteques;
        }

        public LivreBibliothequeDto GetLivre(int id)
        {
            return _mapper.Map<LivreBibliothequeDto>(_livreDAO.GetById(id));
        }

        public TuileLivreBibliotequeVM GetTuileLivreBibliotequeVMs(int livreBibliothequeId)
        {
            return _mapper.Map<TuileLivreBibliotequeVM>(_livreDAO.GetById(livreBibliothequeId));
        }

        public LivreDetailVM GetLivreDetailVM(int livreBibliothequeId)
        {
            return _mapper.Map<LivreDetailVM>(_livreDAO.GetById(livreBibliothequeId));
        }
    }
}
