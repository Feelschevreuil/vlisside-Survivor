using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.DTO;
using vlissides_bibliotheque.Enums;
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

            TuileLivreBibliotequeVM tuileVM = new()
            {
                livreBibliotheque = _mapper.Map<LivreBibliothequeDto>(livreBibliotheque),
                prixEtatLivre = _mapper.Map<List<PrixEtatLivreDto>>(await _context.PrixEtatsLivres
                    .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId).OrderBy(p=> (int)p.EtatLivre).ToListAsync()),
                auteurs = await _context.AuteursLivres.Include(a => a.Auteur)
                .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId)
                .Select(x => x.Auteur.GetNomComplet())
            .ToListAsync(),
            };

            if (tuileVM.livreBibliotheque.PhotoCouverture == null || tuileVM.livreBibliotheque.PhotoCouverture == "")
            {
                tuileVM.livreBibliotheque.PhotoCouverture = GetImageParDefaut();
            }

            var programmeEtudeNom = _context.CoursLivres.Include(c => c.Cours).ThenInclude(c => c.ProgrammeEtude)
                .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId).FirstOrDefault();

            if (programmeEtudeNom != null) { tuileVM.programmeEtudeNom = programmeEtudeNom.Cours.ProgrammeEtude.Nom; }
            else { tuileVM.programmeEtudeNom = ""; }

            return tuileVM;
        }

        public async Task<List<TuileLivreBibliotequeVM>> GetTuileLivreBibliotequeInventaire()
        {
            List<TuileLivreBibliotequeVM> tuileInventaire = new();

            foreach (int livre in _livreDAO.GetAll().Select(l=> l.LivreId))
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
               .Select(l=> l.LivreId)
               .ToList();

            foreach (int livre in list)
            {
                tuileLivreBiblioteques.Add(await GetTuileLivreBibliotequeVMs(livre));
            }
            return tuileLivreBiblioteques;
        }

        public async Task<LivreDetailVM> GetLivreDetailVM(int livreBibliothequeId)
        {
            LivreBibliotheque livreBibliotheque = _livreDAO.GetById(livreBibliothequeId);

            LivreDetailVM tuileVM = new()
            {
                livreBibliotheque = _mapper.Map<LivreBibliothequeDto>(livreBibliotheque),
                prixEtatLivre = _mapper.Map<List<PrixEtatLivreDto>>(await _context.PrixEtatsLivres
                    .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId).OrderBy(p => (int)p.EtatLivre).ToListAsync()),
                auteurs = await _context.AuteursLivres.Include(a => a.Auteur)
                .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId)
                .Select(x => x.Auteur.GetNomComplet())
            .ToListAsync(),
            };

            if (tuileVM.livreBibliotheque.PhotoCouverture == null || tuileVM.livreBibliotheque.PhotoCouverture == "")
            {
                tuileVM.livreBibliotheque.PhotoCouverture = GetImageParDefaut();
            }

            var programmeEtudeNom = _context.CoursLivres.Include(c => c.Cours).ThenInclude(c => c.ProgrammeEtude)
                .Where(x => x.LivreBibliothequeId == livreBibliotheque.LivreId).FirstOrDefault();

            if (programmeEtudeNom != null) { tuileVM.programmeEtudeNom = programmeEtudeNom.Cours.ProgrammeEtude.Nom; }
            else { tuileVM.programmeEtudeNom = ""; }

            return tuileVM;
        }

        public string GetImageParDefaut()
        {
            return "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-livre.jpg";
        }
    }
}
