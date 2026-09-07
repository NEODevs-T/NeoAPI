using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.RRHHModels;

public partial class DbRRHHContext : DbContext
{
    public DbRRHHContext()
    {
    }

    public DbRRHHContext(DbContextOptions<DbRRHHContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PermisosNomDiariaHistV> PermisosNomDiariaHistVs { get; set; }

    public virtual DbSet<VRotacion> VRotacions { get; set; }

    public virtual DbSet<VRotacionHist> VRotacionHists { get; set; }

    public virtual DbSet<AusenciaV> AusenciaVs { get; set; }

    public virtual DbSet<PeriodosV> PeriodosVs { get; set; }

    public virtual DbSet<RepososV> RepososVs { get; set; }

    public virtual DbSet<VAusencia> VAusencias { get; set; }

    public virtual DbSet<VHistoricoCarg> VHistoricoCargs { get; set; }

    public virtual DbSet<VHistCargProcesada> VHistCargProcesadas { get; set; }

    public virtual DbSet<PermisosNomDiariaV> PermisosNomDiariaVs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AusenciaV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Ausencia_v", "SPI");
            entity.Property(e => e.Añodnh)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("AÑODNH");
            entity.Property(e => e.Candnh)
                .HasColumnType("decimal(7, 3)")
                .HasColumnName("CANDNH");
            entity.Property(e => e.Ciadnh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIADNH");
            entity.Property(e => e.Ctodnh)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("CTODNH");
            entity.Property(e => e.Dptdnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DPTDNH");
            entity.Property(e => e.Fecmdh)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("FECMDH");
            entity.Property(e => e.Ficdnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FICDNH");
            entity.Property(e => e.Mesdnh)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("MESDNH");
            entity.Property(e => e.Prddnh)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PRDDNH");
            entity.Property(e => e.Tpndnh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNDNH");
        });

        modelBuilder.Entity<PeriodosV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Periodos_v", "SPI");

            entity.Property(e => e.Añofpr)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("AÑOFPR");
            entity.Property(e => e.Ciafpr)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIAFPR");
            entity.Property(e => e.Fecffp)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("FECFFP");
            entity.Property(e => e.Fecifp)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("FECIFP");
            entity.Property(e => e.Mesfpr)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("MESFPR");
            entity.Property(e => e.Prdfpr)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PRDFPR");
            entity.Property(e => e.Tpnfpr)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNFPR");
        });

        modelBuilder.Entity<RepososV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Reposos_v");

            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.CanDiasReposo).HasColumnName("CAN_DIAS_REPOSO");
            entity.Property(e => e.Compania)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("COMPANIA");
            entity.Property(e => e.Departamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DEPARTAMENTO");
            entity.Property(e => e.DescripcionCausa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_CAUSA");
            entity.Property(e => e.FechaDesde).HasColumnName("FECHA_DESDE");
            entity.Property(e => e.FechaHasta).HasColumnName("FECHA_HASTA");
            entity.Property(e => e.Ficha)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FICHA");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.OrigenReposo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ORIGEN_REPOSO");
            entity.Property(e => e.Reintegro).HasColumnName("REINTEGRO");
            entity.Property(e => e.TipoNomina)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_NOMINA");
        });

        modelBuilder.Entity<VAusencia>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_Ausencias", "SPI");

            entity.Property(e => e.Candnh)
                .HasColumnType("decimal(7, 3)")
                .HasColumnName("CANDNH");

            entity.Property(e => e.Ctoddh)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("CTODDH");

            entity.Property(e => e.Ciahnh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIAHNH");

            entity.Property(e => e.Tpnhnh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNHNH");

            entity.Property(e => e.Fichnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FICHNH");

            entity.Property(e => e.Gpohnh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("GPOHNH");

            entity.Property(e => e.Cgphnh)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("CGPHNH");

            entity.Property(e => e.Ctodnh)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("CTODNH");

            entity.Property(e => e.Prddnh)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PRDDNH");

            entity.Property(e => e.Dptdnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DPTDNH");
        });

        modelBuilder.Entity<VHistoricoCarg>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_HistoricoCarg", "SPI");

            entity.Property(e => e.Apefi1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("APEFI1");

            entity.Property(e => e.Cgohcg)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("CGOHCG");

            entity.Property(e => e.Ciahcg)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIAHCG");

            entity.Property(e => e.Codcgo)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("CODCGO");

            entity.Property(e => e.Descgo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("DESCGO");

            entity.Property(e => e.Desdpt)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("DESDPT");

            entity.Property(e => e.Dptfic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DPTFIC");

            entity.Property(e => e.Fecchc)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("FECCHC");

            entity.Property(e => e.Fichcg)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FICHCG");

            entity.Property(e => e.Nomfi1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("NOMFI1");

            entity.Property(e => e.Tpnfic)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNFIC");
        });

            modelBuilder.Entity<PermisosNomDiariaHistV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Permisos_NomDiariaHist_v", "SPI");

            entity.Property(e => e.Añohnh)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("AÑOHNH");

            entity.Property(e => e.Ciahnh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIAHNH");

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

            entity.Property(e => e.Dpthnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DPTHNH");

            entity.Property(e => e.Fichnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FICHNH");

            entity.Property(e => e.Prdhnh)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PRDHNH");

            entity.Property(e => e.Tpnhnh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNHNH");
        });

            modelBuilder.Entity<VRotacion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_Rotacion", "SPI");

            entity.Property(e => e.Añohnh)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("AÑOHNH");

            entity.Property(e => e.Ciahnh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIAHNH");

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

            entity.Property(e => e.Dg10hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG10HH");

            entity.Property(e => e.Dg11hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG11HH");

            entity.Property(e => e.Dg12hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG12HH");

            entity.Property(e => e.Dg13hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG13HH");

            entity.Property(e => e.Dg14hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG14HH");

            entity.Property(e => e.Dg15hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG15HH");

            entity.Property(e => e.Dpthnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DPTHNH");

            entity.Property(e => e.Fichnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FICHNH");

            entity.Property(e => e.Prdhnh)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PRDHNH");

            entity.Property(e => e.Tpnhnh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNHNH");
        });

            modelBuilder.Entity<VRotacionHist>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_RotacionHist", "SPI");

            entity.Property(e => e.Añohnh)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("AÑOHNH");

            entity.Property(e => e.Ciahnh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIAHNH");

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

            entity.Property(e => e.Dg10hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG10HH");

            entity.Property(e => e.Dg11hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG11HH");

            entity.Property(e => e.Dg12hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG12HH");

            entity.Property(e => e.Dg13hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG13HH");

            entity.Property(e => e.Dg14hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG14HH");

            entity.Property(e => e.Dg15hh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DG15HH");

            entity.Property(e => e.Dpthnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DPTHNH");

            entity.Property(e => e.Fichnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FICHNH");

            entity.Property(e => e.Prdhnh)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PRDHNH");

            entity.Property(e => e.Tpnhnh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNHNH");
        });

                modelBuilder.Entity<VHistCargProcesada>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_HistCargProcesadas", "SPI");

            entity.Property(e => e.Cedfic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CEDFIC");

            entity.Property(e => e.Ciafic)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIAFIC");

            entity.Property(e => e.Codfic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CODFIC");

            entity.Property(e => e.Dgadlf)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DGADLF");

            entity.Property(e => e.Diadlf)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DIADLF");

            entity.Property(e => e.Diidlf)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DIIDLF");

            entity.Property(e => e.Dimdlf)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DIMDLF");

            entity.Property(e => e.Divdlf)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DIVDLF");

            entity.Property(e => e.Dmedlf)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DMEDLF");

            entity.Property(e => e.Nacfic)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("NACFIC");

            entity.Property(e => e.Otddlf)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("OTDDLF");

            entity.Property(e => e.Tpnfic)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNFIC");
        });

        modelBuilder.Entity<PermisosNomDiariaV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Permisos_NomDiaria_v", "SPI");

            entity.Property(e => e.Añohnh)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("AÑOHNH");
            entity.Property(e => e.Ciahnh)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CIAHNH");
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
            entity.Property(e => e.Dpthnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DPTHNH");
            entity.Property(e => e.Fichnh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FICHNH");
            entity.Property(e => e.Prdhnh)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PRDHNH");
            entity.Property(e => e.Tpnhnh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("TPNHNH");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
