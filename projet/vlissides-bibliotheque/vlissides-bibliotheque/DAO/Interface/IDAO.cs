using System.Collections.Generic;

namespace vlissides_bibliotheque.DAO.Interface
{
    /// <summary>
    /// Interface <c>DAO</c> qui définit les propriétés que les DAO's doivent avoir.
    /// </summary>
    public interface IDAO<T>
    {

        /// <summary>
        /// Cherche tous les objets.
        /// </summary>
        /// <returns>Les objets en liste.</returns>

        T GetById(int id);

        IEnumerable<T> GetAll();

        bool Insert(T t);

        T Update(T t);

        bool Delete(int id);

        bool Save();
    }
}
