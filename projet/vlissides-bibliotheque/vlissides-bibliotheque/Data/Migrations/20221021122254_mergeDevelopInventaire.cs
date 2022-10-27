using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class QuantiterUsage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreUsage",
                table: "PrixEtatsLivres",
                newName: "QuantiterUsage");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEKfPWIoI0aTMnitWufi4KvQYlkjMBniU/SX8yZVY3AvPP39yd8sfhwZanFpXjjRBUA==", "fb2fa0c8-fc15-4622-8aa6-e4b04be45882" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantiterUsage",
                table: "PrixEtatsLivres",
                newName: "NombreUsage");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEF7Z74Z43gW2rX7iIxY1mu4Jf1POsPZkUx/dz7d91xVCl4eIHRK1sfGnOBBrGdv5jg==", "8f47fd20-3442-4865-987a-19b09810984e" });
        }
    }
}
