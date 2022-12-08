using System.ComponentModel.DataAnnotations;

namespace vlissides_bibliotheque.Utils
{

    /// <summary>
    /// Classe <c>CollectionUtils</c> contient les utilitées reliées aux <c>ICollection</c>.
    /// </summary>
    public class CollectionUtils
    {

        /// <summary>
        /// Confirme qu'une collection est nulle ou vide.
        /// </summary>
        public static bool CollectionNulleOuVide<T>(ICollection<T> collection)
        {

            bool collectionVideOuNulle;

            collectionVideOuNulle = collection == null ? true : collection.Count() == 0;

            return collectionVideOuNulle;
        }
    }
}
