using System;
using FizzWare.NBuilder;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Data;

namespace seeder {

	class Program {

		private static ApplicationDbContext? context;

		public static void Main(String[] args) {

			context = DbContextBibliotheque.CreateDbContext();

		}
	}
}
