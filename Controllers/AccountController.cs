using System.Security.Claims;
using ArtGestion.Data;
using ArtGestion.Helpers;
using ArtGestion.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var utilisateur = await _context.Utilisateurs
                .Include(u => u.Service)
                .FirstOrDefaultAsync(u => u.Login == model.Login && u.Actif);

            if (utilisateur == null || !PasswordHelper.VerifyPassword(model.Password, utilisateur.MotDePasseHash))
            {
                ModelState.AddModelError("", "Login ou mot de passe invalide.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString()),
                new Claim(ClaimTypes.Name, utilisateur.NomComplet),
                new Claim(ClaimTypes.Role, utilisateur.Role),
                new Claim("ServiceId", utilisateur.ServiceId.ToString()),
                new Claim("ServiceNom", utilisateur.Service?.Nom ?? "")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}