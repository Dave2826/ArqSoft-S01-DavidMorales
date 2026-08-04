using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.Services
{
    public class GastoService
    {
        private readonly IGastoRepository _gastoRepository;

        public GastoService(IGastoRepository gastoRepository)
        {
            _gastoRepository = gastoRepository;
        }

        public void Agregar(Gasto gasto)
        {
            _gastoRepository.Agregar(gasto);
        }

        public List<Gasto> ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return _gastoRepository
                .ObtenerPorMotocicleta(motocicletaId);
        }

        public Gasto? ObtenerPorId(Guid id)
        {
            return _gastoRepository
                .ObtenerPorId(id);
        }
    }
}