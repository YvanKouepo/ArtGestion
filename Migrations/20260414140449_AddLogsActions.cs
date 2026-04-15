using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtGestion.Migrations
{
    /// <inheritdoc />
    public partial class AddLogsActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogsActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeAction = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Entite = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntiteId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DateAction = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    UtilisateurId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogsActions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogsActions_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogsActions_ServiceId",
                table: "LogsActions",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_LogsActions_UtilisateurId",
                table: "LogsActions",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogsActions");
        }
    }
}
