using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class NombreUsager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreUsager",
                table: "PrixEtatsLivres",
                newName: "NombreUsage");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJiOLUQyzAWj2lAzGlRC4orGIo84oZba+8A+wMWq+1Q/kzId2iyCkv/VgNLf6x/IyA==", "5703cb2f-e46d-48d9-bbd4-de01b05f3468" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreUsage",
                table: "PrixEtatsLivres",
                newName: "NombreUsager");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEJi3EAmbUicz6w99QUE5GrrRDOBIc8bXJl2q9dSC3L5+Bmea6fY2u7a0Pvj38q0/fQ==", "3be2793b-ea9f-4ecb-9a5c-6a01ed4aee70" });
        }
    }
}
