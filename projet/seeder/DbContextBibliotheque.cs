using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration;
using vlissides_bibliotheque.Data;
using vlissides_bibliotheque.Services;

namespace seeder
{
           class DbContextBibliotheque {

		public static ApplicationDbContext CreateDbContext() {

			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(Directory.GetCurrentDirectory() + "/appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

			BuilderServices.ApplyConnectionString(builder);

			return new ApplicationDbContext(builder.Options);
		}
	}
}
