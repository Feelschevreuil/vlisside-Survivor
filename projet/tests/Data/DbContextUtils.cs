using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque_tests.Constantes;
using vlissides_bibliotheque.Data;

namespace vlissides_bibliotheque_tests.Data
{

    /// <summary>
    /// </summary>
    public class DbContextUtils
    {

        /// <summary>
        /// Constructeur vide du <c>DbContextInMemory</c>
        /// </summary>
        public static ApplicationDbContext GetInMemoryDb()
        {

            DbContextOptions<ApplicationDbContext> options;
            DbContextOptionsBuilder<ApplicationDbContext> builder;
            ApplicationDbContext context;

            builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(ConstantesDb.DB_NOM);
            options = builder.Options;
            context = new(options);

            return context;
        }
    }
}
