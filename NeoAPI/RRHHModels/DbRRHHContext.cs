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

    public virtual DbSet<AusenciaV> AusenciaVs { get; set; }

    public virtual DbSet<PeriodosV> PeriodosVs { get; set; }

    public virtual DbSet<PermisosNomDiariaV> PermisosNomDiariaVs { get; set; }

    public virtual DbSet<RepososV> RepososVs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AZTDTDB03\\DBVEN01;Initial Catalog=SERVICIO_MEDICO;TrustServerCertificate=True;Persist Security Info=True;User ID=UsrConexion;Password=Sql*Db-2626**");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
