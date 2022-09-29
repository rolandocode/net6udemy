using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using Core.Entities;

namespace Infrastructure.Data
{
    public partial class TiendaContext : DbContext
    {
        public TiendaContext()
        {
        }

        public TiendaContext(DbContextOptions<TiendaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseMySql("server=localhost;user=root;password=admin;database=tiendaBd", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
        //     }
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            // modelBuilder.Entity<EfmigrationsHistory>(entity =>
            // {
            //     entity.HasKey(e => e.MigrationId)
            //         .HasName("PRIMARY");

            //     entity.ToTable("__EFMigrationsHistory");

            //     entity.Property(e => e.MigrationId).HasMaxLength(150);

            //     entity.Property(e => e.ProductVersion).HasMaxLength(32);
            // });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());

        }

    }
}
