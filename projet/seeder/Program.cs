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

            context.FacturesEtudiants
            .RemoveRange(context.FacturesEtudiants);

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

            context.Professeurs
            .RemoveRange(context.Professeurs);
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

            setCoursLivres(context);

            setAuteursParLivres(context);

            context.Professeurs.AddRange(getProfesseurs());

            context.SaveChanges();

            setCoursParProfesseur(context);

            context.TypesPaiement.AddRange(getTypesPaiement());

            context.Commanditaires.AddRange(getCommanditaires());

            context.SaveChanges();

            context.Evenements.AddRange(getEvenementsCommanditaires(context));

            context.SaveChanges();

            context.Etudiants.AddRange(getEtudiants(context));

            context.SaveChanges();

            setCoursParEtudiants(context);

            setFacturesEtudiants(context);

            setLivresEtudiants(context);

            setEvaluations(context);
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
        private static ICollection<Adresse> getAdresses(ApplicationDbContext context)
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
            .CreateListOfSize(25)
            .All()
            .With(auteur => auteur.AuteurId = 0)
            .With(auteur => auteur.Nom = Faker.Name.Last())
            .With(auteur => auteur.Prenom = Faker.Name.First())
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

        // TODO: mettre dans le dbcontext.
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

        // TODO: mettre dans le dbcontext.
        /// <summary>
        /// Crée une liste des livres des cours.
        /// </summary>
        /// <returns>Les cours liste.</returns>
        private static List<Cours> getListeCours(ApplicationDbContext context)
        {

	    ProgrammeEtude techniquesTourisme;
	    ProgrammeEtude sciencesNature;
	    ProgrammeEtude techniquesEducationSpecialisee;
	    ProgrammeEtude techniquesGenieMecanique;
	    ProgrammeEtude formationGenerale;

            techniquesTourisme = context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "414"
            );

            sciencesNature = context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "201"
            );

            techniquesEducationSpecialisee = context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "351"
            );

            techniquesGenieMecanique = context.ProgrammesEtudes.SingleOrDefault(
                programmeEtude => programmeEtude.Code == "241"
            );

            formationGenerale = context.ProgrammesEtudes.SingleOrDefault(
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
        private static ICollection<LivreBibliotheque> getLivresBibliotheques(ApplicationDbContext context)
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

            foreach (LivreBibliotheque livreBibliotheque in context.LivresBibliotheque)
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

                    context.PrixEtatsLivres.Add(prixEtatLivreUsage);
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

                context.PrixEtatsLivres.Add(prixEtatLivreNeuf);
                context.PrixEtatsLivres.Add(prixEtatLivreDigital);

            }

            context.SaveChanges();
        }

        /// <summary>
        /// Assigne des livres nécessaires à un cours.
        /// </summary>
        private static void setCoursLivres(ApplicationDbContext context)
        {

            foreach (Cours cours in context.Cours)
            {

                List<CoursLivre> listeCoursLivre = new();

                for (int i = 0; i < Faker.RandomNumber.Next(5, 12); i++)
                {

                    CoursLivre coursLivre;
                    LivreBibliotheque livreBibliotheque;

                    livreBibliotheque = context
			.LivresBibliotheque
			.Skip(Faker.RandomNumber.Next(0, context.LivresBibliotheque.Count()) - 1)
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

                context.CoursLivres.AddRange(listeCoursLivre);

            }

            context.SaveChanges();
        }

        /// <summary>
        /// Assigne les auteurs à des livres.
        /// </summary>
        private static void setAuteursParLivres(ApplicationDbContext context)
        {

            foreach (LivreBibliotheque livreBibliotheque in context.LivresBibliotheque)
            {

                var auteurs = context
                    .Auteurs
                    .Skip(Faker.RandomNumber.Next(0, context.Auteurs.Count()) - 4)
                    .Take(Faker.RandomNumber.Next(1, 3));

                foreach (Auteur auteur in auteurs)
                {
		    AuteurLivre auteurLivre;

                    auteurLivre = new()
                    {
                        Auteur = auteur,
                        LivreBibliotheque = livreBibliotheque
                    };

                    context.AuteursLivres.Add(auteurLivre);
                }
            }

            context.SaveChanges();
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
        private static void setCoursParProfesseur(ApplicationDbContext context)
        {

            foreach (Professeur professeur in context.Professeurs)
            {

                var choixCours = context
                    .Cours
                    .Skip(Faker.RandomNumber.Next(0, context.Cours.Count() - 4))
                    .Take(Faker.RandomNumber.Next(1, 3));

                foreach (Cours cours in choixCours)
                {

		    CoursProfesseur coursProfesseur;

                    coursProfesseur = new()
                    {
                        Professeur = professeur,
                        Cours = cours
                    };

                    context.CoursProfesseurs.Add(coursProfesseur);
                }

            }

            context.SaveChanges();
            assignerCoursSansProfesseurs(context);
        }

        /// <summary>
        /// Assigne un professeur à un cours s'il n'en a pas un assigné. Dans le cas
        /// qu'un cours reste sans professeurs après l'assignation des professeurs à
        /// un cours.
        /// </summary>
        private static void assignerCoursSansProfesseurs(ApplicationDbContext context)
        {

            foreach (Cours cours in context.Cours)
            {

                int nombreProfesseurs;

                nombreProfesseurs = context
                    .CoursProfesseurs
                    .Where(coursProfesseurs => coursProfesseurs.CoursId == cours.CoursId)
                    .Count();

                if (nombreProfesseurs == 0)
                {

                    CoursProfesseur coursProfesseur;
                    Professeur professeur;

                    professeur = context
                    .Professeurs
                    // TODO: optimiser
                    .Take(1)
                    .First();

                    coursProfesseur = new()
                    {
                        Cours = cours,
                        Professeur = professeur
                    };

                    context.CoursProfesseurs.Add(coursProfesseur);
                }
            }

            context.SaveChanges();
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
        private static ICollection<Evenement> getEvenementsCommanditaires(ApplicationDbContext context)
        {

	    List<Evenement> evenements;

            evenements = new();

            foreach (Commanditaire commanditaire in context.Commanditaires)
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
        private static ICollection<Etudiant> getEtudiants(ApplicationDbContext context)
        {

                   Adresse adresse;

                   adresse = getAdresseAleatoire(context);

                   context.Adresses.Add(adresse);
                   context.SaveChanges();

            return Builder<Etudiant>
               .CreateListOfSize(50)
               .All()
               .With(etudiant => etudiant.Email = Faker.Internet.Email())
               .With(etudiant => etudiant.Nom = Faker.Name.Last())
               .With(etudiant => etudiant.Prenom = Faker.Name.First())
               .With(etudiant => etudiant
				    .ProgrammeEtude = context
					.ProgrammesEtudes
					.Skip(Faker.RandomNumber.Next(0, context.ProgrammesEtudes.Count() - 1))
					.Take(1)
					.First())
               .With(etudiant => etudiant.Adresse = adresse)
               .Build();
        }

        /// <summary>
        /// Crée une adresse aléatoirement.
        /// </summary>
        private static Adresse getAdresseAleatoire(ApplicationDbContext context)
        {
	    return new Adresse()
            {
		AdresseId = 0,
		App = Faker.RandomNumber.Next(1, 55),
		CodePostal = Faker.Address.UkPostCode(),
		NumeroCivique = Faker.RandomNumber.Next(1, 666),
		Province = context
			    .Provinces
				.Skip(Faker.RandomNumber.Next(0, context.Provinces.Count() - 1))
				.Take(1)
				.First(),
		Rue = Faker.Address.StreetName(),
		Ville = Faker.Address.City()
	    };
        }

        /// <summary>
        /// Assigne les cours aux étudiants.
        /// </summary>
        private static void setCoursParEtudiants(ApplicationDbContext context)
        {

            foreach (Etudiant etudiant in context.Etudiants)
            {

                var listeCoursEtudiant = context
                    .Cours
                    .Skip(Faker.RandomNumber.Next(0, context.Cours.Count() - 9))
                    .Take(Faker.RandomNumber.Next(3, 8));

                foreach (Cours cours in listeCoursEtudiant)
                {

		    CoursEtudiant coursEtudiant;

                    coursEtudiant = new()
                    {
                        Etudiant = etudiant,
                        Cours = cours
                    };

                    context.CoursEtudiants.Add(coursEtudiant);
                }
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Génère des factures aux étudiants.
        /// </summary>
        private static void setFacturesEtudiants(ApplicationDbContext context)
        {

            foreach (Etudiant etudiant in context.Etudiants)
            {

                bool asDejaCommande;

                asDejaCommande = Faker.Boolean.Random();

                if (asDejaCommande)
                {

                    for (int factures = 0; factures < Faker.RandomNumber.Next(1, 5); factures++)
                    {

                        FactureEtudiant factureEtudiant;
                        List<CommandeEtudiant> commandesEtudiants;

                        factureEtudiant = creerFactureEtudiant(context, etudiant);

                        context.FacturesEtudiants.Add(factureEtudiant);

                        commandesEtudiants = new();

                        for (int commandes = 0; commandes < Faker.RandomNumber.Next(1, 8); commandes++)
                        {

                            CommandeEtudiant commandeEtudiant;

                            commandeEtudiant = creerCommandeEtudiant(context, factureEtudiant);

                            if (!livreDejaDansCommande(commandesEtudiants, commandeEtudiant))
                            {
                                commandesEtudiants.Add(commandeEtudiant);
                            }
                            else
                            {
                                commandes--;
                            }

                        }

                        context.CommandesEtudiants.AddRange(commandesEtudiants);
                    }
                }

            }

            context.SaveChanges();
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
        private static FactureEtudiant creerFactureEtudiant(ApplicationDbContext context, Etudiant etudiant)
        {

            FactureEtudiant factureEtudiant;

            factureEtudiant = new()
            {
                FactureEtudiantId = 0,
                TypePaiement = context
				.TypesPaiement
				    .Skip(Faker.RandomNumber.Next(0, context.TypesPaiement.Count() - 1))
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
        private static CommandeEtudiant creerCommandeEtudiant(ApplicationDbContext context, FactureEtudiant factureEtudiant)
        {

            CommandeEtudiant commandeEtudiant;

            commandeEtudiant = new()
            {
                FactureEtudiant = factureEtudiant,
                PrixEtatLivre = context
				    .PrixEtatsLivres
					.Skip(Faker.RandomNumber.Next(0, context.PrixEtatsLivres.Count() - 1))
					.Take(1)
					.First(),
                Quantite = Faker.RandomNumber.Next(1, 2)
            };

            return commandeEtudiant;
        }


        /// <summary>
        /// Génère la liste des livres des étudiants.
        /// </summary>
        private static void setLivresEtudiants(ApplicationDbContext context)
        {

            foreach (Etudiant etudiant in context.Etudiants)
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

                        context.LivresEtudiants.Add(livreEtudiant);
                    }
                }
            }

            context.SaveChanges();
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
        private static void setEvaluations(ApplicationDbContext context)
        {

            var livresComplementaires = context.CoursLivres.Where(coursLivre => coursLivre.Complementaire);

            foreach (CoursLivre coursLivre in livresComplementaires)
            {

                bool ajouterEvaluations;

                ajouterEvaluations = Faker.Boolean.Random();

                if (ajouterEvaluations)
                {

                    int nombreMaximumEtudiants;

                    nombreMaximumEtudiants = Convert.ToInt32((context.Etudiants.Count() - 1) / 2);

                    foreach (Etudiant etudiant in context.Etudiants.Take(Faker.RandomNumber.Next(1, nombreMaximumEtudiants)))
                    {

                        Evaluation evaluation;
                        EvaluationLivre evaluationLivre;

                        evaluation = creerEvaluation(context, coursLivre, etudiant);

			// TODO: valider si c'est sauvegardé
                        context.Evaluations.Add(evaluation);

                        // TODO: valider l'utilité d'une table de liaison. Pourquoi ne pas uniquement ajouter l'étudiant à l'évaluation?on?
                        evaluationLivre = new()
                        {
                            Evaluation = evaluation,
                            LivreBibliothequeId = coursLivre.LivreBibliothequeId
                        };

                        context.EvaluationsLivres.Add(evaluationLivre);

                    }
                }
            }

            context.SaveChanges();
        }

        // TODO: valider l'utilité d'une table de liaison. Pourquoi ne pas uniquement ajouter l'étudiant à l'évaluation?
        /// <summary>
        /// Crée une évaluation d'un livre complémentaire.
        /// <paramref name="coursLivre">Objet CoursLivre contenant le livre de la bibliothèque.</param>
        /// <paramref name="etudiant">L'étudiant qui évalue le livre.</param>
        /// </summary>
        /// <returns>le livre de l'étudiant.</returns>
        private static Evaluation creerEvaluation(ApplicationDbContext context, CoursLivre coursLivre, Etudiant etudiant)
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

