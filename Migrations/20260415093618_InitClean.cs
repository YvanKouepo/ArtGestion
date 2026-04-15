using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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

            migrationBuilder.CreateTable(
                name: "TitresExploitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExploitantId = table.Column<int>(type: "integer", nullable: false),
                    ReferenceTitre = table.Column<string>(type: "text", nullable: true),
                    TypeTitreId = table.Column<int>(type: "integer", nullable: false),
                    TypeReseauId = table.Column<int>(type: "integer", nullable: true),
                    DateSignature = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateExpiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Statut = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NombreTitres = table.Column<int>(type: "integer", nullable: true),
                    FrequencesAssignees = table.Column<string>(type: "text", nullable: true),
                    Observation = table.Column<string>(type: "text", nullable: true),
                    DateSaisie = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    UtilisateurId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_TitresExploitation_Services_ServiceId",
                        column: x => x.ServiceId,
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
                        name: "FK_TitresExploitation_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alertes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Lue = table.Column<bool>(type: "boolean", nullable: false),
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
                values: new object[] { 1, true, new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7738), "admin@art.local", true, "admin", "temp-hash", "Admin SSVTSE", "Admin", 1 });

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
                    { 1, "EXP001", null, new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7792), "MTN Cameroun", 1, 1, 1, 1, 1 },
                    { 2, "EXP002", null, new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7800), "Orange Cameroun", 1, 1, 1, 1, 1 },
                    { 3, "EXP003", null, new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7802), "Nexttel", 2, 1, 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "TitresExploitation",
                columns: new[] { "Id", "Actif", "DateExpiration", "DateSaisie", "DateSignature", "ExploitantId", "FrequencesAssignees", "NombreTitres", "Observation", "ReferenceTitre", "ServiceId", "Statut", "TypeReseauId", "TypeTitreId", "UtilisateurId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7827), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, 1, null, "TITRE-001", 1, "Actif", null, 1, 1 },
                    { 2, true, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7836), new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, null, 2, null, "TITRE-002", 1, "Bientôt expiré", null, 1, 1 },
                    { 3, false, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 15, 9, 36, 18, 232, DateTimeKind.Utc).AddTicks(7838), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, null, 1, null, "TITRE-003", 1, "Expiré", null, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_ServiceId",
                table: "Alertes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_TitreExploitationId_TypeAlerte",
                table: "Alertes",
                columns: new[] { "TitreExploitationId", "TypeAlerte" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_UtilisateurResponsableId",
                table: "Alertes",
                column: "UtilisateurResponsableId");

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
                name: "IX_LogsActions_ServiceId",
                table: "LogsActions",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_LogsActions_UtilisateurId",
                table: "LogsActions",
                column: "UtilisateurId");

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
                name: "IX_TitresExploitation_ServiceId",
                table: "TitresExploitation",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_TypeReseauId",
                table: "TitresExploitation",
                column: "TypeReseauId");

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_TypeTitreId",
                table: "TitresExploitation",
                column: "TypeTitreId");

            migrationBuilder.CreateIndex(
                name: "IX_TitresExploitation_UtilisateurId",
                table: "TitresExploitation",
                column: "UtilisateurId");

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
                name: "Alertes");

            migrationBuilder.DropTable(
                name: "LogsActions");

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
