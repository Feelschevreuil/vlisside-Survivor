using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class QuantiteUsage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantiterUsage",
                table: "PrixEtatsLivres",
                newName: "QuantiteUsage");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEEtq954nTyh5o9id8LQHEXBf2fBeOIfr+NJzP/eQaI9GAGpTxLotWqjQS0dX3D0Sjw==", "5338cf0c-8d03-4183-b9a0-f2167e871458" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantiteUsage",
                table: "PrixEtatsLivres",
                newName: "QuantiterUsage");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEKfPWIoI0aTMnitWufi4KvQYlkjMBniU/SX8yZVY3AvPP39yd8sfhwZanFpXjjRBUA==", "fb2fa0c8-fc15-4622-8aa6-e4b04be45882" });
        }
    }
}
