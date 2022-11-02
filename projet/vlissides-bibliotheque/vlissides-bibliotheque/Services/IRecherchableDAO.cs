using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Services
{
    /// <summary>
    /// Interface <c>IRecherchableDAO</c> qui définit les fonctionnalités 
    /// à implémenter pour les DAO offrant une recherche.
    /// </summary>
    interface IRecherchableDAO<T>
    {
	/// <summary>
	/// Compte le nombre de pages par la quantité.
	/// </summary>
	/// <param name="quantityPerPage">
	/// La quantité d'éléments que l'on veut afficher par page.
	/// </param>
	/// <returns>Le nombre de pages selon la quantité désirée.</returns>
	int GetPageCount(int quantityPerPage);

	/// <summary>
	/// Cherche les objets par leur nom.
	/// </summary>
	/// <param name="title">
	/// Nom des objets à chercher.
	/// </param>
	/// <param name="quantityPerPage">
	/// La quantité d'objets que l'on veut afficher par page.
	/// </param>
	/// <param name="page">
	/// Le numéro de page des résultats.
	/// </param>
	/// <returns>
	/// Une liste d'objets ayant un nom similaire ayant un nom similaire ou égal.
	/// </returns>
	IEnumerable<T> SearchByName(String name, int quantityPerPage= 20, int page = 0);
    }
}
