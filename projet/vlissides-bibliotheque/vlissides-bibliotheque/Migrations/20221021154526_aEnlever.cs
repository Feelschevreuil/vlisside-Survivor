using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class aEnlever : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivresBibliotheque_MaisonsEdition_MaisonsEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.DropIndex(
                name: "IX_LivresBibliotheque_MaisonsEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.DropColumn(
                name: "MaisonsEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.RenameColumn(
                name: "MaisonsEditionId",
                table: "MaisonsEdition",
                newName: "MaisonEditionId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJi3EAmbUicz6w99QUE5GrrRDOBIc8bXJl2q9dSC3L5+Bmea6fY2u7a0Pvj38q0/fQ==", "3be2793b-ea9f-4ecb-9a5c-6a01ed4aee70" });

            migrationBuilder.CreateIndex(
                name: "IX_LivresBibliotheque_MaisonEditionId",
                table: "LivresBibliotheque",
                column: "MaisonEditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivresBibliotheque_MaisonsEdition_MaisonEditionId",
                table: "LivresBibliotheque",
                column: "MaisonEditionId",
                principalTable: "MaisonsEdition",
                principalColumn: "MaisonEditionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivresBibliotheque_MaisonsEdition_MaisonEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.DropIndex(
                name: "IX_LivresBibliotheque_MaisonEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.RenameColumn(
                name: "MaisonEditionId",
                table: "MaisonsEdition",
                newName: "MaisonsEditionId");

            migrationBuilder.AddColumn<int>(
                name: "MaisonsEditionId",
                table: "LivresBibliotheque",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEOr9zKl1qkaVmS6X2GEuWIlgnphMIHOf89O+sjxMyQNVw6cD0wvPRSaMn90e9XX4wQ==", "01e96cd5-4712-4d02-8401-a0f40db8d320" });

            migrationBuilder.CreateIndex(
                name: "IX_LivresBibliotheque_MaisonsEditionId",
                table: "LivresBibliotheque",
                column: "MaisonsEditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivresBibliotheque_MaisonsEdition_MaisonsEditionId",
                table: "LivresBibliotheque",
                column: "MaisonsEditionId",
                principalTable: "MaisonsEdition",
                principalColumn: "MaisonsEditionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
