using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArtGestion.Models
{
    public class DashboardViewModel
    {
        public int TotalExploitants { get; set; }
        public int TotalTitres { get; set; }
        public int Actifs { get; set; }
        public int BientotExpires { get; set; }
        public int Expires { get; set; }
        public int AlertesNonLues { get; set; }

        public int? RegionId { get; set; }
        public string? Statut { get; set; }

        public List<SelectListItem> Regions { get; set; } = new();
        public List<SelectListItem> Statuts { get; set; } = new();

        public List<string> RegionsLabels { get; set; } = new();
        public List<int> RegionsCounts { get; set; } = new();

        public List<string> TypesTitreLabels { get; set; } = new();
        public List<int> TypesTitreCounts { get; set; } = new();
    }
}