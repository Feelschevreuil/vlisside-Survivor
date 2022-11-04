using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Constantes;

namespace vlissides_bibliotheque.DAO
{
    /// <summary>
    /// Interface <c>IDAORecherchable</c> qui définit les fonctionnalités 
    /// à implémenter pour les DAO offrant une recherche.
    /// </summary>
    interface IDAORecherchable<T>
    {

	/// <summary>
	/// Cherche les objets par une propriété.
	/// </summary>
	/// <param name="propriete">
	/// Propriété à chercher.
	/// </param>
	/// <param name="quantityPerPage">
	/// La quantité d'objets que l'on veut afficher par page.
	/// </param>
	/// <param name="page">
	/// Le numéro de page des résultats.
	/// </param>
	/// <returns>
	/// Une liste d'objets ayant la propriété similaire égale.
	/// </returns>
	IEnumerable<T> GetSelonPropriete(string propriete, int quantiteParPage = ConstantesDAO.QUANTITE_PAR_PAGE, int page = 0);
    }
}
