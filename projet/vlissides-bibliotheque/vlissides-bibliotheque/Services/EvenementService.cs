using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Mapper;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{
    public class EvenementService : IEvenementVM
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EvenementService(ApplicationDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public Evenement GetEvenement(EvenementVM evenementRecus)
        {
            Evenement Evenement = _mapper.Map<Evenement>(evenementRecus);

            return Evenement;
        }

        public EvenementVM GetEvenementVM(Evenement evenementRecus)
        {
            EvenementVM evenementVM = new()
            {
                EvenementId = evenementRecus.EvenementId,
                Commanditaire = evenementRecus.Commanditaire,
                CommanditaireId = evenementRecus.CommanditaireId,
                Debut = evenementRecus.Debut,
                Fin = evenementRecus.Fin,
                Image = evenementRecus.Image,
                Nom = evenementRecus.Nom,
                Description = evenementRecus.Description,
            };

            return evenementVM;
        }

        public async Task<List<EvenementVM>> GetEvenementAccueil()
        {
            List<Evenement> listQuatreEvenement = await _context.Evenements.Take(4).ToListAsync();
            List<EvenementVM> listEvenementsVM = new();

            foreach (Evenement evenement in listQuatreEvenement)
            {
                EvenementVM evenementVM = GetEvenementVM(evenement);


                if (evenement.Image == "" || evenement.Image == null)
                {
                    evenementVM.Image = "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-evenement.jpg";
                }
                listEvenementsVM.Add(evenementVM);
            };

            return listEvenementsVM;
        }

        public async  Task<List<EvenementVM>> GetEvenementInventaire()
        {
            IEnumerable<Evenement> EvenementInventaire = await _context.Evenements.ToListAsync();
            List<EvenementVM> listEvenementsVM = new();

            foreach (Evenement evenement in EvenementInventaire)
            {
                EvenementVM evenementVM = GetEvenementVM(evenement);


                if (evenement.Image == "" || evenement.Image == null)
                {
                    evenementVM.Image = "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-evenement.jpg";
                }
                listEvenementsVM.Add(evenementVM);
            };

            return listEvenementsVM;
        }
    }
}
