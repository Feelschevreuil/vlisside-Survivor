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

	public List<int> ProgrammesEtudeId { get; set; }

	public List<int> CoursId { get; set; }

	public List<int> ProfesseursId { get; set; }

	// TODO: no. cours et nom du cours
	
	/// <summary> 
	/// Vérifie que la recherche d'Isbn est valide.
	/// </summary>
	public bool IsbnQueryValid()
	{

	    bool isbnQueryValid;
	    int isbnLength;

	    isbnLength = Isbn.Length;

	    isbnQueryValid = isbnLength > 0 && isbnLength < 14;

	    return isbnQueryValid;
	}

	/// <summary>
	/// Vérifie si la recherche contient un prix minimum.
	/// </summary>
	public bool ChercheAvecPrixMinimum()
	{

	    bool prixMinimumPresent;

	    prixMinimumPresent = PrixMinimum > 0;

	    return prixMinimumPresent;
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
	/// Véerifie que la recherche ne contient aucune restriction de prix.
	/// </summary>
	public bool AucuneRestrictionPrix()
	{

	    bool aucuneRestrictionPrix;

	    aucuneRestrictionPrix = !ChercheAvecPrixMaximum() && !ChercheAvecPrixMaximum();

	    return aucuneRestrictionPrix;
	}

	/// <summary>
	/// Vérifie  que la recherche contient une restriction de professeur.
	/// </summary>
	public bool ChercheAvecProfesseur()
	{

	    bool chercheAvecProfesseur;

	    chercheAvecProfesseur = ProfesseursId != null && ProfesseursId.Count() > 0 && ChercheAvecProgrammeEtude() && ChercheAvecCours();

	    return chercheAvecProfesseur;
	}

	/// <summary>
	/// Vérifie que la recherche contient une restriction du programme d'étude.
	/// </summary>
	public bool ChercheAvecProgrammeEtude()
	{

	    bool chercheAvecProgrammeEtude;

	    chercheAvecProgrammeEtude = ProgrammesEtudeId != null && ProgrammesEtudeId.Count() > 0;

	    return chercheAvecProgrammeEtude;
	}

	/// <summary>
	/// Vérifie que la recherche contient une restriction de cours.
	/// </summary>
	public bool ChercheAvecCours()
	{

	    bool chercheAvecCours;

	    chercheAvecCours = CoursId != null && CoursId.Count() > 0 && ChercheAvecProgrammeEtude();

	    return chercheAvecCours;
	}

	/// <summary>
	/// Vérifie si la recherche à des contraintes reliées aux cours, 
	/// aux programmes d'études et ou aux professeurs
	/// </summary>
	public bool ChercheAvecContraintesCoursLivre()
	{

	    bool chercheAvecContraintesCoursLivre;

	    chercheAvecContraintesCoursLivre = ChercheAvecCours() || ChercheAvecProgrammeEtude() || ChercheAvecProfesseur();

	    return chercheAvecContraintesCoursLivre;
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
