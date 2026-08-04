namespace MotoTrack.Domain.Interfaces
{
    public interface IEstadoMantenimientoStrategy
    {
        string DeterminarEstado(int kilometrajeActual, int kilometrajeProximo, int warningThresholdKm);
    }
}
