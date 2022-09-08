﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vlissides_bibliotheque.Data;

#nullable disable

namespace vlissides_bibliotheque.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220908145835_Initi")]
    partial class Initi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "834684ee-d07f-470a-91ea-01feb16d2f90",
                            ConcurrencyStamp = "6494238c-5ee0-4d6a-925d-20f0e932e406",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "c2c54011-c8a3-44b7-a560-b76da1383d79",
                            ConcurrencyStamp = "69162fbd-767b-4ecd-8cc9-fd1fe2e0322f",
                            Name = "Utilisateur",
                            NormalizedName = "UTILISATEUR"
                        },
                        new
                        {
                            Id = "c7a578b8-1d4b-43c3-a85e-179d132e2aed",
                            ConcurrencyStamp = "9985b076-ab9a-4538-b692-34b21ed3d2e6",
                            Name = "Etudiant",
                            NormalizedName = "ETUDIANT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                            RoleId = "834684ee-d07f-470a-91ea-01feb16d2f90"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Adresse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("App")
                        .HasColumnType("int");

                    b.Property<string>("CodePostale")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<int>("NumeroCivique")
                        .HasColumnType("int");

                    b.Property<string>("Rue")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Auteur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Auteurs");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.AuteurLivre", b =>
                {
                    b.Property<int>("AuteurId")
                        .HasColumnType("int");

                    b.Property<int>("LivreBibliothequeId")
                        .HasColumnType("int");

                    b.HasKey("AuteurId", "LivreBibliothequeId");

                    b.HasIndex("LivreBibliothequeId");

                    b.ToTable("AuteursLivres");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.CommandeEtudiant", b =>
                {
                    b.Property<int>("FactureEtudiantId")
                        .HasColumnType("int");

                    b.Property<int>("LivreBibliothequeId")
                        .HasColumnType("int");

                    b.HasKey("FactureEtudiantId", "LivreBibliothequeId");

                    b.HasIndex("LivreBibliothequeId");

                    b.ToTable("CommandesEtudiants");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Commanditaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Courriel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Commanditaires");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.EtatLivre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EtatsLivres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nom = "Neuf"
                        },
                        new
                        {
                            Id = 2,
                            Nom = "Usagé"
                        },
                        new
                        {
                            Id = 3,
                            Nom = "Numérique"
                        });
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Commentaire")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Etoile")
                        .HasColumnType("int");

                    b.Property<int>("EtudiantId")
                        .HasColumnType("int");

                    b.Property<string>("EtudiantId1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Titre")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("EtudiantId1");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.EvaluationLivre", b =>
                {
                    b.Property<int>("EvaluationId")
                        .HasColumnType("int");

                    b.Property<int>("LivreBibliothequeId")
                        .HasColumnType("int");

                    b.HasKey("EvaluationId", "LivreBibliothequeId");

                    b.HasIndex("LivreBibliothequeId");

                    b.ToTable("EvaluationsLivres");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Evenement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CommanditaireId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Debut")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("Fin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CommanditaireId");

                    b.ToTable("Evenements");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.FactureEtudiant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AdresseLivraisonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFacturation")
                        .HasColumnType("datetime2");

                    b.Property<int>("EtudiantId")
                        .HasColumnType("int");

                    b.Property<string>("EtudiantId1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Tps")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tvq")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TypePaiementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdresseLivraisonId");

                    b.HasIndex("EtudiantId1");

                    b.HasIndex("TypePaiementId");

                    b.ToTable("FactureEtudiant");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreBibliotheque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateEdition")
                        .HasColumnType("datetime2");

                    b.Property<int>("EtatLivreId")
                        .HasColumnType("int");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoCouverture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProgrammeEtudeId")
                        .HasColumnType("int");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("EtatLivreId");

                    b.HasIndex("ProgrammeEtudeId");

                    b.ToTable("LivresBibliotheques");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreEtudiant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("EtudiantId")
                        .HasColumnType("int");

                    b.Property<string>("EtudiantId1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhotoCouverture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProgrammeEtudeId")
                        .HasColumnType("int");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("EtudiantId1");

                    b.HasIndex("ProgrammeEtudeId");

                    b.ToTable("LivresEtudiants");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.ProgrammeEtude", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProgrammesEtudes");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.TypePaiement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypesPaiements");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Utilisateur", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasDiscriminator().HasValue("Utilisateur");

                    b.HasData(
                        new
                        {
                            Id = "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d67bb86f-d158-4f17-8142-49f7c65c082c",
                            Email = "gordon.john@gunclub-alabama.us",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GORDON.JOHN@GUNCLUB-ALABAMA.US",
                            NormalizedUserName = "GORDON.JOHN@GUNCLUB-ALABAMA.US",
                            PasswordHash = "AQAAAAEAACcQAAAAELSomS0hfJEsUloZycenE1Q5thWUmaONIfn18gcWTHwnpwNs8vQ9GZzs3Y4aPflIAA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e940dec0-5761-48d0-bd69-c9a297b4aa6b",
                            TwoFactorEnabled = false,
                            UserName = "gordon.john@gunclub-alabama.us",
                            Nom = "John",
                            Prenom = "Gordon"
                        });
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Etudiant", b =>
                {
                    b.HasBaseType("vlissides_bibliotheque.Models.Utilisateur");

                    b.Property<int>("AdresseFacturationId")
                        .HasColumnType("int");

                    b.Property<int>("AdresseLivraisonId")
                        .HasColumnType("int");

                    b.Property<int>("ProgrammeEtudeId")
                        .HasColumnType("int");

                    b.HasIndex("AdresseFacturationId");

                    b.HasIndex("AdresseLivraisonId");

                    b.HasIndex("ProgrammeEtudeId");

                    b.HasDiscriminator().HasValue("Etudiant");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.AuteurLivre", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Auteur", "Auteur")
                        .WithMany()
                        .HasForeignKey("AuteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.LivreBibliotheque", "LivreBibliotheque")
                        .WithMany()
                        .HasForeignKey("LivreBibliothequeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auteur");

                    b.Navigation("LivreBibliotheque");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.CommandeEtudiant", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.FactureEtudiant", "FactureEtudiant")
                        .WithMany()
                        .HasForeignKey("FactureEtudiantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.LivreBibliotheque", "LivreBibliotheque")
                        .WithMany()
                        .HasForeignKey("LivreBibliothequeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FactureEtudiant");

                    b.Navigation("LivreBibliotheque");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Evaluation", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("EtudiantId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etudiant");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.EvaluationLivre", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Evaluation", "Evaluation")
                        .WithMany()
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.LivreBibliotheque", "LivreBibliotheque")
                        .WithMany()
                        .HasForeignKey("LivreBibliothequeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evaluation");

                    b.Navigation("LivreBibliotheque");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Evenement", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Commanditaire", "Commanditaire")
                        .WithMany()
                        .HasForeignKey("CommanditaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commanditaire");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.FactureEtudiant", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Adresse", "AdresseLivraison")
                        .WithMany()
                        .HasForeignKey("AdresseLivraisonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("EtudiantId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.TypePaiement", "TypePaiement")
                        .WithMany()
                        .HasForeignKey("TypePaiementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdresseLivraison");

                    b.Navigation("Etudiant");

                    b.Navigation("TypePaiement");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreBibliotheque", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.EtatLivre", "EtatLivre")
                        .WithMany()
                        .HasForeignKey("EtatLivreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.ProgrammeEtude", "ProgrammeEtude")
                        .WithMany()
                        .HasForeignKey("ProgrammeEtudeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EtatLivre");

                    b.Navigation("ProgrammeEtude");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreEtudiant", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("EtudiantId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.ProgrammeEtude", "ProgrammeEtude")
                        .WithMany()
                        .HasForeignKey("ProgrammeEtudeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etudiant");

                    b.Navigation("ProgrammeEtude");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Etudiant", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Adresse", "AdresseFacturation")
                        .WithMany()
                        .HasForeignKey("AdresseFacturationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.Adresse", "AdresseLivraison")
                        .WithMany()
                        .HasForeignKey("AdresseLivraisonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.ProgrammeEtude", "ProgrammeEtude")
                        .WithMany()
                        .HasForeignKey("ProgrammeEtudeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AdresseFacturation");

                    b.Navigation("AdresseLivraison");

                    b.Navigation("ProgrammeEtude");
                });
#pragma warning restore 612, 618
        }
    }
}
