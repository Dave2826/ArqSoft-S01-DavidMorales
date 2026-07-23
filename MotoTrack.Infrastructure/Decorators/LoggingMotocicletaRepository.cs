using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Decorators
{
    public class LoggingMotocicletaRepository : IMotocicletaRepository
    {
        private readonly IMotocicletaRepository _inner;

        public LoggingMotocicletaRepository(IMotocicletaRepository inner)
        {
            _inner = inner;
        }

        public List<Motocicleta> ObtenerTodas()
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.ObtenerTodas — inicio");
            var resultado = _inner.ObtenerTodas();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.ObtenerTodas — fin: {resultado.Count} motocicletas");
            return resultado;
        }

        public List<Motocicleta> ObtenerPorUsuario(Guid usuarioId)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.ObtenerPorUsuario(usuarioId={usuarioId}) — inicio");
            var resultado = _inner.ObtenerPorUsuario(usuarioId);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.ObtenerPorUsuario — fin: {resultado.Count} motocicletas");
            return resultado;
        }

        public Motocicleta? ObtenerPorId(Guid id)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.ObtenerPorId(id={id}) — inicio");
            var resultado = _inner.ObtenerPorId(id);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.ObtenerPorId — fin: {(resultado != null ? "encontrada" : "no encontrada")}");
            return resultado;
        }

        public void Agregar(Motocicleta motocicleta)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.Agregar(id={motocicleta.Id}, modelo={motocicleta.Modelo}) — inicio");
            _inner.Agregar(motocicleta);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.Agregar — fin");
        }

        public void Actualizar(Motocicleta motocicleta)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.Actualizar(id={motocicleta.Id}) — inicio");
            _inner.Actualizar(motocicleta);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.Actualizar — fin");
        }

        public void Eliminar(Guid id)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.Eliminar(id={id}) — inicio");
            _inner.Eliminar(id);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] IMotocicletaRepository.Eliminar — fin");
        }
    }
}
