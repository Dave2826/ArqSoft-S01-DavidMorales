namespace MotoTrack.Domain.Models
{
    public class LecturaKilometraje
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid MotocicletaId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int Kilometraje { get; set; }

        public string Observaciones { get; set; } = "";
    }
}
