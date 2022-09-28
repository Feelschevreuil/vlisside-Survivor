using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using vlissides_bibliotheque.Data;
using System.Runtime.InteropServices;

namespace seeder
{
    class DbContextBibliotheque {

        public static ApplicationDbContext CreateDbContext() {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

                string connectionString;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
            {
                connectionString = configuration.GetConnectionString("mssql") ?? throw new InvalidOperationException("Connection string 'mssql' not found.");
            
                builder.UseSqlServer(connectionString);
            }
            else
            {
                connectionString = configuration.GetConnectionString("sqlite") ?? throw new InvalidOperationException("Connection string 'sqlite' not found.");

                builder.UseSqlite(connectionString);
            }

            builder.EnableSensitiveDataLogging(true);
            builder.EnableDetailedErrors(true);

            return new ApplicationDbContext(builder.Options);
        }
    }
}
