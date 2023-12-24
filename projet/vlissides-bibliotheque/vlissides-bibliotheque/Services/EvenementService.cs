using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
       

        public async Task<List<EvenementVM>> GetEvenementAccueil()
        {
            List<Evenement> listQuatreEvenement = await _context.Evenements.Take(4).ToListAsync();
            List<EvenementVM> listEvenementsVM = new();

            foreach (Evenement evenement in listQuatreEvenement)
            {
                EvenementVM evenementVM = _mapper.Map<EvenementVM>(evenement);


                if (string.IsNullOrEmpty(evenement.Image))
                {
                    evenementVM.Image = "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-evenement.jpg";
                }
                listEvenementsVM.Add(evenementVM);
            };

            return listEvenementsVM;
        }

        public async Task<List<EvenementVM>> GetEvenementInventaire()
        {
            IEnumerable<Evenement> EvenementInventaire = await _context.Evenements.ToListAsync();
            List<EvenementVM> listEvenementsVM = new();

            foreach (Evenement evenement in EvenementInventaire)
            {
                EvenementVM evenementVM = _mapper.Map<EvenementVM>(evenement);

                if (string.IsNullOrEmpty(evenement.Image))
                    evenementVM.Image = "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-evenement.jpg";

                listEvenementsVM.Add(evenementVM);
            };

            return listEvenementsVM;
        }
    }
}
