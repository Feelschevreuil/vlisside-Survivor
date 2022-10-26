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
	/// <param name="quantiteParPage">
	/// La quantité d'éléments que l'on veut afficher par page.
	/// </param>
	/// <returns>Le nombre de pages selon la quantité désirée.</returns>
	int GetPageCount(int quantiteParPage);

	/// <summary>
	/// Cherche les objets par leur ttire.
	/// </summary>
	/// <param name="title">
	/// Titre des objets à chercher.
	/// </param>
	/// <param name="quantiteParPage">
	/// La quantité d'objets que l'on veut afficher par page.
	/// </param>
	/// <param name="pages">
	/// Le numéro de page des résultats.
	/// </param>
	/// <returns>
	/// Une liste d'objets ayant un titre similaire ayant un titre similaire ou égal.
	/// </returns>
	IEnumerable<T> SearchByTitle(String title, int quantiteParPage = 20, int pages = 0);

	/// <summary>Cherche les objets par leurs propriétés.</summary>
	/// <param name="bookQueries">
	/// Objet contenant les champs du livre à chercher.
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
	IEnumerable<T> SearchByProperties(T recherche, int quantiteParPage = 20, int page = 0);
    }
}
