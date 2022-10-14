using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class AjoutNombreUsager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursEtudiant_Cours_CoursId",
                table: "CoursEtudiant");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursEtudiant_Etudiants_EtudiantId",
                table: "CoursEtudiant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursEtudiant",
                table: "CoursEtudiant");

            migrationBuilder.RenameTable(
                name: "CoursEtudiant",
                newName: "CoursEtudiants");

            migrationBuilder.RenameIndex(
                name: "IX_CoursEtudiant_EtudiantId",
                table: "CoursEtudiants",
                newName: "IX_CoursEtudiants_EtudiantId");

            migrationBuilder.AddColumn<int>(
                name: "NombreUsager",
                table: "PrixEtatsLivres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursEtudiants",
                table: "CoursEtudiants",
                columns: new[] { "CoursId", "EtudiantId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAECSH/iSX7LF/+L/HUC31vJK9LcYwH45uHnPLiSGeCB/pLLLa1Gdzkuz5OXOT9GJvUg==", "59988518-c493-4365-ae8e-95310de33675" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoursEtudiants_Cours_CoursId",
                table: "CoursEtudiants",
                column: "CoursId",
                principalTable: "Cours",
                principalColumn: "CoursId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursEtudiants_Etudiants_EtudiantId",
                table: "CoursEtudiants",
                column: "EtudiantId",
                principalTable: "Etudiants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursEtudiants_Cours_CoursId",
                table: "CoursEtudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursEtudiants_Etudiants_EtudiantId",
                table: "CoursEtudiants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursEtudiants",
                table: "CoursEtudiants");

            migrationBuilder.DropColumn(
                name: "NombreUsager",
                table: "PrixEtatsLivres");

            migrationBuilder.RenameTable(
                name: "CoursEtudiants",
                newName: "CoursEtudiant");

            migrationBuilder.RenameIndex(
                name: "IX_CoursEtudiants_EtudiantId",
                table: "CoursEtudiant",
                newName: "IX_CoursEtudiant_EtudiantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursEtudiant",
                table: "CoursEtudiant",
                columns: new[] { "CoursId", "EtudiantId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEN0+w5nVThlyeGS6bsqDcwHDeAOpJjiAjP8vq+kRWJcbMqn9FMukqV3K4dWzWUOwOw==", "7060b88f-b773-4a89-b9e4-b83ecfc468d2" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoursEtudiant_Cours_CoursId",
                table: "CoursEtudiant",
                column: "CoursId",
                principalTable: "Cours",
                principalColumn: "CoursId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursEtudiant_Etudiants_EtudiantId",
                table: "CoursEtudiant",
                column: "EtudiantId",
                principalTable: "Etudiants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
