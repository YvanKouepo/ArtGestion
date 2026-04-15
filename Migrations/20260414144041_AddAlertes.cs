using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtGestion.Migrations
{
    /// <inheritdoc />
    public partial class AddAlertes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alertes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitreExploitationId = table.Column<int>(type: "integer", nullable: false),
                    TypeAlerte = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DateGeneration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstLue = table.Column<bool>(type: "boolean", nullable: false),
                    DateLecture = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    UtilisateurResponsableId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alertes_TitresExploitation_TitreExploitationId",
                        column: x => x.TitreExploitationId,
                        principalTable: "TitresExploitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alertes_Utilisateurs_UtilisateurResponsableId",
                        column: x => x.UtilisateurResponsableId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_ServiceId",
                table: "Alertes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_TitreExploitationId",
                table: "Alertes",
                column: "TitreExploitationId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_UtilisateurResponsableId",
                table: "Alertes",
                column: "UtilisateurResponsableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertes");
        }
    }
}
