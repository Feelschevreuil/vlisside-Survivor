using Xunit;
using System;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque_tests.Data;
using System.Collections.Generic;
using System.Linq;

namespace vlissides_bibliotheque_tests.DAO.Tests
{

    /// Classe <c>LivresBibliothequeDAOTests</c> qui effectue des tests concernant le DAO
    /// <c>LivresBibliothequeDAO</c> du projet.
    public class LivresBibliothequeDAOTests
    {

	private ApplicationDbContext _context;
	private LivresBibliothequeDAO _livresBibliothequeDao;

	/// <summary>
	/// Constructeur surchagé.
	/// </summary>
	public LivresBibliothequeDAOTests()
	{

	    _context = DbContextUtils.GetInMemoryDb();
	    _livresBibliothequeDao = new LivresBibliothequeDAO(_context);
	}

	[Fact]
	public void TestSave()
	{
	    
	    bool livreSauvegarde;
	    LivreBibliotheque livreBibliothequeAjouter;
	    MaisonEdition maisonEdition;

	    maisonEdition = new()
	    {
		Nom = "BitchX - irc"
	    };

	    _context.MaisonsEditions.Add(maisonEdition);
	    _context.SaveChanges();

	    livreBibliothequeAjouter = new()
	    {
		Titre = "42",
		DatePublication = DateTime.Now,
		Isbn = "6663XX666X",
		MaisonEdition = maisonEdition,
		PhotoCouverture = "N/A",
		Resume = "The meaning of life"
	    };

	    livreSauvegarde = _livresBibliothequeDao.Save(livreBibliothequeAjouter);

	    Assert.True(livreSauvegarde);
	}

	[Fact]
	public void TestGet()
	{

	    LivreBibliotheque livreBibliothequeAjouter;
	    LivreBibliotheque livreBibliothequeRetourne;
	    MaisonEdition maisonEdition;

	    maisonEdition = new()
	    {
		Nom = "PornHub"
	    };

	    _context.MaisonsEditions.Add(maisonEdition);
	    _context.SaveChanges();

	    livreBibliothequeAjouter = new()
	    {
		Titre = "Big booty bitches",
		DatePublication = DateTime.Now,
		Isbn = "666XXX666X",
		MaisonEdition = maisonEdition,
		PhotoCouverture = "N/A",
		Resume = "The meaning of iDubbbz triple B's"
	    };

	    _livresBibliothequeDao.Save(livreBibliothequeAjouter);

	    livreBibliothequeRetourne = _livresBibliothequeDao
		.Get(livreBibliothequeAjouter.LivreId);

	    Assert
		.Equal
		(
		    livreBibliothequeAjouter.LivreId, 
		    livreBibliothequeRetourne.LivreId
		);
	}

	[Fact]
	public void TestGetAll()
	{

	    LivreBibliotheque livreBibliothequeAjouter0;
	    LivreBibliotheque livreBibliothequeAjouter1;
	    MaisonEdition maisonEdition;
	    int nombreEnregistrements;
	    int nombreEnregistrementsDeBase;

	    nombreEnregistrementsDeBase = 2;

	    ViderTable();

	    maisonEdition = new()
	    {
		Nom = "PornHub"
	    };

	    _context.MaisonsEditions.Add(maisonEdition);
	    _context.SaveChanges();

	    livreBibliothequeAjouter0 = new()
	    {
		Titre = "42",
		DatePublication = DateTime.Now,
		Isbn = "6663XX666X",
		MaisonEdition = maisonEdition,
		PhotoCouverture = "N/A",
		Resume = "The meaning of life"
	    };

	    livreBibliothequeAjouter1 = new()
	    {
		Titre = "Big booty bitches",
		DatePublication = DateTime.Now,
		Isbn = "666XXX666X",
		MaisonEdition = maisonEdition,
		PhotoCouverture = "N/A",
		Resume = "The meaning of iDubbbz triple B's"
	    };

	    _livresBibliothequeDao.Save(livreBibliothequeAjouter0);
	    _livresBibliothequeDao.Save(livreBibliothequeAjouter1);

	    nombreEnregistrements = _livresBibliothequeDao.GetAll().Count();

	    Assert
		.Equal
		(
		    nombreEnregistrementsDeBase, 
		    nombreEnregistrements
		);
	}

	[Fact]
	public void TestUpdate()
	{

	    throw new NotImplementedException("Not implemented");
	}

	[Fact]
	public void TestDelete()
	{

	    int nombreEnregistrementsInitial;
	    int nombreEnregistrementsApresEffacer;
	    LivreBibliotheque livreBibliotheque;

	    AjouterLivres(10);

	    nombreEnregistrementsInitial = _livresBibliothequeDao.GetAll().Count();

	    livreBibliotheque = _livresBibliothequeDao.GetAll().FirstOrDefault();

	    _livresBibliothequeDao.Delete(livreBibliotheque.LivreId);

	    nombreEnregistrementsApresEffacer = _livresBibliothequeDao.GetAll().Count();

	    Assert
		.True
		(
		    nombreEnregistrementsInitial > nombreEnregistrementsApresEffacer
		);
	}

	[Fact]
	public void TestGetSuggestions()
	{

	    IEnumerable<string> resultats;
	    LivreBibliotheque livreBibliothequeAjouter;
	    MaisonEdition maisonEdition;
	    int nombreSuggestions;
	    string titreLivre;
	    string recherche;

	    ViderTable();
	    AjouterLivres(10);

	    maisonEdition = new()
	    {
		Nom = "PornHub"
	    };

	    _context.MaisonsEditions.Add(maisonEdition);
	    _context.SaveChanges();

	    nombreSuggestions = 1;
	    titreLivre = "Big booty bitches";
	    recherche = "booty";

	    livreBibliothequeAjouter = new()
	    {
		Titre = titreLivre,
		DatePublication = DateTime.Now,
		Isbn = "666XXX666X",
		MaisonEdition = maisonEdition,
		PhotoCouverture = "N/A",
		Resume = "The meaning of iDubbbz triple B's"
	    };

	    _livresBibliothequeDao.Save(livreBibliothequeAjouter);

	    resultats = _livresBibliothequeDao.GetSuggestions(recherche);

	    Assert
		.True
		(
		    (resultats.Count() == nombreSuggestions) && 
		    string
			.Equals
			(
			    resultats.FirstOrDefault(), 
			    titreLivre
			)
		);

	    nombreSuggestions = 2;

	    livreBibliothequeAjouter = new()
	    {
		Titre = "booty bitches",
		DatePublication = DateTime.Now,
		Isbn = "666XXX666X",
		MaisonEdition = maisonEdition,
		PhotoCouverture = "N/A",
		Resume = "The meaning of iDubbbz triple B's"
	    };

	    _livresBibliothequeDao.Save(livreBibliothequeAjouter);

	    resultats = _livresBibliothequeDao.GetSuggestions(recherche);

	    Assert
		.Equal
		(
		    resultats.Count(),
		    nombreSuggestions
		);
	}

	[Fact]
	public void TestGetSelonPropriete()
	{

	    IEnumerable<LivreBibliotheque> resultats;
	    LivreBibliotheque livreBibliothequeAjouter;
	    MaisonEdition maisonEdition;
	    int nombreResultats;
	    string titreLivre;
	    string recherche;

	    ViderTable();
	    AjouterLivres(10);

	    maisonEdition = new()
	    {
		Nom = "PornHub"
	    };

	    _context.MaisonsEditions.Add(maisonEdition);
	    _context.SaveChanges();

	    nombreResultats = 1;
	    titreLivre = "Big booty bitches";
	    recherche = "booty";

	    livreBibliothequeAjouter = new()
	    {
		Titre = titreLivre,
		DatePublication = DateTime.Now,
		Isbn = "666XXX666X",
		MaisonEdition = maisonEdition,
		PhotoCouverture = "N/A",
		Resume = "The meaning of iDubbbz triple B's"
	    };

	    _livresBibliothequeDao.Save(livreBibliothequeAjouter);

	    resultats = _livresBibliothequeDao.GetSelonPropriete(recherche);

	    Assert
		.True
		(
		    (resultats.Count() == nombreResultats) && 
		    string
			.Equals
			(
			    resultats.FirstOrDefault().Titre, 
			    titreLivre
			)
		);

	    nombreResultats = 2;

	    livreBibliothequeAjouter = new()
	    {
		Titre = "booty bitches",
		DatePublication = DateTime.Now,
		Isbn = "666XXX666X",
		MaisonEdition = maisonEdition,
		PhotoCouverture = "N/A",
		Resume = "The meaning of iDubbbz triple B's"
	    };

	    _livresBibliothequeDao.Save(livreBibliothequeAjouter);

	    resultats = _livresBibliothequeDao.GetSelonPropriete(recherche);

	    Assert
		.Equal
		(
		    nombreResultats,
		    resultats.Count()
		);
	}

	[Fact]
	public void TestGetSelonProprietes()
	{

	    throw new NotImplementedException("Not implemented");
	}

	/// <summary>
	/// Ajoute N nombre de livres à la base de données.
	/// </summary>
	/// <param name="nombreLivresAjouter">Nombre de livres à ajouter.</param>
	private void AjouterLivres(int nombreLivresAjouter = 10)
	{

	    List<LivreBibliotheque> livresBiliotheque;

	    livresBiliotheque = new();

	    for(int i = 0; i < nombreLivresAjouter; i++)
	    {

		MaisonEdition maisonEdition;
		LivreBibliotheque livreBibliotheque;

		maisonEdition = new()
		{
		    Nom = Faker.Company.Name()
		};

		_context.MaisonsEditions.Add(maisonEdition);
		_context.SaveChanges();

		livreBibliotheque = new()
		{
		    Titre = Faker.Company.CatchPhrase(),
		    DatePublication = Faker.Identification.DateOfBirth(),
		    Isbn = 666 + Faker.Identification.UkNhsNumber(),
		    MaisonEdition = maisonEdition,
		    PhotoCouverture = "N/A",
		    Resume = Faker.Lorem.Sentence()
		};
	    }

	    _context.LivresBibliotheque.AddRange(livresBiliotheque);
	    _context.SaveChanges();
	}

	/// <summary>
	/// Vide la table des livres de la bibliothèque.
	/// </summary>
	private void ViderTable()
	{

	    _context.LivresBibliotheque.RemoveRange(_livresBibliothequeDao.GetAll());
	}
    }
}
