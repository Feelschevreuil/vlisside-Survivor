using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Constantes;

namespace vlissides_bibliotheque.DAO
{
    /// <summary>
    /// Interface <c>IDAORecherchableAvancee</c> qui définit les fonctionnalités 
    /// à implémenter pour les DAO offrant une recherche avancée.
    /// </summary>
    interface IDAORecherchableAvancee<T,J>
    {
    
        /// <summary>Cherche les objets par leurs propriétés.</summary>
        /// <param name="proprietes">
        /// Objet contenant les propriétés à chercher.
        /// </param>
        /// <param name="quantiteParPage">
        /// La quantité d'objets que l'on veut afficher par page.
        /// </param>
        /// <param name="page">
        /// Le numéro de page des résultats.
        /// </param>
        /// <returns>
        /// Une liste d'objets ayant les propriétés désirées ou une liste vide 
        /// s'il n'y en a pas.
        /// </returns>
        ICollection<T> GetSelonProprietes(J proprietes, int quantiteParPage = ConstantesDAO.QUANTITE_PAR_PAGE, int page = 0);
    }
}
