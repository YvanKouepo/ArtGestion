using ArtGestion.Data;
using ArtGestion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _context.Services
                .OrderBy(s => s.Nom)
                .ToListAsync();

            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
                return View(service);

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            if (id != service.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(service);

            _context.Services.Update(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}