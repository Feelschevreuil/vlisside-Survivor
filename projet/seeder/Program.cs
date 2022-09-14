using System;
using FizzWare.NBuilder;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;

namespace seeder {

	class Program {

		private static ApplicationDbContext? context;

		public static void Main(String[] args) {

			context = DbContextBibliotheque.CreateDbContext();

			enleverDonnees();
		}

		/// <summary>
		/// Fonction qui enlève les données présentes de
		/// la base de données.
		/// </summary>
		private static void enleverDonnees() {

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

		}
	}
}
