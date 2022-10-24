using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.ViewModels;

namespace vlissides_bibliotheque
{
    public static class GetQuatreLivres
    {
        public static List<TuileLivreBibliotequeVM> GetInventaireBibliotequeVMs(ApplicationDbContext _context)
        {
            List<TuileLivreBibliotequeVM> listTuileLivreBibliotequeVMs = new();
            IEnumerable<LivreBibliotheque> listQuatreLivre = _context.LivresBibliotheque.Take(4);

            foreach (LivreBibliotheque livre in listQuatreLivre)
            {
                var livreConvertie = livre.GetTuileLivreBibliotequeVMs(_context);
                listTuileLivreBibliotequeVMs.Add(livreConvertie);
            };

            return listTuileLivreBibliotequeVMs;
        }
    }
}
