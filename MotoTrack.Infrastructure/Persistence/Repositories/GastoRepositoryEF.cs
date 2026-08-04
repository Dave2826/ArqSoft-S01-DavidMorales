using Microsoft.EntityFrameworkCore;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Repositories
{
    public class GastoRepositoryEF : IGastoRepository
    {
        private readonly MotoTrackDbContext _context;

        public GastoRepositoryEF(MotoTrackDbContext context)
        {
            _context = context;
        }

        public void Agregar(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
            _context.SaveChanges();
        }

        public List<Gasto> ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return _context.Gastos.AsNoTracking()
                .Where(g => g.MotocicletaId == motocicletaId)
                .OrderByDescending(g => g.Fecha)
                .ToList();
        }

        public Gasto? ObtenerPorId(Guid id)
        {
            return _context.Gastos.AsNoTracking()
                .FirstOrDefault(g => g.Id == id);
        }
    }
}
