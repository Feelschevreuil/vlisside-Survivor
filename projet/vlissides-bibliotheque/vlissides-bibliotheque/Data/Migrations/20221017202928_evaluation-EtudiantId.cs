using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class evaluationEtudiantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENYs1UxkVpUhmnVwrnWjQd7Ku5UueG5wnHPaWJzeben8ZcKUgwqRKyGdsblUCscsDw==", "d636487a-03a4-4334-b9fb-6c6034a47f98" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEHSXlukD9Aon0oV0jPtIsmedeJu4I4M7Uk0pEwgbjczexcQAYRM82kfilSqvyXl92A==", "d58a8076-77b7-4aa0-acf0-44a5a4b3c8b0" });
        }
    }
}
