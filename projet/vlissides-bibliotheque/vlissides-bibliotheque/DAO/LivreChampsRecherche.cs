using vlissides_bibliotheque.Models;

namespace vlissides_bibliotheque.DAO
{

    /// <summary>
    /// Classe <c>LivreCampsRecherche</c> définit les champs de recherche des livres.
    /// </summary>
    public class LivreChampsRecherche
    {

	// TODO: all properties to lower

	public int LivreId { get; set; }

	public string Titre { get; set; }

	public string MaisonEdition { get; set; }

	public string Auteur { get; set; }

	public string Isbn { get; set; }

	public string Resume { get; set; }

	public DateTime DatePublicationMinimum { get; set; }

	public DateTime DatePublication { get; set; }

	public DateTime DatePublicationMaximale { get; set; }

	public double PrixMinimum { get; set; }

	public double PrixMaximum { get; set; }

	public bool Neuf { get; set; }

	public bool Digital { get; set; }

	public bool Usage { get; set; }

	// TODO: no. cours et nom du cours

	/// <summary>
	/// Vérifie si la recherche contient un prix minimum.
	/// </summary>
	public bool ChercheAvecPrixMinimum()
	{

	    bool prixMaxmiumPresent;

	    prixMaxmiumPresent = PrixMinimum > 0;

	    return prixMaxmiumPresent;
	}

	/// <summary>
	/// Vérifie si la recherche contient un prix maximum.
	/// </summary>
	public bool ChercheAvecPrixMaximum()
	{

	    bool prixMaxmiumPresent;

	    prixMaxmiumPresent = PrixMaximum > 1;

	    return prixMaxmiumPresent;
	}

	/// <summary>
	/// Vérifie si la recherche est entre un "range" de prix.
	/// </summary>
	public bool ChercheAvecEtenduePrix()
	{

	    bool searchesWithPriceRange;

	    searchesWithPriceRange = ChercheAvecPrixMaximum() && ChercheAvecPrixMinimum();

	    return searchesWithPriceRange;
	}

	/// <summary>
	/// Vérifie que le prix maxmium soit plus petit que le prix minimum.
	/// </summary>
	public bool EtenduePrixEstValide()
	{

	    bool prixMinimumSmallerThanPrixMaximum;

	    prixMinimumSmallerThanPrixMaximum = false;

	    if(ChercheAvecEtenduePrix())
	    {

		prixMinimumSmallerThanPrixMaximum = PrixMaximum > PrixMinimum;

		return prixMinimumSmallerThanPrixMaximum;
	    }

	    return prixMinimumSmallerThanPrixMaximum;
	}

	/// <summary>
	/// Regarde si une recherche est valide.
	/// </summary>
	public bool EstValide()
	{

	    bool rechercheValide;

	    rechercheValide = false;

	    if(Neuf || Digital || Usage)
	    {

		rechercheValide = true;
	    }

	    return rechercheValide;
	}
    }
}
