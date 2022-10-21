using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class prixLivreEtudiant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Prix",
                table: "LivresEtudiants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEGk3wuJ1eaVNQgQiq1naF2fH9DX1u2AHMs9PQc6zXAC1/glLkqdPIiU3syts1AlBGA==", "bafaf299-a28b-4c4d-b77c-af326a261d99" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prix",
                table: "LivresEtudiants");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEFDdb6IzMQH2YofMyTKbY/yC0rmle+pbUWRvONClJ4o6FAH/g0IYRJRcpxu6+VYRGw==", "333a2acb-b57d-4bb8-bac6-4bf4c3bf54ff" });
        }
    }
}
