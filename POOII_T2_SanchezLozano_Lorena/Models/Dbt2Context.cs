using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POOII_T2_SanchezLozano_Lorena.Models;

public partial class Dbt2Context : DbContext
{
    public Dbt2Context()
    {
    }

    public Dbt2Context(DbContextOptions<Dbt2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categorias { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__A3C02A106D9FE6D2");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacionCat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCate)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__098892106A108FF0");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.DescripcionPro)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacionPro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombrePro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.oCategoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__PRODUCTO__IdCate__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
