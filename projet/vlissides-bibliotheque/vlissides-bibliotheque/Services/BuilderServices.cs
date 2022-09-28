using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Data;

namespace vlissides_bibliotheque.Services {

	/// <summary>
	/// Classe <c>BuilderServices</c> possède les services pour <c>WebApplicationBuilder</c>.
	/// <summary>
	class BuilderServices {

		///
		/// <summary>
		/// Applique la connection string appropriée selon le système 
		/// d'exploitation.
		/// <param name="builder">Builder ayant besoin de connection string.</param>
		/// </summary>
		public static void ApplyConnectionString(WebApplicationBuilder builder) {

		    string connectionString;

	            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
		    {
			connectionString = builder.Configuration.GetConnectionString("mssql") ?? throw new InvalidOperationException("Connection string 'mssql' not found.");
	    
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			    options.UseSqlServer(connectionString));
		    }
		    else
		    {
			connectionString = builder.Configuration.GetConnectionString("sqlite") ?? throw new InvalidOperationException("Connection string 'sqlite' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(
			    options => options.UseSqlite(
				connectionString
			    )
			);
		    }

		    Console.WriteLine("Connection string: " + connectionString);
		}
	}
}
