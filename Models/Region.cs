using System.ComponentModel.DataAnnotations;

namespace ArtGestion.Models
{
    public class Region
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Code { get; set; }

        public bool Actif { get; set; } = true;

        public ICollection<Ville> Villes { get; set; } = new List<Ville>();
        public ICollection<Exploitant> Exploitants { get; set; } = new List<Exploitant>();
    }
}