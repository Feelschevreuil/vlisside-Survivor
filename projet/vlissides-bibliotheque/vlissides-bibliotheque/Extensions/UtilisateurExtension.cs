using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Extensions
{
    public static class UtilisateurExtension
    {
        public static Adresse GetAdresseLivraison(this Etudiant etudiant, ApplicationDbContext context)
        {
            int adresseLivraisonId = etudiant.AdresseLivraisonId;

            Adresse adresseLivraison = context.Adresses.Find(adresseLivraisonId);

            return adresseLivraison;
        }

        public static Adresse GetAdresseFacturation(this Etudiant etudiant, ApplicationDbContext context)
        {
            int adresseFacturationId = etudiant.AdresseFacturationId;

            Adresse adresseFacturation = context.Adresses.Find(adresseFacturationId);

            return adresseFacturation;
        }
    }
}
