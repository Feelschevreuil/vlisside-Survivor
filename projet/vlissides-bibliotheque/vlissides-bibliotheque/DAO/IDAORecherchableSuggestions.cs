using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{
    /// <summary>
    /// Interface <c>IRecherchableDAO</c> définit les fonctionnalités à implémenter 
    /// pour les DAO offrant une recherche des titres.
    /// </summary>
    interface IDAORecherchableSuggestions
    {

	/// <summary>
	/// Cherche une propriété des objet.
	/// </summary>
	/// <param name="recherche">Recherche de propriété.</param>
	/// <param name="quantiteSuggestions">Quantité de suggestions à retourner.</param>
	/// <returns>
	/// Une liste de Strings contenant les titres correspondant à la recherche.
	/// </returns>
	IEnumerable<string> GetSuggestions(string recherche, int quantiteSuggestions);
    }
}
