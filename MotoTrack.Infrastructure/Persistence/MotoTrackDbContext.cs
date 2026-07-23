using Microsoft.EntityFrameworkCore;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence
{
    public class MotoTrackDbContext : DbContext
    {
        public MotoTrackDbContext(DbContextOptions<MotoTrackDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Motocicleta> Motocicletas => Set<Motocicleta>();
        public DbSet<Mantenimiento> Mantenimientos => Set<Mantenimiento>();
        public DbSet<LecturaKilometraje> LecturasKilometraje => Set<LecturaKilometraje>();
        public DbSet<Gasto> Gastos => Set<Gasto>();
        public DbSet<ConfiguracionMantenimiento> ConfiguracionesMantenimiento => Set<ConfiguracionMantenimiento>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MotoTrackDbContext).Assembly);
        }
    }
}
