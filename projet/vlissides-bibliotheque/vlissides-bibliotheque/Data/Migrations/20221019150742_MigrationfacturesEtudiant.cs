using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class MigrationfacturesEtudiant : Migration
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
                newName: "FacturesEtudiants");

            migrationBuilder.RenameIndex(
                name: "IX_FactureEtudiant_TypePaiementId",
                table: "FacturesEtudiants",
                newName: "IX_FacturesEtudiants_TypePaiementId");

            migrationBuilder.RenameIndex(
                name: "IX_FactureEtudiant_EtudiantId",
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
                values: new object[] { "AQAAAAEAACcQAAAAEBCPZjtVwX5K+joSsyVaMh89LVFW79kXLPXXXunkfEzD8U4cKYS+DD+DZSIJb8HhoQ==", "2c26d0f8-f4c4-4e56-adca-05f8ad96a81e" });

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
                newName: "FactureEtudiant");

            migrationBuilder.RenameIndex(
                name: "IX_FacturesEtudiants_TypePaiementId",
                table: "FactureEtudiant",
                newName: "IX_FactureEtudiant_TypePaiementId");

            migrationBuilder.RenameIndex(
                name: "IX_FacturesEtudiants_EtudiantId",
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
