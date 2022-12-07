using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class numeroProfesseur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroProfesseur",
                table: "Professeurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEP4DQaHsW70gxx+gqnyk0VESqsmvXB4cMb5BJudXZldJ4NqMBppgRkln+5SoewKbXg==", "51db8005-5e88-47fc-b8b3-cec5f8d3eeb0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroProfesseur",
                table: "Professeurs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEA9XptPjnTXqm390tIf/tCcR+fmIWc9Dw6IFmaxPk04Ag6YyoACj82njChYaz+oVAw==", "a6f3d6cb-c508-4c15-a825-80e9cad8dba7" });
        }
    }
}
