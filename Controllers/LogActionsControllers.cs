using ArtGestion.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Controllers
{
    public class LogsActionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogsActionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? typeAction, string? entite)
        {
            var query = _context.LogsActions
                .Include(l => l.Service)
                .Include(l => l.Utilisateur)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(typeAction))
                query = query.Where(l => l.TypeAction == typeAction);

            if (!string.IsNullOrWhiteSpace(entite))
                query = query.Where(l => l.Entite == entite);

            var logs = await query
                .OrderByDescending(l => l.DateAction)
                .ToListAsync();

            ViewBag.TypeAction = typeAction;
            ViewBag.Entite = entite;

            return View(logs);
        }
    }
}