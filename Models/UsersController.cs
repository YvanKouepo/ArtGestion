using ArtGestion.Data;
using ArtGestion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .Include(u => u.Service)
                .ToListAsync();

            return View(users);
        }

        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name");
            ViewBag.Services = new SelectList(_context.Services, "Id", "Nom");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string username, string email, string password, string role, int serviceId)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Champs obligatoires");
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                NomComplet = username,
                ServiceId = serviceId,
                Actif = true
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name");
            ViewBag.Services = new SelectList(_context.Services, "Id", "Nom");

            return View();
        }
    }
}