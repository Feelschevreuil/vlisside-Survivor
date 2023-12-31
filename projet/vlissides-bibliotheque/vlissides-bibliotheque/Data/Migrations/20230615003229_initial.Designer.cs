﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vlissides_bibliotheque.Data;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230615003229_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
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
                    b.Property<int>("AdresseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdresseId"), 1L, 1);

                    b.Property<string>("App")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<int>("NumeroCivique")
                        .HasColumnType("int");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<string>("Rue")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("AdresseId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Auteur", b =>
                {
                    b.Property<int>("AuteurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuteurId"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("AuteurId");

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

            modelBuilder.Entity("vlissides_bibliotheque.Models.Commanditaire", b =>
                {
                    b.Property<int>("CommanditaireId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommanditaireId"), 1L, 1);

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

                    b.HasKey("CommanditaireId");

                    b.ToTable("Commanditaires");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Cours", b =>
                {
                    b.Property<int>("CoursId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CoursId"), 1L, 1);

                    b.Property<int>("AnneeParcours")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("ProgrammeEtudeId")
                        .HasColumnType("int");

                    b.HasKey("CoursId");

                    b.HasIndex("ProgrammeEtudeId");

                    b.ToTable("Cours");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.CoursEtudiant", b =>
                {
                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<string>("EtudiantId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CoursId", "EtudiantId");

                    b.HasIndex("EtudiantId");

                    b.ToTable("CoursEtudiants");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.CoursLivre", b =>
                {
                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<int>("LivreBibliothequeId")
                        .HasColumnType("int");

                    b.Property<bool>("Complementaire")
                        .HasColumnType("bit");

                    b.Property<int>("CoursLivreId")
                        .HasColumnType("int");

                    b.HasKey("CoursId", "LivreBibliothequeId");

                    b.HasIndex("LivreBibliothequeId");

                    b.ToTable("CoursLivres");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.CoursProfesseur", b =>
                {
                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.Property<int>("ProfesseurId")
                        .HasColumnType("int");

                    b.HasKey("CoursId", "ProfesseurId");

                    b.HasIndex("ProfesseurId");

                    b.ToTable("CoursProfesseurs");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Evenement", b =>
                {
                    b.Property<int>("EvenementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EvenementId"), 1L, 1);

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

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("EvenementId");

                    b.HasIndex("CommanditaireId");

                    b.ToTable("Evenements");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreBibliotheque", b =>
                {
                    b.Property<int>("LivreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LivreId"), 1L, 1);

                    b.Property<DateTime>("DatePublication")
                        .HasColumnType("datetime2");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaisonEditionId")
                        .HasColumnType("int");

                    b.Property<string>("PhotoCouverture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrixJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("LivreId");

                    b.HasIndex("MaisonEditionId");

                    b.ToTable("LivresBibliotheque");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreEtudiant", b =>
                {
                    b.Property<int>("LivreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LivreId"), 1L, 1);

                    b.Property<string>("Auteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePublication")
                        .HasColumnType("datetime2");

                    b.Property<string>("EtudiantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaisonEdition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoCouverture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Prix")
                        .HasColumnType("float");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("LivreId");

                    b.HasIndex("EtudiantId");

                    b.ToTable("LivresEtudiants");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.MaisonEdition", b =>
                {
                    b.Property<int>("MaisonEditionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaisonEditionId"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaisonEditionId");

                    b.ToTable("MaisonsEdition");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Professeur", b =>
                {
                    b.Property<int>("ProfesseurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfesseurId"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroProfesseur")
                        .HasColumnType("int");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProfesseurId");

                    b.ToTable("Professeurs");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.ProgrammeEtude", b =>
                {
                    b.Property<int>("ProgrammeEtudeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgrammeEtudeId"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("ProgrammeEtudeId");

                    b.ToTable("ProgrammesEtudes");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Province", b =>
                {
                    b.Property<int>("ProvinceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProvinceId"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProvinceId");

                    b.ToTable("Provinces");
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

                    b.ToTable("Utilisateurs", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d67bb86f-d158-4f17-8142-49f7c65c082c",
                            Email = "AdminAleatoire@CollegeConnaissanceAleatoire.qc.ca",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMINALEATOIRE@COLLEGECONNAISSANCEALEATOIRE.QC.CA",
                            NormalizedUserName = "ADMINALEATOIRE@COLLEGECONNAISSANCEALEATOIRE.QC.CA",
                            PasswordHash = "AQAAAAEAACcQAAAAEFUdkXS8wC+1GH2m3oAYaQ8LzEmAEEvxq14ApUFVeuDz+1DX0r4wEBn69CwsPHxD5Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a6736028-37a0-41bf-acfd-2ee72393de08",
                            TwoFactorEnabled = false,
                            UserName = "AdminAleatoire@CollegeConnaissanceAleatoire.qc.ca",
                            Nom = "John",
                            Prenom = "Gordon"
                        });
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Etudiant", b =>
                {
                    b.HasBaseType("vlissides_bibliotheque.Models.Utilisateur");

                    b.Property<int>("AdresseId")
                        .HasColumnType("int");

                    b.Property<int>("AnneeParcours")
                        .HasColumnType("int");

                    b.Property<int>("NumeroEtudiant")
                        .HasColumnType("int");

                    b.Property<int>("ProgrammeEtudeId")
                        .HasColumnType("int");

                    b.HasIndex("AdresseId");

                    b.HasIndex("ProgrammeEtudeId");

                    b.ToTable("Etudiants", (string)null);
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

            modelBuilder.Entity("vlissides_bibliotheque.Models.Adresse", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.AuteurLivre", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Auteur", "Auteur")
                        .WithMany("Livres")
                        .HasForeignKey("AuteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.LivreBibliotheque", "LivreBibliotheque")
                        .WithMany("Auteurs")
                        .HasForeignKey("LivreBibliothequeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auteur");

                    b.Navigation("LivreBibliotheque");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Cours", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.ProgrammeEtude", "ProgrammeEtude")
                        .WithMany()
                        .HasForeignKey("ProgrammeEtudeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgrammeEtude");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.CoursEtudiant", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Cours", "Cours")
                        .WithMany()
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("EtudiantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cours");

                    b.Navigation("Etudiant");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.CoursLivre", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Cours", "Cours")
                        .WithMany("Livres")
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.LivreBibliotheque", "LivreBibliotheque")
                        .WithMany("Cours")
                        .HasForeignKey("LivreBibliothequeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cours");

                    b.Navigation("LivreBibliotheque");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.CoursProfesseur", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Cours", "Cours")
                        .WithMany()
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.Professeur", "Professeur")
                        .WithMany()
                        .HasForeignKey("ProfesseurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cours");

                    b.Navigation("Professeur");
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

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreBibliotheque", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.MaisonEdition", "MaisonEdition")
                        .WithMany()
                        .HasForeignKey("MaisonEditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaisonEdition");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreEtudiant", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("EtudiantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etudiant");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Utilisateur", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithOne()
                        .HasForeignKey("vlissides_bibliotheque.Models.Utilisateur", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Etudiant", b =>
                {
                    b.HasOne("vlissides_bibliotheque.Models.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.Utilisateur", null)
                        .WithOne()
                        .HasForeignKey("vlissides_bibliotheque.Models.Etudiant", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("vlissides_bibliotheque.Models.ProgrammeEtude", "ProgrammeEtude")
                        .WithMany()
                        .HasForeignKey("ProgrammeEtudeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Adresse");

                    b.Navigation("ProgrammeEtude");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Auteur", b =>
                {
                    b.Navigation("Livres");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.Cours", b =>
                {
                    b.Navigation("Livres");
                });

            modelBuilder.Entity("vlissides_bibliotheque.Models.LivreBibliotheque", b =>
                {
                    b.Navigation("Auteurs");

                    b.Navigation("Cours");
                });
#pragma warning restore 612, 618
        }
    }
}
