using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class fixQuantiteUsage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreUsager",
                table: "PrixEtatsLivres",
                newName: "QuantiteUsage");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEO2CzuQRDZIsjUCF49elAnJXIiZm0Y6UdFSZCATRUKcMv6ITH1qBds9QzZZqFg444A==", "7f489763-ed24-42d0-bae6-9ccc04f255c9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantiteUsage",
                table: "PrixEtatsLivres",
                newName: "NombreUsager");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEG+aFH2aV1c/q5186fdlKy4fTVSF0dl9JQltHmk+qZKRnut3gq76gTAxSQVFtjhXHw==", "329c20bd-2daf-4932-8b1b-e17f6fe05da0" });
        }
    }
}
