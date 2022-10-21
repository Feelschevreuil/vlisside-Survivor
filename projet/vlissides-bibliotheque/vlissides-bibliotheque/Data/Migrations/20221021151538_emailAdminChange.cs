using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class emailAdminChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "admin@cegep-connaissance-aleatoire.qc.ca", "ADMIN@CEGEP-CONNAISSANCE-ALEATOIRE.QC.CA", "ADMIN@CEGEP-CONNAISSANCE-ALEATOIRE.QC.CA", "AQAAAAEAACcQAAAAEM/4djC/E2dfrWN2gw1yfWn8hUWmBFIeiuBm7z8QxUw7MrbRGJ6ZVKyegtv7M7deow==", "15ad005f-1df6-40f3-b29e-0cdf15e5022f", "admin@cegep-connaissance-aleatoire.qc.ca" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "gordon.john@gunclub-alabama.us", "GORDON.JOHN@GUNCLUB-ALABAMA.US", "GORDON.JOHN@GUNCLUB-ALABAMA.US", "AQAAAAEAACcQAAAAEFDdb6IzMQH2YofMyTKbY/yC0rmle+pbUWRvONClJ4o6FAH/g0IYRJRcpxu6+VYRGw==", "333a2acb-b57d-4bb8-bac6-4bf4c3bf54ff", "gordon.john@gunclub-alabama.us" });
        }
    }
}
