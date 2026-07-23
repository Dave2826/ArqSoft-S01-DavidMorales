using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.Services
{
    public class MantenimientoService
    {
        private readonly IMantenimientoRepository
            _mantenimientoRepository;

        public MantenimientoService(
            IMantenimientoRepository mantenimientoRepository)
        {
            _mantenimientoRepository = mantenimientoRepository;
        }

        public void Agregar(Mantenimiento mantenimiento)
        {
            _mantenimientoRepository
                .Agregar(mantenimiento);
        }

        public List<Mantenimiento> ObtenerPorMotocicleta(
            Guid motocicletaId)
        {
            return _mantenimientoRepository
                .ObtenerPorMotocicleta(motocicletaId);
        }

        public Mantenimiento? ObtenerPorId(Guid id)
        {
            return _mantenimientoRepository
                .ObtenerPorId(id);
        }
    }
}
