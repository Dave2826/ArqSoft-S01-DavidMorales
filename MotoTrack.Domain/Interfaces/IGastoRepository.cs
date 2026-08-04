using MotoTrack.Domain.Models;

namespace MotoTrack.Domain.Interfaces
{
    public interface IGastoRepository
    {
        void Agregar(Gasto gasto);

        List<Gasto> ObtenerPorMotocicleta(Guid motocicletaId);

        Gasto? ObtenerPorId(Guid id);
    }
}