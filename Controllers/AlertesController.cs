using Microsoft.AspNetCore.Mvc;
using ArtGestion.Data;
using ArtGestion.Models;

namespace ArtGestion.Controllers
{
    public class AlertesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var alertes = _context.Alertes.ToList();
            return View(alertes);
        }

        public IActionResult MarquerCommeLue(int id)
        {
            var alerte = _context.Alertes.Find(id);
            if (alerte != null)
            {
                alerte.Lue = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}