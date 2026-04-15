using System.ComponentModel.DataAnnotations;

namespace ArtGestion.Models
{
    public class TypeReseau
    {
        public int Id { get; set; }

        public string Libelle { get; set; } = string.Empty;

        public bool Actif { get; set; } = true;

        public ICollection<TitreExploitation> TitresExploitation { get; set; } = new List<TitreExploitation>();
    }
}