using System.ComponentModel.DataAnnotations;

namespace ArtGestion.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string NomComplet { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Login { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string MotDePasseHash { get; set; } = string.Empty;

        [StringLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "Agent";

        public bool EstResponsableService { get; set; } = false;
        public bool Actif { get; set; } = true;

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        public ICollection<Exploitant> ExploitantsCrees { get; set; } = new List<Exploitant>();
        public ICollection<TitreExploitation> TitresCrees { get; set; } = new List<TitreExploitation>();
    }
}