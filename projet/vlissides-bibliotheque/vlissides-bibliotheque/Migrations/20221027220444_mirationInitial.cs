using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class mirationInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJvXNhwRA1m0KNQc7iuvRMPt056AGno7Avkr6OvE7EuuJsZy36YRByp8+upORwZpFg==", "d06bbc4d-81ed-48d5-bbd7-c2c83542fcf2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEFezD2VGcDeCK/CwBSJ2mmv8EpyABhAltGGLxOYzq52+/FUvr0DtgvI/uK1iAZlZvg==", "1e581cf7-8baf-4346-9bb0-bbcf2ae49f99" });
        }
    }
}
