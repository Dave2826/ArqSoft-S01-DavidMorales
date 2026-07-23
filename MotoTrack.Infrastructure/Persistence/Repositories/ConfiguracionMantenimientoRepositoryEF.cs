using Microsoft.EntityFrameworkCore;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Repositories
{
    public class ConfiguracionMantenimientoRepositoryEF : IConfiguracionMantenimientoRepository
    {
        private readonly MotoTrackDbContext _context;

        public ConfiguracionMantenimientoRepositoryEF(MotoTrackDbContext context)
        {
            _context = context;
        }

        public ConfiguracionMantenimiento? ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return _context.ConfiguracionesMantenimiento.AsNoTracking()
                .FirstOrDefault(c => c.MotocicletaId == motocicletaId);
        }

        public void Guardar(ConfiguracionMantenimiento configuracion)
        {
            var existente = _context.ConfiguracionesMantenimiento
                .FirstOrDefault(c => c.MotocicletaId == configuracion.MotocicletaId);

            if (existente != null)
            {
                _context.Entry(existente).CurrentValues.SetValues(configuracion);
            }
            else
            {
                _context.ConfiguracionesMantenimiento.Add(configuracion);
            }

            _context.SaveChanges();
        }
    }
}
