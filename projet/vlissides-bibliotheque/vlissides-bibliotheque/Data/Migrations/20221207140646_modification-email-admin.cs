using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class modificationemailadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "AdminAleatoire@CollegeConnaissanceAleatoire.qc.ca", "ADMINALEATOIRE@COLLEGECONNAISSANCEALEATOIRE.QC.CA", "ADMINALEATOIRE@COLLEGECONNAISSANCEALEATOIRE.QC.CA", "AQAAAAEAACcQAAAAENK03rTy5x2Em5cW2CItxByjaGaP4TK8yv8OzSPXaN09s5axZiKR/IPKMNF9vcaxiw==", "363e9e18-e74e-4454-9ebd-bdf1ef2b4b65", "AdminAleatoire@CollegeConnaissanceAleatoire.qc.ca" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "AdminAleatoire@collegeConnaissanceAleatoire.com", "ADMINALEATOIRE@COLLEGECONNAISSANCEALEATOIRE.COM", "ADMINALEATOIRE@COLLEGECONNAISSANCEALEATOIRE.COM", "AQAAAAEAACcQAAAAEEkWo6ikjRAnHTmKs1ZpS0nyE2bp3IjnF+ekmaFpcAV3qaHXhV3nAnS2zYcrSeUdfw==", "438052f4-d34b-4cdc-ae36-6bd13d6dc8b6", "AdminAleatoire@collegeConnaissanceAleatoire.com" });
        }
    }
}
