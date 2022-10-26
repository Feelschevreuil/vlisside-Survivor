using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using System.Linq;

namespace vlissides_bibliotheque.Services
{
    /// <summary>
    /// Clsase <c>LivresBibliothequeDAO</c> qui implémente l'interface DAO et l'interface LivresDAO.
    /// </summary>
    public class LivresBibliothequeDAO : IDAO<LivreBibliotheque>, IRecherchableDAO<LivreBibliotheque>,IRecherchableTitleDAO
    {

	private ApplicationDbContext _context;

	public LivresBibliothequeDAO(ApplicationDbContext context)
	{

	    _context = context;
	}

        /// <summary>
        /// Cherche l'objet correspondant avec l'id.
        /// </summary>
	/// <param name="id">L'id de l'objet à chercher.</param>
        /// <returns>L'object correspondant à l'objet.</returns>
	public LivreBibliotheque Get(long id) 
	{

	    return _context.LivresBibliotheque.Where(livre => livre.LivreId == id).FirstOrDefault();
	}

        /// <summary>
        /// Cherche tous les objets.
        /// </summary>
        /// <returns>Les object en liste.</returns>
	public IEnumerable<LivreBibliotheque> GetAll() 
	{

	    return _context.LivresBibliotheque;
	}

        /// <summary>
	/// Sauvegarde l'objet désiré.
        /// </summary>
	/// <param name="t">L'objet à sauvegarder.</param>
        /// <returns>true si l'objet a été sauvegardé avec succès.</returns>
	public bool Save(LivreBibliotheque livre)
	{

	    _context.LivresBibliotheque.Add(livre);
	    _context.SaveChanges();

	    return true;
	}

	// TODO: implement
        /// <summary>
	/// Met à jour l'objet désiré.
        /// </summary>
	/// <param name="idObjetOriginal">L'objet contenant les propriétés originales</param>
	/// <param name="objetAJour">L'objet contenant les modifications.</param>
        /// <returns>true si l'objet a été sauvegardé avec succès.</returns>
	public bool Update(int idObjetOriginal, LivreBibliotheque objetAJour) 
	{

	    return false;
	}

        /// <summary>
	/// Efface l'objet désiré.
        /// </summary>
	/// <param name="id">L'id de l'objet à effacer.</param>
        /// <returns>true si l'objet a été effacé avec succès.</returns>
	public bool Delete(long id)
	{

	    ILivre livreEffacer;

	    livreEffacer = _context.LivresBibliotheque.Where(livre => livre.LivreId == id).FirstOrDefault();

	    if(livreEffacer != null)
	    {

		_context.Remove(livreEffacer);
		_context.SaveChanges();
		return true;
	    }

	    return false;
	}

	/// <summary>
	/// Compte le nombre de pages par la quantité.
	/// </summary>
	/// <param name="quantiteParPage">La quantité de livres que l'on veut afficher par page.</param>
	/// <returns>Le nombre de pages selon la quantité désirée.</returns>
	public int GetPageCount(int quantiteParPage)
	{
	    
	    int pageCount;

	    pageCount = _context.LivresBibliotheque.Count();

	    return pageCount;
	}

	/// <summary>
	/// Cherche les titres des livres par un titre.
	/// </summary>
	/// <param name="title">Titre à chercher.</param>
	/// <returns>Une liste de Strings contenant les titres correspondant à la recherche.</returns>
	public IEnumerable<String> SearchTitlesByTitle(String title, int maxResult = 10)
	{
	    
	    IEnumerable<string> titres;

	    titres = _context.LivresBibliotheque.Where(livre => livre.Titre.StartsWith(title)).Select(livre => livre.Titre);

	    return titres;
	}

	/// <summary>
	/// Cherche les livres par leur ttire.
	/// </summary>
	/// <param name="title">Titre des livres à chercher.</param>
	/// <param name="quantiteParPage">La quantité de livres que l'on veut afficher par page.</param>
	/// <param name="pages">Le numéro de page des résultats.</param>
	/// <returns>Une liste de livres ayant un titre similaire ayant un titre similaire ou égal.</returns>
	public IEnumerable<LivreBibliotheque> SearchByTitle(String title, int quantiteParPage = 20, int pages = 0)
	{

	    IEnumerable<LivreBibliotheque> livresBibliotheque;

	    livresBibliotheque = _context.LivresBibliotheque.Where(livre => livre.Titre.StartsWith(title));

	    return livresBibliotheque;
	}

	// TODO: implement
	/// <summary>Cherche les objets par leurs propriétés.</summary>
	/// <param name="bookQueries">Objet contenant les champs du livre à chercher.</param>
	/// <param name="quantiteParPage">La quantité d'objets que l'on veut afficher par page.</param>
	/// <param name="page">Le numéro de page des résultats.</param>
	/// <returns>Une liste d'objets ayant les propriétés désirées ou une liste vide s'il n'y en a pas.</returns>
	public ICollection<LivreBibliotheque> SearchByProperties(LivreBibliotheque livreRecherche, int quantiteParPage = 20, int page = 0)
	{

	    ICollection<LivreBibliotheque> livresBibliotheque;

	    if(livreRecherche.LivreId != 0)
	    {

		livresBibliotheque = new List<LivreBibliotheque>();

		livresBibliotheque.Add(Get(livreRecherche.LivreId));

	    }
	    else if(
		    livreRecherche.DatePublication != null &&
		    !string.IsNullOrEmpty(livreRecherche.Isbn) &&
		    livreRecherche.MaisonEditionId != 0 &&
		    !string.IsNullOrEmpty(livreRecherche.Resume) &&
		    !string.IsNullOrEmpty(livreRecherche.Titre))
	    {
		
	    }

	    livresBibliotheque = GetAll().ToList();

	    return livresBibliotheque;
	}
    }
}
