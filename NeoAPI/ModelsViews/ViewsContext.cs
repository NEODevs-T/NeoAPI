using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.ModelsViews;

public partial class ViewsContext : DbContext
{
    public ViewsContext()
    {
    }

    public ViewsContext(DbContextOptions<ViewsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AsentamientosFueraRangoV> AsentamientosFueraRangoVs { get; set; }

    public virtual DbSet<CentrosV> CentrosVs { get; set; }

    public virtual DbSet<DivisionesV> DivisionesVs { get; set; }

    public virtual DbSet<EmpresasV> EmpresasVs { get; set; }

    public virtual DbSet<LineaV> LineaVs { get; set; }

    public virtual DbSet<MaestraV> MaestraVs { get; set; }

    public virtual DbSet<ProductosV> ProductosVs { get; set; }

    public virtual DbSet<RangoDeControlActivosV> RangoDeControlActivosVs { get; set; }

    public virtual DbSet<SeccionesV> SeccionesVs { get; set; }

    public virtual DbSet<ValoresDeAsentamientosV> ValoresDeAsentamientosVs { get; set; }

    public virtual DbSet<VarClasificacionV> VarClasificacionVs { get; set; }

    public virtual DbSet<VarTipoV> VarTipoVs { get; set; }

    public virtual DbSet<VariablesAsentamientosV> VariablesAsentamientosVs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.20.1.60\\DESARROLLO;Initial Catalog=DbNeoII;TrustServerCertificate=True;Persist Security Info=True;User ID=UsrEncNeo;Password=L3C7U3A2023*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AsentamientosFueraRangoV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AsentamientosFueraRango_V");

            entity.Property(e => e.Activo).HasColumnName("ACTIVO");
            entity.Property(e => e.Centro)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CENTRO");
            entity.Property(e => e.Division)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DIVISION");
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMPRESA");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.Ficha)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("FICHA");
            entity.Property(e => e.FichaCorte)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("FICHA CORTE");
            entity.Property(e => e.Grupo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("GRUPO");
            entity.Property(e => e.Linea)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LINEA");
            entity.Property(e => e.Max).HasColumnName("MAX");
            entity.Property(e => e.Min).HasColumnName("MIN");
            entity.Property(e => e.Onservacion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ONSERVACION");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAIS");
            entity.Property(e => e.Turno)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TURNO");
            entity.Property(e => e.Unidad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UNIDAD");
            entity.Property(e => e.Valor).HasColumnName("VALOR");
            entity.Property(e => e.Variable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VARIABLE");
        });

        modelBuilder.Entity<CentrosV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Centros_V");

            entity.Property(e => e.Centro)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DivisionesV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Divisiones_V");

            entity.Property(e => e.Ndivision)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("NDivision");
        });

        modelBuilder.Entity<EmpresasV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Empresas_V");

            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LineaV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Linea_V");

            entity.Property(e => e.Linea)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MaestraV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Maestra_V");

            entity.Property(e => e.Centro)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.División)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Linea)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductosV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Productos_V");

            entity.Property(e => e.Codigo)
                .HasMaxLength(9)
                .IsUnicode(false);
            entity.Property(e => e.Producto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoDeProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tipo de Producto");
        });

        modelBuilder.Entity<RangoDeControlActivosV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RangoDeControlActivos_V");

            entity.Property(e => e.Centro)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Clasificación)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.División)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EquipoDeMedición)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Equipo de medición");
            entity.Property(e => e.FechaDeCreacionDelRango)
                .HasColumnType("datetime")
                .HasColumnName("Fecha de Creacion del Rango");
            entity.Property(e => e.IsObservable).HasColumnName("Is observable");
            entity.Property(e => e.LimiteMaximo).HasColumnName("Limite maximo");
            entity.Property(e => e.LimiteMinimo).HasColumnName("Limite minimo");
            entity.Property(e => e.Linea)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NombreDeLaVariable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre de la variable");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Producto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sección)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoDeProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tipo de Producto");
            entity.Property(e => e.Unidad)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SeccionesV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Secciones_V");

            entity.Property(e => e.Seccion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ValoresDeAsentamientosV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ValoresDeAsentamientos_V");

            entity.Property(e => e.Centro)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Clasificación)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.División)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EquipoDeMedición)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Equipo de medición");
            entity.Property(e => e.FechaDeCreacionDelRango)
                .HasColumnType("datetime")
                .HasColumnName("Fecha de Creacion del Rango");
            entity.Property(e => e.IsObservable).HasColumnName("Is observable");
            entity.Property(e => e.LimiteMaximo).HasColumnName("Limite maximo");
            entity.Property(e => e.LimiteMinimo).HasColumnName("Limite minimo");
            entity.Property(e => e.Linea)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.NombreDeLaVariable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre de la variable");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Producto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sección)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoDeProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tipo de Producto");
            entity.Property(e => e.Unidad)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VarClasificacionV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VarClasificacion_V");

            entity.Property(e => e.Clasificación)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VarTipoV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VarTipo_V");

            entity.Property(e => e.TipoDeVariable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tipo de Variable");
        });

        modelBuilder.Entity<VariablesAsentamientosV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VariablesAsentamientos_V");

            entity.Property(e => e.Clasificación)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionDeLaVariable)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("Descripcion de la variable");
            entity.Property(e => e.EquipoDeMedición)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Equipo de medición");
            entity.Property(e => e.FechaDeCreación).HasColumnName("Fecha de creación");
            entity.Property(e => e.IsObservable).HasColumnName("Is observable");
            entity.Property(e => e.NombreDeLaVariable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre de la variable");
            entity.Property(e => e.Sección)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Unidad)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
