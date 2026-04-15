using ArtGestion.Data;
using ArtGestion.Helpers;
using Microsoft.EntityFrameworkCore;
using ArtGestion.Models;
namespace ArtGestion.ServicesMetier
{
    public static class AlerteService
    {
        public static async Task GenererAlertesExpirationAsync(ApplicationDbContext context)
        {
            var today = DateHelper.GetCameroonTime().Date;
            var seuil = today.AddDays(30);

            var titres = await context.TitresExploitation
                .Include(t => t.Exploitant)
                .Include(t => t.TypeTitre)
                .Where(t => t.Actif)
                .ToListAsync();

            foreach (var titre in titres)
            {
                string? typeAlerte = null;
                string? message = null;

                if (titre.DateExpiration.Date < today)
                {
                    typeAlerte = "Expiré";
                    message = $"Le titre {titre.ReferenceTitre ?? "(sans référence)"} de l'exploitant {titre.Exploitant?.NomExploitant} est expiré depuis le {titre.DateExpiration:dd/MM/yyyy}.";
                }
                else if (titre.DateExpiration.Date <= seuil)
                {
                    typeAlerte = "Bientôt expiré";
                    message = $"Le titre {titre.ReferenceTitre ?? "(sans référence)"} de l'exploitant {titre.Exploitant?.NomExploitant} expire le {titre.DateExpiration:dd/MM/yyyy}.";
                }

                if (typeAlerte is null || message is null)
                    continue;

                var alerteExistante = await context.Alertes
                    .FirstOrDefaultAsync(a =>
                        a.TitreExploitationId == titre.Id &&
                        a.TypeAlerte == typeAlerte);

                if (alerteExistante == null)
                {
                    var alerte = new Alerte
                    {
                        TitreExploitationId = titre.Id,
                        TypeAlerte = typeAlerte,
                        Message = message,
                        DateGeneration = DateHelper.GetCameroonTime(),
                        EstLue = false,
                        ServiceId = titre.ServiceId
                    };

                    context.Alertes.Add(alerte);
                }
                else
                {
                    // Mise à jour de l’alerte existante si besoin
                    if (alerteExistante.Message != message)
                        alerteExistante.Message = message;

                    // On remet l’alerte à jour si son contenu change
                    alerteExistante.DateGeneration = DateHelper.GetCameroonTime();
                }

                // Si le titre est expiré, on retire l’ancienne alerte "Bientôt expiré"
                if (typeAlerte == "Expiré")
                {
                    var alerteBientotExpiree = await context.Alertes
                        .FirstOrDefaultAsync(a =>
                            a.TitreExploitationId == titre.Id &&
                            a.TypeAlerte == "Bientôt expiré");

                    if (alerteBientotExpiree != null)
                    {
                        context.Alertes.Remove(alerteBientotExpiree);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}