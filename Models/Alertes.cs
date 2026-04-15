using System.ComponentModel.DataAnnotations;
using ArtGestion.Helpers;

namespace ArtGestion.Models
{
    public class Alerte
    {
        public int Id { get; set; }

        public int TitreExploitationId { get; set; }
        public TitreExploitation? TitreExploitation { get; set; }

        [Required]
        [StringLength(100)]
        public string TypeAlerte { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        public DateTime DateGeneration { get; set; } = DateHelper.GetCameroonTime();

        public bool EstLue { get; set; } = false;

        public DateTime? DateLecture { get; set; }

        public int? ServiceId { get; set; }
        public Service? Service { get; set; }

        public int? UtilisateurResponsableId { get; set; }
        public Utilisateur? UtilisateurResponsable { get; set; }
    }
}