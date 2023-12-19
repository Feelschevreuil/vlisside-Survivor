using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class migrationSeconde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivresEtudiants_Etudiants_EtudiantId",
                table: "LivresEtudiants");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "MaisonsEdition",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Prix",
                table: "LivresEtudiants",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhotoCouverture",
                table: "LivresEtudiants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EtudiantId",
                table: "LivresEtudiants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PrixJson",
                table: "LivresBibliotheque",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAELvdUNOChlF9frsD9/60WEgSa3+RNn8YwU6/74PaTzfzT3fj/4j/snZrRfxAaMAHoA==", "de6b9ac5-d18e-43a4-b457-2677359722d9" });

            migrationBuilder.AddForeignKey(
                name: "FK_LivresEtudiants_Etudiants_EtudiantId",
                table: "LivresEtudiants",
                column: "EtudiantId",
                principalTable: "Etudiants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivresEtudiants_Etudiants_EtudiantId",
                table: "LivresEtudiants");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "MaisonsEdition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Prix",
                table: "LivresEtudiants",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhotoCouverture",
                table: "LivresEtudiants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EtudiantId",
                table: "LivresEtudiants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrixJson",
                table: "LivresBibliotheque",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEFUdkXS8wC+1GH2m3oAYaQ8LzEmAEEvxq14ApUFVeuDz+1DX0r4wEBn69CwsPHxD5Q==", "a6736028-37a0-41bf-acfd-2ee72393de08" });

            migrationBuilder.AddForeignKey(
                name: "FK_LivresEtudiants_Etudiants_EtudiantId",
                table: "LivresEtudiants",
                column: "EtudiantId",
                principalTable: "Etudiants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
