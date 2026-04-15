using System.ComponentModel.DataAnnotations;

namespace ArtGestion.Models
{
    public class Ville
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        public int RegionId { get; set; }
        public Region? Region { get; set; }

        public bool Actif { get; set; } = true;

        public ICollection<Exploitant> Exploitants { get; set; } = new List<Exploitant>();
    }
}