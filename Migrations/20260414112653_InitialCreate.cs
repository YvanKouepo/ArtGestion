using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtGestion.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesEntreprise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    Actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesEntreprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesReseau",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    Actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesReseau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesTitre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    DureeValidite = table.Column<int>(type: "integer", nullable: false),
                    UniteDuree = table.Column<string>(type: "text", nullable: false),
                    Actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesTitre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Villes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    Actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Villes_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomComplet = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MotDePasseHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EstResponsableService = table.Column<bool>(type: "boolean", nullable: false),
                    Actif = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exploitants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodeExploitant = table.Column<string>(type: "text", nullable: false),
                    DateSaisie = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NomExploitant = table.Column<string>(type: "text", nullable: false),
                    Contact = table.Column<string>(type: "text", nullable: true),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    UtilisateurId = table.Column<int>(type: "integer", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    VilleId = table.Column<int>(type: "integer", nullable: false),
                    TypeEntrepriseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exploitants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exploitants_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exploitants_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exploitants_TypesEntreprise_TypeEntrepriseId",
                        column: x => x.TypeEntrepriseId,
                        principalTable: "TypesEntreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exploitants_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exploitants_Villes_VilleId",
                        column: x => x.VilleId,
                        principalTable: "Villes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TitresExploitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExploitantId = table.Column<int>(type: "integer", nullable: false),
                    ReferenceTitre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    TypeTitreId = table.Column<int>(type: "integer", nullable: false),
                    TypeReseauId = table.Column<int>(type: "integer", nullable: true),
                    DateSignature = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateExpiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Statut = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NombreTitres = table.Column<int>(type: "integer", nullable: true),
                    FrequencesAssignees = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Observation = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DateSaisie = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceSourceId = table.Column<int>(type: "integer", nullable: false),
                    UtilisateurCreateurId = table.Column<int>(type: "integer", nullable: false),
                    Actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitresExploitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitresExploitation_Exploitants_ExploitantId",
                        column: x => x.ExploitantId,
                        principalTable: "Exploitants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitresExploitation_Services_ServiceSourceId",
                        column: x => x.ServiceSourceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitresExploitation_TypesReseau_TypeReseauId",
                        column: x => x.TypeReseauId,
                        principalTable: "TypesReseau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitresExploitation_TypesTitre_TypeTitreId",
                        column: x => x.TypeTitreId,
                        principalTable: "TypesTitre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitresExploitation_Utilisateurs_UtilisateurCreateurId",
                        column: x => x.UtilisateurCreateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exploitants_CodeExploitant",
                table: "Exploitants",
                column: "CodeExploitant");

            migrationBuilder.CreateIndex(
                name: "IX_Exploitants_RegionId",
                table: "Exploitants",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Exploitants_ServiceId",
                table: "Exploitants",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Exploitants_TypeEntrepriseId",
                table: "Exploitants",
                column: "TypeEntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Exploitants_UtilisateurId",
                table: "Exploitants",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Exploitants_VilleId",
                table: "Exploitants",
                column: "VilleId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Code",
                table: "Services",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_ExploitantId",
                table: "TitresExploitation",
                column: "ExploitantId");

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_ReferenceTitre",
                table: "TitresExploitation",
                column: "ReferenceTitre");

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_ServiceSourceId",
                table: "TitresExploitation",
                column: "ServiceSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_TypeReseauId",
                table: "TitresExploitation",
                column: "TypeReseauId");

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_TypeTitreId",
                table: "TitresExploitation",
                column: "TypeTitreId");

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_UtilisateurCreateurId",
                table: "TitresExploitation",
                column: "UtilisateurCreateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Login",
                table: "Utilisateurs",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_ServiceId",
                table: "Utilisateurs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Villes_RegionId",
                table: "Villes",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TitresExploitation");

            migrationBuilder.DropTable(
                name: "Exploitants");

            migrationBuilder.DropTable(
                name: "TypesReseau");

            migrationBuilder.DropTable(
                name: "TypesTitre");

            migrationBuilder.DropTable(
                name: "TypesEntreprise");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Villes");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
