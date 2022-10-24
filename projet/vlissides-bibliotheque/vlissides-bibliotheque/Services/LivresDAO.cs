using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.Services
{
    /// <summary>
    /// Interface <c>DAO</c> définit les propriétés que les DAO's doivent avoir.
    /// </summary>
    public class LivresDAO : IDAO<LivreBibliotheque>
    {

	private ApplicationDbContext _context;

	public LivresDAO(ApplicationDbContext context)
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
	/// <param name="objetAJour">L'objet contenant les propriétés originales</param>
	/// <param name="objetOriginal">L'objet contenant les modifications.</param>
        /// <returns>true si l'objet a été sauvegardé avec succès.</returns>
	public bool Update(LivreBibliotheque objetOriginal, LivreBibliotheque objetAJour) 
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
    }
}
