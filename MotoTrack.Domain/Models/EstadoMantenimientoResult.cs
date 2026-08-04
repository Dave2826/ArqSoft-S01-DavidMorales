namespace MotoTrack.Domain.Models
{
    public class EstadoMantenimientoResult
    {
        public enum EstadoPrioridad
        {
            SinRegistro = 0,
            AlDia = 1,
            Proximo = 2,
            Vencido = 3
        }

        public string EstadoAceite { get; set; } = "Sin registro";
        public string EstadoCadena { get; set; } = "Sin registro";
        public string EstadoBalatas { get; set; } = "Sin registro";
        public string EstadoLlantas { get; set; } = "Sin registro";
        public string EstadoFiltroAire { get; set; } = "Sin registro";
        public string EstadoBujias { get; set; } = "Sin registro";
        public string EstadoValvulas { get; set; } = "Sin registro";
        public string EstadoBateria { get; set; } = "Sin registro";
        public string EstadoSuspension { get; set; } = "Sin registro";
        public string EstadoLiquidoFrenos { get; set; } = "Sin registro";
        public string EstadoAnticongelante { get; set; } = "Sin registro";

        public string UltimoAceite { get; set; } = "Sin registro";
        public string UltimaCadena { get; set; } = "Sin registro";
        public string UltimasBalatas { get; set; } = "Sin registro";
        public string UltimasLlantas { get; set; } = "Sin registro";
        public string UltimoFiltroAire { get; set; } = "Sin registro";
        public string UltimasBujias { get; set; } = "Sin registro";
        public string UltimasValvulas { get; set; } = "Sin registro";
        public string UltimaBateria { get; set; } = "Sin registro";
        public string UltimaSuspension { get; set; } = "Sin registro";
        public string UltimoLiquidoFrenos { get; set; } = "Sin registro";
        public string UltimoAnticongelante { get; set; } = "Sin registro";

        public string ProximoAceite { get; set; } = "Sin registro";
        public string ProximaCadena { get; set; } = "Sin registro";
        public string ProximasBalatas { get; set; } = "Sin registro";
        public string ProximasLlantas { get; set; } = "Sin registro";
        public string ProximoFiltroAire { get; set; } = "Sin registro";
        public string ProximasBujias { get; set; } = "Sin registro";
        public string ProximasValvulas { get; set; } = "Sin registro";
        public string ProximaBateria { get; set; } = "Sin registro";
        public string ProximaSuspension { get; set; } = "Sin registro";
        public string ProximoLiquidoFrenos { get; set; } = "Sin registro";
        public string ProximoAnticongelante { get; set; } = "Sin registro";

        public bool AceiteEsEstimado { get; set; }
        public bool CadenaEsEstimado { get; set; }
        public bool BalatasEsEstimado { get; set; }
        public bool LlantasEsEstimado { get; set; }
        public bool FiltroAireEsEstimado { get; set; }
        public bool BujiasEsEstimado { get; set; }
        public bool ValvulasEsEstimado { get; set; }
        public bool BateriaEsEstimado { get; set; }
        public bool SuspensionEsEstimado { get; set; }
        public bool LiquidoFrenosEsEstimado { get; set; }
        public bool AnticongelanteEsEstimado { get; set; }
        public bool TieneEstimados { get; set; }

        public string Resumen { get; set; } = "Sin registro";
        public int TotalVencidos { get; set; }
        public int TotalProximos { get; set; }
        public EstadoPrioridad PrioridadGeneral { get; set; }
    }
}
