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
                if (evenement.Image == "" || evenement.Image == null)
                {
                    evenementVM.Image = "https://sqlinfocg.cegepgranby.qc.ca/1855390/img/photo-evenement.jpg";
                }

                listEvenementsVM.Add(evenementVM);
            };
        


            return listEvenementsVM;
        } 
        public static EvenementVM GetUnEvenement(Evenement evenementRecus)
        {
            EvenementVM EvenementsVM = new();

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
    }
}
