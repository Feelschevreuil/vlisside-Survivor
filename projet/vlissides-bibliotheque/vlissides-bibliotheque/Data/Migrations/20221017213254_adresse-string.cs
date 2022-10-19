using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class adressestring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactureEtudiant_Adresses_AdresseLivraisonId",
                table: "FactureEtudiant");

            migrationBuilder.DropIndex(
                name: "IX_FactureEtudiant_AdresseLivraisonId",
                table: "FactureEtudiant");

            migrationBuilder.AddColumn<string>(
                name: "Auteur",
                table: "LivresEtudiants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublication",
                table: "LivresEtudiants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "LivresEtudiants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaisonEdition",
                table: "LivresEtudiants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "LivresEtudiants",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdresseLivraison",
                table: "FactureEtudiant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEPMqzDzhXBwQv6FwsDqIWZ5QB9IMNGW8s6KUkG42xUtuGv8/P+4qg76VK4xeCBDfbQ==", "e82df89b-f4ba-47e9-ab0b-5e218a6a4682" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auteur",
                table: "LivresEtudiants");

            migrationBuilder.DropColumn(
                name: "DatePublication",
                table: "LivresEtudiants");

            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "LivresEtudiants");

            migrationBuilder.DropColumn(
                name: "MaisonEdition",
                table: "LivresEtudiants");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "LivresEtudiants");

            migrationBuilder.DropColumn(
                name: "AdresseLivraison",
                table: "FactureEtudiant");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEHSXlukD9Aon0oV0jPtIsmedeJu4I4M7Uk0pEwgbjczexcQAYRM82kfilSqvyXl92A==", "d58a8076-77b7-4aa0-acf0-44a5a4b3c8b0" });

            migrationBuilder.CreateIndex(
                name: "IX_FactureEtudiant_AdresseLivraisonId",
                table: "FactureEtudiant",
                column: "AdresseLivraisonId");

            migrationBuilder.AddForeignKey(
                name: "FK_FactureEtudiant_Adresses_AdresseLivraisonId",
                table: "FactureEtudiant",
                column: "AdresseLivraisonId",
                principalTable: "Adresses",
                principalColumn: "AdresseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
