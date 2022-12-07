using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class modificationcompteAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "AdminAleatoire@collegeConnaissanceAleatoire.com", "ADMINALEATOIRE@COLLEGECONNAISSANCEALEATOIRE.COM", "ADMINALEATOIRE@COLLEGECONNAISSANCEALEATOIRE.COM", "AQAAAAEAACcQAAAAEEkWo6ikjRAnHTmKs1ZpS0nyE2bp3IjnF+ekmaFpcAV3qaHXhV3nAnS2zYcrSeUdfw==", "438052f4-d34b-4cdc-ae36-6bd13d6dc8b6", "AdminAleatoire@collegeConnaissanceAleatoire.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "admin@cegep-connaissance-aleatoire.qc.ca", "ADMIN@CEGEP-CONNAISSANCE-ALEATOIRE.QC.CA", "ADMIN@CEGEP-CONNAISSANCE-ALEATOIRE.QC.CA", "AQAAAAEAACcQAAAAEP4DQaHsW70gxx+gqnyk0VESqsmvXB4cMb5BJudXZldJ4NqMBppgRkln+5SoewKbXg==", "51db8005-5e88-47fc-b8b3-cec5f8d3eeb0", "admin@cegep-connaissance-aleatoire.qc.ca" });
        }
    }
}
