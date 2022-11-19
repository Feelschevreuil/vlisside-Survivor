using vlissides_bibliotheque.Data;

namespace vlissides_bibliotheque.DAO
{
    /// <summary>
    /// Interface <c>DAO</c> qui définit les propriétés que les DAO's doivent avoir.
    /// </summary>
    interface IDAO<T>
    {

        /// <summary>
        /// Cherche tous les objets.
        /// </summary>
        /// <returns>Les objets en liste.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Sauvegarde l'objet désiré.
        /// </summary>
        /// <param name="t">L'objet à sauvegarder.</param>
        /// <returns>L'objet modifié.</returns>
        bool Save(T t);
    }
}
