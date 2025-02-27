using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class smartRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etudiants_Adresses_AdresseId",
                table: "Etudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_Etudiants_ProgrammesEtudes_ProgrammeEtudeId",
                table: "Etudiants");

            migrationBuilder.DropTable(
                name: "AuteursLivres");

            migrationBuilder.DropTable(
                name: "CoursEtudiants");

            migrationBuilder.DropTable(
                name: "CoursLivres");

            migrationBuilder.DropTable(
                name: "CoursProfesseurs");

            migrationBuilder.DropIndex(
                name: "IX_Etudiants_AdresseId",
                table: "Etudiants");

            migrationBuilder.AddColumn<int>(
                name: "ProfesseurId",
                table: "Cours",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EtudiantCours",
                columns: table => new
                {
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtudiantCours", x => new { x.CoursId, x.EtudiantId });
                    table.ForeignKey(
                        name: "FK_EtudiantCours_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "CoursId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EtudiantCours_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LivreBibliothequeAuteur",
                columns: table => new
                {
                    AuteurId = table.Column<int>(type: "int", nullable: false),
                    LivreBibliothequeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivreBibliothequeAuteur", x => new { x.AuteurId, x.LivreBibliothequeId });
                    table.ForeignKey(
                        name: "FK_LivreBibliothequeAuteur_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "AuteurId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LivreBibliothequeAuteur_LivresBibliotheque_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheque",
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LivreBibliothequeCours",
                columns: table => new
                {
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    LivreBibliothequeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivreBibliothequeCours", x => new { x.CoursId, x.LivreBibliothequeId });
                    table.ForeignKey(
                        name: "FK_LivreBibliothequeCours_Cours_CoursId",
                        column: x => x.CoursId,
                        principalTable: "Cours",
                        principalColumn: "CoursId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LivreBibliothequeCours_LivresBibliotheque_LivreBibliothequeId",
                        column: x => x.LivreBibliothequeId,
                        principalTable: "LivresBibliotheque",
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJzvFMsemUHa76KNiHR05RuASApQxeB1xJsLrmdTZ3ct/WqG1oCFShH4XNPtga6mFg==", "9c509e78-f1aa-4c4a-90b2-dd799b89972f" });

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_AdresseId",
                table: "Etudiants",
                column: "AdresseId",
                unique: true,
                filter: "[AdresseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cours_ProfesseurId",
                table: "Cours",
                column: "ProfesseurId");

            migrationBuilder.CreateIndex(
                name: "IX_EtudiantCours_EtudiantId",
                table: "EtudiantCours",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_LivreBibliothequeAuteur_LivreBibliothequeId",
                table: "LivreBibliothequeAuteur",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_LivreBibliothequeCours_LivreBibliothequeId",
                table: "LivreBibliothequeCours",
                column: "LivreBibliothequeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cours_Professeurs_ProfesseurId",
                table: "Cours",
                column: "ProfesseurId",
                principalTable: "Professeurs",
                principalColumn: "ProfesseurId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Etudiants_Adresses_AdresseId",
                table: "Etudiants",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "AdresseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Etudiants_ProgrammesEtudes_ProgrammeEtudeId",
                table: "Etudiants",
                column: "ProgrammeEtudeId",
                principalTable: "ProgrammesEtudes",
                principalColumn: "ProgrammeEtudeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cours_Professeurs_ProfesseurId",
                table: "Cours");

            migrationBuilder.DropForeignKey(
                name: "FK_Etudiants_Adresses_AdresseId",
                table: "Etudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_Etudiants_ProgrammesEtudes_ProgrammeEtudeId",
                table: "Etudiants");

            migrationBuilder.DropTable(
                name: "EtudiantCours");

            migrationBuilder.DropTable(
                name: "LivreBibliothequeAuteur");

            migrationBuilder.DropTable(
                name: "LivreBibliothequeCours");

            migrationBuilder.DropIndex(
                name: "IX_Etudiants_AdresseId",
                table: "Etudiants");

            migrationBuilder.DropIndex(
                name: "IX_Cours_ProfesseurId",
                table: "Cours");

            migrationBuilder.DropColumn(
                name: "ProfesseurId",
                table: "Cours");

            migrationBuilder.CreateTable(
                name: "AuteursLivres",
                columns: table => new
                {
                    AuteurId = table.Column<int>(type: "int", nullable: false),
                    LivreBibliothequeId = table.Column<int>(type: "int", nullable: false)
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
                name: "CoursEtudiants",
                columns: table => new
                {
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    EtudiantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "CoursLivres",
                columns: table => new
                {
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    LivreBibliothequeId = table.Column<int>(type: "int", nullable: false),
                    Complementaire = table.Column<bool>(type: "bit", nullable: false),
                    CoursLivreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursLivres", x => new { x.CoursId, x.LivreBibliothequeId });
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
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursProfesseurs",
                columns: table => new
                {
                    CoursId = table.Column<int>(type: "int", nullable: false),
                    ProfesseurId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAELvdUNOChlF9frsD9/60WEgSa3+RNn8YwU6/74PaTzfzT3fj/4j/snZrRfxAaMAHoA==", "de6b9ac5-d18e-43a4-b457-2677359722d9" });

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_AdresseId",
                table: "Etudiants",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_AuteursLivres_LivreBibliothequeId",
                table: "AuteursLivres",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursEtudiants_EtudiantId",
                table: "CoursEtudiants",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursLivres_LivreBibliothequeId",
                table: "CoursLivres",
                column: "LivreBibliothequeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursProfesseurs_ProfesseurId",
                table: "CoursProfesseurs",
                column: "ProfesseurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Etudiants_Adresses_AdresseId",
                table: "Etudiants",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "AdresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Etudiants_ProgrammesEtudes_ProgrammeEtudeId",
                table: "Etudiants",
                column: "ProgrammeEtudeId",
                principalTable: "ProgrammesEtudes",
                principalColumn: "ProgrammeEtudeId");
        }
    }
}
