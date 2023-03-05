using vlissides_bibliotheque.DAO.Interface;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    public class ProgrammeEtudeDAO : IDAO<ProgrammeEtude>
    {
        private readonly ApplicationDbContext _context;
        public ProgrammeEtudeDAO( ApplicationDbContext context)
        {
            _context= context;
        }
        public ProgrammeEtude GetById(int id)
        {
            return _context.ProgrammesEtudes.Single(p=> p.ProgrammeEtudeId == id);
        }

        public IEnumerable<ProgrammeEtude> GetAll()
        {
            return _context.ProgrammesEtudes;
        }

        public bool Insert(ProgrammeEtude programmeEtude)
        {
            _context.ProgrammesEtudes.Add(programmeEtude);
            return true;
        }

        public ProgrammeEtude Update(ProgrammeEtude programmeEtude)
        {
            _context.ProgrammesEtudes.Update(programmeEtude);
            return null;
        }

        public bool Delete(int id)
        {
            _context.ProgrammesEtudes.Remove(GetById(id));
            return true;
        }

        public bool Save()
        {
            _context.SaveChanges();
            return true;
        }
    }
}
