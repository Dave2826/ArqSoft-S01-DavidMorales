using Microsoft.EntityFrameworkCore;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Repositories
{
    public class MantenimientoRepositoryEF : IMantenimientoRepository
    {
        private readonly MotoTrackDbContext _context;

        public MantenimientoRepositoryEF(MotoTrackDbContext context)
        {
            _context = context;
        }

        public void Agregar(Mantenimiento mantenimiento)
        {
            _context.Mantenimientos.Add(mantenimiento);
            _context.SaveChanges();
        }

        public List<Mantenimiento> ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return _context.Mantenimientos.AsNoTracking()
                .Where(m => m.MotocicletaId == motocicletaId)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        public Mantenimiento? ObtenerPorId(Guid id)
        {
            return _context.Mantenimientos.AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
        }
    }
}
