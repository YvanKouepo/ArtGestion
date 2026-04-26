using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArtGestion.Models;

namespace ArtGestion.Controllers;

public class HomeController : Controller
{
    [AllowAnonymous]

    public IActionResult Index()
    {
        var hash = ArtGestion.Helpers.PasswordHelper.HashPassword("Admin@123");
        Console.WriteLine(hash);
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
