﻿using System;
using FizzWare.NBuilder;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;

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
					Nom = "Techniques de l'informatique"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 2,
					Nom = "Techniques de tourisme"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 3,
					Nom = "Technologie du génie életrique"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 4,
					Nom = "Technologie du génie industriel"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 5,
					Nom = "Techniques de génie mécanique"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 6,
					Nom = "Techniques de comptabilité et de gestion"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 7,
					Nom = "Techniques d'éducation spécialisée"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 8,
					Nom = "Techniques d'éducation à l'enfance"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 9,
					Nom = "Soins infirmiers"
				},
				new ProgrammeEtude() {
					ProgrammeEtudeId = 10,
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

