using ArtGestion.Data;
using ArtGestion.Helpers;
using ArtGestion.Models;

namespace ArtGestion.ServicesMetier
{
    public static class LogService
    {
        public static async Task EnregistrerAsync(
            ApplicationDbContext context,
            string typeAction,
            string entite,
            int? entiteId,
            string? description,
            int? serviceId = null,
            int? utilisateurId = null)
        {
            var log = new LogAction
            {
                TypeAction = typeAction,
                Entite = entite,
                EntiteId = entiteId,
                Description = description,
                DateAction = DateHelper.GetCameroonTime(),
                ServiceId = serviceId,
                UtilisateurId = utilisateurId
            };

            context.LogsActions.Add(log);
            await context.SaveChangesAsync();
        }
    }
}