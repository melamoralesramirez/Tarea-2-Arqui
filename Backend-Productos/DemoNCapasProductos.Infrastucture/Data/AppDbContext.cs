using DemoNCapasProductos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNCapasProductos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .HasColumnName("id");

                entity.Property(p => p.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(255);

                entity.Property(p => p.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(p => p.Stock)
                    .HasColumnName("stock")
                    .IsRequired();

                entity.Property(p => p.Activo)
                    .HasColumnName("activo")
                    .IsRequired();

                entity.Property(p => p.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .IsRequired();
            });
        }
    }
}
