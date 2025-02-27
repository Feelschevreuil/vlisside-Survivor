using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vlissides_bibliotheque.Models;
using System.Collections.Generic;
using vlissides_bibliotheque.Commun;

namespace vlissides_bibliotheque.Data
{
    /// <summary>
    /// Classe <c>ApplicationDbContext</c> définit la compostion de la base de données.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Commanditaire> Commanditaires { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<LivreBibliotheque> LivresBibliotheque { get; set; }
        public DbSet<LivreEtudiant> LivresEtudiants { get; set; }
        public DbSet<ProgrammeEtude> ProgrammesEtudes { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<MaisonEdition> MaisonsEdition { get; set; }
        public DbSet<Professeur> Professeurs { get; set; }
        public DbSet<Province> Provinces { get; set; }


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

            CreerTableUtilisateurs(builder);

            CreerRoles(builder);

            CreerAdmin(builder);

            CreerDoubleFK(builder);
        }

        /// <summary>
        /// Génère les tables d'utilisateurs.
        /// </summary>
        /// <param name="builder"></param>
        private void CreerTableUtilisateurs(ModelBuilder builder)
        {
            builder.Entity<Utilisateur>().ToTable(nameof(Utilisateurs));
            builder.Entity<Etudiant>().ToTable(nameof(Etudiants));
        }

        /// <summary>
        /// Crée les rôles d'utilisateur.
        /// </summary>
        /// <param name="builder"></param>
        private void CreerRoles(ModelBuilder builder)
        {

            // UTILISATEUR
            const string ROLE_UTILISATEUR_ID = "c2c54011-c8a3-44b7-a560-b76da1383d79";
            const string ROLE_UTILISATEUR_CONCURRENCYSTAMP = "69162fbd-767b-4ecd-8cc9-fd1fe2e0322f";
            // ETUDIANT
            const string ROLE_ETUDIANT_ID = "c7a578b8-1d4b-43c3-a85e-179d132e2aed";
            const string ROLE_ETUDIANT_CONCURRENCYSTAMP = "9985b076-ab9a-4538-b692-34b21ed3d2e6";
            // AJOUT DES RÔLES
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>(){
                new IdentityRole()
                {
                    Id = ROLE_ADMIN_ID,
                    Name = Constante.Admin,
                    NormalizedName = Constante.Admin.ToUpper(),
                    ConcurrencyStamp = ROLE_ADMIN_CONCURRENCYSTAMP
                },
                new IdentityRole(){
                    Id = ROLE_UTILISATEUR_ID,
                    Name = Constante.Utilisateur,
                    NormalizedName = Constante.Utilisateur.ToUpper(),
                    ConcurrencyStamp = ROLE_UTILISATEUR_CONCURRENCYSTAMP
                },
                new IdentityRole()
                {
                    Id = ROLE_ETUDIANT_ID,
                    Name = Constante.Etudiant,
                    NormalizedName = Constante.Etudiant.ToUpper(),
                    ConcurrencyStamp = ROLE_ETUDIANT_CONCURRENCYSTAMP
                }
            });
        }

        /// <summary>
        /// Crée un administrateur initial.
        /// </summary>
        /// <param name="builder"></param>
        private void CreerAdmin(ModelBuilder builder)
        {
            // Obj pour HASHER un mot de passe
            PasswordHasher<Utilisateur> passwordHasher = new();
            // AJOUT D'UN ADMIN
            builder.Entity<Utilisateur>().HasData(new Utilisateur()
            {
                Id = USER_ADMIN_ID,
                Email = Constante.EMAIL_ADMIN,
                EmailConfirmed = true,
                NormalizedEmail = Constante.EMAIL_ADMIN.ToUpper(),
                UserName = Constante.EMAIL_ADMIN,
                NormalizedUserName = Constante.EMAIL_ADMIN.ToUpper(),
                Nom = "John",
                Prenom = "Gordon",
                ConcurrencyStamp = USER_ADMIN_CONCURRENCYSTAMP,
                PasswordHash = passwordHasher.HashPassword(null, "Aleatoire1!")
            });

            // Lier utilisateur et rôle
            builder.Entity<IdentityUserRole<string>>().HasData(new List<IdentityUserRole<string>>(){
                new IdentityUserRole<string>()
                {
                    RoleId = ROLE_ADMIN_ID,
                    UserId = USER_ADMIN_ID
                }
            });
        }


        /// <summary>
        /// Gère les doubles liaisons.
        /// </summary>
        /// <param name="builder"></param>
        private void CreerDoubleFK(ModelBuilder builder)
        {
            builder.Entity<Etudiant>()
                .HasOne(e => e.Adresse)
                .WithOne(a => a.Etudiant)
                .HasForeignKey<Etudiant>(e => e.AdresseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Etudiant>()
                .HasMany(e => e.Cours)
                .WithMany(c => c.Etudiants) 
                .UsingEntity<Dictionary<string, object>>(
                    "EtudiantCours",
                    j => j
                        .HasOne<Cours>()
                        .WithMany()
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Etudiant>()
                        .WithMany()
                        .HasForeignKey("EtudiantId")
                        .OnDelete(DeleteBehavior.Restrict)
                );

            builder.Entity<Professeur>()
                .HasMany(p => p.Cours)
                .WithOne(c => c.Professeur)
                .HasForeignKey(c => c.ProfesseurId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LivreBibliotheque>()
                .HasMany(l => l.Auteurs)
                .WithMany(a => a.Livres)
                .UsingEntity<Dictionary<string, object>>(
                    "LivreBibliothequeAuteur",
                    j => j
                        .HasOne<Auteur>()
                        .WithMany()
                        .HasForeignKey("AuteurId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<LivreBibliotheque>()
                        .WithMany()
                        .HasForeignKey("LivreBibliothequeId")
                        .OnDelete(DeleteBehavior.Restrict)
                );


            builder.Entity<LivreBibliotheque>()
                .HasMany(l => l.Cours)
                .WithMany(c => c.LivresBibliotheque)
                .UsingEntity<Dictionary<string, object>>(
                    "LivreBibliothequeCours",
                    j => j
                        .HasOne<Cours>()
                        .WithMany()
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<LivreBibliotheque>()
                        .WithMany()
                        .HasForeignKey("LivreBibliothequeId")
                        .OnDelete(DeleteBehavior.Restrict)
                );
        }

    }
}
