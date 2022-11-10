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
	public void TestGetSelonMaisonEdition()
	{

	    string nomMaisonEdition;
	    string nomMaisonEditionQuery;
	    int nombreLivresAvecMaisonEditionDesire;
	    int nombreLivresSelonRecherche;
	    MaisonEdition maisonEdition;
	    LivreChampsRecherche livreChampsRecherche;

	    ViderTable();

	    nomMaisonEdition = "AAT triple ex";
	    nomMaisonEditionQuery = "TrIPle Ex";
	    maisonEdition = CreateMaisonEdition(nomMaisonEdition);
	    nombreLivresAvecMaisonEditionDesire = 10;

	    AjouterLivres(nombreLivresAvecMaisonEditionDesire, maisonEdition);
	    AjouterLivres(20);

	    livreChampsRecherche = new()
	    {
		MaisonEdition = nomMaisonEditionQuery,
		Neuf = true,
		Digital = true,
		Usage = true
	    };

	    nombreLivresSelonRecherche = _livresBibliothequeDao
		.GetSelonProprietes(livreChampsRecherche)
		.Count();

	    Assert
		.Equal
		(
		    nombreLivresAvecMaisonEditionDesire,
		    nombreLivresSelonRecherche
		);
	}

	[Fact]
	public void TestGetSelonAuteur()
	{

	    string nomAuteurQuery;
	    int nombreLivresAvecAuteurDesire;
	    int nombreLivresSelonRecherche;
	    Auteur auteur;
	    LivreChampsRecherche livreChampsRecherche;

	    ViderTable();

	    nomAuteurQuery = "khAlif";
	    auteur = CreateAuteur("Khalifa", "Mia");
	    nombreLivresAvecAuteurDesire = 10;

	    AjouterLivres(nombreLivresAvecAuteurDesire, null, default(DateTime), auteur);
	    AjouterLivres(20);

	    livreChampsRecherche = new()
	    {
		Auteur = nomAuteurQuery,
		Neuf = true,
		Digital = true,
		Usage = true
	    };

	    nombreLivresSelonRecherche = _livresBibliothequeDao
		.GetSelonProprietes(livreChampsRecherche)
		.Count();

	    Assert
		.Equal
		(
		    nombreLivresAvecAuteurDesire,
		    nombreLivresSelonRecherche
		);
	}

	/// <summary>
	/// Ajoute N nombre de livres à la base de données.
	/// </summary>
	/// <param name="nombreLivresAjouter">Nombre de livres à ajouter.</param>
	/// <param name="maisonEditionFixe">
	/// Maison d'Édition fixe dans le cas où on veut ajouter plusieurs livres
	/// aléatoires publiés par la même maison d'édition. Si omis,
	/// une maison d'édition aléatoire sera ajoutée et assignée par livre.
	/// </param>
	/// <param name="dateFixe">
	/// Date fixe dans le cas où on veut ajouter plusieurs livres aléatoires
	/// ayant étés faits à une data particulière. Si omis, une date aléatoire
	/// sera aojutée et assignée par livre.
	/// </param>
	/// <param name="auteurFixe">
	/// Auteur fixe dans le cas où on veut ajouter plusieurs livres aléatoires
	/// ayant été écrits par le même auteur. Si omis, un auteur aléatoire
	/// sera ajouté et assigné par livre.
	/// <param name="prixMaximum">
	/// Prix maximum fixe dans le cas où on veut ajouter plusieurs livres
	/// aléatoires sous un certain prix.
	/// </param>
	/// <param name="prixMinimum">
	/// Prix minimum fixe dans le cas où on veut ajouter plusieurs livres
	/// aléatoires au dessus d'un certain prix.
	/// </param>
	/// <param name="neuf">
	/// État neuf dans le cas où on veut rendre disponible le livre.
	/// </param>
	/// <param name="usage">
	/// État usagé dans le cas où on veut rendre disponible le livre.
	/// </param>
	/// <param name="digital">
	/// État digital dans le cas où on veut rendre disponible le livre.
	/// </param>
	private void AjouterLivres
	(
	    int nombreLivresAjouter = 10,
	    MaisonEdition maisonEditionFixe = null,
	    DateTime dateFixe = new DateTime(),
	    Auteur auteurFixe = null,
	    double prixMaximum = 0.0,
	    double prixMinimum = 0.0,
	    bool neuf = false,
	    bool usage = false,
	    bool digital = false
	)
	{

	    LivreBibliotheque livreBibliotheque;

	    for(int i = 0; i < nombreLivresAjouter; i++)
	    {

		livreBibliotheque = new()
		{
		    Titre = Faker.Company.CatchPhrase(),
		    DatePublication = dateFixe
			.Equals
			(
			    new DateTime()) ? 
				Faker.Identification.DateOfBirth() : 
				dateFixe,
		    Isbn = 666 + Faker.Identification.UkNhsNumber(),
		    MaisonEdition = maisonEditionFixe == null ? 
			CreateMaisonEdition
			(
			    Faker.Company.Name()
			) : 
			maisonEditionFixe,
		    PhotoCouverture = "N/A",
		    Resume = Faker.Lorem.Sentence()
		};

		_context.LivresBibliotheque.Add(livreBibliotheque);
		_context.SaveChanges();

		LierAuteurALivre
		(
		    livreBibliotheque, 
		    auteurFixe == null ? 
			CreateAuteur
			(
			    Faker.Name.Last(), 
			    Faker.Name.First()
			) : 
			auteurFixe
		);
	    }
	}

	/// <summary>
	/// Crée une maison d'édition et la sauvegarde dans la base
	/// de données.
	/// </summary>
	/// <param name="nom">Nom que la maison d'éditon va posséder.</param>
	private MaisonEdition CreateMaisonEdition(string nom)
	{

	    MaisonEdition maisonEdition;

	    maisonEdition = new()
	    {
		Nom = nom
	    };

	    _context.MaisonsEditions.Add(maisonEdition);
	    _context.SaveChanges();

	    return maisonEdition;
	}

	/// <summary>
	/// Crée un auteur et le sauvegarde dans la base de 
	/// données.
	/// </summary>
	/// <param name="nom">Nom de l'auteur.</param>
	/// <param name="prenom">Prénom de l'auteur.</param>
	private Auteur CreateAuteur(string nom, string prenom)
	{

	    Auteur auteur;

	    auteur = new()
	    {

		Nom = nom,
		Prenom = prenom
	    };

	    _context.Auteurs.Add(auteur);
	    _context.SaveChanges();

	    return auteur;
	}

	/// <summary>
	/// Lie un livre de bibliothèque à un auteur.
	/// </summary>
	/// <param name="livreBibliotheque">Livre de bibliothèque à lier avec l'auteur.</param>
	/// <param name="auteur">Autuer à lier avec le livre de bibliothèque.</param>
	private void LierAuteurALivre(LivreBibliotheque livreBibliotheque, Auteur auteur)
	{

	    AuteurLivre auteurLivre;

	    auteurLivre = new()
	    {

		Auteur = auteur,
		LivreBibliotheque = livreBibliotheque 
	    };

	    _context.AuteursLivres.Add(auteurLivre);
	    _context.SaveChanges();
	}

	/// <summary>
	/// Lie un livre à un état.
	/// </summary>
	private void LierLivreAEtat
	(
	    LivreBibliotheque livreBibliotheque, 
	    EtatLivre etatLivre, 
	    double prixFixe = 0.0,
	    double prixMaximum = 0.0,
	    double prixMinimum = 0.0 
	)
	{

	    PrixEtatLivre prixEtatLivre;

	    prixEtatLivre = new()
	    {

		EtatLivre = etatLivre,
		LivreBibliotheque = livreBibliotheque,
		Prix = prixFixe != 0.0 ? 
		    prixFixe : 
		    GenererPrix(prixMaximum, prixMinimum)
	    };
	}

	/// <summary>
	/// Génère un prix aléatoire selon les paramètres.
	/// </summary>
	/// <param name="prixMaximum">Limite maximale du prix.</param>
	/// <param name="prixMinimum">Limite miniale du prix.</param>
	private double GenererPrix(double prixMaximum = 0.0, double prixMinimum = 0.0)
	{

	    double prixGenere;

	    prixGenere = Faker.RandomNumber.Next((long)prixMinimum, (long)prixMaximum);

	    return prixGenere;
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
