using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Services
{
    /// <summary>
    /// Interface <c>IRecherchableAvanceeDAO</c> qui définit les fonctionnalités 
    /// à implémenter pour les DAO offrant une recherche avancée.
    /// </summary>
    interface IRecherchableAvanceeDAO<T>
    {
	
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
	ICollection<T> SearchByProperties(T recherche, int quantiteParPage = 20, int page = 0);
    }
}
