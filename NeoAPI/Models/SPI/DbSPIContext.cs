using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.Models.SPI;

public partial class DbSPIContext : DbContext
{
    public DbSPIContext()
    {
    }

    public DbSPIContext(DbContextOptions<DbSPIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Compañia> Compañias { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<MaestroTrabajador> MaestroTrabajadors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SPI");

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CARGOS");

            entity.Property(e => e.Ciacgo)
                .HasMaxLength(255)
                .HasColumnName("CIACGO");
            entity.Property(e => e.Codcgo)
                .HasMaxLength(255)
                .HasColumnName("CODCGO");
            entity.Property(e => e.Descgo)
                .HasMaxLength(255)
                .HasColumnName("DESCGO");
            entity.Property(e => e.Fcrcgo).HasColumnName("FCRCGO");
            entity.Property(e => e.Fupcgo).HasColumnName("FUPCGO");
            entity.Property(e => e.Ucrcgo)
                .HasMaxLength(255)
                .HasColumnName("UCRCGO");
            entity.Property(e => e.Uupcgo)
                .HasMaxLength(255)
                .HasColumnName("UUPCGO");
        });

        modelBuilder.Entity<Compañia>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("COMPAÑIAS");

            entity.Property(e => e.Codcia)
                .HasMaxLength(255)
                .HasColumnName("CODCIA");
            entity.Property(e => e.Dirci1)
                .HasMaxLength(255)
                .HasColumnName("DIRCI1");
            entity.Property(e => e.Dirci2)
                .HasMaxLength(255)
                .HasColumnName("DIRCI2");
            entity.Property(e => e.Dirci3)
                .HasMaxLength(255)
                .HasColumnName("DIRCI3");
            entity.Property(e => e.Fcrcia).HasColumnName("FCRCIA");
            entity.Property(e => e.Fupcia).HasColumnName("FUPCIA");
            entity.Property(e => e.Nomci1)
                .HasMaxLength(255)
                .HasColumnName("NOMCI1");
            entity.Property(e => e.Nomci2)
                .HasMaxLength(255)
                .HasColumnName("NOMCI2");
            entity.Property(e => e.Rifci1)
                .HasMaxLength(255)
                .HasColumnName("RIFCI1");
            entity.Property(e => e.Ucrcia)
                .HasMaxLength(255)
                .HasColumnName("UCRCIA");
            entity.Property(e => e.Uupcia)
                .HasMaxLength(255)
                .HasColumnName("UUPCIA");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DEPARTAMENTOS");

            entity.Property(e => e.Ciadpt)
                .HasMaxLength(255)
                .HasColumnName("CIADPT");
            entity.Property(e => e.Coddpt)
                .HasMaxLength(255)
                .HasColumnName("CODDPT");
            entity.Property(e => e.Desdpt)
                .HasMaxLength(255)
                .HasColumnName("DESDPT");
            entity.Property(e => e.Fcrdpt).HasColumnName("FCRDPT");
            entity.Property(e => e.Fupdpt).HasColumnName("FUPDPT");
            entity.Property(e => e.Ucrdpt)
                .HasMaxLength(255)
                .HasColumnName("UCRDPT");
            entity.Property(e => e.Uupdpt)
                .HasMaxLength(255)
                .HasColumnName("UUPDPT");
        });

        modelBuilder.Entity<MaestroTrabajador>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MAESTRO_TRABAJADOR");

            entity.Property(e => e.Apefi1)
                .HasMaxLength(255)
                .HasColumnName("APEFI1");
            entity.Property(e => e.Apefi2)
                .HasMaxLength(255)
                .HasColumnName("APEFI2");
            entity.Property(e => e.Cdanfi)
                .HasMaxLength(255)
                .HasColumnName("CDANFI");
            entity.Property(e => e.Cedfic)
                .HasMaxLength(255)
                .HasColumnName("CEDFIC");
            entity.Property(e => e.Cgofic)
                .HasMaxLength(255)
                .HasColumnName("CGOFIC");
            entity.Property(e => e.Ciafic)
                .HasMaxLength(255)
                .HasColumnName("CIAFIC");
            entity.Property(e => e.Codfic)
                .HasMaxLength(255)
                .HasColumnName("CODFIC");
            entity.Property(e => e.Dptfic)
                .HasMaxLength(255)
                .HasColumnName("DPTFIC");
            entity.Property(e => e.Emlfic)
                .HasMaxLength(255)
                .HasColumnName("EMLFIC");
            entity.Property(e => e.Fcrfic).HasColumnName("FCRFIC");
            entity.Property(e => e.Fecing).HasColumnName("FECING");
            entity.Property(e => e.Fecnac).HasColumnName("FECNAC");
            entity.Property(e => e.Fecret).HasColumnName("FECRET");
            entity.Property(e => e.Ficjef)
                .HasMaxLength(255)
                .HasColumnName("FICJEF");
            entity.Property(e => e.Fupfic).HasColumnName("FUPFIC");
            entity.Property(e => e.Lcdfic)
                .HasMaxLength(4)
                .HasColumnName("LCDFIC");
            entity.Property(e => e.Nacfic).HasColumnName("NACFIC");
            entity.Property(e => e.Nomfi1)
                .HasMaxLength(255)
                .HasColumnName("NOMFI1");
            entity.Property(e => e.Nomfi2)
                .HasMaxLength(255)
                .HasColumnName("NOMFI2");
            entity.Property(e => e.Sexfic)
                .HasMaxLength(255)
                .HasColumnName("SEXFIC");
            entity.Property(e => e.Tlffic)
                .HasMaxLength(255)
                .HasColumnName("TLFFIC");
            entity.Property(e => e.Tpnfic)
                .HasMaxLength(255)
                .HasColumnName("TPNFIC");
            entity.Property(e => e.Ucrfic)
                .HasMaxLength(255)
                .HasColumnName("UCRFIC");
            entity.Property(e => e.Uupfic)
                .HasMaxLength(255)
                .HasColumnName("UUPFIC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
