using MotoTrack.Domain.Models;

namespace MotoTrack.Domain.Interfaces
{
    public interface IMotocicletaRepository
    {
        List<Motocicleta> ObtenerTodas();

        List<Motocicleta> ObtenerPorUsuario(Guid usuarioId);

        Motocicleta? ObtenerPorId(Guid id);

        void Agregar(Motocicleta motocicleta);

        void Actualizar(Motocicleta motocicleta);
        
        void Eliminar(Guid id);
    }
}
