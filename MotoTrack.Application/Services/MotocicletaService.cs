using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.Services
{
    public class MotocicletaService
    {
        private readonly IMotocicletaRepository _motocicletaRepository;

        public MotocicletaService(
            IMotocicletaRepository motocicletaRepository)
        {
            _motocicletaRepository = motocicletaRepository;
        }

        public List<Motocicleta> ObtenerTodas()
        {
            return _motocicletaRepository.ObtenerTodas();
        }

        public List<Motocicleta> ObtenerPorUsuario(Guid usuarioId)
        {
            return _motocicletaRepository
                .ObtenerPorUsuario(usuarioId);
        }

        public Motocicleta? ObtenerPorId(Guid id)
        {
            return _motocicletaRepository
                .ObtenerPorId(id);
        }

        public void Agregar(Motocicleta motocicleta)
        {
            _motocicletaRepository
                .Agregar(motocicleta);
        }

        public void Actualizar(
    Motocicleta motocicleta)
        {
            _motocicletaRepository
                .Actualizar(motocicleta);
        }
        
        public void Eliminar(Guid id)
        {
            _motocicletaRepository.Eliminar(id);
        }
    }
}
