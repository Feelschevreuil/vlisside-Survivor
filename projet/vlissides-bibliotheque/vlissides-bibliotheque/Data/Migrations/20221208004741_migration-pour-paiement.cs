using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class migrationpourpaiement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrixUnitaireGele",
                table: "CommandesEtudiants",
                newName: "Prix");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEPTH+Iow/mATjOhFiK0RoaISADj/L+bM9ReUo3yZa8mtPP4E6BHR6in2Ee/dh9sYkg==", "9c839054-e1fb-42b2-a55a-44ac730319f0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prix",
                table: "CommandesEtudiants",
                newName: "PrixUnitaireGele");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENK03rTy5x2Em5cW2CItxByjaGaP4TK8yv8OzSPXaN09s5axZiKR/IPKMNF9vcaxiw==", "363e9e18-e74e-4454-9ebd-bdf1ef2b4b65" });
        }
    }
}
