using ArtGestion.Data;
using ArtGestion.Helpers;
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
            var today = DateHelper.GetCameroonTime().Date;

            var totalExploitants = await _context.Exploitants.CountAsync();
            var totalTitres = await _context.TitresExploitation.CountAsync();

            var actifs = await _context.TitresExploitation
                .CountAsync(t => t.DateExpiration >= today.AddDays(30));

            var bientotExpires = await _context.TitresExploitation
                .CountAsync(t => t.DateExpiration >= today && t.DateExpiration < today.AddDays(30));

            var expires = await _context.TitresExploitation
                .CountAsync(t => t.DateExpiration < today);

            var alertesNonLues = await _context.Alertes
                .CountAsync(a => !a.EstLue);

            ViewBag.TotalExploitants = totalExploitants;
            ViewBag.TotalTitres = totalTitres;
            ViewBag.Actifs = actifs;
            ViewBag.BientotExpires = bientotExpires;
            ViewBag.Expires = expires;
            ViewBag.AlertesNonLues = alertesNonLues;

            return View();
        }
    }
}