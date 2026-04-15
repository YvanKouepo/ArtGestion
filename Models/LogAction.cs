using System.ComponentModel.DataAnnotations;
using ArtGestion.Helpers;

namespace ArtGestion.Models
{
    public class LogAction
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TypeAction { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Entite { get; set; } = string.Empty;

        public int? EntiteId { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime DateAction { get; set; } = DateHelper.GetCameroonTime();

        public int? ServiceId { get; set; }
        public Service? Service { get; set; }

        public int? UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; }
    }
}