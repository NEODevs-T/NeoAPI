using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.Models.RRHHModels;

public partial class DbRegistNominaContext : DbContext
{
    public DbRegistNominaContext(DbContextOptions<DbRegistNominaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RegistNomina> RegistNominas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.Entity<RegistNomina>(entity =>
    {
        entity
            .HasNoKey()
            .ToTable("RegistNomina", tb => tb.HasComment("Base de dtaos para el registro de las faltas y permisos en nomina de la pagina RRHH"));

        entity.Property(e => e.Añohnh).HasColumnName("AÑOHNH");

        entity.Property(e => e.Ciahnh)
            .HasMaxLength(2)
            .IsUnicode(false)
            .HasColumnName("CIAHNH");

        entity.Property(e => e.Dg010hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG010HH");

        entity.Property(e => e.Dg011hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG011HH");

        entity.Property(e => e.Dg012hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG012HH");

        entity.Property(e => e.Dg013hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG013HH");

        entity.Property(e => e.Dg014hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG014HH");

        entity.Property(e => e.Dg015hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG015HH");

        entity.Property(e => e.Dg01hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG01HH");

        entity.Property(e => e.Dg02hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG02HH");

        entity.Property(e => e.Dg03hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG03HH");

        entity.Property(e => e.Dg04hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG04HH");

        entity.Property(e => e.Dg05hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG05HH");

        entity.Property(e => e.Dg06hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG06HH");

        entity.Property(e => e.Dg07hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG07HH");

        entity.Property(e => e.Dg08hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG08HH");

        entity.Property(e => e.Dg09hh)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("DG09HH");

        entity.Property(e => e.Dpthnh)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("DPTHNH");

        entity.Property(e => e.Fecregt)
            .HasColumnType("datetime")
            .HasColumnName("FECREGT");

        entity.Property(e => e.Fichnh)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("FICHNH");

        entity.Property(e => e.Prdhnh)
            .HasColumnName("PRDHNH");

        entity.Property(e => e.Tpnhnh)
            .HasMaxLength(4)
            .IsUnicode(false)
            .HasColumnName("TPNHNH");

        entity.Property(e => e.Tpnom)
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasColumnName("TPNOM");

        entity.Property(e => e.Stareg)
            .HasColumnName("STAREG");
    });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
