using ArtGestion.Data;
using ArtGestion.Helpers;
using ArtGestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.UtcNow.Date;

            var model = new DashboardViewModel
            {
                TotalExploitants = await _context.Exploitants.CountAsync(),
                TotalTitres = await _context.TitresExploitation.CountAsync(),
                Actifs = await _context.TitresExploitation.CountAsync(t => t.DateExpiration >= today.AddDays(30)),
                BientotExpires = await _context.TitresExploitation.CountAsync(t => t.DateExpiration >= today && t.DateExpiration < today.AddDays(30)),
                Expires = await _context.TitresExploitation.CountAsync(t => t.DateExpiration < today),
                AlertesNonLues = await _context.Alertes.CountAsync(a => !a.EstLue)
            };

            var regionsData = await _context.Exploitants
                .Include(e => e.Region)
                .GroupBy(e => e.Region!.Nom)
                .Select(g => new { Region = g.Key, Count = g.Count() })
                .OrderBy(x => x.Region)
                .ToListAsync();

            model.RegionsLabels = regionsData.Select(x => x.Region).ToList();
            model.RegionsCounts = regionsData.Select(x => x.Count).ToList();

            var typesTitreData = await _context.TitresExploitation
                .Include(t => t.TypeTitre)
                .GroupBy(t => t.TypeTitre!.Libelle)
                .Select(g => new { TypeTitre = g.Key, Count = g.Count() })
                .OrderBy(x => x.TypeTitre)
                .ToListAsync();

            model.TypesTitreLabels = typesTitreData.Select(x => x.TypeTitre).ToList();
            model.TypesTitreCounts = typesTitreData.Select(x => x.Count).ToList();

            return View(model);
        }
    }
}