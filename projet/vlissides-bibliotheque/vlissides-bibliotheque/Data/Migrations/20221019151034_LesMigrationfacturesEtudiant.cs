using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class LesMigrationfacturesEtudiant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandesEtudiants_FactureEtudiants_FactureEtudiantId",
                table: "CommandesEtudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_FactureEtudiants_Etudiants_EtudiantId",
                table: "FactureEtudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_FactureEtudiants_TypesPaiement_TypePaiementId",
                table: "FactureEtudiants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FactureEtudiants",
                table: "FactureEtudiants");

            migrationBuilder.RenameTable(
                name: "FactureEtudiants",
                newName: "FacturesEtudiants");

            migrationBuilder.RenameIndex(
                name: "IX_FactureEtudiants_TypePaiementId",
                table: "FacturesEtudiants",
                newName: "IX_FacturesEtudiants_TypePaiementId");

            migrationBuilder.RenameIndex(
                name: "IX_FactureEtudiants_EtudiantId",
                table: "FacturesEtudiants",
                newName: "IX_FacturesEtudiants_EtudiantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacturesEtudiants",
                table: "FacturesEtudiants",
                column: "FactureEtudiantId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJwRm0DJ2NJAk3MGdHFiJlMqseRCMUdEwvmodS/XheH2gRCHrVJ43zbT+cKHl9dWTg==", "9ae603d9-880b-4087-a975-a139e92d51dc" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommandesEtudiants_FacturesEtudiants_FactureEtudiantId",
                table: "CommandesEtudiants",
                column: "FactureEtudiantId",
                principalTable: "FacturesEtudiants",
                principalColumn: "FactureEtudiantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturesEtudiants_Etudiants_EtudiantId",
                table: "FacturesEtudiants",
                column: "EtudiantId",
                principalTable: "Etudiants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturesEtudiants_TypesPaiement_TypePaiementId",
                table: "FacturesEtudiants",
                column: "TypePaiementId",
                principalTable: "TypesPaiement",
                principalColumn: "TypePaiementId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandesEtudiants_FacturesEtudiants_FactureEtudiantId",
                table: "CommandesEtudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_FacturesEtudiants_Etudiants_EtudiantId",
                table: "FacturesEtudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_FacturesEtudiants_TypesPaiement_TypePaiementId",
                table: "FacturesEtudiants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacturesEtudiants",
                table: "FacturesEtudiants");

            migrationBuilder.RenameTable(
                name: "FacturesEtudiants",
                newName: "FactureEtudiants");

            migrationBuilder.RenameIndex(
                name: "IX_FacturesEtudiants_TypePaiementId",
                table: "FactureEtudiants",
                newName: "IX_FactureEtudiants_TypePaiementId");

            migrationBuilder.RenameIndex(
                name: "IX_FacturesEtudiants_EtudiantId",
                table: "FactureEtudiants",
                newName: "IX_FactureEtudiants_EtudiantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FactureEtudiants",
                table: "FactureEtudiants",
                column: "FactureEtudiantId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEH/jlebl7a/MpcBzkKxug7EQ+2R8wOhq2LSHMCINIlM9n5luG1NsS0nQCFxth/uyYQ==", "5ee0e41b-e19d-4780-b6e4-1c5fef42653d" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommandesEtudiants_FactureEtudiants_FactureEtudiantId",
                table: "CommandesEtudiants",
                column: "FactureEtudiantId",
                principalTable: "FactureEtudiants",
                principalColumn: "FactureEtudiantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactureEtudiants_Etudiants_EtudiantId",
                table: "FactureEtudiants",
                column: "EtudiantId",
                principalTable: "Etudiants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactureEtudiants_TypesPaiement_TypePaiementId",
                table: "FactureEtudiants",
                column: "TypePaiementId",
                principalTable: "TypesPaiement",
                principalColumn: "TypePaiementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
