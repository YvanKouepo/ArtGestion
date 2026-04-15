using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGestion.Migrations
{
    /// <inheritdoc />
    public partial class PreventDuplicateAlertes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Alertes_TitreExploitationId",
                table: "Alertes");

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_TitreExploitationId_TypeAlerte",
                table: "Alertes",
                columns: new[] { "TitreExploitationId", "TypeAlerte" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Alertes_TitreExploitationId_TypeAlerte",
                table: "Alertes");

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_TitreExploitationId",
                table: "Alertes",
                column: "TitreExploitationId");
        }
    }
}
