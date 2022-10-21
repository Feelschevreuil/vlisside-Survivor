using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class maisonsEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivresBibliotheque_MaisonsEditions_MaisonEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.DropTable(
                name: "MaisonsEditions");

            migrationBuilder.DropIndex(
                name: "IX_LivresBibliotheque_MaisonEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.AddColumn<int>(
                name: "MaisonsEditionId",
                table: "LivresBibliotheque",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MaisonsEdition",
                columns: table => new
                {
                    MaisonsEditionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaisonsEdition", x => x.MaisonsEditionId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivresBibliotheque_MaisonsEdition_MaisonsEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.DropTable(
                name: "MaisonsEdition");

            migrationBuilder.DropIndex(
                name: "IX_LivresBibliotheque_MaisonsEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.DropColumn(
                name: "MaisonsEditionId",
                table: "LivresBibliotheque");

            migrationBuilder.CreateTable(
                name: "MaisonsEditions",
                columns: table => new
                {
                    MaisonEditionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaisonsEditions", x => x.MaisonEditionId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEGk3wuJ1eaVNQgQiq1naF2fH9DX1u2AHMs9PQc6zXAC1/glLkqdPIiU3syts1AlBGA==", "bafaf299-a28b-4c4d-b77c-af326a261d99" });

            migrationBuilder.CreateIndex(
                name: "IX_LivresBibliotheque_MaisonEditionId",
                table: "LivresBibliotheque",
                column: "MaisonEditionId");

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
