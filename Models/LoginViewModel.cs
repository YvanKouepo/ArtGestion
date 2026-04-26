using System.ComponentModel.DataAnnotations;

namespace ArtGestion.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}