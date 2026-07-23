using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Configurations
{
    public class GastoConfiguration : IEntityTypeConfiguration<Gasto>
    {
        public void Configure(EntityTypeBuilder<Gasto> builder)
        {
            builder.ToTable("Gastos");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Fecha)
                .IsRequired();

            builder.Property(g => g.Monto)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(g => g.Categoria)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(g => g.Descripcion)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne<Motocicleta>()
                .WithMany()
                .HasForeignKey(g => g.MotocicletaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(g => g.MotocicletaId);
        }
    }
}
