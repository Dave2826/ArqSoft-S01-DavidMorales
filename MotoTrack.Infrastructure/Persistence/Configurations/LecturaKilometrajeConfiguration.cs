using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Configurations
{
    public class LecturaKilometrajeConfiguration : IEntityTypeConfiguration<LecturaKilometraje>
    {
        public void Configure(EntityTypeBuilder<LecturaKilometraje> builder)
        {
            builder.ToTable("LecturasKilometraje");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Kilometraje)
                .IsRequired();

            builder.Property(l => l.Fecha)
                .IsRequired();

            builder.Property(l => l.Observaciones)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne<Motocicleta>()
                .WithMany()
                .HasForeignKey(l => l.MotocicletaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(l => l.MotocicletaId);

            builder.HasIndex(l => new { l.MotocicletaId, l.Fecha });
        }
    }
}
