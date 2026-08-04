using MotoTrack.Domain.Models;

namespace MotoTrack.Domain.Interfaces
{
    public interface IMantenimientoRepository
    {
        void Agregar(Mantenimiento mantenimiento);

        List<Mantenimiento> ObtenerPorMotocicleta(
            Guid motocicletaId);

        Mantenimiento? ObtenerPorId(Guid id);
    }
}
