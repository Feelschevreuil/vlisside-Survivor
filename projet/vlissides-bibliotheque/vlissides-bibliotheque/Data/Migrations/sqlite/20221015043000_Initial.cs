using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    AuteurId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.AuteurId);
                });

            migrationBuilder.CreateTable(
                name: "Commanditaires",
                columns: table => new
                {
                    CommanditaireId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Courriel = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commanditaires", x => x.CommanditaireId);
                });

            migrationBuilder.CreateTable(
                name: "EtatsLivres",
                columns: table => new
                {
                    EtatLivreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtatsLivres", x => x.EtatLivreId);
                });

            migrationBuilder.CreateTable(
                name: "MaisonsEditions",
                columns: table => new
                {
                    MaisonEditionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaisonsEditions", x => x.MaisonEditionId);
                });

            migrationBuilder.CreateTable(
                name: "Professeurs",
                columns: table => new
                {
                    ProfesseurId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professeurs", x => x.ProfesseurId);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammesEtudes",
                columns: table => new
                {
                    ProgrammeEtudeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammesEtudes", x => x.ProgrammeEtudeId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "TypesPaiement",
                columns: table => new
                {
                    TypePaiementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesPaiement", x => x.TypePaiementId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evenements",
                columns: table => new
                {
                    EvenementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommanditaireId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Debut = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenements", x => x.EvenementId);
                    table.ForeignKey(
                        name: "FK_Evenements_Commanditaires_CommanditaireId",
                        column: x => x.CommanditaireId,
                        principalTable: "Commanditaires",
                        principalColumn: "CommanditaireId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivresBibliotheque",
                columns: table => new
                {
                    LivreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaisonEditionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Isbn = table.Column<string>(type: "TEXT", nullable: false),
                    Titre = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Resume = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    PhotoCouverture = table.Column<string>(type: "TEXT", nullable: false),
                    DatePublication = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivresBibliotheque", x => x.LivreId);
                    table.ForeignKey(
                        name: "FK_LivresBibliotheque_MaisonsEditions_MaisonEditionId",
                        column: x => x.MaisonEditionId,
                        principalTable: "MaisonsEditions",
                        principalColumn: "MaisonEditionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    CoursId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgrammeEtudeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    AnneeParcours = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.CoursId);
                    table.ForeignKey(
                        name: "FK_Cours_ProgrammesEtudes_ProgrammeEtudeId",
                        column: x => x.ProgrammeEtudeId,
                        principalTable: "ProgrammesEtudes",
                        principalColumn: "ProgrammeEtudeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    AdresseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ville = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    NumeroCivique = table.Column<int>(type: "INTEGER", nullable: false),
                    App = table.Column<int>(type: "INTEGER", nullable: false),
                    Rue = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    CodePostal = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    ProvinceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.AdresseId);
                    table.ForeignKey(
                        name: "FK_Adresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuteursLivres",
                columns: table => new
                {
                    LivreBibliothequeId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuteurId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuteursLivres", x => new { x.AuteurId, x.LivreBibliothequeId });
                    table.ForeignKey(
                        name: "FK_AuteursLivres_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "AuteurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuteursLivres_LivresBibliotheque_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheque",
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrixEtatsLivres",
                columns: table => new
                {
                    PrixEtatLivreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EtatLivreId = table.Column<int>(type: "INTEGER", nullable: false),
                    LivreBibliothequeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Prix = table.Column<double>(type: "REAL", nullable: false),
                    NombreUsager = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrixEtatsLivres", x => x.PrixEtatLivreId);
                    table.ForeignKey(
                        name: "FK_PrixEtatsLivres_EtatsLivres_EtatLivreId",
                        column: x => x.EtatLivreId,
                        principalTable: "EtatsLivres",
                        principalColumn: "EtatLivreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrixEtatsLivres_LivresBibliotheque_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheque",
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursLivres",
                columns: table => new
                {
                    CoursLivreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoursId = table.Column<int>(type: "INTEGER", nullable: false),
                    LivreBibliothequeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Complementaire = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursLivres", x => x.CoursLivreId);
                    table.ForeignKey(
                        name: "FK_CoursLivres_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "CoursId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursLivres_LivresBibliotheque_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheque",
                        principalColumn: "LivreId");
                });

            migrationBuilder.CreateTable(
                name: "CoursProfesseurs",
                columns: table => new
                {
                    ProfesseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoursId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursProfesseurs", x => new { x.CoursId, x.ProfesseurId });
                    table.ForeignKey(
                        name: "FK_CoursProfesseurs_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "CoursId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursProfesseurs_Professeurs_ProfesseurId",
                        column: x => x.ProfesseurId,
                        principalTable: "Professeurs",
                        principalColumn: "ProfesseurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Etudiants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ProgrammeEtudeId = table.Column<int>(type: "INTEGER", nullable: false),
                    AdresseId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnneeParcours = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etudiants_Adresses_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adresses",
                        principalColumn: "AdresseId");
                    table.ForeignKey(
                        name: "FK_Etudiants_ProgrammesEtudes_ProgrammeEtudeId",
                        column: x => x.ProgrammeEtudeId,
                        principalTable: "ProgrammesEtudes",
                        principalColumn: "ProgrammeEtudeId");
                    table.ForeignKey(
                        name: "FK_Etudiants_Utilisateurs_Id",
                        column: x => x.Id,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursEtudiants",
                columns: table => new
                {
                    CoursId = table.Column<int>(type: "INTEGER", nullable: false),
                    EtudiantId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursEtudiants", x => new { x.CoursId, x.EtudiantId });
                    table.ForeignKey(
                        name: "FK_CoursEtudiants_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "CoursId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursEtudiants_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    EvaluationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EtudiantId = table.Column<string>(type: "TEXT", nullable: false),
                    Etoiles = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Titre = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    Commentaire = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.EvaluationId);
                    table.ForeignKey(
                        name: "FK_Evaluations_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactureEtudiant",
                columns: table => new
                {
                    FactureEtudiantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypePaiementId = table.Column<int>(type: "INTEGER", nullable: false),
                    EtudiantId = table.Column<string>(type: "TEXT", nullable: false),
                    AdresseLivraisonId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateFacturation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tps = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tvq = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactureEtudiant", x => x.FactureEtudiantId);
                    table.ForeignKey(
                        name: "FK_FactureEtudiant_Adresses_AdresseLivraisonId",
                        column: x => x.AdresseLivraisonId,
                        principalTable: "Adresses",
                        principalColumn: "AdresseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactureEtudiant_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactureEtudiant_TypesPaiement_TypePaiementId",
                        column: x => x.TypePaiementId,
                        principalTable: "TypesPaiement",
                        principalColumn: "TypePaiementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivresEtudiants",
                columns: table => new
                {
                    LivreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EtudiantId = table.Column<string>(type: "TEXT", nullable: false),
                    Titre = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    PhotoCouverture = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivresEtudiants", x => x.LivreId);
                    table.ForeignKey(
                        name: "FK_LivresEtudiants_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationsLivres",
                columns: table => new
                {
                    LivreBibliothequeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EvaluationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationsLivres", x => new { x.EvaluationId, x.LivreBibliothequeId });
                    table.ForeignKey(
                        name: "FK_EvaluationsLivres_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "EvaluationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationsLivres_LivresBibliotheque_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheque",
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandesEtudiants",
                columns: table => new
                {
                    FactureEtudiantId = table.Column<int>(type: "INTEGER", nullable: false),
                    LivreBibliothequeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantite = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandesEtudiants", x => new { x.FactureEtudiantId, x.LivreBibliothequeId });
                    table.ForeignKey(
                        name: "FK_CommandesEtudiants_FactureEtudiant_FactureEtudiantId",
                        column: x => x.FactureEtudiantId,
                        principalTable: "FactureEtudiant",
                        principalColumn: "FactureEtudiantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandesEtudiants_LivresBibliotheque_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheque",
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "834684ee-d07f-470a-91ea-01feb16d2f90", "6494238c-5ee0-4d6a-925d-20f0e932e406", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2c54011-c8a3-44b7-a560-b76da1383d79", "69162fbd-767b-4ecd-8cc9-fd1fe2e0322f", "Utilisateur", "UTILISATEUR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7a578b8-1d4b-43c3-a85e-179d132e2aed", "9985b076-ab9a-4538-b692-34b21ed3d2e6", "Etudiant", "ETUDIANT" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "83c10a40-c3f6-49bd-b230-f6975cc7befd", 0, "d67bb86f-d158-4f17-8142-49f7c65c082c", "gordon.john@gunclub-alabama.us", true, false, null, "GORDON.JOHN@GUNCLUB-ALABAMA.US", "GORDON.JOHN@GUNCLUB-ALABAMA.US", "AQAAAAEAACcQAAAAEKumz2dijrRE9BD/NbNPS4+EPXqx9Jp3jfLfpUyKiI88Kd873gtdw7L6UIExvukVwQ==", null, false, "ba76ca5e-84b2-4e8f-9041-ed5c4796f8ac", false, "gordon.john@gunclub-alabama.us" });

            migrationBuilder.InsertData(
                table: "EtatsLivres",
                columns: new[] { "EtatLivreId", "Nom" },
                values: new object[] { 1, "Neuf" });

            migrationBuilder.InsertData(
                table: "EtatsLivres",
                columns: new[] { "EtatLivreId", "Nom" },
                values: new object[] { 2, "Usagé" });

            migrationBuilder.InsertData(
                table: "EtatsLivres",
                columns: new[] { "EtatLivreId", "Nom" },
                values: new object[] { 3, "Numérique" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "834684ee-d07f-470a-91ea-01feb16d2f90", "83c10a40-c3f6-49bd-b230-f6975cc7befd" });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "Id", "Nom", "Prenom" },
                values: new object[] { "83c10a40-c3f6-49bd-b230-f6975cc7befd", "John", "Gordon" });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_ProvinceId",
                table: "Adresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuteursLivres_LivreBibliothequeId",
                table: "AuteursLivres",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandesEtudiants_LivreBibliothequeId",
                table: "CommandesEtudiants",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ProgrammeEtudeId",
                table: "Cours",
                column: "ProgrammeEtudeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursEtudiants_EtudiantId",
                table: "CoursEtudiants",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursLivres_CoursId",
                table: "CoursLivres",
                column: "CoursId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursLivres_LivreBibliothequeId",
                table: "CoursLivres",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursProfesseurs_ProfesseurId",
                table: "CoursProfesseurs",
                column: "ProfesseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_AdresseId",
                table: "Etudiants",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_ProgrammeEtudeId",
                table: "Etudiants",
                column: "ProgrammeEtudeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EtudiantId",
                table: "Evaluations",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationsLivres_LivreBibliothequeId",
                table: "EvaluationsLivres",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_CommanditaireId",
                table: "Evenements",
                column: "CommanditaireId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureEtudiant_AdresseLivraisonId",
                table: "FactureEtudiant",
                column: "AdresseLivraisonId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureEtudiant_EtudiantId",
                table: "FactureEtudiant",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureEtudiant_TypePaiementId",
                table: "FactureEtudiant",
                column: "TypePaiementId");

            migrationBuilder.CreateIndex(
                name: "IX_LivresBibliotheque_MaisonEditionId",
                table: "LivresBibliotheque",
                column: "MaisonEditionId");

            migrationBuilder.CreateIndex(
                name: "IX_LivresEtudiants_EtudiantId",
                table: "LivresEtudiants",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_PrixEtatsLivres_EtatLivreId",
                table: "PrixEtatsLivres",
                column: "EtatLivreId");

            migrationBuilder.CreateIndex(
                name: "IX_PrixEtatsLivres_LivreBibliothequeId",
                table: "PrixEtatsLivres",
                column: "LivreBibliothequeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuteursLivres");

            migrationBuilder.DropTable(
                name: "CommandesEtudiants");

            migrationBuilder.DropTable(
                name: "CoursEtudiants");

            migrationBuilder.DropTable(
                name: "CoursLivres");

            migrationBuilder.DropTable(
                name: "CoursProfesseurs");

            migrationBuilder.DropTable(
                name: "EvaluationsLivres");

            migrationBuilder.DropTable(
                name: "Evenements");

            migrationBuilder.DropTable(
                name: "LivresEtudiants");

            migrationBuilder.DropTable(
                name: "PrixEtatsLivres");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Auteurs");

            migrationBuilder.DropTable(
                name: "FactureEtudiant");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Professeurs");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Commanditaires");

            migrationBuilder.DropTable(
                name: "EtatsLivres");

            migrationBuilder.DropTable(
                name: "LivresBibliotheque");

            migrationBuilder.DropTable(
                name: "TypesPaiement");

            migrationBuilder.DropTable(
                name: "Etudiants");

            migrationBuilder.DropTable(
                name: "MaisonsEditions");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "ProgrammesEtudes");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
