using ArtGestion.Data;
using ArtGestion.ServicesMetier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Controllers
{
    public class AlertesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AlerteService _alerteService;

        public AlertesController(ApplicationDbContext context, AlerteService alerteService)
        {
            _context = context;
            _alerteService = alerteService;
        }

        public async Task<IActionResult> Index()
        {
            await _alerteService.GenererAlertesExpirationAsync();

            var alertes = await _context.Alertes
                .Include(a => a.TitreExploitation)
                    .ThenInclude(t => t!.Exploitant)
                .OrderByDescending(a => a.DateGeneration)
                .ToListAsync();

            return View(alertes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarquerCommeLue(int id)
        {
            var alerte = await _context.Alertes.FindAsync(id);
            if (alerte != null)
            {
                alerte.EstLue = true;
                alerte.DateLecture = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}