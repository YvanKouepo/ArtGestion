using Microsoft.AspNetCore.Identity;

namespace ArtGestion.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NomComplet { get; set; } = string.Empty;

        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        public bool EstResponsableService { get; set; } = false;

        public bool Actif { get; set; } = true;

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    }
}