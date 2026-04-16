using ArtGestion.Data;
using ArtGestion.Models;
using Microsoft.EntityFrameworkCore;

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
                string? typeAlerte = null;

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

                bool existe = await _context.Alertes.AnyAsync(a =>
                    a.TitreExploitationId == titre.Id &&
                    a.TypeAlerte == typeAlerte);

                if (existe)
                    continue;

                var alerte = new Alerte
                {
                    TitreExploitationId = titre.Id,
                    Message = $"Le titre {titre.ReferenceTitre} de {titre.Exploitant?.NomExploitant} est {typeAlerte}",
                    TypeAlerte = typeAlerte,
                    DateGeneration = DateTime.UtcNow,
                    EstLue = false
                };

                _context.Alertes.Add(alerte);
            }

            await _context.SaveChangesAsync();
        }
    }
}