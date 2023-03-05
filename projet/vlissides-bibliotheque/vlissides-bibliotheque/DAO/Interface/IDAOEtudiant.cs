using vlissides_bibliotheque.Data;

namespace vlissides_bibliotheque.DAO.Interface
{
    /// <summary>
    /// Interface <c>DAO</c> qui définit les propriétés que les DAO's doivent avoir.
    /// </summary>
    public interface IDAOEtudiant<T>
    {

        /// <summary>
        /// Cherche tous les objets.
        /// </summary>
        /// <returns>Les objets en liste.</returns>

        T GetById(string id);

        IEnumerable<T> GetAll();

        bool Insert(T t);

        T Update(T t);

        bool Delete(string id);

        bool Save();
    }
}
