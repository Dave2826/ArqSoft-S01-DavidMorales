using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Configurations
{
    public class ConfiguracionMantenimientoConfiguration : IEntityTypeConfiguration<ConfiguracionMantenimiento>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionMantenimiento> builder)
        {
            builder.ToTable("ConfiguracionesMantenimiento");

            builder.HasKey(cm => cm.MotocicletaId);

            builder.Property(cm => cm.CambioAceiteKm)
                .IsRequired();

            builder.Property(cm => cm.RevisionCadenaKm)
                .IsRequired();

            builder.Property(cm => cm.RevisionBalatasKm)
                .IsRequired();

            builder.Property(cm => cm.RevisionLlantasKm)
                .IsRequired();

            builder.Property(cm => cm.RevisionFiltroAireKm)
                .IsRequired();

            builder.Property(cm => cm.AjusteValvulasKm)
                .IsRequired();

            builder.HasOne<Motocicleta>()
                .WithOne()
                .HasForeignKey<ConfiguracionMantenimiento>(cm => cm.MotocicletaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
