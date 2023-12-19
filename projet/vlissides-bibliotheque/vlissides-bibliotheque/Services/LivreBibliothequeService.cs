using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public TuileLivreBibliotequeVM GetTuileLivreBibliotequeVMs(int livreBibliothequeId)
        {
            LivreBibliotheque livreBibliotheque = _livreDAO.GetById(livreBibliothequeId);
            LivreBibliothequeDto livre = _mapper.Map<LivreBibliothequeDto>(livreBibliotheque);

            TuileLivreBibliotequeVM tuileVM = new()
            {
                LivreBibliotheque = livre,
                Auteurs = livreBibliotheque.Auteurs.Select(a=> a.Auteur.NomComplet).ToList(),
                ProgrammeEtudeNom = livreBibliotheque.Cours.First().Cours.Nom,
                Quantite = livre.Prix.QuantiteUsage
            };


            if (string.IsNullOrEmpty(tuileVM.LivreBibliotheque.PhotoCouverture))
                tuileVM.LivreBibliotheque.PhotoCouverture = GetImageParDefaut();

            return  tuileVM;
        }

        public List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeInventaire()
        {
            List<TuileLivreBibliotequeVM> tuileInventaire = new();

            foreach (int livre in _livreDAO.GetAll().Select(l => l.LivreId))
            {
                tuileInventaire.Add(GetTuileLivreBibliotequeVMs(livre));
            }

            return tuileInventaire;
        }

        public List<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeAccueil()
        {
            List<TuileLivreBibliotequeVM> tuileLivreBiblioteques = new();

            List<int> list = _context.LivresBibliotheque
               .Take(4)
               .Select(l => l.LivreId)
               .ToList();

            foreach (int livre in list)
            {
                tuileLivreBiblioteques.Add(GetTuileLivreBibliotequeVMs(livre));
            }
            return tuileLivreBiblioteques;
        }

        public LivreDetailVM GetLivreDetailVM(int livreBibliothequeId)
        {
            LivreBibliotheque livreBibliotheque = _livreDAO.GetById(livreBibliothequeId);

            LivreDetailVM tuileVM = new()
            {
                LivreBibliotheque = _mapper.Map<LivreBibliothequeDto>(livreBibliotheque),
                Auteurs = livreBibliotheque.Auteurs.Select(a=> a.Auteur.NomComplet).ToList(),
                ProgrammeEtudeNom = livreBibliotheque.Cours.FirstOrDefault()?.Cours.ProgrammeEtude.Nom
            };

            if (string.IsNullOrEmpty(tuileVM.LivreBibliotheque.PhotoCouverture))
                tuileVM.LivreBibliotheque.PhotoCouverture = GetImageParDefaut();

            return tuileVM;
        }

        public string GetImageParDefaut()
        {
            return "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-livre.jpg";
        }
    }
}
