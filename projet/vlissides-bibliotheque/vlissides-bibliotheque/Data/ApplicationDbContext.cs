using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Constantes;

namespace vlissides_bibliotheque.Data
{
    /// <summary>
    /// Classe <c>ApplicationDbContext</c> définit la compostion de la base de données.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext, IDbContext
	{
		public DbSet<Adresse> Adresses { get; set; }
		public DbSet<Auteur> Auteurs { get; set; }
		public DbSet<AuteurLivre> AuteursLivres { get; set; }
		public DbSet<CommandeEtudiant> CommandesEtudiants { get; set; }
		public DbSet<Commanditaire> Commanditaires { get; set; }
		public DbSet<EtatLivre> EtatsLivres { get; set; }
		public DbSet<Etudiant> Etudiants { get; set; }
		public DbSet<Evaluation> Evaluations { get; set; }
		public DbSet<EvaluationLivre> EvaluationsLivres { get; set; }
		public DbSet<Evenement> Evenements { get; set; }
		public DbSet<LivreBibliotheque> LivresBibliotheque { get; set; }
		public DbSet<LivreEtudiant> LivresEtudiants { get; set; }
		public DbSet<ProgrammeEtude> ProgrammesEtudes { get; set; }
		public DbSet<Utilisateur> Utilisateurs { get; set; }
		public DbSet<TypePaiement> TypesPaiement { get; set; }
		public DbSet<MaisonEdition> MaisonsEditions { get; set; }
		public DbSet<PrixEtatLivre> PrixEtatsLivres { get; set; }
		public DbSet<CoursLivre> CoursLivres { get; set; }
		public DbSet<Cours> Cours { get; set; }
		public DbSet<CoursProfesseur> CoursProfesseurs { get; set; }
		public DbSet<Professeur> Professeurs { get; set; }
		public DbSet<Province> Provinces { get; set; }
		public DbSet<CoursEtudiant> CoursEtudiants { get; set; }
		public DbSet<FactureEtudiant> FacturesEtudiants { get; set; }

		private const string ROLE_ADMIN_ID = "834684ee-d07f-470a-91ea-01feb16d2f90";
		private const string ROLE_ADMIN_CONCURRENCYSTAMP = "6494238c-5ee0-4d6a-925d-20f0e932e406";
		private const string USER_ADMIN_ID = "83c10a40-c3f6-49bd-b230-f6975cc7befd";
		private const string USER_ADMIN_CONCURRENCYSTAMP = "d67bb86f-d158-4f17-8142-49f7c65c082c";

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			DbContextConfiguration dbContextConfiguration;

			dbContextConfiguration = new(this);

			dbContextConfiguration.CreerTableUtilisateurs(builder);

			dbContextConfiguration.CreerEtatLivre(builder);

			dbContextConfiguration.CreerRoles(builder);

			dbContextConfiguration.CreerAdmin(builder);

			dbContextConfiguration.CreerTablesLiaison(builder);

			dbContextConfiguration.CreerDoubleFK(builder);
		}
	}
}
