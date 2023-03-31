using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Services.Interface;

namespace vlissides_bibliotheque.Services
{
    public class LivreEtudiantService: ILivreEtudiant
    {
        private readonly ApplicationDbContext _context;

        public LivreEtudiantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<LivreEtudiant> GetAllLivreEtudiant(string id)
        {
           return _context.LivresEtudiants.Where(e => e.Etudiant.Id == id).ToList();
        }

        public LivreEtudiant GetLivreByEtudiantId(string id)
        {
            return _context.LivresEtudiants.SingleOrDefault(e => e.Etudiant.Id == id);
        }
    }
}
