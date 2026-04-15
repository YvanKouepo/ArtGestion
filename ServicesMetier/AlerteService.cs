using ArtGestion.Data;
using ArtGestion.Helpers;
using Microsoft.EntityFrameworkCore;
using ArtGestion.Models;
namespace ArtGestion.ServicesMetier
{
    public class AlerteService
    {

        private readonly ApplicationDbContext _context;

public AlerteService(ApplicationDbContext context)
{
    _context = context;
}
        public async Task GenererAlertesExpirationAsync()
{
    var aujourdHui = DateTime.UtcNow;
    var seuil = aujourdHui.AddDays(30);
    var titres = await _context.TitresExploitation
        .Include(t => t.Exploitant)
        .ToListAsync();

    foreach (var titre in titres)
    {
        string typeAlerte = null;

        if (titre.DateExpiration < aujourdHui)
        {
            typeAlerte = "Expiré";
        }
        else if (titre.DateExpiration <= seuil)
        {
            typeAlerte = "Bientôt expiré";
        }

        if (typeAlerte == null)
            continue;

        // 🔥 Anti doublon
        bool existe = await _context.Alertes.AnyAsync(a =>
            a.TitreExploitationId == titre.Id &&
            a.Type == typeAlerte);

        if (existe)
            continue;

        var alerte = new Alerte
        {
            TitreExploitationId = titre.Id,
            Message = $"Le titre {titre.ReferenceTitre} de {titre.Exploitant.NomExploitant} est {typeAlerte}",
            Type = typeAlerte,
            DateCreation = DateTime.UtcNow,
            Lue = false
        };

        _context.Alertes.Add(alerte);
    }

    await _context.SaveChangesAsync();
}
    }
}