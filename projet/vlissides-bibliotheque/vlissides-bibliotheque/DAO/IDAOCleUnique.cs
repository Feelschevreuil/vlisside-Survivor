using vlissides_bibliotheque.Data;

namespace vlissides_bibliotheque.DAO
{
    /// <summary>
    /// Interface <c>IDAOCleUnique</c> qui définit les propriétés que les DAO's manipulant des objets 
    /// à clé unique doivent avoir.
    /// </summary>
    interface IDAOCleUnique<T>
    {

        /// <summary>
        /// Cherche l'objet correspondant avec l'id.
        /// </summary>
        /// <param name="id">L'id de l'objet à chercher.</param>
        /// <returns>L'object correspondant à l'objet.</returns>
        T Get(long id);

        /// <summary>
        /// Met à jour l'objet désiré.
        /// </summary>
        /// <param name="idObjetOriginal">L'objet contenant les propriétés originales</param>
        /// <param name="objetAJour">L'objet contenant les modifications.</param>
        /// <returns>true si l'objet a été sauvegardé avec succès.</returns>
        T Update(long idObjetOriginal, T objetAJour);

        /// <summary>
        /// Efface l'objet désiré.
        /// </summary>
        /// <param name="id">L'id de l'objet à effacer.</param>
        /// <returns>true si l'objet a été effacé avec succès.</returns>
        bool Delete(long id);
    }
}
