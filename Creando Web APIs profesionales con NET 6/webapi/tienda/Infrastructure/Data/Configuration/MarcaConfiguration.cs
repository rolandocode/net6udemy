using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;


namespace Infrastructure.Data.Configuration
{
    public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
    {
        public void Configure (EntityTypeBuilder<Marca> builder)
        {
                builder.ToTable ("Marca");

                builder.Property (p => p.Id )
                .IsRequired () ;

                builder.Property (p => p.Nombre)
                        .IsRequired ()
                        .HasMaxLength ( 100 ) ;

        }
    }
}