using System;
using FizzWare.NBuilder;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;
using System.Linq;

namespace seeder {

	class Program {

		public static void Main(String[] args) {

			var context = DbContextBibliotheque.CreateDbContext();

			// Enlever les données
			context.Province
				.RemoveRange(context.Province);

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

			context.LivresBibliotheques
				.RemoveRange(context.LivresBibliotheques);

			context.LivresEtudiants
				.RemoveRange(context.LivresEtudiants);

			context.ProgrammesEtudes
				.RemoveRange(context.ProgrammesEtudes);

			context.Cours
				.RemoveRange(context.Cours);

			context.MaisonsEditions
				.RemoveRange(context.MaisonsEditions);

			context.Utilisateurs
				.RemoveRange(context.Utilisateurs);

			context.TypesPaiements
				.RemoveRange(context.TypesPaiements);
			// FIN Enlever les données
			
			context.Province.AddRange(getProvinces());

			context.Adresses.AddRange(getAdresses());

			context.Auteurs.AddRange(getAuteurs());

			context.EtatsLivres.AddRange(getEtatsLivres());

			context.ProgrammesEtudes.AddRange(getProgrammesEtudes());

			context.SaveChanges();

			context.Cours.AddRange(getListeCours());

			context.SaveChanges();

			context.MaisonsEditions.AddRange(getMaisonsEdition());

			// TODO: rename LivresBibliotheques à LivresBibliotheque
			context.LivresBibliotheques.AddRange(getLivresBibliotheques());

			context.SaveChanges();
		}

		/// <summary>
		/// Crée une liste de provinces.
		/// </summary>
		/// <returns>Les provinces en liste.</returns>
		private static List<Province> getProvinces() {

			return new List<Province> {
				new Province() {
					ProvinceId = 1,
					Nom = "Québec"
				},
				new Province() {
					ProvinceId = 2,
					Nom = "Ontario"
				}
			};
		}

		/// <summary>
		/// Crée une liste d'Adresses.
		/// </summary>
		/// <returns>Les adresses en liste.</returns>
		private static List<Adresse> getAdresses() {

			return new List<Adresse> {
				new Adresse() {
					AdresseId = 1,
					Ville = "Berkeley",
					NumeroCivique = 386,
					App = 1,
					Rue = "Distribution",
					CodePostal = "X6X6X6",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 2,
					Ville = "Hell",
					NumeroCivique = 666,
					App = 69,
					Rue = "Roadin Bud",
					CodePostal = "X0X1X1",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 3,
					Ville = "e-railed",
					NumeroCivique = 30,
					App = 3,
					Rue = "Open",
					CodePostal = "X1X1X1",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 4,
					Ville = "systemagic",
					NumeroCivique = 31,
					Rue = "BSD",
					CodePostal = "X2X2X2",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 5,
					Ville = "Goldflipper",
					NumeroCivique = 32,
					App = 23,
					Rue = "Software",
					CodePostal = "X3X3X3",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 6,
					Ville = "Puff the Barbian",
					NumeroCivique = 33,
					App = 33,
					Rue = "Barbian2",
					CodePostal = "X4X4X4",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 7,
					Ville = "Legend of Puffy Hood",
					NumeroCivique = 34,
					App = 43,
					Rue = "Legend",
					CodePostal = "X5X5X5",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 8,
					Ville = "Redundency",
					NumeroCivique = 35,
					App = 53,
					Rue = "CARP",
					CodePostal = "X7X7X7",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 9,
					Ville = "Pond-erosa",
					NumeroCivique = 36,
					App = 63,
					Rue = "Puff",
					CodePostal = "X8X8X8",
					ProvinceId = 1
				},
				new Adresse() {
					AdresseId = 10,
					Ville = "Wizard",
					NumeroCivique = 37,
					App = 73,
					Rue = "OS",
					CodePostal = "X9X9X9",
					ProvinceId = 1
				}
			};
		}

		/// <summary>
		/// Crée une liste d'Auteurs.
		/// </summary>
		/// <returns>Les auteurs liste.</returns>
		private static List<Auteur> getAuteurs() {

			return new List<Auteur> {
				new Auteur() {
					AuteurId = 1,
					Nom = "DeRaad",
					Prenom = "Theo"
				},
				new Auteur() {
					AuteurId = 2,
					Nom = "Stallman",
					Prenom = "Richard"
				},
				new Auteur() {
					AuteurId = 3,
					Nom = "Thompson",
					Prenom = "Ken"
				},
				new Auteur() {
					AuteurId = 4,
					Nom = "Ritchie",
					Prenom = "Dennis"
				},
				new Auteur() {
					AuteurId = 5,
					Nom = "Torvalds",
					Prenom = "Linus"
				},
				new Auteur() {
					AuteurId = 6,
					Nom = "Tanenbaum",
					Prenom = "Andrew"
				},
				new Auteur() {
					AuteurId = 7,
					Nom = "Kernhigan",
					Prenom = "Brian"
				},
				new Auteur() {
					AuteurId = 8,
					Nom = "Lovelace",
					Prenom = "Ada"
				},
				new Auteur() {
					AuteurId = 9,
					Nom = "Unix",
					Prenom = "Research"
				},
				new Auteur() {
					AuteurId = 10,
					Nom = "Al",
					Prenom = "Et"
				}
			};
		}

		/// <summary>
		/// Crée une liste des États des livres.
		/// </summary>
		/// <returns>Les États des livres en liste.</returns>
		private static List<EtatLivre> getEtatsLivres() {
			
			return new List<EtatLivre> {
				new EtatLivre() {
					EtatLivreId = 1,
					Nom = "Neuf"
				},
				new EtatLivre() {
					EtatLivreId = 2,
					Nom = "Usagé"
				},
				new EtatLivre() {
					EtatLivreId = 3,
					Nom = "Digital"
				}
			};
		}

		/// <summary>
		/// Crée une liste des programmes d'études.
		/// </summary>
		/// <returns>Les programmes d'études en liste.</returns>
		private static List<ProgrammeEtude> getProgrammesEtudes() {
			
			return new List<ProgrammeEtude> {
				new ProgrammeEtude() {
					ProgrammeEtudeId = 1,
					Nom = "Techniques de tourisme",
					Code = "414"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 2,
					Nom = "Sciences de la Nature",
					Code = "201"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 3,
					Nom = "Techniques d'éducatoin spécialisée",
					Code = "351"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 4,
					Nom = "Techniques de génie mécanique",
					Code = "241"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 5,
					Nom = "Formation générale",
					Code = "x"
				}
			};
		}

		/// <summary>
		/// Crée une liste des livres des cours.
		/// </summary>
		/// <returns>Les cours liste.</returns>
		private static List<Cours> getListeCours() {

			return new List<Cours> {
				new Cours() {
					CoursId = 1,
					ProgrammeEtudeId = 1,
					Nom = "Exploration des carrières en tourisme",
					Description = "N/A",
					Code = "414313CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 2,
					ProgrammeEtudeId = 1,
					Nom = "Introduction au programme de Tourisme",
					Description = "N/A",
					Code = "414133CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 3,
					ProgrammeEtudeId = 1,
					Nom = "Accueil et service à la clientèle",
					Description = "N/A",
					Code = "414154CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 4,
					ProgrammeEtudeId = 1,
					Nom = "Destinations touristiques : les Amériques",
					Description = "N/A",
					Code = "414234CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 5,
					ProgrammeEtudeId = 1,
					Nom = "Communication et supervision",
					Description = "N/A",
					Code = "414323CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 6,
					ProgrammeEtudeId = 5,
					Nom = "Écriture et littérature",
					Description = "N/A",
					Code = "601101MQ",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 7,
					ProgrammeEtudeId = 5,
					Nom = "Littérature et imaginaire",
					Description = "N/A",
					Code = "601102MQ",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 8,
					ProgrammeEtudeId = 2,
					Nom = "Calcul intégral",
					Description = "N/A",
					Code = "201NYB05",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 9,
					ProgrammeEtudeId = 2,
					Nom = "Chimie des solutions",
					Description = "N/A",
					Code = "202NYB05",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 10,
					ProgrammeEtudeId = 2,
					Nom = "Électricité et magnétisme",
					Description = "N/A",
					Code = "203NYB05",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 11,
					ProgrammeEtudeId = 5,
					Nom = "Astrophysique",
					Description = "N/A",
					Code = "203314CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 12,
					ProgrammeEtudeId = 3,
					Nom = "Psychologie de l’enfance",
					Description = "N/A",
					Code = "350114CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 13,
					ProgrammeEtudeId = 3,
					Nom = "Introduction aux problématiques d’adaptation",
					Description = "N/A",
					Code = "351124CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 14,
					ProgrammeEtudeId = 4,
					Nom = "Mathématiques du génie mécanique",
					Description = "N/A",
					Code = "201224CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 15,
					ProgrammeEtudeId = 4,
					Nom = "Mathématiques appliquées",
					Description = "N/A",
					Code = "201115CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 16,
					ProgrammeEtudeId = 4,
					Nom = "Statique et cinématique",
					Description = "N/A",
					Code = "203214CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 17,
					ProgrammeEtudeId = 4,
					Nom = "Techniques d’usinage 1",
					Description = "N/A",
					Code = "241216CA",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 18,
					ProgrammeEtudeId = 4,
					Nom = "Techniques d’usinage 1",
					Description = "N/A",
					Code = "241316",
					AnneeParcours = 1
				},
				new Cours() {
					CoursId = 19,
					ProgrammeEtudeId = 4,
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
		private static List<MaisonEdition> getMaisonsEdition() {
			return new List<MaisonEdition> {
				new MaisonEdition() {
					MaisonEditionId = 1,
					Nom = "OpenBSD"
				},
				new MaisonEdition() {
					MaisonEditionId = 2,
					Nom = "NetBSD"
				},
				new MaisonEdition() {
					MaisonEditionId = 3,
					Nom = "FreeBSD"
				},
				new MaisonEdition() {
					MaisonEditionId = 4,
					Nom = "*nix"
				},
				new MaisonEdition() {
					MaisonEditionId = 5,
					Nom = "GNU/Linux"
				},
				new MaisonEdition() {
					MaisonEditionId = 6,
					Nom = "Minix"
				}
			};
		}

		/// <summary>
		/// Crée une liste des livres de la bibliothèque.
		/// </summary>
		/// <returns>Les livres de la bibliothèque en liste.</returns>
		private static List<LivreBibliotheque> getLivresBibliotheques() {

			return new List<LivreBibliotheque> {
				new LivreBibliotheque() {
					LivreId = 1,
					Isbn = "9999999990",
					Titre = "foobar's book",
					Resume = "foobar's goes to the polls.",
					PhotoCouverture = "N/A",
					DatePublication = DateTime.Now,
					MaisonEditionId = 1
				}
			};
		}
	}
}

