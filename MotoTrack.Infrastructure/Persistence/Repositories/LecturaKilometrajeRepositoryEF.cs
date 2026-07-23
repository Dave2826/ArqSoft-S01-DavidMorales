using Microsoft.EntityFrameworkCore;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Repositories
{
    public class LecturaKilometrajeRepositoryEF : ILecturaKilometrajeRepository
    {
        private readonly MotoTrackDbContext _context;

        public LecturaKilometrajeRepositoryEF(MotoTrackDbContext context)
        {
            _context = context;
        }

        public List<LecturaKilometraje> ObtenerTodas()
        {
            return _context.LecturasKilometraje.AsNoTracking().ToList();
        }

        public List<LecturaKilometraje> ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return _context.LecturasKilometraje.AsNoTracking()
                .Where(l => l.MotocicletaId == motocicletaId)
                .OrderBy(l => l.Fecha)
                .ToList();
        }

        public LecturaKilometraje? ObtenerUltimaLectura(Guid motocicletaId)
        {
            return _context.LecturasKilometraje.AsNoTracking()
                .Where(l => l.MotocicletaId == motocicletaId)
                .OrderByDescending(l => l.Fecha)
                .FirstOrDefault();
        }

        public void Agregar(LecturaKilometraje lectura)
        {
            _context.LecturasKilometraje.Add(lectura);
            _context.SaveChanges();
        }
    }
}
