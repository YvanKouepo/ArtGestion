using ArtGestion.Data;
using ArtGestion.Models;
using ArtGestion.ServicesMetier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AlerteService _alerteService;

        public DashboardController(ApplicationDbContext context, AlerteService alerteService)
        {
            _context = context;
            _alerteService = alerteService;
        }

        public async Task<IActionResult> Index()
        {
            await _alerteService.GenererAlertesExpirationAsync();

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

            return View(vm);
        }
    }
}