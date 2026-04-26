using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGestion.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthenticationSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 16, 13, 22, 48, 238, DateTimeKind.Utc).AddTicks(3373));

            migrationBuilder.UpdateData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 16, 13, 22, 48, 238, DateTimeKind.Utc).AddTicks(3381));

            migrationBuilder.UpdateData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 16, 13, 22, 48, 238, DateTimeKind.Utc).AddTicks(3383));

            migrationBuilder.UpdateData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 16, 13, 22, 48, 238, DateTimeKind.Utc).AddTicks(3411));

            migrationBuilder.UpdateData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 16, 13, 22, 48, 238, DateTimeKind.Utc).AddTicks(3423));

            migrationBuilder.UpdateData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 16, 13, 22, 48, 238, DateTimeKind.Utc).AddTicks(3425));

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreation",
                value: new DateTime(2026, 4, 16, 13, 22, 48, 238, DateTimeKind.Utc).AddTicks(3300));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7792));

            migrationBuilder.UpdateData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "Exploitants",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7802));

            migrationBuilder.UpdateData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7827));

            migrationBuilder.UpdateData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7836));

            migrationBuilder.UpdateData(
                table: "TitresExploitation",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateSaisie",
                value: new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7838));

            migrationBuilder.UpdateData(
                table: "Utilisateurs",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreation",
                value: new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7738));
        }
    }
}
