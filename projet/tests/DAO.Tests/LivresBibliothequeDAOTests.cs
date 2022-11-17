using Xunit;
using System;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.DAO;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque_tests.Data;
using System.Collections.Generic;
using System.Linq;
using vlissides_bibliotheque.Constantes;

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

            List<LivreBibliotheque> livresBibliotheque;
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

            livresBibliotheque = AjouterLivres
            (
                nombreLivresAvecMaisonEditionDesire, 
                maisonEdition
            );

            livresBibliotheque.AddRange(AjouterLivres(20));

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

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

            List<LivreBibliotheque> livresBibliotheque;
            string nomAuteurQuery;
            int nombreLivresAvecAuteurDesire;
            int nombreLivresSelonRecherche;
            Auteur auteur;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            nomAuteurQuery = "khAlif";
            auteur = CreateAuteur("Khalifa", "Mia");
            nombreLivresAvecAuteurDesire = 10;

            livresBibliotheque = AjouterLivres
            (
                nombreLivresAvecAuteurDesire, 
                null, 
                default(DateTime), auteur
            );

            livresBibliotheque.AddRange(AjouterLivres(20));

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

            livreChampsRecherche = new()
            {
                Auteur = nomAuteurQuery,
                Neuf = true
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

        [Fact]
        public void TestGetSelonIsbn()
        {

            List<LivreBibliotheque> livresBibliotheque;
            string isbn;
            string isbnQuery;
            int nombreLivresAvecIsbnDesire;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            isbn = "6666666666969";
            isbnQuery = "6666969";
            nombreLivresAvecIsbnDesire = 1;

            livresBibliotheque = AjouterLivres
            (
                nombreLivresAvecIsbnDesire, 
                null, 
                default(DateTime),
                null,
                isbn
            );

            livresBibliotheque.AddRange(AjouterLivres(10));

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

            livreChampsRecherche = new()
            {
                Isbn = isbn,
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
                    nombreLivresAvecIsbnDesire,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonDatePublicationMinimum()
        {

            List<LivreBibliotheque> livresBibliotheque;
            DateTime dateFixePasse;
            DateTime dateMinimumQuery;
            int nombreLivresAvecDateMinimumDesire;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            dateFixePasse = new DateTime(1996, 12, 31);
            dateMinimumQuery = new DateTime(1995, 10, 5);
            nombreLivresAvecDateMinimumDesire = 10;

            livresBibliotheque = AjouterLivresSelonContrainteDeDate
            (
                dateFixePasse,
                nombreLivresAvecDateMinimumDesire,
                20,
                false,
                1000
            );

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

            livreChampsRecherche = new()
            {
                DatePublicationMinimum = dateMinimumQuery,
                Neuf = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresAvecDateMinimumDesire,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonDatePublicationMaximum()
        {

            List<LivreBibliotheque> livresBibliotheque;
            DateTime dateFixeMaximum;
            DateTime dateMaxiumQuery;
            int nombreLivresAvecDateMaximumDesire;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            dateFixeMaximum = new DateTime(2000, 12, 31);
            dateMaxiumQuery = new DateTime(2002, 10, 5);
            nombreLivresAvecDateMaximumDesire = 10;

            livresBibliotheque = AjouterLivresSelonContrainteDeDate
            (
                dateFixeMaximum,
                nombreLivresAvecDateMaximumDesire,
                20,
                true,
                1000
            );

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

            livreChampsRecherche = new()
            {
                DatePublicationMaximale = dateMaxiumQuery,
                Neuf = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresAvecDateMaximumDesire,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonDatePublicationRange()
        {

            List<LivreBibliotheque> livresBibliotheque;
            DateTime dateFixePasse;
            DateTime dateFixeMaximum;
            DateTime dateMaxiumQuery;
            DateTime dateMinimumQuery;
            int nombreLivresAvecDatesLimites;
            int nombreLivresAvecDateEtendueDesire;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            dateFixeMaximum = new DateTime(2000, 12, 31);
            dateFixePasse = new DateTime(1996, 12, 31);
            dateMinimumQuery = new DateTime(1960, 10, 5);
            dateMaxiumQuery = new DateTime(2002, 10, 5);

            nombreLivresAvecDatesLimites = 10;
            nombreLivresAvecDateEtendueDesire = 20;

            livresBibliotheque = AjouterLivresSelonContrainteDeDate
            (
                dateFixePasse,
                nombreLivresAvecDatesLimites,
                20,
                false,
                10000
            );

            livresBibliotheque.AddRange(
            AjouterLivresSelonContrainteDeDate
            (
                dateFixeMaximum,
                nombreLivresAvecDatesLimites,
                20,
                true,
                10000
            )
            );

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

            livreChampsRecherche = new()
            {
                DatePublicationMinimum = dateMinimumQuery,
                DatePublicationMaximale = dateMaxiumQuery,
                Neuf = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresAvecDateEtendueDesire,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonDatePublicationExacte()
        {
            
            List<LivreBibliotheque> livresBibliotheque;
            DateTime dateExacte;
            DateTime dateMauvaise;
            int nombreLivresAvecDateExate;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            dateExacte = new DateTime(1969, 12, 31);
            dateMauvaise = new DateTime(2005, 05, 10);
            nombreLivresAvecDateExate = 10;

            livresBibliotheque = AjouterLivres
            (
                nombreLivresAvecDateExate, 
                null, 
                dateExacte
            );

            livresBibliotheque.AddRange
            (
                AjouterLivres
                (
                    20, 
                    null, 
                    dateMauvaise
                )
            );

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

            livreChampsRecherche = new()
            {
                DatePublication = dateExacte,
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
                    nombreLivresAvecDateExate,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonPrixMinimum()
        {

            List<LivreBibliotheque> livresBibliotheque;
            double prixMinimum;
            double prixCorrect;
            double prixNonCorrect;
            int nombreLivresAvecPrixMinimum;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            prixMinimum = 10.0;
            prixCorrect = 15.0;
            prixNonCorrect = 5.0;
            nombreLivresAvecPrixMinimum = 10;

            livresBibliotheque = AjouterLivres(nombreLivresAvecPrixMinimum);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque, 
                0.0, 
                0.0, 
                prixCorrect, 
                5
            );

            livresBibliotheque = AjouterLivres(20);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                0.0,
                prixNonCorrect,
                5
            );

            livreChampsRecherche = new()
            {
                PrixMinimum = prixMinimum,
                Usage = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresAvecPrixMinimum,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonPrixMaximum()
        {

            List<LivreBibliotheque> livresBibliotheque;
            double prixMaximum;
            double prixCorrect;
            double prixNonCorrect;
            int nombreLivresAvecPrixMaximum;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            prixMaximum = 15.00;
            prixCorrect = 10.00;
            prixNonCorrect = 20.00;
            nombreLivresAvecPrixMaximum = 10;

            livresBibliotheque = AjouterLivres(nombreLivresAvecPrixMaximum);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                0.0,
                prixCorrect,
                5
            );

            livresBibliotheque = AjouterLivres(20);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                0.0,
                prixNonCorrect,
                5
            );

            livreChampsRecherche = new()
            {
                PrixMaximum = prixMaximum,
                Usage = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresAvecPrixMaximum,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonPrixRange()
        {

            List<LivreBibliotheque> livresBibliotheque;
            double prixMaximum;
            double prixMinimum;
            double prixCorrect;
            double prixNonCorrect;
            double prixNonCorrectTropBas;
            double prixNonCorrectTropHaut;
            int nombreLivresDansEtendue;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            prixMaximum = 20.0;
            prixMinimum = 10.0;
            prixCorrect = 15.0;
            prixNonCorrectTropBas = 5.0;
            prixNonCorrectTropHaut = 25.0;
            nombreLivresDansEtendue = 8;

            livresBibliotheque = AjouterLivres(nombreLivresDansEtendue);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                0.0, 
                prixCorrect,
                5
            );

            livresBibliotheque = AjouterLivres(10);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                0.0,
                prixNonCorrectTropBas,
                5
            );

            livresBibliotheque = AjouterLivres(10);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                0.0,
                prixNonCorrectTropHaut,
                5
            );

            livreChampsRecherche = new()
            {
                PrixMaximum = prixMaximum,
                PrixMinimum = prixMinimum,
                Usage = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();
            
            Assert
                .Equal
                (
                    nombreLivresDansEtendue,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonEtatNeuf()
        {

            List<LivreBibliotheque> livresBibliotheque;
            int nombreLivresNeufs;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            nombreLivresNeufs = 10;

            livresBibliotheque = AjouterLivres(nombreLivresNeufs);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

            livresBibliotheque = AjouterLivres(20);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                10.0,
                10.0,
                5
            );

            livreChampsRecherche = new()
            {
                Neuf = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresNeufs,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonEtatDigital()
        {

            List<LivreBibliotheque> livresBibliotheque;
            int nombreLivresDigitaux;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            nombreLivresDigitaux = 10;

            livresBibliotheque = AjouterLivres(nombreLivresDigitaux);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                10.0
            );

            livresBibliotheque = AjouterLivres(20);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0,
                0.0,
                10.0,
                5
            );

            livreChampsRecherche = new()
            {
                Digital = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresDigitaux,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonEtatUsage()
        {

            List<LivreBibliotheque> livresBibliotheque;
            int nombreLivresUsages;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            nombreLivresUsages = 10;

            livresBibliotheque = AjouterLivres(nombreLivresUsages);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                0.0,
                10.0,
                5
            );

            livresBibliotheque = AjouterLivres(20);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0,
                10.0
            );

            livreChampsRecherche = new()
            {
                Usage = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresUsages,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonEtatNeufEtDigital()
        {

            List<LivreBibliotheque> livresBibliotheque;
            int nombreLivresNeufsEtDigitaux;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            nombreLivresNeufsEtDigitaux = 10;

            livresBibliotheque = AjouterLivres(nombreLivresNeufsEtDigitaux);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0,
                10.0
            );

            livresBibliotheque = AjouterLivres(20);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                0.0,
                10.0,
                5
            );

            livreChampsRecherche = new()
            {
                Neuf = true,
                Digital = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresNeufsEtDigitaux,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonEtatNeufEtUsage()
        {

            List<LivreBibliotheque> livresBibliotheque;
            int nombreLivresNeufsEtUsage;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            nombreLivresNeufsEtUsage = 10;

            livresBibliotheque = AjouterLivres(nombreLivresNeufsEtUsage);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0,
                0.0,
                10.0,
                5
            );

            livresBibliotheque = AjouterLivres(20);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                10.0
            );

            livreChampsRecherche = new()
            {
                Neuf = true,
                Usage = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresNeufsEtUsage,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonEtatDigitalEtUsage()
        {

            List<LivreBibliotheque> livresBibliotheque;
            int nombreLivresDigitauxEtUsages;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            ViderTable();

            nombreLivresDigitauxEtUsages = 10;

            livresBibliotheque = AjouterLivres(nombreLivresDigitauxEtUsages);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                0.0,
                10.0,
                10.0,
                5
            );

            livresBibliotheque = AjouterLivres(20);

            LierPrixEtatLivresAuxLivresPresents
            (
                livresBibliotheque,
                10.0
            );

            livreChampsRecherche = new()
            {
                Digital = true,
                Usage = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresDigitauxEtUsages,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonProgramme()
        {
            
            List<LivreBibliotheque> livresBibliotheque;
            List<int> programmesEtudeId;
            ProgrammeEtude programmeEtude;
            int nombreLivresAvecBonProgramme;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            nombreLivresAvecBonProgramme = 10;
            programmesEtudeId = new();

            programmeEtude = CreateProgrammeEtude("Production vidéo", "6969");
            livresBibliotheque = AjouterLivres(nombreLivresAvecBonProgramme);

            LierProgrammeEtudeAuxLivres(livresBibliotheque, programmeEtude);
            LierPrixEtatLivresAuxLivresPresents(livresBibliotheque, 10.0);

            programmesEtudeId.Add(programmeEtude.ProgrammeEtudeId);

            livresBibliotheque = AjouterLivres(20);
            programmeEtude = CreateProgrammeEtude("Aucun rapport", "6669");

            LierProgrammeEtudeAuxLivres(livresBibliotheque, programmeEtude);
            LierPrixEtatLivresAuxLivresPresents(livresBibliotheque, 10.0);

            livreChampsRecherche = new()
            {
                ProgrammesEtudeId = programmesEtudeId,
                Neuf = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresAvecBonProgramme,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestGetSelonCours()
        {

            List<LivreBibliotheque> livresBibliotheque;
            List<int> programmesEtudeId;
            List<int> coursId;
            Cours cours;
            ProgrammeEtude programmeEtude;
            int nombreLivresAvecBonCours;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            nombreLivresAvecBonCours = 10;
            programmesEtudeId = new();
            coursId = new();

            programmeEtude = CreateProgrammeEtude("Arts", "69696969");
            cours = CreateCours(programmeEtude, "Cinéma", "6969");
            livresBibliotheque = AjouterLivres(nombreLivresAvecBonCours);

            LierCoursAuxLivres(livresBibliotheque, cours);
            LierPrixEtatLivresAuxLivresPresents(livresBibliotheque, 10.0);

            programmesEtudeId.Add(programmeEtude.ProgrammeEtudeId);
            coursId.Add(cours.CoursId);

            programmeEtude = CreateProgrammeEtude("Autre", "66666666");
            cours = CreateCours(programmeEtude, "Sports", "6666");
            livresBibliotheque = AjouterLivres(20);

            LierCoursAuxLivres(livresBibliotheque, cours);
            LierPrixEtatLivresAuxLivresPresents(livresBibliotheque, 10.0);
            
            livreChampsRecherche = new()
            {
                ProgrammesEtudeId = programmesEtudeId,
                CoursId = coursId,
                Neuf = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresAvecBonCours,
                    nombreLivresSelonRecherche
                );
        }

        [Fact]
        public void TestSelonProfesseur()
        {

            List<LivreBibliotheque> livresBibliotheque;
            List<int> coursId;
            List<int> programmesEtudeId;
            List<int> professeursId;
            Cours cours;
            Professeur professeur;
            ProgrammeEtude programmeEtude;
            int nombreLivresAvecBonProfesseur;
            int nombreLivresSelonRecherche;
            LivreChampsRecherche livreChampsRecherche;

            nombreLivresAvecBonProfesseur = 10;
            programmesEtudeId = new();
            coursId = new();
            professeursId = new();

            professeur = CreateProfesseur("Mia", "Khalifa");
            programmeEtude = CreateProgrammeEtude("Arts visuels", "69696969");
            cours = CreateCours(programmeEtude, "Production vidéo", "6969");
            livresBibliotheque = AjouterLivres(nombreLivresAvecBonProfesseur);

            LierCoursAuxLivres(livresBibliotheque, cours);
            LierCoursAuProfesseur(cours, professeur);
            LierPrixEtatLivresAuxLivresPresents(livresBibliotheque, 10.0);

            programmesEtudeId.Add(programmeEtude.ProgrammeEtudeId);
            coursId.Add(cours.CoursId);
            professeursId.Add(professeur.ProfesseurId);

            professeur = CreateProfesseur("John", "Doe");
            programmeEtude = CreateProgrammeEtude("Sports", "66666666");
            cours = CreateCours(programmeEtude, "Plein air", "6666");
            livresBibliotheque = AjouterLivres(20);

            LierCoursAuxLivres(livresBibliotheque, cours);
            LierCoursAuProfesseur(cours, professeur);
            LierPrixEtatLivresAuxLivresPresents(livresBibliotheque, 10.0);

            livreChampsRecherche = new()
            {
                ProgrammesEtudeId = programmesEtudeId,
                CoursId = coursId,
                ProfesseursId = professeursId,
                Neuf = true
            };

            nombreLivresSelonRecherche = _livresBibliothequeDao
                .GetSelonProprietes(livreChampsRecherche)
                .Count();

            Assert
                .Equal
                (
                    nombreLivresAvecBonProfesseur,
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
        /// <param name="IsbnFixe">
        /// Isbn fixe à mettre sur un livre. Si omis, un isbn aléatoire sera
        /// assigné par livre.
        /// </param>
        private List<LivreBibliotheque> AjouterLivres
        (
            int nombreLivresAjouter = 10,
            MaisonEdition maisonEditionFixe = null,
            DateTime dateFixe = new DateTime(),
            Auteur auteurFixe = null,
            string IsbnFixe = null
        )
        {

            List<LivreBibliotheque> livresBibliotheque;
            LivreBibliotheque livreBibliotheque;

            livresBibliotheque = new List<LivreBibliotheque>();

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
                    Isbn = !string.IsNullOrEmpty(IsbnFixe) ?  
                        IsbnFixe :
                        666 + Faker.Identification.UkNhsNumber(),
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
                livresBibliotheque.Add(livreBibliotheque);

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

            return livresBibliotheque;
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
        /// Crée un cours et le sauvegarde dans la base de 
        /// données.
        /// </summary>
        /// <param name="nom">
        /// Nom du cours.
        /// </param>
        /// <param name="code">
        /// Code du cours.
        /// </param>
        private Cours CreateCours
        (
            ProgrammeEtude programmeEtude, 
            string nom, 
            string code
        )
        {
            
            Cours cours;

            cours = new()
            {
                Nom = nom,
                Code = code,
                Description = "foobar",
                ProgrammeEtude = programmeEtude
            };

            _context.Cours.Add(cours);

            _context.SaveChanges();

            return cours;
        }

        /// <summary>
        /// Crée un programme d'étude et le sauvegarde dans la base
        /// de données.
        /// </summary>
        /// <param name="nom">Nom du programme d'étude</param>
        /// <param name="code">Code du programme d'étude</param>
        private ProgrammeEtude CreateProgrammeEtude(string nom, string code)
        {

            ProgrammeEtude programmeEtude;

            programmeEtude = new()
            {
                Nom = nom,
                Code = code
            };

            _context.ProgrammesEtudes.Add(programmeEtude);

            _context.SaveChanges();

            return programmeEtude;
        }

        /// <summary>
        /// Crée un professeur et le sauvegarde dans la base de 
        /// données.
        /// </summary>
        /// <param name="nom">Nom du professeur.</param>
        /// <param name="prenom">Prénom du professeur.</param>
        private Professeur CreateProfesseur(string nom, string prenom)
        {

            Professeur professeur;

            professeur = new()
            {
                Nom = nom,
                Prenom = prenom,
            };

            _context.Professeurs.Add(professeur);
            
            _context.SaveChanges();

            return professeur;
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
        /// Lie un livre de bibliothèque à un programme d'étude. 
        /// </summary>
        /// <param name="livresBibliotheque">
        /// Livres de bibliothèque à lier avec le programme d'étude.
        /// </param>
        /// <param name="programmeEtude">
        /// Programme d'étude à lier avec le livre bibliothèque.
        /// </param>
        private void LierProgrammeEtudeAuxLivres
        (
            List<LivreBibliotheque> livresBibliotheque,
            ProgrammeEtude programmeEtude
        )
        {

            List<CoursLivre> coursLivres;
            Cours cours;

            cours = new()
            {
                AnneeParcours = 1,
                Nom = "introduction à la pornographie",
                Description = "You know who you are",
                Code = "69696969",
                ProgrammeEtude = programmeEtude
            };

            _context
                .Cours
                .Add(cours);

            _context.SaveChanges();

            coursLivres = new();

            foreach(LivreBibliotheque livreBibliotheque in livresBibliotheque)
            {

                coursLivres.Add
                (
                    new CoursLivre()
                    {
                        Complementaire = false,
                        Cours = cours,
                        LivreBibliotheque = livreBibliotheque
                    }
                );
            }

            _context
                .CoursLivres
                .AddRange(coursLivres);

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

            _context.PrixEtatsLivres.Add(prixEtatLivre);
            _context.SaveChanges();
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
        /// Ajoute des livres ayant une date maximale.
        /// </summary>
        /// <param name="dateLimite">
        /// Date limite que les livres doivent respecter.
        /// </param>
        /// <param name="nombreLivresRespectantLimite">
        /// Nombre de livres à ajouter qui respectent la limite.
        /// </param>
        /// <param name="nombreLivresNonRespectantLimite">
        /// Nombre de livres à ajouter qui ne respectent pas la limite.
        /// </param>
        /// <param name="maximale">
        /// Indique si c'est la date maximale à respecter. Si omis, la date
        /// indiquée sera la data minimale à respecter.
        /// </param>
        /// <param name="differenceEntreDates">
        /// Spécifie la différence à ajouter ou à enlever sur la date limite en jours.
        ///
        /// Si on spécifie 500 à une génération de livres avec une date limite, les
        /// livres qui respecterons la limite de date seront 500 jours en dessous et ceux
        /// qui ne respecterons pas la limite, seront 500 jours au dessus.
        /// </param>
        private List<LivreBibliotheque> AjouterLivresSelonContrainteDeDate
        (
            DateTime dateLimite,
            int nombreLivresRespectantLimite,
            int nombreLivresNonRespectantLimite,
            bool maximale = false,
            int differenceEntreDates = 666
        )
        {

            List<LivreBibliotheque> livresBibliotheque;
            TimeSpan timeSpanRespectantLimite;
            TimeSpan timeSpanNonRespectantLimite;
            DateTime dateCorrecte;
            DateTime dateNonCorrecte;

            timeSpanRespectantLimite = TimeSpan
                .FromDays
                (
                    maximale ? 
                    -differenceEntreDates: 
                    differenceEntreDates
                );

            timeSpanNonRespectantLimite = TimeSpan
                .FromDays
                (
                    maximale ? 
                    differenceEntreDates : 
                    -differenceEntreDates
                );

            dateCorrecte = dateLimite.Add(timeSpanRespectantLimite);
            dateNonCorrecte = dateLimite.Add(timeSpanNonRespectantLimite);

            livresBibliotheque = AjouterLivres
            (
                nombreLivresRespectantLimite,
                null,
                dateCorrecte
            );

            livresBibliotheque.AddRange
            (
                AjouterLivres
                (
                    nombreLivresNonRespectantLimite,
                    null,
                    dateNonCorrecte
                )
            );

            return livresBibliotheque;
        }

        /// <summary>
        /// Ajoute les États des livres nécessaires au fontionnement du projet.
        /// </summary>
        private void AjouterEtatLivres()
        {

            EtatLivre etatLivreNeuf;
            EtatLivre etatLivreDigital;
            EtatLivre etatLivreUsage;

            etatLivreNeuf = new()
            {
                Nom = NomEtatLivre.Neuf
            };

            etatLivreDigital = new()
            {
                Nom = NomEtatLivre.Numerique
            };

            etatLivreUsage = new()
            {
                Nom = NomEtatLivre.Usagee
            };

            _context.EtatsLivres.Add(etatLivreNeuf);
            _context.EtatsLivres.Add(etatLivreDigital);
            _context.EtatsLivres.Add(etatLivreUsage);

            _context.SaveChanges();
        }

        /// <summary>
        /// Crée des prix états livres selon les critères sur les livres présents
        /// dans la base de données.
        /// </summary>
        /// <param name="prixNeuf">
        /// Prix pour les livres neufs.
        /// </param>
        /// <param name="prixDigital">
        /// Prix pour les livres en format digial.
        /// </param>
        /// <param name="prixUsage">
        /// Pirx pour les livres usagés.
        /// </param>
        /// <param name="quantiteUsage">
        /// Quantité de livres usagés.
        /// </param>
        private void LierPrixEtatLivresAuxLivresPresents
        (
            List<LivreBibliotheque> livresBibliotheque,
            double prixNeuf = 0.0,
            double prixDigital = 0.0,
            double prixUsage = 0.0,
            int quantiteUsage = 0
        )
        {

            EtatLivre etatLivreNeuf;
            EtatLivre etatLivreDigital;
            EtatLivre etatLivreUsage;
            bool neuf;
            bool digital;
            bool usage;

            if(_context.EtatsLivres.Count() == 0) 
            {

                AjouterEtatLivres();
            }

            etatLivreNeuf = null;
            etatLivreDigital = null;
            etatLivreUsage = null;

            neuf = prixNeuf > 0;
            digital = prixDigital > 0;
            usage = prixUsage > 0;

            if(neuf)
            {
            
                etatLivreNeuf = GetEtatLivreSelonEtat(NomEtatLivre.Neuf);
            }

            if(digital)
            {

                etatLivreDigital = GetEtatLivreSelonEtat(NomEtatLivre.Numerique);
            }

            if(usage)
            {

                etatLivreUsage = GetEtatLivreSelonEtat(NomEtatLivre.Usagee);
            }

            foreach(LivreBibliotheque livre in livresBibliotheque)
            {

                if(neuf)
                {

                    LierEtatLivreALivreBibliotheque
                    (
                        etatLivreNeuf, 
                        livre, 
                        prixNeuf
                    );
                }

                if(digital)
                {

                    LierEtatLivreALivreBibliotheque
                    (
                        etatLivreDigital, 
                        livre, 
                        prixDigital
                    );
                }

                if(usage)
                {

                    LierEtatLivreALivreBibliotheque
                    (
                        etatLivreUsage, 
                        livre, 
                        prixUsage, 
                        quantiteUsage
                    );
                }
            }
        }

        //TODO : utiliser le DAO
        /// <summary>
        /// Lie un <c>EtatLivre</c> à un <c>LivreBibliotheque</c>.
        /// </summary>
        private void LierEtatLivreALivreBibliotheque
        (
            EtatLivre etatLivre, 
            LivreBibliotheque livreBibliotheque, 
            double prix,
            int quantite = 0
        )
        {

            PrixEtatLivre prixEtatLivre;

            prixEtatLivre = new()
            {
                LivreBibliotheque = livreBibliotheque,
                EtatLivre = etatLivre,
                Prix = prix,
                QuantiteUsage = quantite
            };

            _context.PrixEtatsLivres.Add(prixEtatLivre);
            _context.SaveChanges();
        }

        /// <summary>
        /// Lie les <c>LivreBibliotheque</c>s à un <c>Cours</c>
        /// </summary>
        /// <param name="livresBibliotheque">
        /// Livres de la bibliothèque à lier à un cours.
        /// </param>
        /// <param name="cours">
        /// <c>Cours</c> à lier aux livres.
        /// </param>
        private void LierCoursAuxLivres
        (
            List<LivreBibliotheque> livresBibliotheque, 
            Cours cours
        )
        {

            List<CoursLivre> coursLivres;

            coursLivres = new();

            foreach(LivreBibliotheque livreBibliotheque in livresBibliotheque)
            {

                coursLivres.Add
                (
                    new CoursLivre()
                    {
                    Cours = cours,
                    LivreBibliotheque = livreBibliotheque,
                    Complementaire = false
                    }
                );
            }

            _context.CoursLivres.AddRange(coursLivres);

            _context.SaveChanges();
        }

        /// <summary>
        /// Lie un <c>Cours</c> au <c>Professeur</c> spécifie dans la base de données.
        /// </summary>
        /// <param name="cours">
        /// <c>Cours</c> à lier au <c>Professeur</c>.
        /// </param>
        /// <param name="professeur">
        /// <c>Professeur</c> à lier au <c>Cours</c>.
        /// </param>
        private void LierCoursAuProfesseur
        (
            Cours cours,
            Professeur professeur
        )
        {

            CoursProfesseur coursProfesseur;

            coursProfesseur = new()
            {
                Cours = cours,
                Professeur = professeur
            };

            _context.CoursProfesseurs.Add(coursProfesseur);

            _context.SaveChanges();
        }

        //TODO : utiliser le DAO
        /// <summary>
        /// Cherche l'objet correspondant à l'état du livre désiré.
        /// </summary>
        /// <param name="nomEtat">Nom de l'état à aller chercher.</param>
        private EtatLivre GetEtatLivreSelonEtat(string nomEtat)
        {

            EtatLivre etatLivre;

            etatLivre = _context
                .EtatsLivres
                .Where
                (
                    etatLivre =>
                    etatLivre.Nom.Equals(nomEtat)
                )
                .FirstOrDefault();

            return etatLivre;
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
