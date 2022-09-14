using System;

namespace seeder {

	class Program {

		public static void Main(String[] args) {

			// Créer le dbcontext
			var context = DbContextBibliotheque.CreateDbContext();
		}
	}

}
