using System;
using FizzWare.NBuilder;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;

namespace seeder {

	class Program {

		public static void Main(String[] args) {

			var context = DbContextBibliotheque.CreateDbContext();

			// Enlever les données
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

			context.Utilisateurs
				.RemoveRange(context.Utilisateurs);

			context.TypesPaiements
				.RemoveRange(context.TypesPaiements);
			// FIN Enlever les données

			context.Adresses.AddRange(getAdresses());

			context.Auteurs.AddRange(getAuteurs());

			context.EtatsLivres.AddRange(getEtatsLivres());

			context.ProgrammesEtudes.AddRange(getProgrammesEtudes());

			// TODO: rename LivresBibliotheques à LivresBibliotheque
			context.LivresBibliotheques.AddRange(getLivresBibliotheques());
		}

		/// <summary>
		/// Crée une liste d'Adresses.
		/// </summary>
		/// <returns>Les adresses en liste.</returns>
		private static List<Adresse> getAdresses() {

			return new List<Adresse> {
				new Adresse() {
					Id = 0,
					Ville = "Berkeley",
					NumeroCivique = 386,
					App = 1,
					Rue = "Distribution",
					CodePostal = "X6X6X6"
				},
				new Adresse() {
					Id = 1,
					Ville = "Hell",
					NumeroCivique = 666,
					App = 69,
					Rue = "Roadin Bud",
					CodePostal = "X0X1X1"
				},
				new Adresse() {
					Id = 2,
					Ville = "e-railed",
					NumeroCivique = 30,
					App = 3,
					Rue = "Open",
					CodePostal = "X1X1X1"
				},
				new Adresse() {
					Id = 3,
					Ville = "systemagic",
					NumeroCivique = 31,
					Rue = "BSD",
					CodePostal = "X2X2X2"
				},
				new Adresse() {
					Id = 4,
					Ville = "Goldflipper",
					NumeroCivique = 32,
					App = 23,
					Rue = "Software",
					CodePostal = "X3X3X3"
				},
				new Adresse() {
					Id = 5,
					Ville = "Puff the Barbian",
					NumeroCivique = 33,
					App = 33,
					Rue = "Barbian2",
					CodePostal = "X4X4X4"
				},
				new Adresse() {
					Id = 6,
					Ville = "Legend of Puffy Hood",
					NumeroCivique = 34,
					App = 43,
					Rue = "Legend",
					CodePostal = "X5X5X5"
				},
				new Adresse() {
					Id = 7,
					Ville = "Redundency",
					NumeroCivique = 35,
					App = 53,
					Rue = "CARP",
					CodePostal = "X7X7X7"
				},
				new Adresse() {
					Id = 8,
					Ville = "Pond-erosa",
					NumeroCivique = 36,
					App = 63,
					Rue = "Puff",
					CodePostal = "X8X8X8"
				},
				new Adresse() {
					Id = 9,
					Ville = "Wizard",
					NumeroCivique = 37,
					App = 73,
					Rue = "OS",
					CodePostal = "X9X9X9"
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
					Id = 0,
					Nom = "DeRaad",
					Prenom = "Theo"
				},
				new Auteur() {
					Id = 1,
					Nom = "Stallman",
					Prenom = "Richard"
				},
				new Auteur() {
					Id = 2,
					Nom = "Thompson",
					Prenom = "Ken"
				},
				new Auteur() {
					Id = 3,
					Nom = "Ritchie",
					Prenom = "Dennis"
				},
				new Auteur() {
					Id = 4,
					Nom = "Torvalds",
					Prenom = "Linus"
				},
				new Auteur() {
					Id = 5,
					Nom = "Tanenbaum",
					Prenom = "Andrew"
				},
				new Auteur() {
					Id = 6,
					Nom = "Kernhigan",
					Prenom = "Brian"
				},
				new Auteur() {
					Id = 7,
					Nom = "Lovelace",
					Prenom = "Ada"
				},
				new Auteur() {
					Id = 8,
					Nom = "Unix",
					Prenom = "Research"
				},
				new Auteur() {
					Id = 9,
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
					Id = 0,
					Nom = "Neuf"
				},
				new EtatLivre() {
					Id = 1,
					Nom = "Usagé"
				},
				new EtatLivre() {
					Id = 2,
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
					Id = 0,
					Nom = "Techniques de l'informatique"
				},
				new ProgrammeEtude() {
					Id = 1,
					Nom = "Techniques de tourisme"
				},
				new ProgrammeEtude() {
					Id = 2,
					Nom = "Technologie du génie életrique"
				},
				new ProgrammeEtude() {
					Id = 3,
					Nom = "Technologie du génie industriel"
				},
				new ProgrammeEtude() {
					Id = 4,
					Nom = "Techniques de génie mécanique"
				},
				new ProgrammeEtude() {
					Id = 5,
					Nom = "Techniques de comptabilité et de gestion"
				},
				new ProgrammeEtude() {
					Id = 6,
					Nom = "Techniques d'éducation spécialisée"
				},
				new ProgrammeEtude() {
					Id = 7,
					Nom = "Techniques d'éducation à l'enfance"
				},
				new ProgrammeEtude() {
					Id = 8,
					Nom = "Soins infirmiers"
				},
				new ProgrammeEtude() {
					Id = 9,
					Nom = "Gestion de commerces"
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
					Id = 0,
					EtatLivreId = 0,
					ProgrammeEtudeId = 0,
					Isbn = "9999999990",
					Titre = "",
					Resume = "",
					PhotoCouverture = "N/A",
					DateEdition = DateTime.Now
				}
			};
		}
	}
}

