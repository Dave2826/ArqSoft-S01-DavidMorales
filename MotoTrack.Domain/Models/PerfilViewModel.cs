namespace MotoTrack.Domain.Models
{
    public class PerfilViewModel
    {
        public string Nombre { get; set; } = "";

        public string Apellido { get; set; } = "";

        public string Correo { get; set; } = "";

        public DateTime FechaRegistro { get; set; }

        // Estadísticas futuras

        public int TotalMotocicletas { get; set; }

        public int ServiciosRealizados { get; set; }

        public decimal GastoAcumulado { get; set; }
    }
}
