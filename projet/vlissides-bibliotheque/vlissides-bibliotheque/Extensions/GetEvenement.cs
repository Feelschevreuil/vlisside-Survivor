using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public static class GetEvenement
    {
        public static List<EvenementVM> GetEvenements(List<Evenement> listEvenements)
        {
            List<EvenementVM> listEvenementsVM = new();
            IEnumerable<Evenement> listQuatreEvenement = listEvenements;

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
                    Nom =evenement.Nom,
                    Description = evenement.Description,
                };
                listEvenementsVM.Add(evenementVM);
            };

            return listEvenementsVM;
        }
    }
}
