using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using vlissides_bibliotheque.Models;
using vlissides_bibliotheque.Constantes;
using Microsoft.EntityFrameworkCore;

namespace vlissides_bibliotheque.Data
{
    /// <summary>
    /// Classe <c>DbContextConfiguration</c> configure la compostion de la base de données.
    /// </summary>
    public class DbContextConfiguration
    {

	private IDbContext _context;
	private const string ROLE_ADMIN_ID = "834684ee-d07f-470a-91ea-01feb16d2f90";
	private const string ROLE_ADMIN_CONCURRENCYSTAMP = "6494238c-5ee0-4d6a-925d-20f0e932e406";
	private const string USER_ADMIN_ID = "83c10a40-c3f6-49bd-b230-f6975cc7befd";
	private const string USER_ADMIN_CONCURRENCYSTAMP = "d67bb86f-d158-4f17-8142-49f7c65c082c";
	private const string ROLE_UTILISATEUR_ID = "c2c54011-c8a3-44b7-a560-b76da1383d79";
	private const string ROLE_UTILISATEUR_CONCURRENCYSTAMP = "69162fbd-767b-4ecd-8cc9-fd1fe2e0322f";
	private const string ROLE_ETUDIANT_ID = "c7a578b8-1d4b-43c3-a85e-179d132e2aed";
	private const string ROLE_ETUDIANT_CONCURRENCYSTAMP = "9985b076-ab9a-4538-b692-34b21ed3d2e6";

	/// <summary>
	/// Constructeur du <c>DbContextConfiguration</c>
	/// <param name="context">dbcontext représentant la db.</param>
	/// </summary>
	public DbContextConfiguration(IDbContext context)
	{

	    _context = context;
	}

	/// <summary>
	/// Génère les tables d'utilisateurs.
	/// </summary>
	/// <param name="builder"></param>
	public void CreerTableUtilisateurs(ModelBuilder builder)
	{
	    builder.Entity<Utilisateur>().ToTable(nameof(_context.Utilisateurs));
	    builder.Entity<Etudiant>().ToTable(nameof(_context.Etudiants));
	}

	/// <summary>
	/// Crée les états de livre.
	/// </summary>
	/// <param name="builder"></param>
	public void CreerEtatLivre(ModelBuilder builder)
	{
	    List<EtatLivre> EtatLivres = new() {
		    new EtatLivre() {
			    EtatLivreId = 1,
			    Nom = NomEtatLivre.Neuf
		    },
		    new EtatLivre() {
			    EtatLivreId = 2,
			    Nom = NomEtatLivre.Usagee
		    },
		    new EtatLivre() {
			    EtatLivreId = 3,
			    Nom = NomEtatLivre.Numerique
		    }
	    };

	    builder.Entity<EtatLivre>().HasData(EtatLivres);
	}

	/// <summary>
	/// Crée les rôles d'utilisateur.
	/// </summary>
	/// <param name="builder"></param>
	public void CreerRoles(ModelBuilder builder)
	{

	    builder
		.Entity<IdentityRole>()
		.HasData
		(
		    new List<IdentityRole>()
		    {
			    new IdentityRole()
			    {
				    Id = ROLE_ADMIN_ID,
				    Name = RolesName.Admin,
				    NormalizedName = RolesName.Admin.ToUpper(),
				    ConcurrencyStamp = ROLE_ADMIN_CONCURRENCYSTAMP
			    },
			    new IdentityRole(){
				    Id = ROLE_UTILISATEUR_ID,
				    Name = RolesName.Utilisateur,
				    NormalizedName = RolesName.Utilisateur.ToUpper(),
				    ConcurrencyStamp = ROLE_UTILISATEUR_CONCURRENCYSTAMP
			    },
			    new IdentityRole()
			    {
				    Id = ROLE_ETUDIANT_ID,
				    Name = RolesName.Etudiant,
				    NormalizedName = RolesName.Etudiant.ToUpper(),
				    ConcurrencyStamp = ROLE_ETUDIANT_CONCURRENCYSTAMP
			    }
		    }
		);
	}

	/// <summary>
	/// Crée un administrateur initial.
	/// </summary>
	/// <param name="builder"></param>
	public void CreerAdmin(ModelBuilder builder)
	{

	    PasswordHasher<Utilisateur> passwordHasher = new();

	    builder
		.Entity<Utilisateur>()
		.HasData
		(
		    new Utilisateur() 
		    {
			Id = USER_ADMIN_ID,
			Email = Emails.EmailAdmin,
			EmailConfirmed = true,
			NormalizedEmail = Emails.EmailAdmin.ToUpper(),
			UserName = Emails.EmailAdmin,
			NormalizedUserName = Emails.EmailAdmin.ToUpper(),
			Nom = "John",
			Prenom = "Gordon",
			ConcurrencyStamp = USER_ADMIN_CONCURRENCYSTAMP,
			PasswordHash = passwordHasher.HashPassword(null, "Jaimelaprog1!")
		    }
		);

	    builder
		.Entity<IdentityUserRole<string>>()
		.HasData
		(
		    new List<IdentityUserRole<string>>()
		    {
			new IdentityUserRole<string>()
			{
				RoleId = ROLE_ADMIN_ID,
				UserId = USER_ADMIN_ID
			}
		    }
		);
	}

	/// <summary>
	/// Crée les tables de liaison.
	/// </summary>
	/// <param name="builder"></param>
	public void CreerTablesLiaison(ModelBuilder builder)
	{

	    builder.Entity<AuteurLivre>().HasKey(auteurLivre => new { auteurLivre.AuteurId, auteurLivre.LivreBibliothequeId });
	    builder.Entity<CommandeEtudiant>().HasKey(CommandeEtudiant => new { CommandeEtudiant.FactureEtudiantId, CommandeEtudiant.PrixEtatLivreId });
	    builder.Entity<EvaluationLivre>().HasKey(evaluationLivre => new { evaluationLivre.EvaluationId, evaluationLivre.LivreBibliothequeId });
	    builder.Entity<CoursProfesseur>().HasKey(coursProfesseur => new { coursProfesseur.CoursId, coursProfesseur.ProfesseurId });
	    builder.Entity<CoursEtudiant>().HasKey(coursEtudiant => new { coursEtudiant.CoursId, coursEtudiant.EtudiantId });
	}

	/// <summary>
	/// Gère les doubles liaisons.
	/// </summary>
	/// <param name="builder"></param>
	public void CreerDoubleFK(ModelBuilder builder)
	{
		builder.Entity<Etudiant>()
		   .HasOne(m => m.ProgrammeEtude)
		   .WithMany()
		   .OnDelete(DeleteBehavior.NoAction);

		builder.Entity<Etudiant>()
			.HasOne(m => m.Adresse)
			.WithMany()
			.OnDelete(DeleteBehavior.NoAction);

		builder.Entity<CoursLivre>()
			.HasOne(m => m.LivreBibliotheque)
			.WithMany()
			.OnDelete(DeleteBehavior.NoAction);
	}
    }
}
