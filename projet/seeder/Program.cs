using System;
using FizzWare.NBuilder;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;
using System.Linq;

namespace seeder
{

    class Program
    {

        public static void Main(String[] args)
        {

            var context = DbContextBibliotheque.CreateDbContext();

            // Enlever les données
            context.Provinces
                .RemoveRange(context.Provinces);

            context.Adresses
                .RemoveRange(context.Adresses);

            context.Auteurs
                .RemoveRange(context.Auteurs);

            context.AuteursLivres
                .RemoveRange(context.AuteursLivres);

            context.CommandesEtudiants
                .RemoveRange(context.CommandesEtudiants);

            context.Commanditaires
                .RemoveRange(context.Commanditaires);

            context.EtatsLivres
                .RemoveRange(context.EtatsLivres);

            context.Etudiants
                .RemoveRange(context.Etudiants);

            context.Evaluations
                .RemoveRange(context.Evaluations);

            context.EvaluationsLivres
                .RemoveRange(context.EvaluationsLivres);

            context.Evenements
                .RemoveRange(context.Evenements);

            context.LivresBibliotheque
                .RemoveRange(context.LivresBibliotheque);

            context.LivresEtudiants
                .RemoveRange(context.LivresEtudiants);

            context.ProgrammesEtudes
                .RemoveRange(context.ProgrammesEtudes);

            context.Cours
                .RemoveRange(context.Cours);

            context.MaisonsEditions
                .RemoveRange(context.MaisonsEditions);

            context.TypesPaiement
                .RemoveRange(context.TypesPaiement);
            // FIN Enlever les données

            context.Provinces.AddRange(getProvinces());

            context.SaveChanges();

            context.Adresses.AddRange(getAdresses(context));

            context.Auteurs.AddRange(getAuteurs());

            context.EtatsLivres.AddRange(getEtatsLivres());

            context.ProgrammesEtudes.AddRange(getProgrammesEtudes());

            context.SaveChanges();

            context.Cours.AddRange(getListeCours(context));

            // Save changes ici, puisqu'un problème de mémoire
            // arrivait si on enregistrait tout à la fin.
            context.SaveChanges();

            context.MaisonsEditions.AddRange(getMaisonsEdition());

            context.SaveChanges();

            context.LivresBibliotheque.AddRange(getLivresBibliotheques(context));

            context.SaveChanges();

	    setPrixEtatsLivres(context);
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
		.With(province => province.Nom = Faker.Address.UsState())
		.Build();
	}

        /// <summary>
        /// Crée une liste d'Adresses.
        /// </summary>
        /// <returns>Les adresses en liste.</returns>
        private static ICollection<Adresse> getAdresses(ApplicationDbContext context)
        {

	    return Builder<Adresse>
		.CreateListOfSize(10)
		.All()
		.With(adresse => adresse.Ville = Faker.Address.City())
		.With(adresse => adresse.NumeroCivique = Faker.RandomNumber.Next(1000))
		.With(adresse => adresse.App = Faker.RandomNumber.Next(100))
		.With(adresse => adresse.Rue = Faker.Address.StreetName())
		.With(adresse => adresse.CodePostal = Faker.Address.ZipCode())
		.With(adresse => adresse.Province = context.Provinces.First())
		.Build();
        }

        /// <summary>
        /// Crée une liste d'Auteurs.
        /// </summary>
        /// <returns>Les auteurs liste.</returns>
        private static ICollection<Auteur> getAuteurs()
        {

	    return Builder<Auteur>
		.CreateListOfSize(10)
		.All()
		.With(auteur => auteur.Nom = Faker.Name.Last())
		.With(auteur => auteur.Prenom = Faker.Name.Last())
		.Build();
        }

	// TODO: mettre dans le dbcontext.
        /// <summary>
        /// Crée une liste des États des livres.
        /// </summary>
        /// <returns>Les États des livres en liste.</returns>
        private static List<EtatLivre> getEtatsLivres()
        {

            return new List<EtatLivre> {
                new EtatLivre() {
                    Nom = "Neuf"
                },
                new EtatLivre() {
                    Nom = "Usagé"
                },
                new EtatLivre() {
                    Nom = "Digital"
                }
            };
        }

	// TODO: mettre dans le dbcontext.
        /// <summary>
        /// Crée une liste des programmes d'études.
        /// </summary>
        /// <returns>Les programmes d'études en liste.</returns>
        private static List<ProgrammeEtude> getProgrammesEtudes()
        {

            return new List<ProgrammeEtude> {
                new ProgrammeEtude() {
                    Nom = "Techniques de tourisme",
                    Code = "414"
                },
                new ProgrammeEtude() {
                    Nom = "Sciences de la Nature",
                    Code = "201"
                },
                new ProgrammeEtude() {
                    Nom = "Techniques d'éducatoin spécialisée",
                    Code = "351"
                },
                new ProgrammeEtude() {
                    Nom = "Techniques de génie mécanique",
                    Code = "241"
                },
                new ProgrammeEtude() {
                    Nom = "Formation générale",
                    Code = "x"
                }
            };
        }

	// TODO: mettre dans le dbcontext.
        /// <summary>
        /// Crée une liste des livres des cours.
        /// </summary>
        /// <returns>Les cours liste.</returns>
        private static List<Cours> getListeCours(ApplicationDbContext context)
        {

	    ProgrammeEtude techniquesTourisme = context.ProgrammesEtudes.SingleOrDefault(
		    programmeEtude => programmeEtude.Code == "414"
	    );

	    ProgrammeEtude sciencesNature = context.ProgrammesEtudes.SingleOrDefault(
		    programmeEtude => programmeEtude.Code == "201"
	    );

	    ProgrammeEtude techniquesEducationSpecialisee = context.ProgrammesEtudes.SingleOrDefault(
		    programmeEtude => programmeEtude.Code == "351"
	    );

	    ProgrammeEtude techniquesGenieMecanique = context.ProgrammesEtudes.SingleOrDefault(
		    programmeEtude => programmeEtude.Code == "241"
	    );

	    ProgrammeEtude formationGenerale = context.ProgrammesEtudes.SingleOrDefault(
		    programmeEtude => programmeEtude.Code == "x"
	    );

            return new List<Cours> {
                new Cours() {
		    ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
                    Nom = "Exploration des carrières en tourisme",
                    Description = "N/A",
                    Code = "414313CA",
                    AnneeParcours = 1
                },
                new Cours() {
		    ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
                    Nom = "Introduction au programme de Tourisme",
                    Description = "N/A",
                    Code = "414133CA",
                    AnneeParcours = 1
                },
                new Cours() {
		    ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
                    Nom = "Accueil et service à la clientèle",
                    Description = "N/A",
                    Code = "414154CA",
                    AnneeParcours = 1
                },
                new Cours() {
		    ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
                    Nom = "Destinations touristiques : les Amériques",
                    Description = "N/A",
                    Code = "414234CA",
                    AnneeParcours = 1
                },
                new Cours() {
		    ProgrammeEtudeId = techniquesTourisme.ProgrammeEtudeId,
                    Nom = "Communication et supervision",
                    Description = "N/A",
                    Code = "414323CA",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = formationGenerale.ProgrammeEtudeId,
                    Nom = "Écriture et littérature",
                    Description = "N/A",
                    Code = "601101MQ",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = formationGenerale.ProgrammeEtudeId,
                    Nom = "Littérature et imaginaire",
                    Description = "N/A",
                    Code = "601102MQ",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = sciencesNature.ProgrammeEtudeId,
                    Nom = "Calcul intégral",
                    Description = "N/A",
                    Code = "201NYB05",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = sciencesNature.ProgrammeEtudeId,
                    Nom = "Chimie des solutions",
                    Description = "N/A",
                    Code = "202NYB05",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = sciencesNature.ProgrammeEtudeId,
                    Nom = "Électricité et magnétisme",
                    Description = "N/A",
                    Code = "203NYB05",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = formationGenerale.ProgrammeEtudeId,
                    Nom = "Astrophysique",
                    Description = "N/A",
                    Code = "203314CA",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = techniquesEducationSpecialisee.ProgrammeEtudeId,
                    Nom = "Psychologie de l’enfance",
                    Description = "N/A",
                    Code = "350114CA",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = techniquesEducationSpecialisee.ProgrammeEtudeId,
                    Nom = "Introduction aux problématiques d’adaptation",
                    Description = "N/A",
                    Code = "351124CA",
                    AnneeParcours = 1
                },
                new Cours() {
		    ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
                    Nom = "Mathématiques du génie mécanique",
                    Description = "N/A",
                    Code = "201224CA",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
                    Nom = "Mathématiques appliquées",
                    Description = "N/A",
                    Code = "201115CA",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
                    Nom = "Statique et cinématique",
                    Description = "N/A",
                    Code = "203214CA",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
                    Nom = "Techniques d’usinage 1",
                    Description = "N/A",
                    Code = "241216CA",
                    AnneeParcours = 1
                },
                new Cours() {
                    ProgrammeEtudeId = techniquesGenieMecanique.ProgrammeEtudeId,
                    Nom = "Techniques d’usinage 1",
                    Description = "N/A",
                    Code = "241316",
                    AnneeParcours = 1
                },
                new Cours() {
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
		.With(maisonEdition => maisonEdition.Nom = Faker.Company.Name())
		.Build();
	}

        /// <summary>
        /// Crée une liste des livres de la bibliothèque.
        /// </summary>
        /// <returns>Les livres de la bibliothèque en liste.</returns>
        private static ICollection<LivreBibliotheque> getLivresBibliotheques(ApplicationDbContext context)
        {

            return Builder<LivreBibliotheque>
		.CreateListOfSize(50)
		.All()
		.With(livre => livre.Isbn = "666" + Faker.Identification.UkNhsNumber())
		.With(livre => livre.Titre = Faker.Lorem.Sentence(Faker.RandomNumber.Next(1,8)))
		.With(livre => livre.Resume = Faker.Lorem.Paragraph())
		.With(livre => livre.PhotoCouverture = "N/A")
		.With(livre => livre.DatePublication = Faker.Identification.DateOfBirth())
		.With(livre => livre.MaisonEditionId = context.MaisonsEditions.First().MaisonEditionId)
		.Build();

	}

        /// <summary>
	/// Assigne un état et un prix à chaque livre de la bibliothèque.
        /// </summary>
	private static void setPrixEtatsLivres(ApplicationDbContext context) 
	{
	    EtatLivre etatUsage;
	    EtatLivre etatNeuf;
	    EtatLivre etatDigital;

	    etatUsage = context.EtatsLivres
		    .Where(etatLivre => etatLivre.Nom == "Usagé").First();

	    etatNeuf = context.EtatsLivres
		    .Where(etatLivre => etatLivre.Nom == "Neuf").First();

	    etatDigital = context.EtatsLivres
		    .Where(etatLivre => etatLivre.Nom == "Digital").First();

	    foreach(LivreBibliotheque livreBibliotheque in context.LivresBibliotheque)
	    {

		PrixEtatLivre prixEtatLivreNeuf;
		PrixEtatLivre prixEtatLivreDigital;

		if(Faker.Boolean.Random())
		{

		    PrixEtatLivre prixEtatLivreUsage = new() 
		    {
			EtatLivre = etatUsage,
			LivreBibliotheque = livreBibliotheque,
			Prix = Convert.ToDouble(Faker.RandomNumber.Next(3,500))
		    };

		    prixEtatLivreNeuf = new() 
		    {
			EtatLivre = etatNeuf,
			LivreBibliotheque = livreBibliotheque,
			Prix = Convert.ToDouble(Faker.RandomNumber.Next(3,500))
		    };

		    prixEtatLivreDigital = new() 
		    {
			EtatLivre = etatDigital,
			LivreBibliotheque = livreBibliotheque,
			Prix = Convert.ToDouble(Faker.RandomNumber.Next(3,500))
		    };

		    context.PrixEtatsLivres.Add(prixEtatLivreUsage);
		} 
		else 
		{

		    prixEtatLivreNeuf = new() 
		    {
			EtatLivre = etatNeuf,
			LivreBibliotheque = livreBibliotheque,
			Prix = Convert.ToDouble(Faker.RandomNumber.Next(3,500))
		    };

		    prixEtatLivreDigital = new() 
		    {
			EtatLivre = etatDigital,
			LivreBibliotheque = livreBibliotheque,
			Prix = Convert.ToDouble(Faker.RandomNumber.Next(3,500))
		    };
		}

		context.PrixEtatsLivres.Add(prixEtatLivreNeuf);
		context.PrixEtatsLivres.Add(prixEtatLivreDigital);
		context.SaveChanges();
	    }
	}
    }
}

