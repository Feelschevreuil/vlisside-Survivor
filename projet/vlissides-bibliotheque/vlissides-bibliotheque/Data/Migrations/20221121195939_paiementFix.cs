using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vlissides_bibliotheque.Migrations
{
    public partial class paiementFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturesEtudiants_TypesPaiement_TypePaiementId",
                table: "FacturesEtudiants");

            migrationBuilder.DropForeignKey(
                name: "FK_PrixEtatsLivres_EtatsLivres_EtatLivreId",
                table: "PrixEtatsLivres");

            migrationBuilder.DropTable(
                name: "EtatsLivres");

            migrationBuilder.DropTable(
                name: "TypesPaiement");

            migrationBuilder.DropIndex(
                name: "IX_PrixEtatsLivres_EtatLivreId",
                table: "PrixEtatsLivres");

            migrationBuilder.DropIndex(
                name: "IX_FacturesEtudiants_TypePaiementId",
                table: "FacturesEtudiants");

            migrationBuilder.DropColumn(
                name: "AdresseLivraisonId",
                table: "FacturesEtudiants");

            migrationBuilder.RenameColumn(
                name: "EtatLivreId",
                table: "PrixEtatsLivres",
                newName: "EtatLivre");

            migrationBuilder.RenameColumn(
                name: "TypePaiementId",
                table: "FacturesEtudiants",
                newName: "Statut");

            migrationBuilder.AlterColumn<string>(
                name: "AdresseLivraison",
                table: "FacturesEtudiants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "FacturesEtudiants",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "FacturesEtudiants",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EtatLivre",
                table: "CommandesEtudiants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "CommandesEtudiants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PrixUnitaireGele",
                table: "CommandesEtudiants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEMULLh300qyfsTBd6dET3I4gKcskIv3VID3gAJ+S5CvIqwxcwoTyUxYwnFmI6dzIGA==", "d0c6315c-aa62-460f-8f59-09ecfcb4f70d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "FacturesEtudiants");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "FacturesEtudiants");

            migrationBuilder.DropColumn(
                name: "EtatLivre",
                table: "CommandesEtudiants");

            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "CommandesEtudiants");

            migrationBuilder.DropColumn(
                name: "PrixUnitaireGele",
                table: "CommandesEtudiants");

            migrationBuilder.RenameColumn(
                name: "EtatLivre",
                table: "PrixEtatsLivres",
                newName: "EtatLivreId");

            migrationBuilder.RenameColumn(
                name: "Statut",
                table: "FacturesEtudiants",
                newName: "TypePaiementId");

            migrationBuilder.AlterColumn<string>(
                name: "AdresseLivraison",
                table: "FacturesEtudiants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdresseLivraisonId",
                table: "FacturesEtudiants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EtatsLivres",
                columns: table => new
                {
                    EtatLivreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtatsLivres", x => x.EtatLivreId);
                });

            migrationBuilder.CreateTable(
                name: "TypesPaiement",
                columns: table => new
                {
                    TypePaiementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesPaiement", x => x.TypePaiementId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83c10a40-c3f6-49bd-b230-f6975cc7befd",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEAhyqDr7YBPQznUrc2G0dn3Ghs0SimOVXdCzkt3biTev2alh5v0/CEhJoefA4g0h1g==", "8a46a597-83af-4249-8e73-2cb6ce29adcd" });

            migrationBuilder.InsertData(
                table: "EtatsLivres",
                columns: new[] { "EtatLivreId", "Nom" },
                values: new object[,]
                {
                    { 1, "Neuf" },
                    { 2, "Usagé" },
                    { 3, "Digital" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrixEtatsLivres_EtatLivreId",
                table: "PrixEtatsLivres",
                column: "EtatLivreId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturesEtudiants_TypePaiementId",
                table: "FacturesEtudiants",
                column: "TypePaiementId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturesEtudiants_TypesPaiement_TypePaiementId",
                table: "FacturesEtudiants",
                column: "TypePaiementId",
                principalTable: "TypesPaiement",
                principalColumn: "TypePaiementId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrixEtatsLivres_EtatsLivres_EtatLivreId",
                table: "PrixEtatsLivres",
                column: "EtatLivreId",
                principalTable: "EtatsLivres",
                principalColumn: "EtatLivreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
