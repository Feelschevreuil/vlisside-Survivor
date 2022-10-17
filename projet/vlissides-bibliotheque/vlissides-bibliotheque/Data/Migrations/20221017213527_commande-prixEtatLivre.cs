using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class commandeprixEtatLivre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandesEtudiants_LivresBibliotheque_LivreBibliothequeId",
                table: "CommandesEtudiants");

            migrationBuilder.RenameColumn(
                name: "LivreBibliothequeId",
                table: "CommandesEtudiants",
                newName: "PrixEtatLivreId");

            migrationBuilder.RenameIndex(
                name: "IX_CommandesEtudiants_LivreBibliothequeId",
                table: "CommandesEtudiants",
                newName: "IX_CommandesEtudiants_PrixEtatLivreId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEO3pHumP0Qise9xTp2j4ZLbaWLbFRa8whvUsRub5KZYVTVrWpUUAxm1Pwa3QjRyQBw==", "fc17a5df-0470-42cb-b403-bcbe728fec74" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommandesEtudiants_PrixEtatsLivres_PrixEtatLivreId",
                table: "CommandesEtudiants",
                column: "PrixEtatLivreId",
                principalTable: "PrixEtatsLivres",
                principalColumn: "PrixEtatLivreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandesEtudiants_PrixEtatsLivres_PrixEtatLivreId",
                table: "CommandesEtudiants");

            migrationBuilder.RenameColumn(
                name: "PrixEtatLivreId",
                table: "CommandesEtudiants",
                newName: "LivreBibliothequeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommandesEtudiants_PrixEtatLivreId",
                table: "CommandesEtudiants",
                newName: "IX_CommandesEtudiants_LivreBibliothequeId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEPMqzDzhXBwQv6FwsDqIWZ5QB9IMNGW8s6KUkG42xUtuGv8/P+4qg76VK4xeCBDfbQ==", "e82df89b-f4ba-47e9-ab0b-5e218a6a4682" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommandesEtudiants_LivresBibliotheque_LivreBibliothequeId",
                table: "CommandesEtudiants",
                column: "LivreBibliothequeId",
                principalTable: "LivresBibliotheque",
                principalColumn: "LivreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
