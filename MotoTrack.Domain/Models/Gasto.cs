using System.ComponentModel.DataAnnotations;

namespace MotoTrack.Domain.Models
{
    public class Gasto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid MotocicletaId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        public string Descripcion { get; set; } = "";

        [Range(0, double.MaxValue)]
        public decimal Monto { get; set; }

        [Required]
        public string Categoria { get; set; } = "";
    }
}