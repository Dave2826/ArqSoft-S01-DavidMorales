using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Configurations
{
    public class MotocicletaConfiguration : IEntityTypeConfiguration<Motocicleta>
    {
        public void Configure(EntityTypeBuilder<Motocicleta> builder)
        {
            builder.ToTable("Motocicletas");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Marca)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Modelo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Ano)
                .IsRequired();

            builder.Property(m => m.Cilindrada)
                .IsRequired();

            builder.Property(m => m.KilometrajeActual)
                .IsRequired();

            builder.Property(m => m.KilometrajeCompra)
                .IsRequired(false);

            builder.Property(m => m.FotoUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(m => m.Placas)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(m => m.VIN)
                .HasMaxLength(17)
                .IsRequired(false);

            builder.Property(m => m.NumeroMotor)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(m => m.FechaRegistro)
                .IsRequired();

            builder.Property(m => m.Activa)
                .IsRequired();

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(m => m.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(m => m.UsuarioId);
        }
    }
}
