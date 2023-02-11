using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Interface;
using vlissides_bibliotheque.MethodeGlobal;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque.Services
{
    public class EvenementService : IEvenementVM
    {
        private readonly ApplicationDbContext _context;
        private readonly Mapping _mapper;

        public EvenementService(ApplicationDbContext context, Mapping mapper) 
        {
            _context = context;
            _mapper = mapper;
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

        public List<EvenementVM> GetEvenementAccueil()
        {
            IEnumerable<Evenement> listQuatreEvenement = _context.Evenements.Take(4);
            List<EvenementVM> listEvenementsVM = new();

            foreach (Evenement evenement in listQuatreEvenement)
            {
                EvenementVM evenementVM = new()
                {
                    EvenementId = evenement.EvenementId,
                    Commanditaire = evenement.Commanditaire,
                    CommanditaireId = evenement.CommanditaireId,
                    Debut = evenement.Debut,
                    Fin = evenement.Fin,
                    Image = evenement.Image,
                    Nom = evenement.Nom,
                    Description = evenement.Description,

                };
                if (evenement.Image == "" || evenement.Image == null)
                {
                    evenementVM.Image = "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-evenement.jpg";
                }
                listEvenementsVM.Add(evenementVM);
            };

            return listEvenementsVM;
        }

        public List<EvenementVM> GetEvenementInventaire()
        {
            IEnumerable<Evenement> listQuatreEvenement = _context.Evenements.ToList();
            List<EvenementVM> listEvenementsVM = new();

            foreach (Evenement evenement in listQuatreEvenement)
            {
                EvenementVM evenementVM = new()
                {
                    EvenementId = evenement.EvenementId,
                    Commanditaire = evenement.Commanditaire,
                    CommanditaireId = evenement.CommanditaireId,
                    Debut = evenement.Debut,
                    Fin = evenement.Fin,
                    Image = evenement.Image,
                    Nom = evenement.Nom,
                    Description = evenement.Description,

                };
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
