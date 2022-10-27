﻿using System;
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
                MaisonEdition = Faker.Company.Name(),
                Prix = 0
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
            return "data:image/jpeg;base64,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";
        }
    }
}

