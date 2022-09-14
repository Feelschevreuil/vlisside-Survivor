using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using vlissides_bibliotheque.Data;

namespace seeder {
	class DbContextBibliotheque {

		public static ApplicationDbContext CreateDbContext() {

			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

			// mssql
			var connectionString = configuration.getConnectionString("mssql");
			builder.UseSqlServer(connectionString);
		}
	}
}

