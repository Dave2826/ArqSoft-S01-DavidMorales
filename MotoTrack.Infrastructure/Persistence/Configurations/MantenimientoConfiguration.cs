using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Configurations
{
    public class MantenimientoConfiguration : IEntityTypeConfiguration<Mantenimiento>
    {
        public void Configure(EntityTypeBuilder<Mantenimiento> builder)
        {
            builder.ToTable("Mantenimientos");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Fecha)
                .IsRequired();

            builder.Property(m => m.KilometrajeServicio)
                .IsRequired();

            builder.Property(m => m.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Costo)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(m => m.Descripcion)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(m => m.Taller)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.HasOne<Motocicleta>()
                .WithMany()
                .HasForeignKey(m => m.MotocicletaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => m.MotocicletaId);

            builder.HasIndex(m => m.Fecha);
        }
    }
}
