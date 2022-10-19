using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class MigrationfactureEtudiant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandesEtudiants_FactureEtudiant_FactureEtudiantId",
                table: "CommandesEtudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_FactureEtudiant_Etudiants_EtudiantId",
                table: "FactureEtudiant");

            migrationBuilder.DropForeignKey(
                name: "FK_FactureEtudiant_TypesPaiement_TypePaiementId",
                table: "FactureEtudiant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FactureEtudiant",
                table: "FactureEtudiant");

            migrationBuilder.RenameTable(
                name: "FactureEtudiant",
                newName: "FactureEtudiants");

            migrationBuilder.RenameIndex(
                name: "IX_FactureEtudiant_TypePaiementId",
                table: "FactureEtudiants",
                newName: "IX_FactureEtudiants_TypePaiementId");

            migrationBuilder.RenameIndex(
                name: "IX_FactureEtudiant_EtudiantId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "FactureEtudiant");

            migrationBuilder.RenameIndex(
                name: "IX_FactureEtudiants_TypePaiementId",
                table: "FactureEtudiant",
                newName: "IX_FactureEtudiant_TypePaiementId");

            migrationBuilder.RenameIndex(
                name: "IX_FactureEtudiants_EtudiantId",
                table: "FactureEtudiant",
                newName: "IX_FactureEtudiant_EtudiantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FactureEtudiant",
                table: "FactureEtudiant",
                column: "FactureEtudiantId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENMbVPDF6+YJIe94pyR5xJhoJ+V7AOy94jRSAD8LkKqwdzjAoUlSLnQfQ54H1PVvxw==", "22b2c344-f882-4001-aa7d-dd7e12fef201" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommandesEtudiants_FactureEtudiant_FactureEtudiantId",
                table: "CommandesEtudiants",
                column: "FactureEtudiantId",
                principalTable: "FactureEtudiant",
                principalColumn: "FactureEtudiantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactureEtudiant_Etudiants_EtudiantId",
                table: "FactureEtudiant",
                column: "EtudiantId",
                principalTable: "Etudiants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactureEtudiant_TypesPaiement_TypePaiementId",
                table: "FactureEtudiant",
                column: "TypePaiementId",
                principalTable: "TypesPaiement",
                principalColumn: "TypePaiementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
