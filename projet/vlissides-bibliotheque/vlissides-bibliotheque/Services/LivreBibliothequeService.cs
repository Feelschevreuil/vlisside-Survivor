using AutoMapper;
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
        public async Task<TuileLivreBibliotequeVM> GetTuileLivreBibliotequeVMs(int livreBibliothequeId)
        {
            LivreBibliotheque livreBibliotheque = _livreDAO.GetById(livreBibliothequeId);
            LivreBibliothequeDto livre = _mapper.Map<LivreBibliothequeDto>(livreBibliotheque);

            TuileLivreBibliotequeVM tuileVM = new()
            {
                livreBibliotheque = livre,
                auteurs = livreBibliotheque.Auteurs.Select(a=> a.Auteur.NomComplet).ToList(),
                programmeEtudeNom = livreBibliotheque.Cours.First().Cours.Nom,
                Quantite = livre.prix.QuantiteUsage
            };


            if (string.IsNullOrEmpty(tuileVM.livreBibliotheque.PhotoCouverture))
                tuileVM.livreBibliotheque.PhotoCouverture = GetImageParDefaut();

            return  tuileVM;
        }

        public async Task<List<TuileLivreBibliotequeVM>> GetTuileLivreBibliotequeInventaire()
        {
            List<TuileLivreBibliotequeVM> tuileInventaire = new();

            foreach (int livre in _livreDAO.GetAll().Select(l => l.LivreId))
            {
                tuileInventaire.Add(await GetTuileLivreBibliotequeVMs(livre));
            }

            return tuileInventaire;
        }

        public async Task<List<TuileLivreBibliotequeVM>> GetTuileLivreBibliotequeAccueil()
        {
            List<TuileLivreBibliotequeVM> tuileLivreBiblioteques = new();

            List<int> list = _context.LivresBibliotheque
               .Take(4)
               .Select(l => l.LivreId)
               .ToList();

            foreach (int livre in list)
            {
                tuileLivreBiblioteques.Add(await GetTuileLivreBibliotequeVMs(livre));
            }
            return tuileLivreBiblioteques;
        }

        public LivreDetailVM GetLivreDetailVM(int livreBibliothequeId)
        {
            LivreBibliotheque livreBibliotheque = _livreDAO.GetById(livreBibliothequeId);

            LivreDetailVM tuileVM = new()
            {
                livreBibliotheque = _mapper.Map<LivreBibliothequeDto>(livreBibliotheque),
                auteurs = livreBibliotheque.Auteurs.Select(a=> a.Auteur.NomComplet).ToList(),
                programmeEtudeNom = livreBibliotheque.Cours.FirstOrDefault()?.Cours.ProgrammeEtude.Nom
            };

            if (string.IsNullOrEmpty(tuileVM.livreBibliotheque.PhotoCouverture))
                tuileVM.livreBibliotheque.PhotoCouverture = GetImageParDefaut();

            return tuileVM;
        }

        public string GetImageParDefaut()
        {
            return "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-livre.jpg";
        }
    }
}
