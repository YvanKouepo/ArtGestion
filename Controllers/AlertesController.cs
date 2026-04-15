using ArtGestion.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtGestion.ServicesMetier;

namespace ArtGestion.Controllers
{
    public class AlertesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            await AlerteService.GenererAlertesExpirationAsync(_context);
            await ServicesMetier.AlerteService.GenererAlertesExpirationAsync(_context);
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
                alerte.DateLecture = Helpers.DateHelper.GetCameroonTime();
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}