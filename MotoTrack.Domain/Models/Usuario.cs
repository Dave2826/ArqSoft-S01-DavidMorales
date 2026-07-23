using System.ComponentModel.DataAnnotations;

namespace MotoTrack.Domain.Models
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Nombre { get; set; } = "";

        [Required]
        public string Apellido { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}
