using vlissides_bibliotheque.Data;

namespace vlissides_bibliotheque.DAO
{
    /// <summary>
    /// Interface <c>IDAOTableLiason</c> qui définit les propriétés que les DAO's manipulant des objets 
    /// représentant des tables de liaison doivent avoir.
    /// </summary>
    interface IDAOTableLiason<T>
    {

        /// <summary>
        /// Cherche les objets correspondants au premier id.
        /// </summary>
        IEnumerable<T> GetSelonPremierId(long id);

        /// <summary>
        /// Cherche les objets correspondants au deuxième id.
        /// </summary>
        IEnumerable<T> GetSelonDeuxiemeId(long id);

        /// <summary>
        /// Cherche l'objets correspondants aux id's.
        /// </summary>
        /// <param name="idPremier">Id du premier objet.</param>
        /// <param name="idSecond">Id du second objet.</param>
        /// <returns>L'objet correspondant aux id's.</returns>
        T Get(long idPremier, long idSecond);

        /// <summary>
        /// Met à jour l'objet désiré.
        /// </summary>
        /// <param name="idPremier">Id du premier objet.</param>
        /// <param name="idSecond">Id du second objet.</param>
        /// <param name="objetAJour">L'objet contenant les modifications.</param>
        /// <returns>L'objet modifié.</returns>
        T Update(long idPremier, long idSecond, T objetAJour);

        /// <summary>
        /// Efface l'objet désiré.
        /// </summary>
        /// <param name="idPremier">Id du premier objet.</param>
        /// <param name="idSecond">Id du second objet.</param>
        /// <returns>true si l'objet a été effacé avec succès.</returns>
        bool Delete(long idPremier, long idSecond);
    }
}
