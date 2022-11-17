using vlissides_bibliotheque.Data;

namespace vlissides_bibliotheque.DAO
{
    /// <summary>
    /// Interface <c>DAO</c> qui définit les propriétés que les DAO's doivent avoir.
    /// </summary>
    interface IDAO<T>
    {

        /// <summary>
        /// Cherche l'objet correspondant avec l'id.
        /// </summary>
        /// <param name="id">L'id de l'objet à chercher.</param>
        /// <returns>L'object correspondant à l'objet.</returns>
        T Get(long id);

        /// <summary>
        /// Cherche tous les objets.
        /// </summary>
        /// <returns>Les objets en liste.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Sauvegarde l'objet désiré.
        /// </summary>
        /// <param name="t">L'objet à sauvegarder.</param>
        /// <returns>true si l'objet a été sauvegardé avec succès.</returns>
        bool Save(T t);

        /// <summary>
        /// Met à jour l'objet désiré.
        /// </summary>
        /// <param name="idObjetOriginal">L'objet contenant les propriétés originales</param>
        /// <param name="objetAJour">L'objet contenant les modifications.</param>
        /// <returns>true si l'objet a été sauvegardé avec succès.</returns>
        T Update(int idObjetOriginal, T objetAJour);

        /// <summary>
        /// Efface l'objet désiré.
        /// </summary>
        /// <param name="id">L'id de l'objet à effacer.</param>
        /// <returns>true si l'objet a été effacé avec succès.</returns>
        bool Delete(long id);
    }
}
