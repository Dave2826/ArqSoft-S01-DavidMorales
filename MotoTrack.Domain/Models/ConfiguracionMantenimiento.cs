using MotoTrack.Domain.Enums;

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

        public int? ObtenerIntervalo(MaintenanceType tipo)
        {
            return tipo switch
            {
                MaintenanceType.Aceite => CambioAceiteKm,
                MaintenanceType.Cadena => RevisionCadenaKm,
                MaintenanceType.Balatas => RevisionBalatasKm,
                MaintenanceType.Llantas => RevisionLlantasKm,
                MaintenanceType.FiltroAire => RevisionFiltroAireKm,
                MaintenanceType.Valvulas => AjusteValvulasKm,
                _ => null
            };
        }

        public void AsignarIntervalo(MaintenanceType tipo, int intervaloKm)
        {
            switch (tipo)
            {
                case MaintenanceType.Aceite:
                    CambioAceiteKm = intervaloKm;
                    break;
                case MaintenanceType.Cadena:
                    RevisionCadenaKm = intervaloKm;
                    break;
                case MaintenanceType.Balatas:
                    RevisionBalatasKm = intervaloKm;
                    break;
                case MaintenanceType.Llantas:
                    RevisionLlantasKm = intervaloKm;
                    break;
                case MaintenanceType.FiltroAire:
                    RevisionFiltroAireKm = intervaloKm;
                    break;
                case MaintenanceType.Valvulas:
                    AjusteValvulasKm = intervaloKm;
                    break;
            }
        }
    }
}
