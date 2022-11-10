using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Extentions;
using System.Linq;

namespace vlissides_bibliotheque.DAO
{

    /// <summary>
    /// Clsase <c>LivresBibliothequeDAO</c> qui implémente l'interface DAO et l'interface LivresDAO.
    /// </summary>
    public class LivresBibliothequeDAO : IDAO<LivreBibliotheque>, IDAORecherchable<LivreBibliotheque>, IDAORecherchableAvancee<LivreBibliotheque, LivreChampsRecherche>, IDAORecherchableSuggestions
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
	/// Cherche une propriété des objet.
	/// </summary>
	/// <param name="recherche">Recherche de propriété.</param>
	/// <param name="quantiteSuggestions">Quantité de suggestions à retourner.</param>
	/// <returns>
	/// Une liste de Strings contenant les titres correspondant à la recherche.
	/// </returns>
	public IEnumerable<string> GetSuggestions(string recherche, int quantiteSuggestions = ConstantesDAO.QUANTITE_SUGGESTIONS)
	{
	    
	    IEnumerable<string> titres;

	    titres = _context
			.LivresBibliotheque
			.Where
			(
			    livre => 
				livre
				    .Titre
				    .ContainsCaseInsensitive(recherche)
			)
			.Take(quantiteSuggestions)
			.Select
			(
			    livre => livre.Titre
			);

	    return titres;
	}
	
	/// <summary>
	/// Cherche les livres par leur titre.
	/// </summary>
	/// <param name="propriete">
	/// Propriété à chercher.
	/// </param>
	/// <param name="quantiteParPage">
	/// La quantité d'objets que l'on veut afficher par page.
	/// </param>
	/// <param name="page">
	/// Le numéro de page des résultats.
	/// </param>
	/// <returns>
	/// Une liste d'objets ayant la propriété similaire égale.
	/// </returns>
	public IEnumerable<LivreBibliotheque> GetSelonPropriete
	(
	    string propriete, 
	    int quantiteParPage = ConstantesDAO.QUANTITE_PAR_PAGE, 
	    int page = ConstantesDAO.PAGE_PAR_DEFAULT
	)
	{

	    int quantiteASauter;
	    IEnumerable<LivreBibliotheque> livresBibliotheque;

	    quantiteASauter = DAOUtils.GetQuantityOfElementsToSkip(quantiteParPage, page); 

	    livresBibliotheque = _context
		.LivresBibliotheque
		.Where
		(
		    livre => 
			livre
			    .Titre
			    .ContainsCaseInsensitive(propriete)
		)
		.If
		(
		    quantiteASauter > 0,
		    livres => livres.Skip(quantiteASauter)
		)
		.Take(quantiteParPage);

	    return livresBibliotheque;
	}

	/// <summary>Cherche les objets par leurs propriétés.</summary>
	/// <param name="proprietes">
	/// Objet contenant les propriétés à chercher.
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
	public ICollection<LivreBibliotheque> GetSelonProprietes
	(
	    LivreChampsRecherche livreChampsRecherche,
	    int quantiteParPage = ConstantesDAO.QUANTITE_PAR_PAGE,
	    int page = ConstantesDAO.PAGE_PAR_DEFAULT
	)
	{

	    if(livreChampsRecherche.LivreId != 0)
	    {

		List<LivreBibliotheque> livresBibliotheque;
		LivreBibliotheque livreBibliothequeParId;

		livreBibliothequeParId = Get(livreChampsRecherche.LivreId);
		livresBibliotheque = new();

		livresBibliotheque.Add(livreBibliothequeParId);

		return livresBibliotheque;
	    }
	    else if(livreChampsRecherche.EstValide())
	    {
		
		IEnumerable<LivreBibliotheque> livresBibliotheque;
		int quantiteASauter;

		quantiteASauter = DAOUtils.GetQuantityOfElementsToSkip(quantiteParPage, page); 

		livresBibliotheque = _context
		    .LivresBibliotheque
		    .If
		    (
			!string.IsNullOrEmpty(livreChampsRecherche.Titre),
			livres => 
			    livres
				.Where
				(
				    livre =>
					livre
					    .Titre
					    .ContainsCaseInsensitive(livreChampsRecherche.Titre)
				)
		    )
		    .If
		    (
			!string.IsNullOrEmpty(livreChampsRecherche.MaisonEdition),
			livres => livres
			    .Where
			    (
				livre =>
				    livre
					.MaisonEdition
					    .Nom
					    .ContainsCaseInsensitive(livreChampsRecherche.MaisonEdition)
			    )
		    )
		    .If
		    (
			!string.IsNullOrEmpty(livreChampsRecherche.Auteur),
			livres => livres
			    .Where
			    ( 
				livre =>
				    _context
					.AuteursLivres
					    .Where
					    (
						auteurLivre =>
						    auteurLivre
							.Auteur
							    .Nom
							    .ContainsCaseInsensitive(livreChampsRecherche.Auteur)
					    )
					    .Select
					    (
						auteurLivre =>
						    auteurLivre.LivreBibliotheque
					    )
					    .Contains
					    (
						livre
					    )
			    )
		    )
		    /*
		    .If
		    (
			livreChampsRecherche.DatePublicationMinimum != null,
			livres => livres
			    .Where
			    (
				livre =>
				    DateTime.Compare(livre.DatePublication, livreChampsRecherche.DatePublicationMinimum) > 0
			    )
		    )
		    .If
		    (
			livreChampsRecherche.DatePublication != null,
			livres => livres
			    .Where
			    (
				livre =>
				    DateTime.Compare(livre.DatePublication, livreChampsRecherche.DatePublicationMinimum) == 0
			    )
		    )
		    .If
		    (
			livreChampsRecherche.DatePublication != null,
			livres => livres
			    .Where
			    (
				livre =>
				    DateTime.Compare(livre.DatePublication, livreChampsRecherche.DatePublicationMinimum) < 0
			    )
		    )
		    

		    // TODO: utiliser le DAO lorsqu'implementé (temp: livre usagés only), prix minimum and usage
		    .If
		    (
			livreChampsRecherche.ChercheAvecEtenduePrix(),
			livres => livres
			    .Where
			    (
				livre =>
				    livre ==
				    _context
					.PrixEtatsLivres
					.Where
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque == livre &&
						prixEtatLivre.Prix < livreChampsRecherche.PrixMaximum &&
						prixEtatLivre.Prix > livreChampsRecherche.PrixMinimum &&
						PredicateEtatUsage(prixEtatLivre)
						// TODO: not working
						// GetCorrectEtatPredicate(livreChampsRecherche)
					)
					.Select
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque
					)
			    )

			    /*
			livreChampsRecherche.SearchesWithPriceRange(),
			livres => livres
			    .Where
			    (
				livre =>
				    _context
					.PrixEtatsLivres
					.Where
					(
					    prixEtatsLivre =>
						prixEtatsLivre.LivreBibliotheque == livre &&
						prixEtatsLivre.Prix < livreChampsRecherche.PrixMaximum &&
						prixEtatsLivre.Prix > livreChampsRecherche.PrixMinimum &&
						GetCorrectEtatPredicate(livreChampsRecherche)
					)
			    )
		    )
			    */
		    // TODO: utiliser le DAO lorsqu'implementé
		    /*
		    .If
		    (
			livreChampsRecherche.ChercheAvecPrixMinimum() && !livreChampsRecherche.ChercheAvecPrixMaximum(),
			livres => livres
			    .Where
			    (
				livre =>
				    livre ==
				    _context
					.PrixEtatsLivres
					.Where
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque == livre &&
						prixEtatLivre.Prix > livreChampsRecherche.PrixMinimum
						// TODO: get état correct as well
					)
					.Select
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque
					)
			    )
		    )
		    // TODO: utiliser le DAO lorsqu'implementé
		    .If
		    (
			!livreChampsRecherche.ChercheAvecPrixMinimum() && livreChampsRecherche.ChercheAvecPrixMaximum(),
			livres => livres
			    .Where
			    (
				livre =>
				    livre ==
				    _context
					.PrixEtatsLivres
					.Where
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque == livre &&
						prixEtatLivre.Prix < livreChampsRecherche.PrixMaximum
						// TODO: get état correct as well
					)
					.Select
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque
					)
			    )
		    )
		    */
		    // TODO: programme
		    // TODO: cours
		    // TODO: prof.
		    .If
		    (
			quantiteASauter > 0,
			livres => livres.Skip(quantiteASauter)
		    )
		    .Take(quantiteParPage);

		return livresBibliotheque.ToList();
	    }

	    return new List<LivreBibliotheque>();
	}

	/*
	/// <summary>Cherche les objets par leurs propriétés.</summary>
	/// <param name="bookQueries">Objet contenant les champs du livre à chercher.</param>
	/// <param name="quantiteParPage">La quantité d'objets que l'on veut afficher par page.</param>
	/// <param name="page">Le numéro de page des résultats.</param>
	/// <returns>Une liste d'objets ayant les propriétés désirées ou null s'il n'y en a pas.</returns>
	//public ICollection<LivreBibliotheque> SearchByProperties(dynamic livreChampsRecherche, int quantiteParPage = 20, int page = 0)
	*/

	/*
	/// <summary>
	/// </summary>
	private Predicate<LivreBibliotheque> GetCorrectEtatPredicate(LivreChampsRecherche livreChampRecherche)
	{

	    Predicate<LivreBibliotheque> predicate;

	    if(livreChampRecherche.Neuf && livreChampRecherche.Digital && livreChampRecherche.Usage)
	    {

		predicate = PredicateEtatNeufDigitalUsage();
	    }
	    else if(livreChampRecherche.Neuf && livreChampRecherche.Digital)
	    {

		predicate = PredicateEtatNeufDigital();
	    }
	    else if(livreChampRecherche.Neuf && livreChampRecherche.Usage)
	    {

		predicate = PredicateEtatNeufUsage();
	    }
	    else if(livreChampRecherche.Neuf)
	    {

		predicate = PredicateEtatNeuf();
	    }
	    else if(livreChampRecherche.Digital)
	    {

		predicate = PredicateEtatDigital();
	    }
	    else if(livreChampRecherche.Digital && livreChampRecherche.Usage)
	    {

		predicate = PredicateEtatDigitalUsage();
	    }
	    else if(livreChampRecherche.Usage)
	    {

		predicate = PredicateEtatNeuf();
		// TODO: predicate = PredicateEtatUsage();
	    }
	    // TODO: same as new
	    else
	    {

		predicate = PredicateEtatNeuf();
	    }

	    return predicate;
	}

	/// <summary>
	/// Construit le prédicat pour chercher les livres neufs, digitals et usagés.
	/// </summary>
	private Predicate<LivreBibliotheque> PredicateEtatNeufDigitalUsage()
	{

	    Predicate<LivreBibliotheque> predicate;

	    predicate = new(WhereBookEtatNew && WhereBookEtatDigital && WhereBookEtatUsage);

	    return predicate;
	}

	/// <summary>
	/// Construit le prédicat pour chercher les livres neufs et digitals.
	/// </summary>
	private Predicate<LivreBibliotheque> PredicateEtatNeufDigital()
	{

	    Predicate<LivreBibliotheque> predicate;

	    predicate = new(WhereBookEtatNew && WhereBookEtatDigital);

	    return predicate;
	}

	/// <summary>
	/// Construit le prédicat pour chercher les livres neufs et usagés.
	/// </summary>
	private Predicate<LivreBibliotheque> PredicateEtatNeufUsage()
	{

	    Predicate<LivreBibliotheque> predicate;

	    predicate = new(WhereBookEtatNew && WhereBookEtatUsage);

	    return predicate;
	}

	/// <summary>
	/// Construit le prédicat pour chercher les livres digitals et usagés.
	/// </summary>
	private Predicate<LivreBibliotheque> PredicateEtatDigitalUsage()
	{

	    Predicate<LivreBibliotheque> predicate;

	    predicate = new(WhereBookEtatDigital && WhereBookEtatUsage);

	    return predicate;
	}

	/// <summary>
	/// Construit le prédicat pour chercher les livres neufs.
	/// </summary>
	private Predicate<LivreBibliotheque> PredicateEtatNeuf()
	{
	    Predicate<LivreBibliotheque> predicate;

	    predicate = new(WhereBookEtatNew);

	    return predicate;
	}

	/// <summary>
	/// Construit le prédicat pour chercher les livres digitals.
	/// </summary>
	private Predicate<LivreBibliotheque> PredicateEtatDigital()
	{
	    Predicate<LivreBibliotheque> predicate;

	    predicate = new(WhereBookEtatDigital);

	    return predicate;
	}
	*/

	/// <summary>
	/// Construit le prédicat pour chercher les livres usagés.
	/// </summary>
	private bool PredicateEtatUsage(PrixEtatLivre prixEtatLivre)
	{
	    Predicate<PrixEtatLivre> predicate;

	    predicate = new Predicate<PrixEtatLivre>(WhereBookEtatUsage);

	    return predicate.Invoke(prixEtatLivre);
	}

	/// <summary>
	/// Condition pour chercher un prix état livre correspondant à l'état désiré.
	/// <param name="prixEtatLivre">L'objet PrixEtatLivre à vérifier.</param>
	/// <param name="etatLivre">L'état du livre désiré.</param>
	/// </summary>
	private bool WhereBookEtat(PrixEtatLivre prixEtatLivre, string etatLivre)
	{

	    return prixEtatLivre.EtatLivre.Nom.Equals(etatLivre);
	}

	/// <summary>
	/// Construit un prédicat pour chercher les livres qui sont en format neuf.
	/// </summary>
	private bool WhereBookEtatNew(PrixEtatLivre prixEtatLivre)
	{

	    return WhereBookEtat(prixEtatLivre, NomEtatLivre.Neuf);
	}

	/// <summary>
	/// Construit un prédicat pour chercher les livres qui sont en format digital.
	/// </summary>
	private bool WhereBookEtatDigital(PrixEtatLivre prixEtatLivre)
	{

	    return WhereBookEtat(prixEtatLivre, NomEtatLivre.Numerique);
	}

	/// <summary>
	/// Construit un prédicat pour chercher les livres qui sont en format usagé.
	/// </summary>
	private bool WhereBookEtatUsage(PrixEtatLivre prixEtatLivre)
	{

	    return WhereBookEtat(prixEtatLivre, NomEtatLivre.Usagee);
	}
    }
}
