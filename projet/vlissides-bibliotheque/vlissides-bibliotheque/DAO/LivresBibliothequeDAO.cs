using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Constantes;
using vlissides_bibliotheque.Extentions;
using System.Linq;
using System.Linq.Expressions;

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
		Expression<Func<PrixEtatLivre, bool>> etatDesire;
		int quantiteASauter;

		quantiteASauter = DAOUtils.GetQuantityOfElementsToSkip(quantiteParPage, page); 
		etatDesire = GetExpressionSelonEtatLivreDesire(livreChampsRecherche);

		livresBibliotheque = _context
		    .LivresBibliotheque
		    .If
		    (
			!string.IsNullOrEmpty(livreChampsRecherche.Isbn) && livreChampsRecherche.IsbnQueryValid(),
			livres =>
			    livres
				.Where
				(
				    livre =>
					livre.Isbn
					    .ContainsCaseInsensitive(livreChampsRecherche.Isbn)
				)
		    )
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
		    .If
		    (
			livreChampsRecherche.DatePublication != default(DateTime),
			livres => livres
			    .Where
			    (
				livre =>
				    DateTime.Compare(livre.DatePublication, livreChampsRecherche.DatePublication) == 0
			    )
		    )
		    .If
		    (
			livreChampsRecherche.DatePublication == default(DateTime) && livreChampsRecherche.DatePublicationMinimum != default(DateTime),
			livres => livres
			    .Where
			    (
				livre =>
				    DateTime.Compare(livre.DatePublication, livreChampsRecherche.DatePublicationMinimum) > 0
			    )
		    )
		    .If
		    (
			livreChampsRecherche.DatePublication == default(DateTime) && livreChampsRecherche.DatePublicationMaximale != default(DateTime),
			livres => livres
			    .Where
			    (
				livre =>
				    DateTime.Compare(livre.DatePublication, livreChampsRecherche.DatePublicationMaximale) < 0
			    )
		    )
		    // TODO: utiliser le DAO lorsqu'implementé (temp: livre usagés only), prix minimum and usage
		    .If
		    (
			livreChampsRecherche.ChercheAvecPrixMinimum() && !livreChampsRecherche.ChercheAvecPrixMaximum(),
			livres => livres
			    .Where
			    (
				livre =>
				    _context
					.PrixEtatsLivres
					.Where
					(
					    prixEtatLivre =>
						prixEtatLivre.Prix >= livreChampsRecherche
						    .PrixMinimum
					)
					.Where
					(
					    etatDesire
					)
					.Select
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque
					)
					.Contains
					(
					    livre
					)
			    )
		    )
		    // TODO: utiliser le DAO lorsqu'implementé (temp: livre usagés only), prix minimum and usage
		    .If
		    (
			livreChampsRecherche.ChercheAvecPrixMaximum() && !livreChampsRecherche.ChercheAvecPrixMinimum(),
			livres => livres
			    .Where
			    (
				livre =>
				    _context
					.PrixEtatsLivres
					.Where
					(
					    prixEtatLivre =>
						prixEtatLivre.Prix <= livreChampsRecherche
						    .PrixMaximum
					)
					.Where
					(
					    etatDesire
					)
					.Select
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque
					)
					.Contains
					(
					    livre
					)
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
				    _context
					.PrixEtatsLivres
					.Where
					(
					    prixEtatLivre =>
						prixEtatLivre.Prix <= livreChampsRecherche
						    .PrixMaximum &&
						prixEtatLivre.Prix >= livreChampsRecherche
						    .PrixMinimum
						    
					)
					.Where
					(
					    etatDesire
					)
					.Select
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque
					)
					.Contains
					(
					    livre
					)
			    )
		    )
		    // TODO: utiliser le DAO lorsqu'implementé (temp: livre usagés only), prix minimum and usage
		    .If
		    (
			livreChampsRecherche.AucuneRestrictionPrix(),
			livres => livres
			    .Where
			    (
				livre =>
				    _context
					.PrixEtatsLivres
					.Where
					(
					    etatDesire
					)
					.Select
					(
					    prixEtatLivre =>
						prixEtatLivre.LivreBibliotheque
					)
					.Contains
					(
					    livre
					)
			    )
		    )
		    .If
		    (
			livreChampsRecherche.ProgrammesEtudeId != null && 
			    livreChampsRecherche.ProgrammesEtudeId.Count() > 0,
			livres => livres
			    .Where
			    ( 
				livre =>
				    _context
					.CoursLivres
					    .Where
					    (
						coursLivre =>
						    livreChampsRecherche
							.ProgrammesEtudeId
							.Contains
							(
							    coursLivre
								.Cours
								    .ProgrammeEtude
									.ProgrammeEtudeId
							)
					    )
					    .Select
					    (
						coursLivre =>
						    coursLivre.LivreBibliotheque
					    )
					    .Contains
					    (
						livre
					    )
			    )
		    )
		    // TODO: test unitaire
		    .If
		    (
			livreChampsRecherche.CoursId != null && 
			    livreChampsRecherche.CoursId.Count() > 0,
			livres => livres
			    .Where
			    ( 
				livre =>
				    _context
					.CoursLivres
					    .Where
					    (
						coursLivre =>
						    livreChampsRecherche
							.CoursId
							.Contains
							(
							    coursLivre
								.CoursId
							)
					    )
					    .Select
					    (
						coursLivre =>
						    coursLivre.LivreBibliotheque
					    )
					    .Contains
					    (
						livre
					    )
			    )
		    )
		    // TODO: par prof. (penser à la possibilité d'une expression linq pour réutiliser en haut pour le filtre par cours
		    /*
		    .If
		    (
			livreChampsRecherche.ProfesseursId != null &&
			    livreChampsRecherche.ProfesseursId.Count() > 0,
			// TODO: implement
		    )
		    */
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

	/// <summary>
	/// Construit l'expression linq pour chercher
	/// les livres selon l'état désiré.
	/// Confirme que le livre est dans l'état désiré.
	/// </summary>
	/// <param name="livreChampsRecherche">
	/// Chapms de recherche contenant l'état du livre désiré.
	/// </param>
	private Expression<Func<PrixEtatLivre, bool>> GetExpressionSelonEtatLivreDesire
	(
	    LivreChampsRecherche livreChampsRecherche
	)
	{

	    Expression<Func<PrixEtatLivre, bool>> expressionEtatLivreDesire;
	    EtatLivre etatLivreNeuf;
	    EtatLivre etatLivreDigital;
	    EtatLivre etatLivreUsage;

	    etatLivreNeuf = GetEtatLivreSelonNom(NomEtatLivre.Neuf);
	    etatLivreDigital = GetEtatLivreSelonNom(NomEtatLivre.Numerique);
	    etatLivreUsage = GetEtatLivreSelonNom(NomEtatLivre.Usagee);

	    if(livreChampsRecherche.Neuf && livreChampsRecherche.Digital && livreChampsRecherche.Usage)
	    {

		expressionEtatLivreDesire = prixEtatLivre =>
		    prixEtatLivre.EtatLivre == etatLivreNeuf ||
		    prixEtatLivre.EtatLivre == etatLivreDigital ||
		    prixEtatLivre.EtatLivre == etatLivreUsage;
	    }
	    else if(livreChampsRecherche.Neuf && livreChampsRecherche.Digital)
	    {

		expressionEtatLivreDesire = prixEtatLivre =>
		    prixEtatLivre.EtatLivre == etatLivreNeuf ||
		    prixEtatLivre.EtatLivre == etatLivreDigital;
	    }
	    else if(livreChampsRecherche.Usage && livreChampsRecherche.Digital)
	    {

		expressionEtatLivreDesire = prixEtatLivre =>
		    prixEtatLivre.EtatLivre == etatLivreDigital ||
		    prixEtatLivre.EtatLivre == etatLivreUsage;
	    }
	    else if(livreChampsRecherche.Neuf && livreChampsRecherche.Usage)
	    {

		expressionEtatLivreDesire = prixEtatLivre =>
		    prixEtatLivre.EtatLivre == etatLivreNeuf ||
		    prixEtatLivre.EtatLivre == etatLivreUsage;
	    }
	    else if(livreChampsRecherche.Usage)
	    {

		expressionEtatLivreDesire = prixEtatLivre =>
		    prixEtatLivre.EtatLivre == etatLivreUsage;
	    }
	    else if(livreChampsRecherche.Digital)
	    {

		expressionEtatLivreDesire = prixEtatLivre =>
		    prixEtatLivre.EtatLivre == etatLivreDigital;
	    }
	    else
	    {

		expressionEtatLivreDesire = prixEtatLivre =>
		    prixEtatLivre.EtatLivre == etatLivreNeuf;
	    }

	    return expressionEtatLivreDesire;
	}

	// TODO: utiliser le DAO
	/// <summary>
	/// Cherche l'<c>EtatLivre</c> correspondant au nom désiré.
	/// </summary>
	/// <param name="nomEtatLivre">
	/// Nom de l'<c>EtatLivre</c> à aller chercher.
	/// </param>
	private EtatLivre GetEtatLivreSelonNom(string nomEtatLivre)
	{

	    EtatLivre etatLivre;

	    etatLivre = _context
		.EtatsLivres
		    .Where
		    (
			etatLivre =>
			    String.Equals(etatLivre.Nom, nomEtatLivre)
		    )
		    .FirstOrDefault();

	    return etatLivre;
	}

    }
}
