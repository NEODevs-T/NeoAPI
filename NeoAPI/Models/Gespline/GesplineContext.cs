using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.Models.Gespline;

public partial class GesplineContext : DbContext
{
    public GesplineContext()
    {
    }

    public GesplineContext(DbContextOptions<GesplineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Centro> Centros { get; set; }

    public virtual DbSet<Entradaejecucion> Entradaejecucions { get; set; }

    public virtual DbSet<OrdenesDeProduccion> OrdenesDeProduccions { get; set; }

    public virtual DbSet<Ordenproduccionxproducto> Ordenproduccionxproductos { get; set; }

    public virtual DbSet<Parada> Paradas { get; set; }

    public virtual DbSet<Paradasejecutada> Paradasejecutadas { get; set; }

    public virtual DbSet<Parte> Partes { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Proceso> Procesos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Productoporproceso> Productoporprocesos { get; set; }

    public virtual DbSet<Puestosdetrabajo> Puestosdetrabajos { get; set; }

    public virtual DbSet<Puestotrabajosegunproducto> Puestotrabajosegunproductos { get; set; }

    public virtual DbSet<Standarparada> Standarparadas { get; set; }

    public virtual DbSet<Tipocargospersonal> Tipocargospersonals { get; set; }

    public virtual DbSet<Tiposdeturno> Tiposdeturnos { get; set; }

    public virtual DbSet<Tiposdeunidadesmateriale> Tiposdeunidadesmateriales { get; set; }

    public virtual DbSet<Transmicionweb> Transmicionwebs { get; set; }

    public virtual DbSet<Tuplaejecucion> Tuplaejecucions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.20.1.60\\DBVEN01;Initial Catalog=SIPDATABASE;TrustServerCertificate=True;Persist Security Info=True;User ID=portaluser;Password=PORT34erySADF");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea);

            entity.ToTable("Area");

            entity.Property(e => e.AcodGes)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("ACodGes");
            entity.Property(e => e.Aparte)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("AParte");
            entity.Property(e => e.AsubParte)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ASubParte");
        });

        modelBuilder.Entity<Centro>(entity =>
        {
            entity.HasKey(e => e.Codigocentro);

            entity.ToTable("CENTROS", tb => tb.HasTrigger("TR_CENTROS"));

            entity.Property(e => e.Codigocentro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOCENTRO");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
        });

        modelBuilder.Entity<Entradaejecucion>(entity =>
        {
            entity.HasKey(e => e.Codigoentradaejecucion);

            entity.ToTable("ENTRADAEJECUCION");

            entity.HasIndex(e => e.Codigotupla, "INDEX_ENTRADAEJECUCION_CODIGOTUPLA").HasFillFactor(90);

            entity.HasIndex(e => e.Fechaentrada, "INDEX_FECHA");

            entity.Property(e => e.Codigoentradaejecucion).HasColumnName("CODIGOENTRADAEJECUCION");
            entity.Property(e => e.Avancex100enentrada).HasColumnName("AVANCEX100ENENTRADA");
            entity.Property(e => e.Cantidadesechasenentrada).HasColumnName("CANTIDADESECHASENENTRADA");
            entity.Property(e => e.Cantidadesechassensor).HasColumnName("CANTIDADESECHASSENSOR");
            entity.Property(e => e.Cantidadesechassensor2).HasColumnName("CANTIDADESECHASSENSOR2");
            entity.Property(e => e.Cantidadesechassensor3).HasColumnName("CANTIDADESECHASSENSOR3");
            entity.Property(e => e.Cerrosinoturno).HasColumnName("CERROSINOTURNO");
            entity.Property(e => e.Codigosupervisor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOSUPERVISOR");
            entity.Property(e => e.Codigotupla).HasColumnName("CODIGOTUPLA");
            entity.Property(e => e.Codigoturno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOTURNO");
            entity.Property(e => e.Desperdicioxproduccion).HasColumnName("DESPERDICIOXPRODUCCION");
            entity.Property(e => e.Desperdicioxpuestamarcha).HasColumnName("DESPERDICIOXPUESTAMARCHA");
            entity.Property(e => e.Factormultiplicadorejecutado).HasColumnName("FACTORMULTIPLICADOREJECUTADO");
            entity.Property(e => e.FechaUltimoDato)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ULTIMO_DATO");
            entity.Property(e => e.Fechaentrada)
                .HasColumnType("datetime")
                .HasColumnName("FECHAENTRADA");
            entity.Property(e => e.Horasdiurnas)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HORASDIURNAS");
            entity.Property(e => e.Horasejecutadas).HasColumnName("HORASEJECUTADAS");
            entity.Property(e => e.Horasfestivas)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HORASFESTIVAS");
            entity.Property(e => e.Horasnocturnas)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HORASNOCTURNAS");
            entity.Property(e => e.Lote)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOTE");
            entity.Property(e => e.Modificafactormultiplicadorejecutado).HasColumnName("MODIFICAFACTORMULTIPLICADOREJECUTADO");
            entity.Property(e => e.Productividadentrada).HasColumnName("PRODUCTIVIDADENTRADA");
            entity.Property(e => e.Sionocapturamanual)
                .HasDefaultValue(0)
                .HasColumnName("SIONOCAPTURAMANUAL");
            entity.Property(e => e.Sionoreprocesoretenidas).HasColumnName("SIONOREPROCESORETENIDAS");
            entity.Property(e => e.Standardelentradaejecutado).HasColumnName("STANDARDELENTRADAEJECUTADO");
            entity.Property(e => e.Standardelpuesto).HasColumnName("STANDARDELPUESTO");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Unidadesreperocesadas).HasColumnName("UNIDADESREPEROCESADAS");
            entity.Property(e => e.Unidadessensor1literal).HasColumnName("UNIDADESSENSOR1LITERAL");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(user_name())")
                .HasColumnName("USUARIO");
            entity.Property(e => e.Velocidadentrada).HasColumnName("VELOCIDADENTRADA");

            entity.HasOne(d => d.CodigosupervisorNavigation).WithMany(p => p.Entradaejecucions)
                .HasForeignKey(d => d.Codigosupervisor)
                .HasConstraintName("FK_ENTRADAE_REFERENCE_PERSONAL");

            entity.HasOne(d => d.CodigotuplaNavigation).WithMany(p => p.Entradaejecucions)
                .HasForeignKey(d => d.Codigotupla)
                .HasConstraintName("FK_ENTRADAE_REFERENCE_TUPLAEJE");

            entity.HasOne(d => d.CodigoturnoNavigation).WithMany(p => p.Entradaejecucions)
                .HasForeignKey(d => d.Codigoturno)
                .HasConstraintName("FK_ENTRADAE_REFERENCE_TIPOSDET");
        });

        modelBuilder.Entity<OrdenesDeProduccion>(entity =>
        {
            entity.HasKey(e => e.Codigoordenproduccion);

            entity.ToTable("ORDENES_DE_PRODUCCION", tb => tb.HasTrigger("TR_ORDENES_DE_PRODUCCION"));

            entity.Property(e => e.Codigoordenproduccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOORDENPRODUCCION");
            entity.Property(e => e.CodigoBodega)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGO_BODEGA");
            entity.Property(e => e.Codigobarras)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOBARRAS");
            entity.Property(e => e.Codigopedido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPEDIDO");
            entity.Property(e => e.DocumentoRemicion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOCUMENTO_REMICION");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ENTREGA");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIO");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_REGISTRO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(user_name())")
                .HasColumnName("USUARIO");
        });

        modelBuilder.Entity<Ordenproduccionxproducto>(entity =>
        {
            entity.HasKey(e => new { e.Codigoordenproduccion, e.Codigoproductos });

            entity.ToTable("ORDENPRODUCCIONXPRODUCTO", tb => tb.HasTrigger("TR_ORDENPRODUCCIONXPRODUCTO"));

            entity.Property(e => e.Codigoordenproduccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOORDENPRODUCCION");
            entity.Property(e => e.Codigoproductos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPRODUCTOS");
            entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");
            entity.Property(e => e.DiasTotal).HasColumnName("DIAS_TOTAL");
            entity.Property(e => e.FechaEntregaProducto)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ENTREGA_PRODUCTO");
            entity.Property(e => e.FechaInicioProducto)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INICIO_PRODUCTO");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_REGISTRO");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(user_name())")
                .HasColumnName("USUARIO");

            entity.HasOne(d => d.CodigoordenproduccionNavigation).WithMany(p => p.Ordenproduccionxproductos)
                .HasForeignKey(d => d.Codigoordenproduccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDENPRO_REFERENCE_ORDENES_");

            entity.HasOne(d => d.CodigoproductosNavigation).WithMany(p => p.Ordenproduccionxproductos)
                .HasForeignKey(d => d.Codigoproductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDENPRO_REFERENCE_PRODUCTO");
        });

        modelBuilder.Entity<Parada>(entity =>
        {
            entity.HasKey(e => e.Codigoparada);

            entity.ToTable("PARADAS", tb => tb.HasTrigger("TR_PARADAS"));

            entity.Property(e => e.Codigoparada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPARADA");
            entity.Property(e => e.Codigoegp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOEGP");
            entity.Property(e => e.Codigogrupoparada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOGRUPOPARADA");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Nombreparada)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBREPARADA");
            entity.Property(e => e.Sionoprogramada).HasColumnName("SIONOPROGRAMADA");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
        });

        modelBuilder.Entity<Paradasejecutada>(entity =>
        {
            entity.HasKey(e => e.Codigoregistrso).HasName("PK__PARADASEJECUTADA__7755B73D");

            entity.ToTable("PARADASEJECUTADAS");

            entity.HasIndex(e => e.Codigoentradaejecucion, "IX_PARADASEJECUTADAS");

            entity.Property(e => e.Codigoregistrso).HasColumnName("CODIGOREGISTRSO");
            entity.Property(e => e.Codigoentradaejecucion).HasColumnName("CODIGOENTRADAEJECUCION");
            entity.Property(e => e.Codigoparada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPARADA");
            entity.Property(e => e.Codigopersonalatiende)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOPERSONALATIENDE");
            entity.Property(e => e.Comentario)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("COMENTARIO");
            entity.Property(e => e.Demoraparada).HasColumnName("DEMORAPARADA");
            entity.Property(e => e.Diurnaonocturnaofestiva)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DIURNAONOCTURNAOFESTIVA");
            entity.Property(e => e.Estandarprogramadas).HasColumnName("ESTANDARPROGRAMADAS");
            entity.Property(e => e.Fechayhoraparada)
                .HasColumnType("datetime")
                .HasColumnName("FECHAYHORAPARADA");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");

            entity.HasOne(d => d.CodigoentradaejecucionNavigation).WithMany(p => p.Paradasejecutada)
                .HasForeignKey(d => d.Codigoentradaejecucion)
                .HasConstraintName("FK__PARADASEJ__CODIG__7849DB76");

            entity.HasOne(d => d.CodigoparadaNavigation).WithMany(p => p.Paradasejecutada)
                .HasForeignKey(d => d.Codigoparada)
                .HasConstraintName("FK__PARADASEJ__CODIG__793DFFAF");

            entity.HasOne(d => d.CodigopersonalatiendeNavigation).WithMany(p => p.Paradasejecutada)
                .HasForeignKey(d => d.Codigopersonalatiende)
                .HasConstraintName("FK__PARADASEJ__CODIG__7A3223E8");
        });

        modelBuilder.Entity<Parte>(entity =>
        {
            entity.HasKey(e => e.PartesId).HasName("PK__Partes__590F666F0804063F");

            entity.Property(e => e.PartesId).HasColumnName("PartesID");
            entity.Property(e => e.Codigo)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.ParteNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("parteNombre");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.Codigopersonal);

            entity.ToTable("PERSONAL", tb => tb.HasTrigger("TR_PERSONAL"));

            entity.Property(e => e.Codigopersonal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOPERSONAL");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.Cedula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CEDULA");
            entity.Property(e => e.Codbarras)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CODBARRAS");
            entity.Property(e => e.Codidocargospersonal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIDOCARGOSPERSONAL");
            entity.Property(e => e.Codigosueldo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOSUELDO");
            entity.Property(e => e.Cursos)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("CURSOS");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Estudios)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("ESTUDIOS");
            entity.Property(e => e.Experienciacargos1)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("EXPERIENCIACARGOS1");
            entity.Property(e => e.Experienciacargos2)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("EXPERIENCIACARGOS2");
            entity.Property(e => e.Experienciacargos3)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("EXPERIENCIACARGOS3");
            entity.Property(e => e.Fechaingreso)
                .HasColumnType("datetime")
                .HasColumnName("FECHAINGRESO");
            entity.Property(e => e.Fechanacimiento)
                .HasColumnType("datetime")
                .HasColumnName("FECHANACIMIENTO");
            entity.Property(e => e.Foto)
                .HasMaxLength(8000)
                .HasColumnName("FOTO");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
            entity.Property(e => e.Sexo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SEXO");
            entity.Property(e => e.Telefonos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TELEFONOS");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Tipodocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TIPODOCUMENTO");

            entity.HasOne(d => d.CodidocargospersonalNavigation).WithMany(p => p.Personals)
                .HasForeignKey(d => d.Codidocargospersonal)
                .HasConstraintName("FK_PERSONAL_REFERENCE_TIPOCARG");
        });

        modelBuilder.Entity<Proceso>(entity =>
        {
            entity.HasKey(e => e.Codigoproceso);

            entity.ToTable("PROCESO", tb => tb.HasTrigger("TR_PROCESO"));

            entity.Property(e => e.Codigoproceso)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOPROCESO");
            entity.Property(e => e.Codigoarea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOAREA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Codigoproductos).IsClustered(false);

            entity.ToTable("PRODUCTOS", tb =>
                {
                    tb.HasComment("Tabla que relaciona los productos");
                    tb.HasTrigger("TR_PRODUCTOS");
                });

            entity.Property(e => e.Codigoproductos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPRODUCTOS");
            entity.Property(e => e.Codigobarras)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CODIGOBARRAS");
            entity.Property(e => e.Codigocentro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOCENTRO");
            entity.Property(e => e.Estadoproducto).HasColumnName("ESTADOPRODUCTO");
            entity.Property(e => e.Nombreproducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBREPRODUCTO");
            entity.Property(e => e.Pesoproducto).HasColumnName("PESOPRODUCTO");
            entity.Property(e => e.Presentacionproducto).HasColumnName("PRESENTACIONPRODUCTO");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Unidades)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UNIDADES");
            entity.Property(e => e.Volumenproducto).HasColumnName("VOLUMENPRODUCTO");

            entity.HasOne(d => d.CodigocentroNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Codigocentro)
                .HasConstraintName("FK_PRODUCTO_REFERENCE_CENTROS");
        });

        modelBuilder.Entity<Productoporproceso>(entity =>
        {
            entity.HasKey(e => new { e.Codigoproductos, e.Codigoproceso });

            entity.ToTable("PRODUCTOPORPROCESO", tb => tb.HasTrigger("TR_PRODUCTOPORPROCESO"));

            entity.Property(e => e.Codigoproductos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPRODUCTOS");
            entity.Property(e => e.Codigoproceso)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOPROCESO");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");

            entity.HasOne(d => d.CodigoprocesoNavigation).WithMany(p => p.Productoporprocesos)
                .HasForeignKey(d => d.Codigoproceso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTO_REFERENCE_PROCESO");

            entity.HasOne(d => d.CodigoproductosNavigation).WithMany(p => p.Productoporprocesos)
                .HasForeignKey(d => d.Codigoproductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTO_REFERENCE_PRODUCTO");
        });

        modelBuilder.Entity<Puestosdetrabajo>(entity =>
        {
            entity.HasKey(e => e.Codigopuesto);

            entity.ToTable("PUESTOSDETRABAJO", tb => tb.HasTrigger("TR_PUESTOSDETRABAJO"));

            entity.Property(e => e.Codigopuesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPUESTO");
            entity.Property(e => e.CargaAmp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CARGA_AMP");
            entity.Property(e => e.Codigocelula)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CODIGOCELULA");
            entity.Property(e => e.Codigotecnologia)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CODIGOTECNOLOGIA");
            entity.Property(e => e.Decidecelula).HasColumnName("DECIDECELULA");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Estadoequipos)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ESTADOEQUIPOS");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHAREGISTRO");
            entity.Property(e => e.Horamantenimientotrabajo).HasColumnName("HORAMANTENIMIENTOTRABAJO");
            entity.Property(e => e.Horasmantenimientocalendario).HasColumnName("HORASMANTENIMIENTOCALENDARIO");
            entity.Property(e => e.Iddispositivo)
                .HasMaxLength(50)
                .HasColumnName("IDDISPOSITIVO");
            entity.Property(e => e.Nombrepuesto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBREPUESTO");
            entity.Property(e => e.Numerooperarios).HasColumnName("NUMEROOPERARIOS");
            entity.Property(e => e.Planta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PLANTA");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Tipopuestotrabajo).HasColumnName("TIPOPUESTOTRABAJO");
            entity.Property(e => e.Toleranciadesperdiciodigitado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TOLERANCIADESPERDICIODIGITADO");
            entity.Property(e => e.Toleranciadesperdiciomaterialppal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TOLERANCIADESPERDICIOMATERIALPPAL");
            entity.Property(e => e.Toleranciadesperdiciopuestaenmarcha)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TOLERANCIADESPERDICIOPUESTAENMARCHA");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(user_name())")
                .HasColumnName("USUARIO");
            entity.Property(e => e.Velocidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VELOCIDAD");
        });

        modelBuilder.Entity<Puestotrabajosegunproducto>(entity =>
        {
            entity.HasKey(e => new { e.Codigoproductos, e.Codigoproceso, e.Codigopuesto });

            entity.ToTable("PUESTOTRABAJOSEGUNPRODUCTO", tb => tb.HasTrigger("TR_PUESTOTRABAJOSEGUNPRODUCTO"));

            entity.Property(e => e.Codigoproductos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPRODUCTOS");
            entity.Property(e => e.Codigoproceso)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOPROCESO");
            entity.Property(e => e.Codigopuesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPUESTO");
            entity.Property(e => e.Codmatdesperdppal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODMATDESPERDPPAL");
            entity.Property(e => e.Factorconversiondesmppal).HasColumnName("FACTORCONVERSIONDESMPPAL");
            entity.Property(e => e.Factorconversiondesperdicio).HasColumnName("FACTORCONVERSIONDESPERDICIO");
            entity.Property(e => e.Factorconversionproduccionfinal)
                .HasDefaultValue(1.0)
                .HasColumnName("FACTORCONVERSIONPRODUCCIONFINAL");
            entity.Property(e => e.Factorvariable1)
                .HasDefaultValue(1.0)
                .HasColumnName("FACTORVARIABLE1");
            entity.Property(e => e.Multiplicadorsensor).HasColumnName("MULTIPLICADORSENSOR");
            entity.Property(e => e.Multiplicadorsensor2y3)
                .HasDefaultValue(1.0)
                .HasColumnName("MULTIPLICADORSENSOR2Y3");
            entity.Property(e => e.Predeterminada).HasColumnName("PREDETERMINADA");
            entity.Property(e => e.Tiemposalidaparada).HasColumnName("TIEMPOSALIDAPARADA");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Timpoesperap).HasColumnName("TIMPOESPERAP");
            entity.Property(e => e.Unidadessalidaparada).HasColumnName("UNIDADESSALIDAPARADA");
            entity.Property(e => e.Velocidadestandar).HasColumnName("VELOCIDADESTANDAR");

            entity.HasOne(d => d.CodigopuestoNavigation).WithMany(p => p.Puestotrabajosegunproductos)
                .HasForeignKey(d => d.Codigopuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PUESTOTR_REFERENCE_PUESTOSD");

            entity.HasOne(d => d.Productoporproceso).WithMany(p => p.Puestotrabajosegunproductos)
                .HasForeignKey(d => new { d.Codigoproductos, d.Codigoproceso })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PUESTOTR_REFERENCE_PRODUCTO");
        });

        modelBuilder.Entity<Standarparada>(entity =>
        {
            entity.HasKey(e => e.IdStandar);

            entity.ToTable("STANDARPARADAS", tb => tb.HasTrigger("TR_STANDARPARADAS"));

            entity.Property(e => e.IdStandar).HasColumnName("ID_STANDAR");
            entity.Property(e => e.Codigoparada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPARADA");
            entity.Property(e => e.Codigoproductos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPRODUCTOS");
            entity.Property(e => e.Codigopuesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPUESTO");
            entity.Property(e => e.Standarparada1).HasColumnName("STANDARPARADA");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");

            entity.HasOne(d => d.CodigoparadaNavigation).WithMany(p => p.Standarparada)
                .HasForeignKey(d => d.Codigoparada)
                .HasConstraintName("FK_STANDARP_REFERENCE_PARADAS");

            entity.HasOne(d => d.CodigoproductosNavigation).WithMany(p => p.Standarparada)
                .HasForeignKey(d => d.Codigoproductos)
                .HasConstraintName("FK_STANDARP_REFERENCE_PRODUCTO");

            entity.HasOne(d => d.CodigopuestoNavigation).WithMany(p => p.Standarparada)
                .HasForeignKey(d => d.Codigopuesto)
                .HasConstraintName("FK_STANDARP_REFERENCE_PUESTOSD");
        });

        modelBuilder.Entity<Tipocargospersonal>(entity =>
        {
            entity.HasKey(e => e.Codidocargospersonal);

            entity.ToTable("TIPOCARGOSPERSONAL", tb => tb.HasTrigger("TR_TIPOCARGOSPERSONAL"));

            entity.Property(e => e.Codidocargospersonal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIDOCARGOSPERSONAL");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Nombredelcargo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBREDELCARGO");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
        });

        modelBuilder.Entity<Tiposdeturno>(entity =>
        {
            entity.HasKey(e => e.Codigoturno);

            entity.ToTable("TIPOSDETURNOS", tb => tb.HasTrigger("TR_TIPOSDETURNOS"));

            entity.Property(e => e.Codigoturno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOTURNO");
            entity.Property(e => e.Cierreautomatico).HasColumnName("CIERREAUTOMATICO");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.Fechaturno)
                .HasColumnType("datetime")
                .HasColumnName("FECHATURNO");
            entity.Property(e => e.Horas).HasColumnName("HORAS");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Turno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TURNO");
            entity.Property(e => e.Turnofecha)
                .HasColumnType("datetime")
                .HasColumnName("TURNOFECHA");
        });

        modelBuilder.Entity<Tiposdeunidadesmateriale>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("TIPOSDEUNIDADESMATERIALES", tb => tb.HasTrigger("TR_TIPOSDEUNIDADESMATERIALES"));

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGO");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHAREGISTRO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(user_name())")
                .HasColumnName("USUARIO");
        });

        modelBuilder.Entity<Transmicionweb>(entity =>
        {
            entity.HasKey(e => e.Codigopuesto);

            entity.ToTable("TRANSMICIONWEB");

            entity.Property(e => e.Codigopuesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPUESTO");
            entity.Property(e => e.Codigoentradaejecucion).HasColumnName("CODIGOENTRADAEJECUCION");
            entity.Property(e => e.Codigoparada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPARADA");
            entity.Property(e => e.Estadopuesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTADOPUESTO");
            entity.Property(e => e.Estadosoftware)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTADOSOFTWARE");
            entity.Property(e => e.Horaparada)
                .HasColumnType("datetime")
                .HasColumnName("HORAPARADA");
            entity.Property(e => e.Horaultimodato)
                .HasColumnType("datetime")
                .HasColumnName("HORAULTIMODATO");
            entity.Property(e => e.Tiempoparado).HasColumnName("TIEMPOPARADO");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");

            entity.HasOne(d => d.CodigoentradaejecucionNavigation).WithMany(p => p.Transmicionwebs)
                .HasForeignKey(d => d.Codigoentradaejecucion)
                .HasConstraintName("FK_TRANSMIC_REFERENCE_ENTRADAE");

            entity.HasOne(d => d.CodigoparadaNavigation).WithMany(p => p.Transmicionwebs)
                .HasForeignKey(d => d.Codigoparada)
                .HasConstraintName("FK_TRANSMIC_REFERENCE_PARADAS");

            entity.HasOne(d => d.CodigopuestoNavigation).WithOne(p => p.Transmicionweb)
                .HasForeignKey<Transmicionweb>(d => d.Codigopuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSMIC_REFERENCE_PUESTOSD");
        });

        modelBuilder.Entity<Tuplaejecucion>(entity =>
        {
            entity.HasKey(e => e.Codigotupla);

            entity.ToTable("TUPLAEJECUCION");

            entity.Property(e => e.Codigotupla).HasColumnName("CODIGOTUPLA");
            entity.Property(e => e.Codigoordenproduccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOORDENPRODUCCION");
            entity.Property(e => e.Codigoproceso)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CODIGOPROCESO");
            entity.Property(e => e.Codigoproductos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPRODUCTOS");
            entity.Property(e => e.Codigoproductosasparte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPRODUCTOSASPARTE");
            entity.Property(e => e.Codigopuesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODIGOPUESTO");
            entity.Property(e => e.Timespan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TIMESPAN");

            entity.HasOne(d => d.Ordenproduccionxproducto).WithMany(p => p.Tuplaejecucions)
                .HasForeignKey(d => new { d.Codigoordenproduccion, d.Codigoproductos })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TUPLAEJE_REFERENCE_ORDENPRO");

            entity.HasOne(d => d.Productoporproceso).WithMany(p => p.Tuplaejecucions)
                .HasForeignKey(d => new { d.Codigoproductosasparte, d.Codigoproceso })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TUPLAEJE_REFERENCE_PRODUCTO");

            entity.HasOne(d => d.Puestotrabajosegunproducto).WithMany(p => p.Tuplaejecucions)
                .HasForeignKey(d => new { d.Codigoproductosasparte, d.Codigoproceso, d.Codigopuesto })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TUPLAEJE_REFERENCE_PUESTOTR");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
