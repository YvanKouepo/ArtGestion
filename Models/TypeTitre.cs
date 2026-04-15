using System.ComponentModel.DataAnnotations;

namespace ArtGestion.Models
{
    public class TypeTitre
    {
        public int Id { get; set; }

        [Required]
        public string Libelle { get; set; } = string.Empty;

        public int DureeValidite { get; set; }

        public string UniteDuree { get; set; } = "Mois";

        public bool Actif { get; set; } = true;

        public ICollection<TitreExploitation> TitresExploitation { get; set; } = new List<TitreExploitation>();
    }
}