using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class mergeDevelopInventaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEG+aFH2aV1c/q5186fdlKy4fTVSF0dl9JQltHmk+qZKRnut3gq76gTAxSQVFtjhXHw==", "329c20bd-2daf-4932-8b1b-e17f6fe05da0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEFDdb6IzMQH2YofMyTKbY/yC0rmle+pbUWRvONClJ4o6FAH/g0IYRJRcpxu6+VYRGw==", "333a2acb-b57d-4bb8-bac6-4bf4c3bf54ff" });
        }
    }
}
