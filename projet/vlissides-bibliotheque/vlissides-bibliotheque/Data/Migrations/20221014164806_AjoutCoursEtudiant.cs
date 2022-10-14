using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class AjoutCoursEtudiant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEN0+w5nVThlyeGS6bsqDcwHDeAOpJjiAjP8vq+kRWJcbMqn9FMukqV3K4dWzWUOwOw==", "7060b88f-b773-4a89-b9e4-b83ecfc468d2" });
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
