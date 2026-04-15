using System.ComponentModel.DataAnnotations;

namespace ArtGestion.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Nom { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; set; }

        public bool Actif { get; set; } = true;

        public ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
        public ICollection<Exploitant> Exploitants { get; set; } = new List<Exploitant>();
        public ICollection<TitreExploitation> TitresExploitation { get; set; } = new List<TitreExploitation>();

        
    }
}