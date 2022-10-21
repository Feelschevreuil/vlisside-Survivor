using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class fixsnapshotinventaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEAFvb6v2Yy2L8QLg0m+B1a0ZSEnGm34PXZt7D7AVXo8WW4ZDfZnt8IrGxl3oBK1MiQ==", "6a04bc8a-6d3a-43d3-8234-25bc31ff88f7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAECSH/iSX7LF/+L/HUC31vJK9LcYwH45uHnPLiSGeCB/pLLLa1Gdzkuz5OXOT9GJvUg==", "59988518-c493-4365-ae8e-95310de33675" });
        }
    }
}
