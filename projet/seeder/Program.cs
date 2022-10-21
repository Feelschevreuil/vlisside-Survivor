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

            // Enlever les données
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

            _context.MaisonsEditions
                .RemoveRange(_context.MaisonsEditions);

            _context.TypesPaiement
                .RemoveRange(_context.TypesPaiement);

            _context.Professeurs
            .RemoveRange(_context.Professeurs);
            // FIN Enlever les données

            _context.SaveChanges();

            _context.Provinces.AddRange(getProvinces());

            _context.SaveChanges();

            _context.Adresses.AddRange(getAdresses());

            _context.Auteurs.AddRange(getAuteurs());

            _context.EtatsLivres.AddRange(getEtatsLivres());

            _context.ProgrammesEtudes.AddRange(getProgrammesEtudes());

            _context.SaveChanges();

            _context.Cours.AddRange(getListeCours());

            // Save changes ici, puisqu'un problème de mémoire
            // arrivait si on enregistrait tout à la fin.
            _context.SaveChanges();

            _context.MaisonsEditions.AddRange(getMaisonsEdition());

            _context.SaveChanges();

            _context.LivresBibliotheque.AddRange(getLivresBibliotheques());

            _context.SaveChanges();

            setPrixEtatsLivres();

            setCoursLivres();

            setAuteursParLivres();

            _context.Professeurs.AddRange(getProfesseurs());

            _context.SaveChanges();

            setCoursParProfesseur();

            _context.TypesPaiement.AddRange(getTypesPaiement());

            _context.Commanditaires.AddRange(getCommanditaires());

            _context.SaveChanges();

            _context.Evenements.AddRange(getEvenementsCommanditaires());

            _context.SaveChanges();

            _context.Etudiants.AddRange(getEtudiants());

            _context.SaveChanges();

            setCoursParEtudiants();

            setFacturesEtudiants();

            setLivresEtudiants();

            setEvaluations();
        }

        /// <summary>
        /// Crée une liste de provinces.
        /// </summary>
        /// <returns>Les provinces en liste.</returns>
        private static ICollection<Province> getProvinces()
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
        private static ICollection<Adresse> getAdresses()
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
        private static ICollection<Auteur> getAuteurs()
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
        private static List<EtatLivre> getEtatsLivres()
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
        private static List<ProgrammeEtude> getProgrammesEtudes()
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
        private static List<Cours> getListeCours()
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
        private static ICollection<MaisonEdition> getMaisonsEdition()
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
        private static ICollection<LivreBibliotheque> getLivresBibliotheques()
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
		.With(livre => livre.MaisonEditionId = _context.MaisonsEditions.First().MaisonEditionId)
		.Build();

        }

        /// <summary>
        /// Assigne un état et un prix à chaque livre de la bibliothèque.
        /// </summary>
        private static void setPrixEtatsLivres()
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
        private static void setCoursLivres()
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
        private static void setAuteursParLivres()
        {

	    int nbAuteurs;

	    nbAuteurs = _context.Auteurs.Count();

            foreach (LivreBibliotheque livreBibliotheque in _context.LivresBibliotheque.ToList())
            {

                var auteurs = _context
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
        private static ICollection<Professeur> getProfesseurs()
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
        private static void setCoursParProfesseur()
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

            assignerCoursSansProfesseurs();
        }

        /// <summary>
        /// Assigne un professeur à un cours s'il n'en a pas un assigné. Dans le cas
        /// qu'un cours reste sans professeurs après l'assignation des professeurs à
        /// un cours.
        /// </summary>
        private static void assignerCoursSansProfesseurs()
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
        private static ICollection<TypePaiement> getTypesPaiement()
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
        private static ICollection<Commanditaire> getCommanditaires()
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
        private static ICollection<Evenement> getEvenementsCommanditaires()
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
        private static ICollection<Etudiant> getEtudiants()
        {

	    Adresse adresse;

	    adresse = getAdresseAleatoire();

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
        private static Adresse getAdresseAleatoire()
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
        private static void setCoursParEtudiants()
        {

	    int nbCours;

	    nbCours = _context.Cours.Count();

            foreach (Etudiant etudiant in _context.Etudiants.ToList())
            {

                var listeCoursEtudiant = _context
                    .Cours
                    .Skip(Faker.RandomNumber.Next(0, nbCours - 9))
                    .Take(Faker.RandomNumber.Next(3, 8));

                foreach (Cours cours in listeCoursEtudiant)
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
        private static void setFacturesEtudiants()
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

                        factureEtudiant = creerFactureEtudiant(etudiant);

                        _context.FacturesEtudiants.Add(factureEtudiant);

                        commandesEtudiants = new();

                        for (int commandes = 0; commandes < Faker.RandomNumber.Next(1, 8); commandes++)
                        {

                            CommandeEtudiant commandeEtudiant;

                            commandeEtudiant = creerCommandeEtudiant(factureEtudiant);

                            if (!livreDejaDansCommande(commandesEtudiants, commandeEtudiant))
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
        private static bool livreDejaDansCommande(ICollection<CommandeEtudiant> commandesEtudiants, CommandeEtudiant commandeEtudiant)
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
        private static FactureEtudiant creerFactureEtudiant(Etudiant etudiant)
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
        private static CommandeEtudiant creerCommandeEtudiant(FactureEtudiant factureEtudiant)
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
        private static void setLivresEtudiants()
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
                PhotoCouverture = "N/A",
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
        private static void setEvaluations()
        {

            var livresComplementaires = _context.CoursLivres.Where(coursLivre => coursLivre.Complementaire);

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

                        evaluation = creerEvaluation(coursLivre, etudiant);

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
        private static Evaluation creerEvaluation(CoursLivre coursLivre, Etudiant etudiant)
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
				.Next(joursDepuisPublicationLivre(coursLivre.LivreBibliotheque), 0)),
                Commentaire = Faker.Lorem.Paragraph(),
                Etudiant = etudiant
            };

            return evaluation;
        }

        /// <summary>
        /// Calcure les jours depius la publication d'un livre.
        /// <param name="livre">Le livre ayant la date de publication.</param>
        /// </summary
        /// <returns>Le nombre de jours depuis la publication d'un livre en int.</returns>
        private static int joursDepuisPublicationLivre(ILivre livre)
        {

            int diffrenceJours;

            diffrenceJours = Convert.ToInt32((livre.DatePublication - DateTime.Now).TotalDays);

            return diffrenceJours;
        }
    }
}

