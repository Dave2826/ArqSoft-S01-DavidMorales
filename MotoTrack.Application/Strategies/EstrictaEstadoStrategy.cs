using MotoTrack.Domain.Interfaces;

namespace MotoTrack.Application.Strategies
{
    public class EstrictaEstadoStrategy : IEstadoMantenimientoStrategy
    {
        public string DeterminarEstado(int kilometrajeActual, int kilometrajeProximo, int warningThresholdKm)
        {
            var faltan = kilometrajeProximo - kilometrajeActual;
            if (faltan < 0) return "VENCIDO";
            if (faltan <= warningThresholdKm / 2) return "PRÓXIMO";
            return "AL DÍA";
        }
    }
}
