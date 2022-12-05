using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class paiementsajoutadresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdresseLivraison",
                table: "FacturesEtudiants");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "ProgrammesEtudes",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AdresseLivraisonId",
                table: "FacturesEtudiants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Cours",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cours",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "StatutCommande",
                table: "CommandesEtudiants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Titre",
                table: "CommandesEtudiants",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENAUureAglN1KyCbispHtBvi7I2QwLw3A74mOpzqG3dc7UbH4BnmbYgKYSjCkxJ1Yw==", "07e44151-5f93-48a5-a778-cdefe28c1000" });

            migrationBuilder.CreateIndex(
                name: "IX_FacturesEtudiants_AdresseLivraisonId",
                table: "FacturesEtudiants",
                column: "AdresseLivraisonId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturesEtudiants_Adresses_AdresseLivraisonId",
                table: "FacturesEtudiants",
                column: "AdresseLivraisonId",
                principalTable: "Adresses",
                principalColumn: "AdresseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturesEtudiants_Adresses_AdresseLivraisonId",
                table: "FacturesEtudiants");

            migrationBuilder.DropIndex(
                name: "IX_FacturesEtudiants_AdresseLivraisonId",
                table: "FacturesEtudiants");

            migrationBuilder.DropColumn(
                name: "AdresseLivraisonId",
                table: "FacturesEtudiants");

            migrationBuilder.DropColumn(
                name: "StatutCommande",
                table: "CommandesEtudiants");

            migrationBuilder.DropColumn(
                name: "Titre",
                table: "CommandesEtudiants");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "ProgrammesEtudes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "AdresseLivraison",
                table: "FacturesEtudiants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Cours",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cours",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEMULLh300qyfsTBd6dET3I4gKcskIv3VID3gAJ+S5CvIqwxcwoTyUxYwnFmI6dzIGA==", "d0c6315c-aa62-460f-8f59-09ecfcb4f70d" });
        }
    }
}
