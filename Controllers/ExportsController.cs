using Microsoft.AspNetCore.Authorization;
using ArtGestion.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using ArtGestion.Documents;
using ArtGestion.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ArtGestion.Controllers
{
    [Authorize]
    public class ExportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PdfSynthese()
{
    QuestPDF.Settings.License = LicenseType.Evaluation;

    var vm = new DashboardViewModel
    {
        TotalExploitants = await _context.Exploitants.CountAsync(),
        TotalTitres = await _context.TitresExploitation.CountAsync(),
        Actifs = await _context.TitresExploitation.CountAsync(t => t.Statut == "Actif"),
        BientotExpires = await _context.TitresExploitation.CountAsync(t => t.Statut == "Bientôt expiré"),
        Expires = await _context.TitresExploitation.CountAsync(t => t.Statut == "Expiré"),
        AlertesNonLues = await _context.Alertes.CountAsync(a => !a.EstLue)
    };

    var regionsData = await _context.Exploitants
        .Include(e => e.Region)
        .GroupBy(e => e.Region!.Nom)
        .Select(g => new { Region = g.Key, Count = g.Count() })
        .OrderBy(x => x.Region)
        .ToListAsync();

    vm.RegionsLabels = regionsData.Select(x => x.Region).ToList();
    vm.RegionsCounts = regionsData.Select(x => x.Count).ToList();

    var typesTitreData = await _context.TitresExploitation
        .Include(t => t.TypeTitre)
        .GroupBy(t => t.TypeTitre!.Libelle)
        .Select(g => new { TypeTitre = g.Key, Count = g.Count() })
        .OrderBy(x => x.TypeTitre)
        .ToListAsync();

    vm.TypesTitreLabels = typesTitreData.Select(x => x.TypeTitre).ToList();
    vm.TypesTitreCounts = typesTitreData.Select(x => x.Count).ToList();

    var document = new SyntheseDashboardDocument(vm);
    var pdf = document.GeneratePdf();

    return File(pdf, "application/pdf", $"Synthese_ArtGestion_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf");
}

        public async Task<IActionResult> ExcelSynthese()
        {
            var exploitants = await _context.Exploitants
                .Include(e => e.Region)
                .Include(e => e.Ville)
                .Include(e => e.TypeEntreprise)
                .ToListAsync();

            var titres = await _context.TitresExploitation
                .Include(t => t.Exploitant)
                .Include(t => t.TypeTitre)
                .Include(t => t.TypeReseau)
                .ToListAsync();

            var alertes = await _context.Alertes
                .Include(a => a.TitreExploitation)
                    .ThenInclude(t => t!.Exploitant)
                .OrderByDescending(a => a.DateGeneration)
                .ToListAsync();

            var logs = await _context.LogsActions
                .Include(l => l.Service)
                .Include(l => l.Utilisateur)
                .OrderByDescending(l => l.DateAction)
                .ToListAsync();

            using var workbook = new XLWorkbook();

            // Feuille 1 - Exploitants
            var wsExploitants = workbook.Worksheets.Add("Exploitants");
            wsExploitants.Cell(1, 1).Value = "Code";
            wsExploitants.Cell(1, 2).Value = "Nom exploitant";
            wsExploitants.Cell(1, 3).Value = "Contact";
            wsExploitants.Cell(1, 4).Value = "Région";
            wsExploitants.Cell(1, 5).Value = "Ville";
            wsExploitants.Cell(1, 6).Value = "Type entreprise";
            wsExploitants.Cell(1, 7).Value = "Date saisie";

            int row = 2;
            foreach (var e in exploitants)
            {
                wsExploitants.Cell(row, 1).Value = e.CodeExploitant;
                wsExploitants.Cell(row, 2).Value = e.NomExploitant;
                wsExploitants.Cell(row, 3).Value = e.Contact;
                wsExploitants.Cell(row, 4).Value = e.Region?.Nom;
                wsExploitants.Cell(row, 5).Value = e.Ville?.Nom;
                wsExploitants.Cell(row, 6).Value = e.TypeEntreprise?.Libelle;
                wsExploitants.Cell(row, 7).Value = e.DateSaisie;
                wsExploitants.Cell(row, 7).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                row++;
            }

            // Feuille 2 - Titres
            var wsTitres = workbook.Worksheets.Add("Titres");
            wsTitres.Cell(1, 1).Value = "Référence";
            wsTitres.Cell(1, 2).Value = "Exploitant";
            wsTitres.Cell(1, 3).Value = "Type titre";
            wsTitres.Cell(1, 4).Value = "Type réseau";
            wsTitres.Cell(1, 5).Value = "Date signature";
            wsTitres.Cell(1, 6).Value = "Date expiration";
            wsTitres.Cell(1, 7).Value = "Statut";
            wsTitres.Cell(1, 8).Value = "Nombre titres";

            row = 2;
            foreach (var t in titres)
            {
                wsTitres.Cell(row, 1).Value = t.ReferenceTitre;
                wsTitres.Cell(row, 2).Value = t.Exploitant?.NomExploitant;
                wsTitres.Cell(row, 3).Value = t.TypeTitre?.Libelle;
                wsTitres.Cell(row, 4).Value = t.TypeReseau?.Libelle;
                wsTitres.Cell(row, 5).Value = t.DateSignature;
                wsTitres.Cell(row, 6).Value = t.DateExpiration;
                wsTitres.Cell(row, 7).Value = t.Statut;
                wsTitres.Cell(row, 8).Value = t.NombreTitres;

                wsTitres.Cell(row, 5).Style.DateFormat.Format = "dd/MM/yyyy";
                wsTitres.Cell(row, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                row++;
            }

            // Feuille 3 - Alertes
            var wsAlertes = workbook.Worksheets.Add("Alertes");
            wsAlertes.Cell(1, 1).Value = "Type alerte";
            wsAlertes.Cell(1, 2).Value = "Message";
            wsAlertes.Cell(1, 3).Value = "Date génération";
            wsAlertes.Cell(1, 4).Value = "État";
            wsAlertes.Cell(1, 5).Value = "Exploitant";

            row = 2;
            foreach (var a in alertes)
            {
                wsAlertes.Cell(row, 1).Value = a.TypeAlerte;
                wsAlertes.Cell(row, 2).Value = a.Message;
                wsAlertes.Cell(row, 3).Value = a.DateGeneration;
                wsAlertes.Cell(row, 4).Value = a.EstLue ? "Lue" : "Non lue";
                wsAlertes.Cell(row, 5).Value = a.TitreExploitation?.Exploitant?.NomExploitant;
                wsAlertes.Cell(row, 3).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                row++;
            }

            // Feuille 4 - Logs
            var wsLogs = workbook.Worksheets.Add("Logs");
            wsLogs.Cell(1, 1).Value = "Date";
            wsLogs.Cell(1, 2).Value = "Type action";
            wsLogs.Cell(1, 3).Value = "Entité";
            wsLogs.Cell(1, 4).Value = "ID entité";
            wsLogs.Cell(1, 5).Value = "Description";
            wsLogs.Cell(1, 6).Value = "Service";
            wsLogs.Cell(1, 7).Value = "Utilisateur";

            row = 2;
            foreach (var l in logs)
            {
                wsLogs.Cell(row, 1).Value = l.DateAction;
                wsLogs.Cell(row, 2).Value = l.TypeAction;
                wsLogs.Cell(row, 3).Value = l.Entite;
                wsLogs.Cell(row, 4).Value = l.EntiteId;
                wsLogs.Cell(row, 5).Value = l.Description;
                wsLogs.Cell(row, 6).Value = l.Service?.Nom;
                wsLogs.Cell(row, 7).Value = l.Utilisateur?.NomComplet;
                wsLogs.Cell(row, 1).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                row++;
            }

            // Mise en forme minimale
            foreach (var ws in workbook.Worksheets)
            {
                var header = ws.Range(1, 1, 1, ws.LastColumnUsed().ColumnNumber());
                header.Style.Font.Bold = true;
                header.Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Columns().AdjustToContents();
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Synthese_ArtGestion_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx");
        }
    }
}