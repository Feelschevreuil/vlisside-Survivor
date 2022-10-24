using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class FixMaisonsEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivresBibliotheque_MaisonsEditions_MaisonEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaisonsEditions",
                table: "MaisonsEditions");

            migrationBuilder.RenameTable(
                name: "MaisonsEditions",
                newName: "MaisonsEdition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaisonsEdition",
                table: "MaisonsEdition",
                column: "MaisonEditionId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEHrf3ISDrEc19sZxlS1Foj5Tld8cCZSRVitfY7/bDeeJ1zm+KSbdWbYuDPoguqqyKg==", "7cfc1c4b-9b22-4b83-a412-4e2e1b69b39c" });

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaisonsEdition",
                table: "MaisonsEdition");

            migrationBuilder.RenameTable(
                name: "MaisonsEdition",
                newName: "MaisonsEditions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaisonsEditions",
                table: "MaisonsEditions",
                column: "MaisonEditionId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEEtq954nTyh5o9id8LQHEXBf2fBeOIfr+NJzP/eQaI9GAGpTxLotWqjQS0dX3D0Sjw==", "5338cf0c-8d03-4183-b9a0-f2167e871458" });

            migrationBuilder.AddForeignKey(
                name: "FK_LivresBibliotheque_MaisonsEditions_MaisonEditionId",
                table: "LivresBibliotheque",
                column: "MaisonEditionId",
                principalTable: "MaisonsEditions",
                principalColumn: "MaisonEditionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
