using System.ComponentModel.DataAnnotations;

namespace MotoTrack.Domain.Models
{
    public class Mantenimiento
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid MotocicletaId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Range(0, int.MaxValue)]
        public int KilometrajeServicio { get; set; }

        [Required]
        public string Tipo { get; set; } = "";

        public decimal? Costo { get; set; }

        public string Descripcion { get; set; } = "";

        public string Taller { get; set; } = "";
    }
}
