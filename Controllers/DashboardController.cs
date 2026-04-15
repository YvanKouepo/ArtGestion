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

            var exploitantsCount = await _context.Exploitants.CountAsync();
            var titresCount = await _context.TitresExploitation.CountAsync();

            var actifs = await _context.TitresExploitation.CountAsync(t => t.Statut == "Actif");
            var bientotExpires = await _context.TitresExploitation.CountAsync(t => t.Statut == "Bientôt expiré");
            var expires = await _context.TitresExploitation.CountAsync(t => t.Statut == "Expiré");
            var alertesNonLues = await _context.Alertes.CountAsync(a => !a.EstLue);

            var vm = new DashboardViewModel
            {
                TotalExploitants = exploitantsCount,
                TotalTitres = titresCount,
                Actifs = actifs,
                BientotExpires = bientotExpires,
                Expires = expires,
                AlertesNonLues = alertesNonLues
            };

            return View(vm);
        }
    }
}