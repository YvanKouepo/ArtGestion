using System.ComponentModel.DataAnnotations;

namespace ArtGestion.Models
{
    public class TypeEntreprise
    {
        public int Id { get; set; }

        public string Libelle { get; set; } = string.Empty;

        public bool Actif { get; set; } = true;

        public ICollection<Exploitant> Exploitants { get; set; } = new List<Exploitant>();
    }
}