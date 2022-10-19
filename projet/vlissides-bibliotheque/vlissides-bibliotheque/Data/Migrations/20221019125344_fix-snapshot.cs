using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class fixsnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "LivresEtudiants");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENMbVPDF6+YJIe94pyR5xJhoJ+V7AOy94jRSAD8LkKqwdzjAoUlSLnQfQ54H1PVvxw==", "22b2c344-f882-4001-aa7d-dd7e12fef201" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LivresEtudiants",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAECSH/iSX7LF/+L/HUC31vJK9LcYwH45uHnPLiSGeCB/pLLLa1Gdzkuz5OXOT9GJvUg==", "59988518-c493-4365-ae8e-95310de33675" });
        }
    }
}
