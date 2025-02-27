using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services.Interface;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{
    public class EvenementService : IEvenementVM
    {
        private readonly IMapper _mapper;
        private readonly IDAO<Evenement> _evenement;

        public EvenementService(IMapper mapper, IDAO<Evenement> evenement)
        {
            _mapper = mapper;
            _evenement = evenement;
        }


        public List<EvenementVM> GetEvenementAccueil()
        {
            List<EvenementVM> listEvenementsVM = new();

            foreach (Evenement evenement in _evenement.GetAll().Take(4))
                listEvenementsVM.Add(_mapper.Map<EvenementVM>(evenement));

            return listEvenementsVM;
        }

        public List<EvenementVM> GetEvenementInventaire()
        {
            List<EvenementVM> listEvenementsVM = new();

            foreach (Evenement evenement in _evenement.GetAll())
                listEvenementsVM.Add(_mapper.Map<EvenementVM>(evenement));

            return listEvenementsVM;
        }
    }
}
