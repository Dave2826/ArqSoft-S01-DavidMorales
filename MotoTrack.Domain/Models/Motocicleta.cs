using System.ComponentModel.DataAnnotations;

namespace MotoTrack.Domain.Models
{
    public class Motocicleta
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UsuarioId { get; set; }

        [Required]
        public string Marca { get; set; } = "";

        [Required]
        public string Modelo { get; set; } = "";

        [Required]
        public int Ano { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cilindrada { get; set; }

        [Required]
        public int KilometrajeActual { get; set; }

        public int? KilometrajeCompra { get; set; }

        public string? FotoUrl { get; set; }

        // Opcionales

        public string? Placas { get; set; }

        public string? VIN { get; set; }

        public string? NumeroMotor { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activa { get; set; } = true;
    }
}
