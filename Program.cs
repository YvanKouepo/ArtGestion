using ArtGestion.Models;
using Microsoft.AspNetCore.Identity;
using ArtGestion.Data;
using Microsoft.EntityFrameworkCore;
using ArtGestion.Seed;
using ArtGestion.ServicesMetier;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AlerteService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DataSeeder.Seed(context);
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // 🔹 Création des rôles
    string[] roles = new[]
    {
        "SuperAdmin",
        "Admin",
        "Superviseur",
        "Agent",
        "Personnel"
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // 🔹 Création du SuperAdmin
    var superAdminEmail = "admin@art.local";
    var superAdminUser = await userManager.FindByEmailAsync(superAdminEmail);

    if (superAdminUser == null)
    {
        var user = new ApplicationUser
        {
            UserName = "superadmin",
            Email = superAdminEmail,
            NomComplet = "Super Admin",
            EmailConfirmed = true,
            Actif = true,
            ServiceId = 1
        };

        var result = await userManager.CreateAsync(user, "Admin@123!");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "SuperAdmin");
        }
    }
}
app.Run();