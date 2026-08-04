using System.ComponentModel.DataAnnotations;

namespace MotoTrack.Domain.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
