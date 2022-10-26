using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Services
{
    /// <summary>
    /// Interface <c>IRecherchableDAO</c> définit les fonctionnalités à implémenter 
    /// pour les DAO offrant une recherche des titres.
    /// </summary>
    interface IRecherchableTitleDAO
    {
	/// <summary>
	/// Cherche les titres des objects.
	/// </summary>
	/// <param name="title">Titre à chercher.</param>
	/// <returns>
	/// Une liste de Strings contenant les titres correspondant à la recherche.
	/// </returns>
	IEnumerable<String> SearchTitlesByTitle(String title, int maxResult = 10);
    }
}
