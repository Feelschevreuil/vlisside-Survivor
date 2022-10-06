using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque
{
    public static class UtilisateurExtension
    {
        public static Adresse GetAdresse(this Etudiant etudiant, ApplicationDbContext context)
        {
            int adresseId = etudiant.AdresseId;

            Adresse adresseFacturation = context.Adresses
                .Include(a => a.Province)
                .FirstOrDefault(a => a.AdresseId == adresseId);

            return adresseFacturation;
        }
    }
}
