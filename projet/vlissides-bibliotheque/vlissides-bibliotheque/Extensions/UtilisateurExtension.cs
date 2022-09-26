using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Extensions
{
    public static class UtilisateurExtension
    {
        private readonly ApplicationDbContext _context;

        public UtilisateurExtension(ApplicationDbContext context)
        {
            _context = context;
        }

        public static Etudiant GetAdresseLivraison(this Etudiant etudiant)
        {
            int adresseId = etudiant.AdresseLivraisonId;



            return 
        }

        public static Etudiant GetAdresseFacturation(this Etudiant etudiant)
        {
            int adresseId = etudiant.AdresseFacturationId;

            return
        }
    }
}
