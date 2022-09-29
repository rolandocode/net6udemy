using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Configuration
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Producto");

            builder.HasIndex(e => e.CategoriaId, "IX_Producto_CategoriaId");

            builder.HasIndex(e => e.MarcaId, "IX_Producto_MarcaId");

            builder.Property(e => e.FechaCreacion).HasMaxLength(6);

            builder.Property(e => e.Nombre).HasMaxLength(100);

            builder.Property(e => e.Precio).HasPrecision(18, 2);

            builder.HasOne(d => d.Categoria)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId);

            builder.HasOne(d => d.Marca)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.MarcaId);
        }
    }
}