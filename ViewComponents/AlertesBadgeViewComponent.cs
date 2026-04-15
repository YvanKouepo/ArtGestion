using ArtGestion.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.ViewComponents
{
    public class AlertesBadgeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AlertesBadgeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = await _context.Alertes.CountAsync(a => !a.EstLue);
            return View(count);
        }
    }
}