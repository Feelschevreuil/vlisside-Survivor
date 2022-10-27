using System;
using FizzWare.NBuilder;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;
using System.Linq;
using System.Collections.Generic;

namespace seeder
{

    class Program
    {

	private static ApplicationDbContext _context;

        public static void Main(String[] args)
        {

            _context = DbContextBibliotheque.CreateDbContext();

	    EffacerDonnees();

	    AjouterDonnees();
        }

        /// <summary>
	/// Efface les données présentes dans la base de données
        /// </summary>
	private static void EffacerDonnees() 
	{

            _context.Provinces
                .RemoveRange(_context.Provinces);

            _context.Adresses
                .RemoveRange(_context.Adresses);

            _context.Auteurs
                .RemoveRange(_context.Auteurs);

            _context.AuteursLivres
                .RemoveRange(_context.AuteursLivres);

            _context.CommandesEtudiants
                .RemoveRange(_context.CommandesEtudiants);

            _context.FacturesEtudiants
		.RemoveRange(_context.FacturesEtudiants);

            _context.Commanditaires
                .RemoveRange(_context.Commanditaires);

            _context.EtatsLivres
                .RemoveRange(_context.EtatsLivres);

            _context.Etudiants
                .RemoveRange(_context.Etudiants);

            _context.Evaluations
                .RemoveRange(_context.Evaluations);

            _context.EvaluationsLivres
                .RemoveRange(_context.EvaluationsLivres);

            _context.Evenements
                .RemoveRange(_context.Evenements);

            _context.LivresBibliotheque
                .RemoveRange(_context.LivresBibliotheque);

            _context.LivresEtudiants
                .RemoveRange(_context.LivresEtudiants);

            _context.ProgrammesEtudes
                .RemoveRange(_context.ProgrammesEtudes);

            _context.Cours
                .RemoveRange(_context.Cours);

            _context.MaisonsEdition
                .RemoveRange(_context.MaisonsEdition);

            _context.TypesPaiement
                .RemoveRange(_context.TypesPaiement);

            _context.Professeurs
		.RemoveRange(_context.Professeurs);
	}

        /// <summary>
	/// Ajouter les données aléatoires dans la base de données
        /// </summary>
	private static void AjouterDonnees()
	{

            _context.SaveChanges();

            _context.Provinces.AddRange(GetProvinces());

            _context.SaveChanges();

            _context.Adresses.AddRange(GetAdresses());

            _context.Auteurs.AddRange(GetAuteurs());

            _context.EtatsLivres.AddRange(GetEtatsLivres());

            _context.ProgrammesEtudes.AddRange(GetProgrammesEtudes());

            _context.SaveChanges();

            _context.Cours.AddRange(GetListeCours());

            _context.SaveChanges();

            _context.MaisonsEdition.AddRange(GetMaisonsEdition());

            _context.SaveChanges();

            _context.LivresBibliotheque.AddRange(GetLivresBibliotheque());

            _context.SaveChanges();

            SetPrixEtatsLivres();

            SetCoursLivres();

            SetAuteursParLivre();

            _context.Professeurs.AddRange(GetProfesseurs());

            _context.SaveChanges();

            SetCoursParProfesseur();

            _context.TypesPaiement.AddRange(GetTypesPaiement());

            _context.Commanditaires.AddRange(GetCommanditaires());

            _context.SaveChanges();

            _context.Evenements.AddRange(GetEvenementsCommanditaires());

            _context.SaveChanges();

            _context.Etudiants.AddRange(GetEtudiants());

            _context.SaveChanges();

            SetCoursParEtudiants();

            SetFacturesEtudiants();

            SetLivresEtudiants();

            SetEvaluations();
	}

        /// <summary>
        /// Crée une liste de provinces.
        /// </summary>
        /// <returns>Les provinces en liste.</returns>
        private static ICollection<Province> GetProvinces()
        {

            return Builder<Province>
		.CreateListOfSize(10)
		.All()
		.With(province => province.ProvinceId = 0)
		.With(province => province.Nom = Faker.Address.UsState())
		.Build();
        }

        /// <summary>
        /// Crée une liste d'Adresses.
        /// </summary>
        /// <returns>Les adresses en liste.</returns>
        private static ICollection<Adresse> GetAdresses()
        {

            return Builder<Adresse>
		.CreateListOfSize(10)
		.All()
		.With(adresse => adresse.AdresseId = 0)
		.With(adresse => adresse.Ville = Faker.Address.City())
		.With(adresse => adresse.NumeroCivique = Faker.RandomNumber.Next(1000))
		.With(adresse => adresse.App = Faker.RandomNumber.Next(100))
		.With(adresse => adresse.Rue = Faker.Address.StreetName())
		.With(adresse => adresse.CodePostal = "X6X6X6")
		.With(adresse => adresse.Province = _context.Provinces.First())
		.Build();
        }

        /// <summary>
        /// Crée une liste d'Auteurs.
        /// </summary>
        /// <returns>Les auteurs liste.</returns>
        private static ICollection<Auteur> GetAuteurs()
        {

            return Builder<Auteur>
		.CreateListOfSize(25)
		.All()
		.With(auteur => auteur.AuteurId = 0)
		.With(auteur => auteur.Nom = Faker.Name.Last())
		.With(auteur => auteur.Prenom = Faker.Name.First())
		.Build();
        }

        // TODO: mettre dans le db_context.
        /// <summary>
        /// Crée une liste des États des livres.
        /// </summary>
        /// <returns>Les États des livres en liste.</returns>
        private static List<EtatLivre> GetEtatsLivres()
        {

            return new List<EtatLivre> {

                new EtatLivre() {
		    EtatLivreId = 0,
                    Nom = "Neuf"
                },
                new EtatLivre() {
		    EtatLivreId = 0,
                    Nom = "Usagé"
                },
                new EtatLivre() {
		    EtatLivreId = 0,
                    Nom = "Digital"
                }
            };
        }

        // TODO: mettre dans le db_context.
        /// <summary>
        /// Crée une liste des programmes d'études.
        /// </summary>
        /// <returns>Les programmes d'études en liste.</returns>
        private static List<ProgrammeEtude> GetProgrammesEtudes()
        {

            return new List<ProgrammeEtude> {

                new ProgrammeEtude() {
		    ProgrammeEtudeId = 0,
                    Nom = "Techniques de tourisme",
                    Code = "414"
                },
                new ProgrammeEtude() {
		    ProgrammeEtudeId = 0,
                    Nom = "Sciences de la Nature",
                    Code = "201"
                },
                new ProgrammeEtude() {
		    ProgrammeEtudeId = 0,
                    Nom = "Techniques d'éducatoin spécialisée",
                    Code = "351"
                },
                new ProgrammeEtude() {
		    ProgrammeEtudeId = 0,
                    Nom = "Techniques de génie mécanique",
                    Code = "241"
                },
                new ProgrammeEtude() {
		    ProgrammeEtudeId = 0,
                    Nom = "Formation générale",
                    Code = "x"
                }
            };
        }

        // TODO: mettre dans le db_context.
        /// <summary>
        /// Crée une liste des livres des cours.
        /// </summary>
        /// <returns>Les cours liste.</returns>
        private static List<Cours> GetListeCours()
        {

	    ProgrammeEtude techniquesTourisme;
	    ProgrammeEtude sciencesNature;
	    ProgrammeEtude techniquesEducationSpecialisee;
	    ProgrammeEtude techniquesGenieMecanique;
	    ProgrammeEtude formationGenerale;

            techniquesTourisme = _context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "414"
            );

            sciencesNature = _context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "201"
            );

            techniquesEducationSpecialisee = _context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "351"
            );

            techniquesGenieMecanique = _context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "241"
            );

            formationGenerale = _context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "x"
            );

            return new List<Cours> {

                new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
			Nom = "Exploration des carrières en tourisme",
			Description = "N/A",
			Code = "414313CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
			Nom = "Introduction au programme de Tourisme",
			Description = "N/A",
			Code = "414133CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
			Nom = "Accueil et service à la clientèle",
			Description = "N/A",
			Code = "414154CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
			Nom = "Destinations touristiques : les Amériques",
			Description = "N/A",
			Code = "414234CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
			Nom = "Communication et supervision",
			Description = "N/A",
			Code = "414323CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = formationGenerale.ProgrammeEtudeId,
			Nom = "Écriture et littérature",
			Description = "N/A",
			Code = "601101MQ",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = formationGenerale.ProgrammeEtudeId,
			Nom = "Littérature et imaginaire",
			Description = "N/A",
			Code = "601102MQ",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = sciencesNature.ProgrammeEtudeId,
			Nom = "Calcul intégral",
			Description = "N/A",
			Code = "201NYB05",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = sciencesNature.ProgrammeEtudeId,
			Nom = "Chimie des solutions",
			Description = "N/A",
			Code = "202NYB05",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = sciencesNature.ProgrammeEtudeId,
			Nom = "Électricité et magnétisme",
			Description = "N/A",
			Code = "203NYB05",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = formationGenerale.ProgrammeEtudeId,
			Nom = "Astrophysique",
			Description = "N/A",
			Code = "203314CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesEducationSpecialisee.ProgrammeEtudeId,
			Nom = "Psychologie de l’enfance",
			Description = "N/A",
			Code = "350114CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesEducationSpecialisee.ProgrammeEtudeId,
			Nom = "Introduction aux problématiques d’adaptation",
			Description = "N/A",
			Code = "351124CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
			Nom = "Mathématiques du génie mécanique",
			Description = "N/A",
			Code = "201224CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
			Nom = "Mathématiques appliquées",
			Description = "N/A",
			Code = "201115CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
			Nom = "Statique et cinématique",
			Description = "N/A",
			Code = "203214CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
			Nom = "Techniques d’usinage 1",
			Description = "N/A",
			Code = "241216CA",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
			Nom = "Techniques d’usinage 1",
			Description = "N/A",
			Code = "241316",
			AnneeParcours = 1
		    },
		    new Cours() {
			CoursId = 0,
			ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
			Nom = "Dessin industriel assisté par ordinateur",
			Description = "N/A",
			Code = "241225CA",
			AnneeParcours = 1
		    }
		};
        }

        /// <summary>
        /// Crée une liste des livres des maisons d'éditoin.
        /// </summary>
        /// <returns>Les maisons d'éditoin en liste.</returns>
        private static ICollection<MaisonEdition> GetMaisonsEdition()
        {

            return Builder<MaisonEdition>
		.CreateListOfSize(10)
		.All()
		.With(maisonEdition => maisonEdition.MaisonEditionId = 0)
		.With(maisonEdition => maisonEdition.Nom = Faker.Company.Name())
		.Build();
        }

        /// <summary>
        /// Crée une liste des livres de la bibliothèque.
        /// </summary>
        /// <returns>Les livres de la bibliothèque en liste.</returns>
        private static ICollection<LivreBibliotheque> GetLivresBibliotheque()
        {

            return Builder<LivreBibliotheque>
		.CreateListOfSize(50)
		.All()
		.With(livre => livre.LivreId = 0)
		.With(livre => livre.Isbn = 666 + Faker.Identification.UkNhsNumber())
		.With(livre => livre.Titre = string.Join(" ", Faker.Lorem.Words(3)))
		.With(livre => livre.Resume = Faker.Lorem.Sentence())
		.With(livre => livre.PhotoCouverture = "N/A")
		.With(livre => livre.DatePublication = Faker.Identification.DateOfBirth())
		.With(livre => livre.MaisonEditionId = _context.MaisonsEdition.First().MaisonEditionId)
		.Build();
        }

        /// <summary>
        /// Assigne un état et un prix à chaque livre de la bibliothèque.
        /// </summary>
        private static void SetPrixEtatsLivres()
        {

            EtatLivre etatUsage;
            EtatLivre etatNeuf;
            EtatLivre etatDigital;

            etatUsage = _context.EtatsLivres
                .Where(etatLivre => etatLivre.Nom == "Usagé").First();

            etatNeuf = _context.EtatsLivres
                .Where(etatLivre => etatLivre.Nom == "Neuf").First();

            etatDigital = _context.EtatsLivres
                .Where(etatLivre => etatLivre.Nom == "Digital").First();

            foreach (LivreBibliotheque livreBibliotheque in _context.LivresBibliotheque)
            {

                if (Faker.Boolean.Random())
                {

		    PrixEtatLivre prixEtatLivreUsage;

                    prixEtatLivreUsage = new()
                    {
                        PrixEtatLivreId = 0,
                        EtatLivre = etatUsage,
                        LivreBibliotheque = livreBibliotheque,
                        Prix = Convert.ToDouble(Faker.RandomNumber.Next(3, 500))
                    };

                    _context.PrixEtatsLivres.Add(prixEtatLivreUsage);
                }

		PrixEtatLivre prixEtatLivreNeuf;
		PrixEtatLivre prixEtatLivreDigital;

                prixEtatLivreNeuf = new()
                {
                    PrixEtatLivreId = 0,
                    EtatLivre = etatNeuf,
                    LivreBibliotheque = livreBibliotheque,
                    Prix = Convert.ToDouble(Faker.RandomNumber.Next(3, 500))
                };

                prixEtatLivreDigital = new()
                {
                    PrixEtatLivreId = 0,
                    EtatLivre = etatDigital,
                    LivreBibliotheque = livreBibliotheque,
                    Prix = Convert.ToDouble(Faker.RandomNumber.Next(3, 500))
                };

                _context.PrixEtatsLivres.Add(prixEtatLivreNeuf);
                _context.PrixEtatsLivres.Add(prixEtatLivreDigital);

            }

	    _context.SaveChanges();
        }

        /// <summary>
        /// Assigne des livres nécessaires à un cours.
        /// </summary>
        private static void SetCoursLivres()
        {

	    int nbLivresBibliotheque;

	    nbLivresBibliotheque = _context.LivresBibliotheque.Count();

	    foreach (Cours cours in _context.Cours.ToList())
            {

                List<CoursLivre> listeCoursLivre;

		listeCoursLivre = new();

                for (int i = 0; i < Faker.RandomNumber.Next(5, 12); i++)
                {

                    CoursLivre coursLivre;
                    LivreBibliotheque livreBibliotheque;

                    livreBibliotheque = _context
			.LivresBibliotheque
			.Skip(Faker.RandomNumber.Next(0, nbLivresBibliotheque - 1))
			.Take(1)
			.First();

                    coursLivre = new()
                    {
                        CoursLivreId = 0,
                        Cours = cours,
                        LivreBibliotheque = livreBibliotheque,
                        Complementaire = Faker.Boolean.Random()
                    };

                    listeCoursLivre.Add(coursLivre);
                }

                _context.CoursLivres.AddRange(listeCoursLivre);
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Assigne les auteurs à des livres.
        /// </summary>
        private static void SetAuteursParLivre()
        {

	    int nbAuteurs;

	    nbAuteurs = _context.Auteurs.Count();

            foreach (LivreBibliotheque livreBibliotheque in _context.LivresBibliotheque.ToList())
            {

		IEnumerable<Auteur> auteurs;

                auteurs = _context
                    .Auteurs
                    .Skip(Faker.RandomNumber.Next(0, nbAuteurs - 4))
                    .Take(Faker.RandomNumber.Next(1, 3));

                foreach (Auteur auteur in auteurs)
                {

		    AuteurLivre auteurLivre;

                    auteurLivre = new()
                    {
                        Auteur = auteur,
                        LivreBibliotheque = livreBibliotheque
                    };

                    _context.AuteursLivres.Add(auteurLivre);
                }
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Crée une liste de professeurs.
        /// </summary>
        /// <returns>Les professeurs liste.</returns>
        private static ICollection<Professeur> GetProfesseurs()
        {

            return Builder<Professeur>
		.CreateListOfSize(15)
		.All()
		.With(professeur => professeur.ProfesseurId = 0)
		.With(professeur => professeur.Nom = Faker.Name.Last())
		.With(professeur => professeur.Prenom = Faker.Name.First())
		.Build();
        }

        /// <summary>
        /// Assigne les professeurs par cours.
        /// </summary>
        private static void SetCoursParProfesseur()
        {

	    int nbCours;

	    nbCours = _context.Cours.Count();

            foreach (Professeur professeur in _context.Professeurs.ToList())
            {

                IEnumerable<Cours> choixCours; 

		choixCours = _context
                    .Cours
                    .Skip(Faker.RandomNumber.Next(0, nbCours - 4))
                    .Take(Faker.RandomNumber.Next(1, 3))
		    .ToList();

                foreach (Cours cours in choixCours)
                {

		    CoursProfesseur coursProfesseur;

                    coursProfesseur = new()
                    {
                        Professeur = professeur,
                        Cours = cours
                    };

                    _context.CoursProfesseurs.Add(coursProfesseur);
                }
            }

            _context.SaveChanges();

            AssignerCoursSansProfesseurs();
        }

        /// <summary>
        /// Assigne un professeur à un cours s'il n'en a pas un assigné. Dans le cas
        /// qu'un cours reste sans professeurs après l'assignation des professeurs à
        /// un cours.
        /// </summary>
        private static void AssignerCoursSansProfesseurs()
        {

            foreach (Cours cours in _context.Cours.ToList())
            {

                int nombreProfesseurs;

                nombreProfesseurs = _context
                    .CoursProfesseurs
                    .Where(coursProfesseurs => coursProfesseurs.CoursId == cours.CoursId)
                    .Count();

                if (nombreProfesseurs == 0)
                {

                    CoursProfesseur coursProfesseur;
                    Professeur professeur;

                    professeur = _context
                    .Professeurs
                    // TODO: optimiser
                    .Take(1)
                    .First();

                    coursProfesseur = new()
                    {
                        Cours = cours,
                        Professeur = professeur
                    };

                    _context.CoursProfesseurs.Add(coursProfesseur);
                }
            }

            _context.SaveChanges();
        }

        // TODO: Sera possiblement enlevé dépendament
        // du système de paiement que l'on va utiliser.
        /// <summary>
        /// Crée une liste des types de paiement.
        /// </summary>
        /// <returns>Les types de paiement liste.</returns>
        private static ICollection<TypePaiement> GetTypesPaiement()
        {

            return new List<TypePaiement>
	    {

		new TypePaiement()
		{
		    TypePaiementId = 0,
		    Nom = "Débit"
		},
		new TypePaiement()
		{
		    TypePaiementId = 0,
		    Nom = "Crédit"
		}
	    };
        }

        /// <summary>
        /// Crée une liste des commanditaires.
        /// </summary>
        /// <returns>Les commanditaires en liste.</returns>
        private static ICollection<Commanditaire> GetCommanditaires()
        {

            return Builder<Commanditaire>
		.CreateListOfSize(10)
		.All()
		.With(commanditaire => commanditaire.CommanditaireId = 0)
		.With(commanditaire => commanditaire.Nom = Faker.Company.Name())
		.With(commanditaire => commanditaire.Courriel = Faker.Internet.Email())
		.With(commanditaire => commanditaire.Url = Faker.Internet.Url())
		.With(commanditaire => commanditaire.Message = Faker.Lorem.Sentence(Faker.RandomNumber.Next(1, 5)))
		.Build();
        }

        /// <summary>
        /// Crée une liste d'événements pour chaque 
        /// commanditaire.
        /// </summary>
        /// <returns>Les événements en liste.</returns>
        private static ICollection<Evenement> GetEvenementsCommanditaires()
        {

	    List<Evenement> evenements;

            evenements = new();

            foreach (Commanditaire commanditaire in _context.Commanditaires)
            {

                Evenement evenement;

                evenement = new()
                {
                    EvenementId = 0,
                    Commanditaire = commanditaire,
                    Nom = Faker.Company.CatchPhrase(),
                    Description = Faker.Lorem.Paragraph(Faker.RandomNumber.Next(2, 5)),
                    Debut = DateTime.Now.AddDays(Faker.RandomNumber.Next(-10, 5)),
                    Fin = DateTime.Now.AddDays(Faker.RandomNumber.Next(6, 15)),
                    Image = "N/A",
                };

                evenements.Add(evenement);
            }

            return evenements;
        }

        /// <summary>
        /// crée une liste d'étudiants.
        /// </summary>
        /// <returns>les étudiants en liste.</returns>
        private static ICollection<Etudiant> GetEtudiants()
        {

	    Adresse adresse;

	    adresse = GetAdresseAleatoire();

	    _context.Adresses.Add(adresse);
	    _context.SaveChanges();

            return Builder<Etudiant>
               .CreateListOfSize(50)
               .All()
               .With(etudiant => etudiant.Email = Faker.Internet.Email())
               .With(etudiant => etudiant.Nom = Faker.Name.Last())
               .With(etudiant => etudiant.Prenom = Faker.Name.First())
               .With(etudiant => etudiant
				    .ProgrammeEtude = _context
					.ProgrammesEtudes
					.Skip(Faker.RandomNumber.Next(0, _context.ProgrammesEtudes.Count() - 1))
					.Take(1)
					.First())
               .With(etudiant => etudiant.Adresse = adresse)
               .Build();
        }

        /// <summary>
        /// Crée une adresse aléatoirement.
        /// </summary>
        private static Adresse GetAdresseAleatoire()
        {

	    return new Adresse()
            {
		AdresseId = 0,
		App = Faker.RandomNumber.Next(1, 55),
		CodePostal = "6X6X6X",
		NumeroCivique = Faker.RandomNumber.Next(1, 666),
		Province = _context
			    .Provinces
				.Skip(Faker.RandomNumber.Next(0, _context.Provinces.Count() - 1))
				.Take(1)
				.First(),
		Rue = Faker.Address.StreetName(),
		Ville = Faker.Address.City()
	    };
        }

        /// <summary>
        /// Assigne les cours aux étudiants.
        /// </summary>
        private static void SetCoursParEtudiants()
        {

	    int nbCours;

	    nbCours = _context.Cours.Count();

            foreach (Etudiant etudiant in _context.Etudiants.ToList())
            {

		IEnumerable<Cours> listeCours;
                
		listeCours= _context
                    .Cours
                    .Skip(Faker.RandomNumber.Next(0, nbCours - 9))
                    .Take(Faker.RandomNumber.Next(3, 8));

                foreach (Cours cours in listeCours)
                {

		    CoursEtudiant coursEtudiant;

                    coursEtudiant = new()
                    {
                        Etudiant = etudiant,
                        Cours = cours
                    };

                    _context.CoursEtudiants.Add(coursEtudiant);
                }
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Génère des factures aux étudiants.
        /// </summary>
        private static void SetFacturesEtudiants()
        {

            foreach (Etudiant etudiant in _context.Etudiants.ToList())
            {

                bool asDejaCommande;

                asDejaCommande = Faker.Boolean.Random();

                if (asDejaCommande)
                {

                    for (int factures = 0; factures < Faker.RandomNumber.Next(1, 5); factures++)
                    {

                        FactureEtudiant factureEtudiant;
                        List<CommandeEtudiant> commandesEtudiants;

                        factureEtudiant = CreerFactureEtudiant(etudiant);

                        _context.FacturesEtudiants.Add(factureEtudiant);

                        commandesEtudiants = new();

                        for (int commandes = 0; commandes < Faker.RandomNumber.Next(1, 8); commandes++)
                        {

                            CommandeEtudiant commandeEtudiant;

                            commandeEtudiant = CreerCommandeEtudiant(factureEtudiant);

                            if (!LivreEstDejaDansCommande(commandesEtudiants, commandeEtudiant))
                            {
                                commandesEtudiants.Add(commandeEtudiant);
                            }
                            else
                            {
                                commandes--;
                            }
                        }

                        _context.CommandesEtudiants.AddRange(commandesEtudiants);
                    }
                }
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Regarde si une commande de livre existe déjà.
        /// </summary>
        /// <returns>true si le livre est déjà présent.</returns>
        private static bool LivreEstDejaDansCommande(ICollection<CommandeEtudiant> commandesEtudiants, CommandeEtudiant commandeEtudiant)
        {
            bool livrePresent;

            livrePresent = false;

            if (commandesEtudiants.Any())
            {
                livrePresent = commandesEtudiants.Where(commande => commande.PrixEtatLivreId == commandeEtudiant.PrixEtatLivreId).Any();
            }

            return livrePresent;
        }

        /// <summary>
        /// Crée une facture pour un étudiant.
        /// </summary>
        /// <returns>la facture de l'étudiant en Facture.</returns>
        private static FactureEtudiant CreerFactureEtudiant(Etudiant etudiant)
        {

            FactureEtudiant factureEtudiant;

            factureEtudiant = new()
            {
                FactureEtudiantId = 0,
                TypePaiement = _context
				.TypesPaiement
				    .Skip(Faker.RandomNumber.Next(0, _context.TypesPaiement.Count() - 1))
				    .Take(1)
				    .First(),
                Etudiant = etudiant,
                // TODO: ne pas enregistrer l'id de l'objet, mais l'adresse au complete en texte.
                AdresseLivraison = "adresse place holder",
                DateFacturation = DateTime
				    .Now
				    .AddDays(Faker.RandomNumber.Next(-355, 0)),
                Tps = 0.05M,
                Tvq = 0.09975M
            };

            return factureEtudiant;
        }

        /// <summary>
        /// Crée une commande pour une facture.
        /// </summary>
        /// <returns>la commande de la facture en Commande.</returns>
        private static CommandeEtudiant CreerCommandeEtudiant(FactureEtudiant factureEtudiant)
        {

            CommandeEtudiant commandeEtudiant;

            commandeEtudiant = new()
            {
                FactureEtudiant = factureEtudiant,
                PrixEtatLivre = _context
				    .PrixEtatsLivres
					.Skip(Faker.RandomNumber.Next(0, _context.PrixEtatsLivres.Count() - 1))
					.Take(1)
					.First(),
                Quantite = Faker.RandomNumber.Next(1, 2)
            };

            return commandeEtudiant;
        }


        /// <summary>
        /// Génère la liste des livres des étudiants.
        /// </summary>
        private static void SetLivresEtudiants()
        {

            foreach (Etudiant etudiant in _context.Etudiants)
            {

                bool aLivresVendre;

                aLivresVendre = Faker.Boolean.Random();

                if (aLivresVendre)
                {

                    int nombreLivresVendre;

                    nombreLivresVendre = Faker.RandomNumber.Next(1, 8);

                    for (int i = 0; i < nombreLivresVendre; i++)
                    {

                        LivreEtudiant livreEtudiant;

                        livreEtudiant = creerLivreEtudiant(etudiant);

                        _context.LivresEtudiants.Add(livreEtudiant);
                    }
                }
            }

            _context.SaveChanges();
        }

        // TODO: à tester lorsque l'objet LivreEtudiant aure les modificaitons
        // nécessaires apportées.
        /// <summary>
        /// Crée un livre d'étudiant.
        /// </summary>
        /// <returns>le livre de l'étudiant.</returns>
        private static LivreEtudiant creerLivreEtudiant(Etudiant etudiant)
        {

            LivreEtudiant livreEtudiant;

            livreEtudiant = new()
            {
                LivreId = 0,
                Etudiant = etudiant,
                Isbn = "666" + Faker.Identification.UkNhsNumber(),
                Titre = string.Join(" ", Faker.Lorem.Words(3)),
                Auteur = Faker.Name.First() + " " + Faker.Name.Last(),
                Resume = Faker.Lorem.Paragraph(),
                PhotoCouverture = GetImageParDefaut(),
                DatePublication = Faker.Identification.DateOfBirth().AddDays(Faker.RandomNumber.Next(-3000, 0)),
                MaisonEdition = Faker.Company.Name()
            };

            return livreEtudiant;
        }

        // TODO: à tester lorsque l'objet LivreBibliotheque aura les modificaitons
        // nécessaires apportées.
        /// <summary>
        /// Génère les évaluations des livres complémentaires.
        /// </summary>
        private static void SetEvaluations()
        {

	    IEnumerable<CoursLivre> livresComplementaires;

            livresComplementaires = _context.CoursLivres.Where(coursLivre => coursLivre.Complementaire);

            foreach (CoursLivre coursLivre in livresComplementaires.ToList())
            {

                bool ajouterEvaluations;

                ajouterEvaluations = Faker.Boolean.Random();

                if (ajouterEvaluations)
                {

                    int nombreMaximumEtudiants;

                    nombreMaximumEtudiants = Convert.ToInt32((_context.Etudiants.Count() - 1) / 2);

                    foreach (Etudiant etudiant in _context.Etudiants.Take(Faker.RandomNumber.Next(1, nombreMaximumEtudiants)))
                    {

                        Evaluation evaluation;
                        EvaluationLivre evaluationLivre;

                        evaluation = CreerEvaluation(coursLivre, etudiant);

			// TODO: valider si c'est sauvegardé
                        _context.Evaluations.Add(evaluation);

                        // TODO: valider l'utilité d'une table de liaison. Pourquoi ne pas uniquement ajouter l'étudiant à l'évaluation?on?
                        evaluationLivre = new()
                        {
                            Evaluation = evaluation,
                            LivreBibliothequeId = coursLivre.LivreBibliothequeId
                        };

                        _context.EvaluationsLivres.Add(evaluationLivre);

                    }
                }
            }

            _context.SaveChanges();
        }

        // TODO: valider l'utilité d'une table de liaison. Pourquoi ne pas uniquement ajouter l'étudiant à l'évaluation?
        /// <summary>
        /// Crée une évaluation d'un livre complémentaire.
        /// <paramref name="coursLivre">Objet CoursLivre contenant le livre de la bibliothèque.</param>
        /// <paramref name="etudiant">L'étudiant qui évalue le livre.</param>
        /// </summary>
        /// <returns>le livre de l'étudiant.</returns>
        private static Evaluation CreerEvaluation(CoursLivre coursLivre, Etudiant etudiant)
        {

            Evaluation evaluation;

            evaluation = new()
            {
                EvaluationId = 0,
                Etoiles = Faker.RandomNumber.Next(0, 10),
                Date = Faker
			.Identification
			.DateOfBirth()
			    .AddDays(Faker
				.RandomNumber
				.Next(JoursDepuisPublicationLivre(coursLivre.LivreBibliotheque), 0)),
                Commentaire = Faker.Lorem.Paragraph(),
                Etudiant = etudiant
            };

            return evaluation;
        }

	// TODO: sortir et créer une extention d'Ilivre!
        /// <summary>
        /// Calcure les jours depius la publication d'un livre.
        /// <param name="livre">Le livre ayant la date de publication.</param>
        /// </summary
        /// <returns>Le nombre de jours depuis la publication d'un livre en int.</returns>
        private static int JoursDepuisPublicationLivre(ILivre livre)
        {

            int diffrenceJours;

            diffrenceJours = Convert.ToInt32((livre.DatePublication - DateTime.Now).TotalDays);

            return diffrenceJours;
        }

        private static string GetImageParDefaut()
        {
            return "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/4RDiRXhpZgAATU0AKgAAAAgABAE7AAIAAAAIAAAISodpAAQAAAABAAAIUpydAAEAAAAQAAAQyuocAAcAAAgMAAAAPgAAAAAc6gAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFZpbmNlbnQAAAWQAwACAAAAFAAAEKCQBAACAAAAFAAAELSSkQACAAAAAzEwAACSkgACAAAAAzEwAADqHAAHAAAIDAAACJQAAAAAHOoAAAAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAyMDIyOjEwOjI2IDE5OjU0OjExADIwMjI6MTA6MjYgMTk6NTQ6MTEAAABWAGkAbgBjAGUAbgB0AAAA/+ELGmh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8APD94cGFja2V0IGJlZ2luPSfvu78nIGlkPSdXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQnPz4NCjx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iPjxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iLz48cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0idXVpZDpmYWY1YmRkNS1iYTNkLTExZGEtYWQzMS1kMzNkNzUxODJmMWIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyI+PHhtcDpDcmVhdGVEYXRlPjIwMjItMTAtMjZUMTk6NTQ6MTEuMTA0PC94bXA6Q3JlYXRlRGF0ZT48L3JkZjpEZXNjcmlwdGlvbj48cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0idXVpZDpmYWY1YmRkNS1iYTNkLTExZGEtYWQzMS1kMzNkNzUxODJmMWIiIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyI+PGRjOmNyZWF0b3I+PHJkZjpTZXEgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj48cmRmOmxpPlZpbmNlbnQ8L3JkZjpsaT48L3JkZjpTZXE+DQoJCQk8L2RjOmNyZWF0b3I+PC9yZGY6RGVzY3JpcHRpb24+PC9yZGY6UkRGPjwveDp4bXBtZXRhPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8P3hwYWNrZXQgZW5kPSd3Jz8+/9sAQwAHBQUGBQQH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";
        }
    }
}

