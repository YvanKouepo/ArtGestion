using ArtGestion.Data;
using ArtGestion.Models;
using ArtGestion.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ArtGestion.ServicesMetier
{
    public static class AlerteService
    {
        public static async Task GenererAlertesExpirationAsync(ApplicationDbContext context)
        {
            var today = DateHelper.GetCameroonTime().Date;

            var titres = await context.TitresExploitation
                .Include(t => t.Exploitant)
                .ToListAsync();

            foreach (var titre in titres)
            {
                var joursRestants = (titre.DateExpiration - today).Days;

                string type = "";
                string message = "";

                if (joursRestants < 0)
                {
                    type = "EXPIRÉ";
                    message = $"Le titre {titre.TypeTitre} de {titre.Exploitant?.NomExploitant} est expiré.";
                }
                else if (joursRestants <= 30)
                {
                    type = "BIENTÔT EXPIRÉ";
                    message = $"Le titre {titre.TypeTitre} de {titre.Exploitant?.NomExploitant} expire dans {joursRestants} jours.";
                }
                else
                {
                    continue;
                }

                bool existe = await context.Alertes.AnyAsync(a =>
                    a.TitreExploitationId == titre.Id &&
                    a.TypeAlerte == type &&
                    !a.EstLue);

                if (!existe)
                {
                    var alerte = new Alerte
                    {
                        TitreExploitationId = titre.Id,
                        TypeAlerte = type,
                        Message = message,
                        DateGeneration = DateHelper.GetCameroonTime(),
                        EstLue = false
                    };

                    context.Alertes.Add(alerte);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}