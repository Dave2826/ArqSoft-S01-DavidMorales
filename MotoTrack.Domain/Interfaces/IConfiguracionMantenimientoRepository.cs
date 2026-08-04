using MotoTrack.Domain.Models;

namespace MotoTrack.Domain.Interfaces
{
    public interface IConfiguracionMantenimientoRepository
    {
        ConfiguracionMantenimiento?
            ObtenerPorMotocicleta(Guid motocicletaId);

        void Guardar(
            ConfiguracionMantenimiento configuracion);
    }
}
