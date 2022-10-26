using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class LivreUsagePrix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Prix",
                table: "LivresEtudiants",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEOgerPmlJgJ77nAVJLUdPF6pDZooTYv9UEgfDZqFWltVl+XbrNbVYPmoUmb5Cg7N4Q==", "57878489-177a-4716-b8a0-c4e4b23277b2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Prix",
                table: "LivresEtudiants",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEHrf3ISDrEc19sZxlS1Foj5Tld8cCZSRVitfY7/bDeeJ1zm+KSbdWbYuDPoguqqyKg==", "7cfc1c4b-9b22-4b83-a412-4e2e1b69b39c" });
        }
    }
}
