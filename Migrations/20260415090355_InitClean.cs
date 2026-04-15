using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArtGestion.Migrations
{
    /// <inheritdoc />
    public partial class InitClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Actif", "Code", "Nom" },
                values: new object[,]
                {
                    { 1, true, "LT", "Littoral" },
                    { 2, true, "SW", "Sud-Ouest" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Actif", "Code", "Description", "Nom" },
                values: new object[] { 1, true, "SSVTSE", null, "Service du Suivi des Services, de la Veille Technologique et des Statistiques Économiques" });

            migrationBuilder.InsertData(
                table: "TypesEntreprise",
                columns: new[] { "Id", "Actif", "Libelle" },
                values: new object[,]
                {
                    { 1, true, "Opérateur" },
                    { 2, true, "Fournisseur" }
                });

            migrationBuilder.InsertData(
                table: "TypesTitre",
                columns: new[] { "Id", "Actif", "DureeValidite", "Libelle", "UniteDuree" },
                values: new object[] { 1, true, 5, "Licence", "Ans" });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "Id", "Actif", "DateCreation", "Email", "EstResponsableService", "Login", "MotDePasseHash", "NomComplet", "Role", "ServiceId" },
                values: new object[] { 1, true, new DateTime(2026, 4, 15, 9, 3, 54, 728, DateTimeKind.Utc).AddTicks(9883), "admin@art.local", true, "admin", "temp-hash", "Admin SSVTSE", "Admin", 1 });

            migrationBuilder.InsertData(
                table: "Villes",
                columns: new[] { "Id", "Actif", "Nom", "RegionId" },
                values: new object[,]
                {
                    { 1, true, "Douala", 1 },
                    { 2, true, "Buea", 2 }
                });

            migrationBuilder.InsertData(
                table: "Exploitants",
                columns: new[] { "Id", "CodeExploitant", "Contact", "DateSaisie", "NomExploitant", "RegionId", "ServiceId", "TypeEntrepriseId", "UtilisateurId", "VilleId" },
                values: new object[,]
                {
                    { 1, "EXP001", null, new DateTime(2026, 4, 15, 9, 3, 54, 728, DateTimeKind.Utc).AddTicks(9950), "MTN Cameroun", 1, 1, 1, 1, 1 },
                    { 2, "EXP002", null, new DateTime(2026, 4, 15, 9, 3, 54, 728, DateTimeKind.Utc).AddTicks(9957), "Orange Cameroun", 1, 1, 1, 1, 1 },
                    { 3, "EXP003", null, new DateTime(2026, 4, 15, 9, 3, 54, 728, DateTimeKind.Utc).AddTicks(9960), "Nexttel", 2, 1, 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "TitresExploitation",
                columns: new[] { "Id", "Actif", "DateExpiration", "DateSaisie", "DateSignature", "ExploitantId", "FrequencesAssignees", "NombreTitres", "Observation", "ReferenceTitre", "ServiceId", "Statut", "TypeReseauId", "TypeTitreId", "UtilisateurId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 15, 9, 3, 54, 728, DateTimeKind.Utc).AddTicks(9995), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, 1, null, "TITRE-001", 1, "Actif", null, 1, 1 },
                    { 2, true, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 15, 9, 3, 54, 729, DateTimeKind.Utc).AddTicks(6), new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, null, 2, null, "TITRE-002", 1, "Bientôt expiré", null, 1, 1 },
                    { 3, false, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 15, 9, 3, 54, 729, DateTimeKind.Utc).AddTicks(8), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, null, 1, null, "TITRE-003", 1, "Expiré", null, 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypesTitre",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypesEntreprise",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypesEntreprise",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
