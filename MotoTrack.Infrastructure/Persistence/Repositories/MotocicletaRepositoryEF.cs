using Microsoft.EntityFrameworkCore;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Repositories
{
    public class MotocicletaRepositoryEF : IMotocicletaRepository
    {
        private readonly MotoTrackDbContext _context;

        public MotocicletaRepositoryEF(MotoTrackDbContext context)
        {
            _context = context;
        }

        public List<Motocicleta> ObtenerTodas()
        {
            return _context.Motocicletas.AsNoTracking().ToList();
        }

        public List<Motocicleta> ObtenerPorUsuario(Guid usuarioId)
        {
            return _context.Motocicletas.AsNoTracking()
                .Where(m => m.UsuarioId == usuarioId)
                .ToList();
        }

        public Motocicleta? ObtenerPorId(Guid id)
        {
            return _context.Motocicletas.AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
        }

        public void Agregar(Motocicleta motocicleta)
        {
            _context.Motocicletas.Add(motocicleta);
            _context.SaveChanges();
        }

        public void Actualizar(Motocicleta motocicleta)
        {
            _context.Motocicletas.Update(motocicleta);
            _context.SaveChanges();
        }

        public void Eliminar(Guid id)
        {
            var motocicleta = _context.Motocicletas.Find(id);

            if (motocicleta != null)
            {
                _context.Motocicletas.Remove(motocicleta);
                _context.SaveChanges();
            }
        }
    }
}
