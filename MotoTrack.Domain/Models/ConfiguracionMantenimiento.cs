namespace MotoTrack.Domain.Models
{
    public class ConfiguracionMantenimiento
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid MotocicletaId { get; set; }

        public int CambioAceiteKm { get; set; }

        public int RevisionCadenaKm { get; set; }

        public int RevisionBalatasKm { get; set; }

        public int RevisionLlantasKm { get; set; }

        public int RevisionFiltroAireKm { get; set; }

        public int AjusteValvulasKm { get; set; }
    }
}
