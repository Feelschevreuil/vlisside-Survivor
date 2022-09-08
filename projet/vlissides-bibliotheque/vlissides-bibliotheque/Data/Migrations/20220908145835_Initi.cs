using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Data.Migrations
{
    public partial class Initi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdresseFacturationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdresseLivraisonId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgrammeEtudeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ville = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    NumeroCivique = table.Column<int>(type: "int", nullable: false),
                    App = table.Column<int>(type: "int", nullable: false),
                    Rue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CodePostale = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commanditaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Courriel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commanditaires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EtatsLivres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtatsLivres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtudiantId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Etoile = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_AspNetUsers_EtudiantId1",
                        column: x => x.EtudiantId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammesEtudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammesEtudes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesPaiements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesPaiements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evenements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommanditaireId = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evenements_Commanditaires_CommanditaireId",
                        column: x => x.CommanditaireId,
                        principalTable: "Commanditaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivresBibliotheques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtatLivreId = table.Column<int>(type: "int", nullable: false),
                    ProgrammeEtudeId = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    PhotoCouverture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEdition = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivresBibliotheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivresBibliotheques_EtatsLivres_EtatLivreId",
                        column: x => x.EtatLivreId,
                        principalTable: "EtatsLivres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivresBibliotheques_ProgrammesEtudes_ProgrammeEtudeId",
                        column: x => x.ProgrammeEtudeId,
                        principalTable: "ProgrammesEtudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivresEtudiants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtudiantId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    PhotoCouverture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgrammeEtudeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivresEtudiants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivresEtudiants_AspNetUsers_EtudiantId1",
                        column: x => x.EtudiantId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivresEtudiants_ProgrammesEtudes_ProgrammeEtudeId",
                        column: x => x.ProgrammeEtudeId,
                        principalTable: "ProgrammesEtudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactureEtudiant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePaiementId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdresseLivraisonId = table.Column<int>(type: "int", nullable: false),
                    DateFacturation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tvq = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactureEtudiant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactureEtudiant_Adresses_AdresseLivraisonId",
                        column: x => x.AdresseLivraisonId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactureEtudiant_AspNetUsers_EtudiantId1",
                        column: x => x.EtudiantId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactureEtudiant_TypesPaiements_TypePaiementId",
                        column: x => x.TypePaiementId,
                        principalTable: "TypesPaiements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuteursLivres",
                columns: table => new
                {
                    LivreBibliothequeId = table.Column<int>(type: "int", nullable: false),
                    AuteurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuteursLivres", x => new { x.AuteurId, x.LivreBibliothequeId });
                    table.ForeignKey(
                        name: "FK_AuteursLivres_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuteursLivres_LivresBibliotheques_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationsLivres",
                columns: table => new
                {
                    LivreBibliothequeId = table.Column<int>(type: "int", nullable: false),
                    EvaluationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationsLivres", x => new { x.EvaluationId, x.LivreBibliothequeId });
                    table.ForeignKey(
                        name: "FK_EvaluationsLivres_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationsLivres_LivresBibliotheques_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandesEtudiants",
                columns: table => new
                {
                    FactureEtudiantId = table.Column<int>(type: "int", nullable: false),
                    LivreBibliothequeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandesEtudiants", x => new { x.FactureEtudiantId, x.LivreBibliothequeId });
                    table.ForeignKey(
                        name: "FK_CommandesEtudiants_FactureEtudiant_FactureEtudiantId",
                        column: x => x.FactureEtudiantId,
                        principalTable: "FactureEtudiant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandesEtudiants_LivresBibliotheques_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "834684ee-d07f-470a-91ea-01feb16d2f90", "6494238c-5ee0-4d6a-925d-20f0e932e406", "Admin", "ADMIN" },
                    { "c2c54011-c8a3-44b7-a560-b76da1383d79", "69162fbd-767b-4ecd-8cc9-fd1fe2e0322f", "Utilisateur", "UTILISATEUR" },
                    { "c7a578b8-1d4b-43c3-a85e-179d132e2aed", "9985b076-ab9a-4538-b692-34b21ed3d2e6", "Etudiant", "ETUDIANT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nom", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Prenom", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "83c10a40-c3f6-49bd-b230-f6975cc7befd", 0, "d67bb86f-d158-4f17-8142-49f7c65c082c", "Utilisateur", "gordon.john@gunclub-alabama.us", false, false, null, "John", "GORDON.JOHN@GUNCLUB-ALABAMA.US", "GORDON.JOHN@GUNCLUB-ALABAMA.US", "AQAAAAEAACcQAAAAELSomS0hfJEsUloZycenE1Q5thWUmaONIfn18gcWTHwnpwNs8vQ9GZzs3Y4aPflIAA==", null, false, "Gordon", "e940dec0-5761-48d0-bd69-c9a297b4aa6b", false, "gordon.john@gunclub-alabama.us" });

            migrationBuilder.InsertData(
                table: "EtatsLivres",
                columns: new[] { "Id", "Nom" },
                values: new object[,]
                {
                    { 1, "Neuf" },
                    { 2, "Usagé" },
                    { 3, "Numérique" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "834684ee-d07f-470a-91ea-01feb16d2f90", "83c10a40-c3f6-49bd-b230-f6975cc7befd" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdresseFacturationId",
                table: "AspNetUsers",
                column: "AdresseFacturationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdresseLivraisonId",
                table: "AspNetUsers",
                column: "AdresseLivraisonId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProgrammeEtudeId",
                table: "AspNetUsers",
                column: "ProgrammeEtudeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuteursLivres_LivreBibliothequeId",
                table: "AuteursLivres",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandesEtudiants_LivreBibliothequeId",
                table: "CommandesEtudiants",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EtudiantId1",
                table: "Evaluations",
                column: "EtudiantId1");

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
                name: "IX_FactureEtudiant_EtudiantId1",
                table: "FactureEtudiant",
                column: "EtudiantId1");

            migrationBuilder.CreateIndex(
                name: "IX_FactureEtudiant_TypePaiementId",
                table: "FactureEtudiant",
                column: "TypePaiementId");

            migrationBuilder.CreateIndex(
                name: "IX_LivresBibliotheques_EtatLivreId",
                table: "LivresBibliotheques",
                column: "EtatLivreId");

            migrationBuilder.CreateIndex(
                name: "IX_LivresBibliotheques_ProgrammeEtudeId",
                table: "LivresBibliotheques",
                column: "ProgrammeEtudeId");

            migrationBuilder.CreateIndex(
                name: "IX_LivresEtudiants_EtudiantId1",
                table: "LivresEtudiants",
                column: "EtudiantId1");

            migrationBuilder.CreateIndex(
                name: "IX_LivresEtudiants_ProgrammeEtudeId",
                table: "LivresEtudiants",
                column: "ProgrammeEtudeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AdresseFacturationId",
                table: "AspNetUsers",
                column: "AdresseFacturationId",
                principalTable: "Adresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AdresseLivraisonId",
                table: "AspNetUsers",
                column: "AdresseLivraisonId",
                principalTable: "Adresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProgrammesEtudes_ProgrammeEtudeId",
                table: "AspNetUsers",
                column: "ProgrammeEtudeId",
                principalTable: "ProgrammesEtudes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AdresseFacturationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AdresseLivraisonId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ProgrammesEtudes_ProgrammeEtudeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AuteursLivres");

            migrationBuilder.DropTable(
                name: "CommandesEtudiants");

            migrationBuilder.DropTable(
                name: "EvaluationsLivres");

            migrationBuilder.DropTable(
                name: "Evenements");

            migrationBuilder.DropTable(
                name: "LivresEtudiants");

            migrationBuilder.DropTable(
                name: "Auteurs");

            migrationBuilder.DropTable(
                name: "FactureEtudiant");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "LivresBibliotheques");

            migrationBuilder.DropTable(
                name: "Commanditaires");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "TypesPaiements");

            migrationBuilder.DropTable(
                name: "EtatsLivres");

            migrationBuilder.DropTable(
                name: "ProgrammesEtudes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdresseFacturationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdresseLivraisonId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProgrammeEtudeId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2c54011-c8a3-44b7-a560-b76da1383d79");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7a578b8-1d4b-43c3-a85e-179d132e2aed");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "834684ee-d07f-470a-91ea-01feb16d2f90", "83c10a40-c3f6-49bd-b230-f6975cc7befd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "834684ee-d07f-470a-91ea-01feb16d2f90");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd");

            migrationBuilder.DropColumn(
                name: "AdresseFacturationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AdresseLivraisonId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProgrammeEtudeId",
                table: "AspNetUsers");
        }
    }
}
