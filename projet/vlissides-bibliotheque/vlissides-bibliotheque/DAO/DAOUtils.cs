namespace vlissides_bibliotheque.DAO
{

    /// <summary>
    /// Clsase <c>DAOUtils</c> contenant des utilitées utilisées à travers les DAO.
    /// </summary>
    public class DAOUtils
    {

        /// <summary>
        /// Calcule la quantité de pages selon le nombre total d'éléments et le nombre d'éléments par page.
        /// </summary>
        public int GetNombrePages(int quantiteEnregistrements,int quantiteParPage)
        {

            double nombreDePages;

            nombreDePages = quantiteEnregistrements / quantiteParPage;
            
            return (int)Math.Ceiling(nombreDePages);
        }

        /// <summary>
        /// Calcule la quantité d'enregistrements à sauter selon le nombre denregistrements affichées par page.
        /// <param name="elementsPerPage">Enregistrements à afficher par page.</param>
        /// <param name="pageToGet">Enregistrements à aller chercher.</param>
        /// </summary>
        public static int GetQuantityOfElementsToSkip(int elementsPerPage, int pageToGet)
        {

            if(pageToGet > 0)
            {

                int elementsToSkip;

                elementsToSkip = elementsPerPage * pageToGet;

                return elementsToSkip;
            }

            return 0;
        }
    }
}
