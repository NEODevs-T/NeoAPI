using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.Models.Neo;

public partial class DbNeoIiContext : DbContext
{
    public DbNeoIiContext()
    {
    }

    public DbNeoIiContext(DbContextOptions<DbNeoIiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AreAfect> AreAfects { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<AreaBon> AreaBons { get; set; }

    public virtual DbSet<AreaCarga> AreaCargas { get; set; }

    public virtual DbSet<AsentamientosFueraRangoV> AsentamientosFueraRangoVs { get; set; }

    public virtual DbSet<Asentum> Asenta { get; set; }

    public virtual DbSet<AsistenReu> AsistenReus { get; set; }

    public virtual DbSet<BloqRang> BloqRangs { get; set; }

    public virtual DbSet<Bloque> Bloques { get; set; }

    public virtual DbSet<CaUnidad> CaUnidads { get; set; }

    public virtual DbSet<CambFec> CambFecs { get; set; }

    public virtual DbSet<CambStat> CambStats { get; set; }

    public virtual DbSet<CambiReuV> CambiReuVs { get; set; }

    public virtual DbSet<CargoReu> CargoReus { get; set; }

    public virtual DbSet<Categori> Categoris { get; set; }

    public virtual DbSet<Causa> Causas { get; set; }

    public virtual DbSet<CausaCal> CausaCals { get; set; }

    public virtual DbSet<CausaCalidadV> CausaCalidadVs { get; set; }

    public virtual DbSet<Causante> Causantes { get; set; }

    public virtual DbSet<Centro> Centros { get; set; }

    public virtual DbSet<CentroCostoV> CentroCostoVs { get; set; }

    public virtual DbSet<CentrosV> CentrosVs { get; set; }

    public virtual DbSet<ClasiVar> ClasiVars { get; set; }

    public virtual DbSet<ClasifiTpm> ClasifiTpms { get; set; }

    public virtual DbSet<CompaniaV> CompaniaVs { get; set; }

    public virtual DbSet<CortCate> CortCates { get; set; }

    public virtual DbSet<CorteDi> CorteDis { get; set; }

    public virtual DbSet<CorteDi1> CorteDis1 { get; set; }

    public virtual DbSet<DeparBon> DeparBons { get; set; }

    public virtual DbSet<DispDefi> DispDefis { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<DivisionesV> DivisionesVs { get; set; }

    public virtual DbSet<Eficiencium> Eficiencia { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EmpresasV> EmpresasVs { get; set; }

    public virtual DbSet<EquiMedi> EquiMedis { get; set; }

    public virtual DbSet<EquipoEam> EquipoEams { get; set; }

    public virtual DbSet<EquiposEamV> EquiposEamVs { get; set; }

    public virtual DbSet<Escalon> Escalons { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Estandar> Estandars { get; set; }

    public virtual DbSet<FechaProg> FechaProgs { get; set; }

    public virtual DbSet<GeneralGbV> GeneralGbVs { get; set; }

    public virtual DbSet<Identifi> Identifis { get; set; }

    public virtual DbSet<InfoAse> InfoAses { get; set; }

    public virtual DbSet<InfoCali> InfoCalis { get; set; }

    public virtual DbSet<Ksf> Ksfs { get; set; }

    public virtual DbSet<LibroNove> LibroNoves { get; set; }

    public virtual DbSet<LibroNovedadesV> LibroNovedadesVs { get; set; }

    public virtual DbSet<LinAre> LinAres { get; set; }

    public virtual DbSet<Linea> Lineas { get; set; }

    public virtual DbSet<LineaV> LineaVs { get; set; }

    public virtual DbSet<MaestraV> MaestraVs { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<MonedBon> MonedBons { get; set; }

    public virtual DbSet<Monto> Montos { get; set; }

    public virtual DbSet<MontoBon> MontoBons { get; set; }

    public virtual DbSet<MontosEscalonV> MontosEscalonVs { get; set; }

    public virtual DbSet<NivePue> NivePues { get; set; }

    public virtual DbSet<Nivel> Nivels { get; set; }

    public virtual DbSet<Objetivo> Objetivos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<ParaTp> ParaTps { get; set; }

    public virtual DbSet<ParsiOee> ParsiOees { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Plantilla> Plantillas { get; set; }

    public virtual DbSet<ProNoCon> ProNoCons { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoNoConformeV> ProductoNoConformeVs { get; set; }

    public virtual DbSet<ProductosV> ProductosVs { get; set; }

    public virtual DbSet<PropDisp> PropDisps { get; set; }

    public virtual DbSet<ProyectoUsr> ProyectoUsrs { get; set; }

    public virtual DbSet<PuesTrab> PuesTrabs { get; set; }

    public virtual DbSet<Rango> Rangos { get; set; }

    public virtual DbSet<RangoDeControlActivosV> RangoDeControlActivosVs { get; set; }

    public virtual DbSet<RegistroPersonalV> RegistroPersonalVs { get; set; }

    public virtual DbSet<RespoReu> RespoReus { get; set; }

    public virtual DbSet<Resuman> Resumen { get; set; }

    public virtual DbSet<ReuDiaV> ReuDiaVs { get; set; }

    public virtual DbSet<Reunion> Reunions { get; set; }

    public virtual DbSet<Reunion2> Reunion2s { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Seccion> Seccions { get; set; }

    public virtual DbSet<SeccionesV> SeccionesVs { get; set; }

    public virtual DbSet<TabuladoresBonoV> TabuladoresBonoVs { get; set; }

    public virtual DbSet<TiParTp> TiParTps { get; set; }

    public virtual DbSet<TieEjeTp> TieEjeTps { get; set; }

    public virtual DbSet<TieParTp> TieParTps { get; set; }

    public virtual DbSet<TieProgr> TieProgrs { get; set; }

    public virtual DbSet<TipIncen> TipIncens { get; set; }

    public virtual DbSet<TipReu> TipReus { get; set; }

    public virtual DbSet<TipSuple> TipSuples { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<TipoProd> TipoProds { get; set; }

    public virtual DbSet<TipoVar> TipoVars { get; set; }

    public virtual DbSet<TurnoTp> TurnoTps { get; set; }

    public virtual DbSet<UnidaObj> UnidaObjs { get; set; }

    public virtual DbSet<Unidad> Unidads { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosV> UsuariosVs { get; set; }

    public virtual DbSet<ValoresDeAsentamientosV> ValoresDeAsentamientosVs { get; set; }

    public virtual DbSet<VarClasificacionV> VarClasificacionVs { get; set; }

    public virtual DbSet<VarTipoV> VarTipoVs { get; set; }

    public virtual DbSet<Variable> Variables { get; set; }

    public virtual DbSet<VariablesAsentamientosV> VariablesAsentamientosVs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DCTDTDB02;Initial Catalog=DbNeoII;TrustServerCertificate=True;Persist Security Info=True;User ID=UsrEncNeo;Password=L3C7U3A2023*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AreAfect>(entity =>
        {
            entity.HasKey(e => e.IdAreAfect).HasName("PK_AreAfect_1");

            entity.ToTable("AreAfect", "tps");

            entity.Property(e => e.IdAreAfect).ValueGeneratedNever();
            entity.Property(e => e.Aadetalle)
                .IsUnicode(false)
                .HasColumnName("AADetalle");
            entity.Property(e => e.Aaestado).HasColumnName("AAEstado");
            entity.Property(e => e.Aanom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AANom");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea).HasName("PK_Area_1");

            entity.ToTable("Area", "mae");

            entity.Property(e => e.Adetalle)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("ADetalle");
            entity.Property(e => e.Aestado).HasColumnName("AEstado");
            entity.Property(e => e.Anom)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ANom");
        });

        modelBuilder.Entity<AreaBon>(entity =>
        {
            entity.HasKey(e => e.IdAreaBon);

            entity.ToTable("AreaBon", "bon", tb => tb.HasComment("Area de los escalones del bono"));

            entity.Property(e => e.Abdescrip)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Descripcion del Area")
                .HasColumnName("ABDescrip");
            entity.Property(e => e.Abestado)
                .HasComment("Estado del Area (1:Activo, 0:Inactivo)")
                .HasColumnName("ABEstado");
            entity.Property(e => e.Abnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Nombre del Area")
                .HasColumnName("ABNombre");
        });

        modelBuilder.Entity<AreaCarga>(entity =>
        {
            entity.HasKey(e => e.IdAreaCarg).HasName("PK_AreaCarga_1");

            entity.ToTable("AreaCarga", "lib");

            entity.Property(e => e.IdAreaCarg).HasColumnName("idAreaCarg");
            entity.Property(e => e.Acdetalle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ACDetalle");
            entity.Property(e => e.Acengllish)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACEngllish");
            entity.Property(e => e.Acestado).HasColumnName("ACEstado");
            entity.Property(e => e.Acnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACNombre");
        });

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

        modelBuilder.Entity<Asentum>(entity =>
        {
            entity.HasKey(e => e.IdAsenta);

            entity.ToTable("Asenta", "ase");

            entity.Property(e => e.AisActivo).HasColumnName("AIsActivo");
            entity.Property(e => e.Aobserv)
                .IsUnicode(false)
                .HasColumnName("AObserv");
            entity.Property(e => e.Avalor).HasColumnName("AValor");

            entity.HasOne(d => d.IdInfoAseNavigation).WithMany(p => p.Asenta)
                .HasForeignKey(d => d.IdInfoAse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asenta_InfoAse");

            entity.HasOne(d => d.IdRangoNavigation).WithMany(p => p.Asenta)
                .HasForeignKey(d => d.IdRango)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asenta_Rago");
        });

        modelBuilder.Entity<AsistenReu>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK_AsistenReu_1");

            entity.ToTable("AsistenReu", "reu");

            entity.Property(e => e.ArObser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ararea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ARArea");
            entity.Property(e => e.Arfecha)
                .HasColumnType("datetime")
                .HasColumnName("ARFecha");

            entity.HasOne(d => d.IdCargoRNavigation).WithMany(p => p.AsistenReus)
                .HasForeignKey(d => d.IdCargoR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AsistenReu_CargoReu");
        });

        modelBuilder.Entity<BloqRang>(entity =>
        {
            entity.HasKey(e => e.IdBloqRang);

            entity.ToTable("BloqRang", "ase");

            entity.Property(e => e.Brestado).HasColumnName("BREstado");

            entity.HasOne(d => d.IdBloqueNavigation).WithMany(p => p.BloqRangs)
                .HasForeignKey(d => d.IdBloque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloqRang_Bloque");

            entity.HasOne(d => d.IdRangoNavigation).WithMany(p => p.BloqRangs)
                .HasForeignKey(d => d.IdRango)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloqRang_Rago");
        });

        modelBuilder.Entity<Bloque>(entity =>
        {
            entity.HasKey(e => e.IdBloque);

            entity.ToTable("Bloque", "ase");

            entity.Property(e => e.Bestado).HasColumnName("BEstado");
            entity.Property(e => e.BfechaCrea)
                .HasColumnType("datetime")
                .HasColumnName("BFechaCrea");
            entity.Property(e => e.BhoraFinal).HasColumnName("BHoraFinal");
            entity.Property(e => e.BhoraInici).HasColumnName("BHoraInici");
            entity.Property(e => e.Bobserv)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("BObserv");
        });

        modelBuilder.Entity<CaUnidad>(entity =>
        {
            entity.HasKey(e => e.IdCaUnidad).HasName("PK_Unidad");

            entity.ToTable("CaUnidad", "pnc");

            entity.Property(e => e.Udescri)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("UDescri");
            entity.Property(e => e.Uestado).HasColumnName("UEstado");
            entity.Property(e => e.Unombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UNombre");
        });

        modelBuilder.Entity<CambFec>(entity =>
        {
            entity.HasKey(e => e.IdCambFec).HasName("PK_CambFec_1");

            entity.ToTable("CambFec", "reu");

            entity.Property(e => e.Cffec)
                .HasColumnType("datetime")
                .HasColumnName("CFFec");
            entity.Property(e => e.CffecNew).HasColumnName("CFFecNew");
            entity.Property(e => e.Cfuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CFUser");

            entity.HasOne(d => d.IdReuDiaNavigation).WithMany(p => p.CambFecs)
                .HasForeignKey(d => d.IdReuDia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CambFec_ReuDia");
        });

        modelBuilder.Entity<CambStat>(entity =>
        {
            entity.HasKey(e => e.IdCambStat).HasName("PK_CambStat_1");

            entity.ToTable("CambStat", "reu");

            entity.Property(e => e.Cbfecha)
                .HasColumnType("datetime")
                .HasColumnName("CBFecha");
            entity.Property(e => e.Cbstat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CBStat");
            entity.Property(e => e.Cbuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CBUser");

            entity.HasOne(d => d.IdReuDiaNavigation).WithMany(p => p.CambStats)
                .HasForeignKey(d => d.IdReuDia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CambStat_ReuDia");
        });

        modelBuilder.Entity<CambiReuV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CambiReu_V");

            entity.Property(e => e.Accion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Centro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Discrepancia)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EquipoCodigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCambio).HasColumnType("datetime");
            entity.Property(e => e.FechaTrabajo).HasColumnType("datetime");
            entity.Property(e => e.Linea)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Responsable)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CargoReu>(entity =>
        {
            entity.HasKey(e => e.IdCargoR).HasName("PK_CargoReu_1");

            entity.ToTable("CargoReu", "reu");

            entity.Property(e => e.Crarea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CRArea");
            entity.Property(e => e.Crbloque).HasColumnName("CRBloque");
            entity.Property(e => e.Crempresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREmpresa");
            entity.Property(e => e.Cresta).HasColumnName("CREsta");
            entity.Property(e => e.Crnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CRNombre");
        });

        modelBuilder.Entity<Categori>(entity =>
        {
            entity.HasKey(e => e.IdCategori);

            entity.ToTable("Categori", "ase");

            entity.Property(e => e.Ccodigo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CCodigo");
            entity.Property(e => e.Cdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("CDescri");
            entity.Property(e => e.Cesta).HasColumnName("CEsta");
            entity.Property(e => e.CfechaCrea)
                .HasColumnType("datetime")
                .HasColumnName("CFechaCrea");
            entity.Property(e => e.Cnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CNombre");
        });

        modelBuilder.Entity<Causa>(entity =>
        {
            entity.HasKey(e => e.IdCausa);

            entity.ToTable("Causa", "pnc");

            entity.Property(e => e.Cdescri)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CDescri");
            entity.Property(e => e.Cestado).HasColumnName("CEstado");
            entity.Property(e => e.Cnombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CNombre");

            entity.HasOne(d => d.IdCausanteNavigation).WithMany(p => p.Causas)
                .HasForeignKey(d => d.IdCausante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Causa_Causante");
        });

        modelBuilder.Entity<CausaCal>(entity =>
        {
            entity.HasKey(e => e.IdCausaCal);

            entity.ToTable("CausaCal", "reu");

            entity.Property(e => e.IdCausaCal).HasColumnName("idCausaCal");
            entity.Property(e => e.Ccdescri)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CCDescri");
            entity.Property(e => e.Ccenglish)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CCEnglish");
            entity.Property(e => e.Ccestado).HasColumnName("CCEstado");
            entity.Property(e => e.Ccfecha)
                .HasColumnType("datetime")
                .HasColumnName("CCFecha");
            entity.Property(e => e.Ccnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CCNombre");
        });

        modelBuilder.Entity<CausaCalidadV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CausaCalidad_V");

            entity.Property(e => e.CausaDeLaCalidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Causa de la Calidad ");
            entity.Property(e => e.Centro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.División)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeReunion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha de Reunion");
            entity.Property(e => e.Idcausa).HasColumnName("IDCausa");
            entity.Property(e => e.Idregistro).HasColumnName("IDRegistro");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Causante>(entity =>
        {
            entity.HasKey(e => e.IdCausante);

            entity.ToTable("Causante", "pnc");

            entity.Property(e => e.Cdescri)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CDescri");
            entity.Property(e => e.Cestado).HasColumnName("CEstado");
            entity.Property(e => e.Cnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CNombre");
        });

        modelBuilder.Entity<Centro>(entity =>
        {
            entity.HasKey(e => e.IdCentro);

            entity.ToTable("Centro", "mae");

            entity.Property(e => e.Cdetalle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CDetalle");
            entity.Property(e => e.Cestado).HasColumnName("CEstado");
            entity.Property(e => e.Cfecha)
                .HasColumnType("datetime")
                .HasColumnName("CFecha");
            entity.Property(e => e.Cnom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CNom");
        });

        modelBuilder.Entity<CentroCostoV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CentroCosto_V");

            entity.Property(e => e.Wdesc)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("WDESC");
            entity.Property(e => e.Wid)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("WID");
            entity.Property(e => e.Wwrkc)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("WWRKC");
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

        modelBuilder.Entity<ClasiVar>(entity =>
        {
            entity.HasKey(e => e.IdClasiVar);

            entity.ToTable("ClasiVar", "ase");

            entity.Property(e => e.Cvdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("CVDescri");
            entity.Property(e => e.Cvestado).HasColumnName("CVEstado");
            entity.Property(e => e.CvfechCrea)
                .HasColumnType("datetime")
                .HasColumnName("CVFechCrea");
            entity.Property(e => e.Cvnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CVNombre");
        });

        modelBuilder.Entity<ClasifiTpm>(entity =>
        {
            entity.HasKey(e => e.IdCtpm).HasName("PK_ClasifiTPM_1");

            entity.ToTable("ClasifiTPM", "lib");

            entity.Property(e => e.IdCtpm).HasColumnName("IdCTPM");
            entity.Property(e => e.Ctpmenglis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CTPMEnglis");
            entity.Property(e => e.Ctpmestado).HasColumnName("CTPMEstado");
            entity.Property(e => e.Ctpmnom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CTPMNom");
        });

        modelBuilder.Entity<CompaniaV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Compania_V");

            entity.Property(e => e.CodRegion).HasColumnName("Cod Region");
            entity.Property(e => e.DescripcionCia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion cia");
            entity.Property(e => e.DescripcionCia2)
                .HasMaxLength(63)
                .IsUnicode(false)
                .HasColumnName("descripcion cia 2");
            entity.Property(e => e.GrupoPaisDescripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Grupo Pais Descripcion");
            entity.Property(e => e.IdDimcompania).HasColumnName("ID DIMCOMPANIA");
            entity.Property(e => e.PaisDescripcionEng)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Pais Descripcion ENG");
            entity.Property(e => e.PaisDescripcionEsp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Pais Descripcion ESP");
            entity.Property(e => e.RegionPapelera)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Region Papelera");
        });

        modelBuilder.Entity<CortCate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CortCate");

            entity.Property(e => e.Cccodigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CCCodigo");
            entity.Property(e => e.Ccdesc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CCDesc");
            entity.Property(e => e.Ccnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CCNombre");
        });

        modelBuilder.Entity<CorteDi>(entity =>
        {
            entity.HasKey(e => e.IdCorteDis);

            entity.ToTable("CorteDis", "ase");

            entity.Property(e => e.CdaccCorr)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CDAccCorr");
            entity.Property(e => e.CdfechAcci)
                .HasColumnType("datetime")
                .HasColumnName("CDFechAcci");
            entity.Property(e => e.CdfechList)
                .HasColumnType("datetime")
                .HasColumnName("CDFechList");
            entity.Property(e => e.CdisLibro).HasColumnName("CDisLibro");
            entity.Property(e => e.CdisListo).HasColumnName("CDIsListo");

            entity.HasOne(d => d.IdAsentaNavigation).WithMany(p => p.CorteDis)
                .HasForeignKey(d => d.IdAsenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CorteDis_Asenta");

            entity.HasOne(d => d.IdCategoriNavigation).WithMany(p => p.CorteDis)
                .HasForeignKey(d => d.IdCategori)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CorteDis_Categori");
        });

        modelBuilder.Entity<CorteDi1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CorteDis");

            entity.Property(e => e.Cdcategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CDCategoria");
            entity.Property(e => e.Cdcentro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CDCentro");
            entity.Property(e => e.CdcodProd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CDCodProd");
            entity.Property(e => e.Cdcorte).HasColumnName("CDCorte");
            entity.Property(e => e.Cdequipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CDEquipo");
            entity.Property(e => e.Cdexpres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CDExpres");
            entity.Property(e => e.CdfecDisc)
                .HasColumnType("datetime")
                .HasColumnName("CDFecDisc");
            entity.Property(e => e.CdfechaAsen)
                .HasColumnType("datetime")
                .HasColumnName("CDFechaAsen");
            entity.Property(e => e.Cdgrupo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CDGrupo");
            entity.Property(e => e.Cdmax).HasColumnName("CDMax");
            entity.Property(e => e.Cdmin).HasColumnName("CDMin");
            entity.Property(e => e.Cdmuestra).HasColumnName("CDMuestra");
            entity.Property(e => e.Cdnuevo).HasColumnName("CDNuevo");
            entity.Property(e => e.CdplanAcc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CDPlanAcc");
            entity.Property(e => e.Cdresuelto).HasColumnName("CDResuelto");
            entity.Property(e => e.Cdsuperv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CDSuperv");
            entity.Property(e => e.CdtipoAsen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CDTipoAsen");
            entity.Property(e => e.Cdturno).HasColumnName("CDTurno");
            entity.Property(e => e.Cdvariable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CDVariable");
        });

        modelBuilder.Entity<DeparBon>(entity =>
        {
            entity.HasKey(e => e.IdDeparBon);

            entity.ToTable("DeparBon", "bon", tb => tb.HasComment("Departamentos de los montos"));

            entity.Property(e => e.Dbestado).HasColumnName("DBEstado");
            entity.Property(e => e.Dbnombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Departamento del monto del Bono")
                .HasColumnName("DBNombre");
        });

        modelBuilder.Entity<DispDefi>(entity =>
        {
            entity.HasKey(e => e.IdDisDefi);

            entity.ToTable("DispDefi", "pnc");

            entity.Property(e => e.Dddescri)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DDDescri");
            entity.Property(e => e.Ddestado).HasColumnName("DDEstado");
            entity.Property(e => e.Ddnombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("DDNombre");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.IdDivision).HasName("PK_Division_1");

            entity.ToTable("Division", "mae");

            entity.Property(e => e.Ddetalle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DDetalle");
            entity.Property(e => e.Destado).HasColumnName("DEstado");
            entity.Property(e => e.Dfecha)
                .HasColumnType("datetime")
                .HasColumnName("DFecha");
            entity.Property(e => e.Dnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DNombre");
        });

        modelBuilder.Entity<DivisionesV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Divisiones_V");

            entity.Property(e => e.Ndivision)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NDivision");
        });

        modelBuilder.Entity<Eficiencium>(entity =>
        {
            entity.HasKey(e => e.IdEficienc);

            entity.ToTable("Eficiencia", "efi", tb =>
                {
                    tb.HasComment("Tabla de eficiencia de producción");
                    tb.HasTrigger("TR_Eficiencia_UpdateFechaAct");
                });

            entity.Property(e => e.IdEficienc).HasComment("identificación de la eficiencia");
            entity.Property(e => e.EfechaAct)
                .HasComment("Fecha de actualización del registro")
                .HasColumnType("datetime")
                .HasColumnName("EFechaAct");
            entity.Property(e => e.EfechaCrea)
                .HasComment("Fecha de creacion del registro")
                .HasColumnType("datetime")
                .HasColumnName("EFechaCrea");
            entity.Property(e => e.EfechaEfic)
                .HasComment("Fecha de la eficiencia")
                .HasColumnName("EFechaEfic");
            entity.Property(e => e.Egrupo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasComment("Grupo al cual se le asinga la eficiencia")
                .HasColumnName("EGrupo");
            entity.Property(e => e.Eobjetivo)
                .HasComment("Objetivo resultante teniendo en cuenta los tiempos programados")
                .HasColumnName("EObjetivo");
            entity.Property(e => e.EprodAdici).HasColumnName("EProdAdici");
            entity.Property(e => e.EprodReal).HasColumnName("EProdReal");
            entity.Property(e => e.Eturno)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasComment("Turno rotativo")
                .HasColumnName("ETurno");
            entity.Property(e => e.Eusuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Ficha del usuario creador del registro")
                .HasColumnName("EUsuario");
            entity.Property(e => e.Evalor)
                .HasComment("Porcentaje de la eficiencia")
                .HasColumnName("EValor");
            entity.Property(e => e.IdMaster).HasComment("identificacion de la maestra");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.Eficiencia)
                .HasForeignKey(d => d.IdMaster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Eficiencia_Master");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK_Empresa_1");

            entity.ToTable("Empresa", "mae");

            entity.Property(e => e.Edescri)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EDescri");
            entity.Property(e => e.Eestado).HasColumnName("EEstado");
            entity.Property(e => e.Efecha)
                .HasColumnType("datetime")
                .HasColumnName("EFecha");
            entity.Property(e => e.Enombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ENombre");
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

        modelBuilder.Entity<EquiMedi>(entity =>
        {
            entity.HasKey(e => e.IdEquiMedi);

            entity.ToTable("EquiMedi", "ase");

            entity.Property(e => e.Emdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("EMDescri");
            entity.Property(e => e.Emestado).HasColumnName("EMEstado");
            entity.Property(e => e.EmfechCrea)
                .HasColumnType("datetime")
                .HasColumnName("EMFechCrea");
            entity.Property(e => e.Emnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMNombre");
        });

        modelBuilder.Entity<EquipoEam>(entity =>
        {
            entity.HasKey(e => e.IdEquipo);

            entity.ToTable("EquipoEAM", "mae");

            entity.Property(e => e.EcodEquiEam)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ECodEquiEAM");
            entity.Property(e => e.EdescriEam)
                .IsUnicode(false)
                .HasColumnName("EDescriEAM");
            entity.Property(e => e.EestaEam).HasColumnName("EEstaEAM");
            entity.Property(e => e.Efecha)
                .HasColumnType("datetime")
                .HasColumnName("EFecha");
            entity.Property(e => e.EnombreEam)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENombreEAM");

            entity.HasOne(d => d.IdLineaNavigation).WithMany(p => p.EquipoEams)
                .HasForeignKey(d => d.IdLinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquipoEAM_Linea");
        });

        modelBuilder.Entity<EquiposEamV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EquiposEAM_V");

            entity.Property(e => e.Centro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CodigoEq)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoEq");
            entity.Property(e => e.Desc)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Equipo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaEq).HasColumnType("datetime");
            entity.Property(e => e.IdMaster).HasColumnName("idMaster");
            entity.Property(e => e.Linea)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lofic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LOFIC");
        });

        modelBuilder.Entity<Escalon>(entity =>
        {
            entity.HasKey(e => e.IdEscalon);

            entity.ToTable("Escalon", "bon", tb => tb.HasComment("Escalones del Bono de Produccion"));

            entity.Property(e => e.Eactualiz)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Actualizador del registro")
                .HasColumnName("EActualiz");
            entity.Property(e => e.Ecreador)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Creador del registro")
                .HasColumnName("ECreador");
            entity.Property(e => e.Eestado)
                .HasComment("1 activo 0 inactivo")
                .HasColumnName("EEstado");
            entity.Property(e => e.EfechaActu)
                .HasComment("Fecha de actualizacion del registro")
                .HasColumnType("datetime")
                .HasColumnName("EFechaActu");
            entity.Property(e => e.EfechaCrea)
                .HasComment("Fecha de creacion del registro")
                .HasColumnType("datetime")
                .HasColumnName("EFechaCrea");
            entity.Property(e => e.EmotDesc)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasComment("Motivo de la desactivacion")
                .HasColumnName("EMotDesc");
            entity.Property(e => e.EporcenMax)
                .HasComment("porcentaje maximo para alcanzar el escalon")
                .HasColumnName("EPorcenMax");
            entity.Property(e => e.EporcenMin)
                .HasComment("porcentaje minimo para alcanzar el escalon")
                .HasColumnName("EPorcenMin");
            entity.Property(e => e.Eposicion)
                .HasComment("Numero de escalon")
                .HasColumnName("EPosicion");
            entity.Property(e => e.IdAreaBon).HasComment("id del area de bonificacion");

            entity.HasOne(d => d.IdAreaBonNavigation).WithMany(p => p.Escalons)
                .HasForeignKey(d => d.IdAreaBon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Escalon_AreaBon");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("Estado", "pnc");

            entity.Property(e => e.Edescri)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EDescri");
            entity.Property(e => e.Enombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ENombre");
            entity.Property(e => e.Estatus).HasColumnName("EStatus");
        });

        modelBuilder.Entity<Estandar>(entity =>
        {
            entity.HasKey(e => e.IdEstandar);

            entity.ToTable("Estandar", "ase");

            entity.Property(e => e.Econcent).HasColumnName("EConcent");
            entity.Property(e => e.Edensid).HasColumnName("EDensid");
            entity.Property(e => e.Edescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("EDescri");
            entity.Property(e => e.Eestado).HasColumnName("EEstado");
            entity.Property(e => e.EfechaCrea)
                .HasColumnType("datetime")
                .HasColumnName("EFechaCrea");
            entity.Property(e => e.Enombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENombre");
        });

        modelBuilder.Entity<FechaProg>(entity =>
        {
            entity.HasKey(e => e.IdFechaPr);

            entity.ToTable("FechaProg", "reu");

            entity.Property(e => e.Fpcrea)
                .HasColumnType("datetime")
                .HasColumnName("FPCrea");
            entity.Property(e => e.Fpdesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FPDesc");
            entity.Property(e => e.Fpestado).HasColumnName("FPEstado");
            entity.Property(e => e.Fpmodic)
                .HasColumnType("datetime")
                .HasColumnName("FPModic");
            entity.Property(e => e.Fpprogra)
                .HasColumnType("datetime")
                .HasColumnName("FPProgra");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.FechaProgs)
                .HasForeignKey(d => d.IdMaster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FechaProg_Master");
        });

        modelBuilder.Entity<GeneralGbV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GeneralGB_V");

            entity.Property(e => e.CodRegion).HasColumnName("Cod Region");
            entity.Property(e => e.DescripcionCia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion cia");
            entity.Property(e => e.DescripcionCia2)
                .HasMaxLength(63)
                .IsUnicode(false)
                .HasColumnName("descripcion cia 2");
            entity.Property(e => e.GrupoPaisDescripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Grupo Pais Descripcion");
            entity.Property(e => e.IdDimcompania).HasColumnName("ID DIMCOMPANIA");
            entity.Property(e => e.PaisDescripcionEng)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Pais Descripcion ENG");
            entity.Property(e => e.PaisDescripcionEsp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Pais Descripcion ESP");
            entity.Property(e => e.RegionPapelera)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Region Papelera");
            entity.Property(e => e.Wdesc)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("WDESC");
            entity.Property(e => e.Wid)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("WID");
            entity.Property(e => e.Wwrkc)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("WWRKC");
        });

        modelBuilder.Entity<Identifi>(entity =>
        {
            entity.HasKey(e => e.IdIdentif);

            entity.ToTable("Identifi", "pnc");

            entity.Property(e => e.Idescri)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IDescri");
            entity.Property(e => e.Iestado).HasColumnName("IEstado");
            entity.Property(e => e.Inombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("INombre");
        });

        modelBuilder.Entity<InfoAse>(entity =>
        {
            entity.HasKey(e => e.IdInfoAse);

            entity.ToTable("InfoAse", "ase");

            entity.Property(e => e.IafechCrea)
                .HasColumnType("datetime")
                .HasColumnName("IAFechCrea");
            entity.Property(e => e.IafechReal)
                .HasColumnType("datetime")
                .HasColumnName("IAFechReal");
            entity.Property(e => e.Iaficha)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("IAFicha");
            entity.Property(e => e.IafichaCor)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("IAFichaCor");
            entity.Property(e => e.Iagrupo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("IAGrupo");
            entity.Property(e => e.Iaobser)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("IAObser");
            entity.Property(e => e.Iaturno)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("IATurno");
        });

        modelBuilder.Entity<InfoCali>(entity =>
        {
            entity.HasKey(e => e.IdInfoCali);

            entity.ToTable("InfoCali", "ase");

            entity.Property(e => e.IccodProd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ICCodProd");
            entity.Property(e => e.IcisCamPro).HasColumnName("ICIsCamPro");
            entity.Property(e => e.Iclote)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ICLote");

            entity.HasOne(d => d.IdInfoAseNavigation).WithMany(p => p.InfoCalis)
                .HasForeignKey(d => d.IdInfoAse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InfoCali_InfoAse");
        });

        modelBuilder.Entity<Ksf>(entity =>
        {
            entity.HasKey(e => e.Idksf).HasName("PK_KSF_1");

            entity.ToTable("KSF", "reu");

            entity.Property(e => e.KsfEnglish)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.KsfNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LibroNove>(entity =>
        {
            entity.HasKey(e => e.IdlibrNov);

            entity.ToTable("LibroNove", "lib");

            entity.Property(e => e.IdCtpm).HasColumnName("IdCTPM");
            entity.Property(e => e.IdEquipo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdParada)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lndiscrepa)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("LNDiscrepa");
            entity.Property(e => e.Lnfecha)
                .HasColumnType("datetime")
                .HasColumnName("LNFecha");
            entity.Property(e => e.LnfichSupe)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LNFichSupe");
            entity.Property(e => e.LnfichaRes)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LNFichaRes");
            entity.Property(e => e.Lngrupo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("LNGrupo");
            entity.Property(e => e.LnisPizUni).HasColumnName("LNIsPizUni");
            entity.Property(e => e.LnisResu).HasColumnName("LNIsResu");
            entity.Property(e => e.Lnobserv)
                .IsUnicode(false)
                .HasColumnName("LNObserv");
            entity.Property(e => e.LntiePerMi).HasColumnName("LNTiePerMi");
            entity.Property(e => e.Lnturno)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("LNTurno");

            entity.HasOne(d => d.IdAreaCarNavigation).WithMany(p => p.LibroNoves)
                .HasForeignKey(d => d.IdAreaCar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroNove_AreaCarga");

            entity.HasOne(d => d.IdCtpmNavigation).WithMany(p => p.LibroNoves)
                .HasForeignKey(d => d.IdCtpm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroNove_ClasifiTPM");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.LibroNoves)
                .HasForeignKey(d => d.IdMaster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroNove_Master");

            entity.HasOne(d => d.IdTipoNoveNavigation).WithMany(p => p.LibroNoves)
                .HasForeignKey(d => d.IdTipoNove)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroNove_TiParTP");
        });

        modelBuilder.Entity<LibroNovedadesV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("LibroNovedades_V");

            entity.Property(e => e.AreaCargador)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Area cargador");
            entity.Property(e => e.Centro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClasificacionTpm)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Clasificacion TPM");
            entity.Property(e => e.CodigoEquipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Codigo Equipo");
            entity.Property(e => e.Discrepancia)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.FichaDelRegistrador)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ficha del Registrador");
            entity.Property(e => e.Grupo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.IdDivision).HasColumnName("idDivision");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdLinea).HasColumnName("idLinea");
            entity.Property(e => e.IdPais).HasColumnName("idPais");
            entity.Property(e => e.IsReunion)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Linea)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lnfecha)
                .HasColumnType("datetime")
                .HasColumnName("LNFecha");
            entity.Property(e => e.Observacion).IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TiempoPerdido).HasColumnName("Tiempo Perdido");
            entity.Property(e => e.TipoDeNovedad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo de Novedad");
            entity.Property(e => e.Turno)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LinAre>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LinAre", "tps");

            entity.Property(e => e.Lacodigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LACodigo");
        });

        modelBuilder.Entity<Linea>(entity =>
        {
            entity.HasKey(e => e.IdLinea).HasName("PK_Linea_1");

            entity.ToTable("Linea", "mae");

            entity.Property(e => e.IdMaster).HasColumnName("idMaster");
            entity.Property(e => e.LcenCos)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LCenCos");
            entity.Property(e => e.Ldetalle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("LDetalle");
            entity.Property(e => e.Lestado).HasColumnName("LEstado");
            entity.Property(e => e.Lfecha)
                .HasColumnType("datetime")
                .HasColumnName("LFecha");
            entity.Property(e => e.Lnom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNom");
            entity.Property(e => e.Lofic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LOFIC");
        });

        modelBuilder.Entity<LineaV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Linea_V");

            entity.Property(e => e.LcenCos)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LCenCos");
            entity.Property(e => e.Linea)
                .HasMaxLength(50)
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
            entity.Property(e => e.CentroDeTrabajo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Centro de Trabajo");
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

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => e.IdMaster);

            entity.ToTable("Master", "mae");

            entity.HasIndex(e => e.IdLinea, "IX_IdLinea").IsUnique();

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.Masters)
                .HasForeignKey(d => d.IdCentro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Master_Centro");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.Masters)
                .HasForeignKey(d => d.IdDivision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Master_Division");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Masters)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Master_Empresa");

            entity.HasOne(d => d.IdLineaNavigation).WithOne(p => p.Master)
                .HasForeignKey<Master>(d => d.IdLinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Master_Linea");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Masters)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Master_Pais");
        });

        modelBuilder.Entity<MonedBon>(entity =>
        {
            entity.HasKey(e => e.IdMonedBon);

            entity.ToTable("MonedBon", "bon", tb => tb.HasComment("Tipo de moneda"));

            entity.Property(e => e.Mbdescri)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasComment("Descripción")
                .HasColumnName("MBDescri");
            entity.Property(e => e.Mbestado)
                .HasComment("1 activo, 0 inactivo")
                .HasColumnName("MBEstado");
            entity.Property(e => e.Mbfcrea)
                .HasColumnType("datetime")
                .HasColumnName("MBFCrea");
            entity.Property(e => e.Mbtipo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("tipo de moneda")
                .HasColumnName("MBTipo");
        });

        modelBuilder.Entity<Monto>(entity =>
        {
            entity.HasKey(e => e.IdMontos).HasName("PK_Montos_1");

            entity.ToTable("Montos", "per");

            entity.Property(e => e.Mesta).HasColumnName("MEsta");
            entity.Property(e => e.MfecAct)
                .HasColumnType("datetime")
                .HasColumnName("MFecAct");
            entity.Property(e => e.Muser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUser");

            entity.HasOne(d => d.IdLineaNavigation).WithMany(p => p.Montos)
                .HasForeignKey(d => d.IdLinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Montos_Linea");

            entity.HasOne(d => d.IdPuesTrabNavigation).WithMany(p => p.Montos)
                .HasForeignKey(d => d.IdPuesTrab)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Montos_PuesTrab");
        });

        modelBuilder.Entity<MontoBon>(entity =>
        {
            entity.HasKey(e => e.IdMontoBon);

            entity.ToTable("MontoBon", "bon", tb => tb.HasComment("Montos de Bonificacion"));

            entity.Property(e => e.IdEscalon).HasComment("Id del escalon");
            entity.Property(e => e.IdMaster).HasComment("Id maestra");
            entity.Property(e => e.IdMonedBon).HasComment("Id Moneda");
            entity.Property(e => e.IdNivePues).HasComment("Id Nivel de puesto");
            entity.Property(e => e.IdTipIncen).HasComment("Id de tipo de incidencia");
            entity.Property(e => e.Mestado)
                .HasComment("1 activo, 0: Inactivo")
                .HasColumnName("MEstado");
            entity.Property(e => e.Mfcreacion)
                .HasComment("Fecha de creacion del registro")
                .HasColumnType("datetime")
                .HasColumnName("MFCreacion");
            entity.Property(e => e.Mfdesac)
                .HasComment("Fecha de desactivación")
                .HasColumnType("datetime")
                .HasColumnName("MFDesac");
            entity.Property(e => e.Mmonto)
                .HasComment("Monto a cancelar")
                .HasColumnName("MMonto");
            entity.Property(e => e.MmotDesac)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasComment("Motivo de la desactivación")
                .HasColumnName("MMotDesac");
            entity.Property(e => e.MusuaCread)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("usuario creador del registro")
                .HasColumnName("MUsuaCread");
            entity.Property(e => e.MusuaDesac)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Usuario que desactivo el registro")
                .HasColumnName("MUsuaDesac");

            entity.HasOne(d => d.IdEscalonNavigation).WithMany(p => p.MontoBons)
                .HasForeignKey(d => d.IdEscalon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MontoBon_Escalon");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.MontoBons)
                .HasForeignKey(d => d.IdMaster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MontoBon_Master");

            entity.HasOne(d => d.IdMonedBonNavigation).WithMany(p => p.MontoBons)
                .HasForeignKey(d => d.IdMonedBon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MontoBon_MonedBon");

            entity.HasOne(d => d.IdNivePuesNavigation).WithMany(p => p.MontoBons)
                .HasForeignKey(d => d.IdNivePues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MontoBon_NivePues");

            entity.HasOne(d => d.IdTipIncenNavigation).WithMany(p => p.MontoBons)
                .HasForeignKey(d => d.IdTipIncen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MontoBon_TipIncen");
        });

        modelBuilder.Entity<MontosEscalonV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MontosEscalon_V");

            entity.Property(e => e.Centro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CentroCosto)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Linea)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NivePue>(entity =>
        {
            entity.HasKey(e => e.IdNivePues);

            entity.ToTable("NivePues", "bon", tb => tb.HasComment("Nivel de los puestos de trabako"));

            entity.Property(e => e.Npdescrip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("descripcion de los puestos de trabajo")
                .HasColumnName("NPDescrip");
            entity.Property(e => e.Npestado)
                .HasComment("1 activo , 0 inactivo")
                .HasColumnName("NPEstado");
            entity.Property(e => e.Npfcreaci)
                .HasComment("fecha de creacion del registro")
                .HasColumnType("datetime")
                .HasColumnName("NPFCreaci");
        });

        modelBuilder.Entity<Nivel>(entity =>
        {
            entity.HasKey(e => e.IdNivel).HasName("PK_Nivel_1");

            entity.ToTable("Nivel", "mae");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.Nivels)
                .HasForeignKey(d => d.IdMaster)
                .HasConstraintName("FK_Nivel_Master");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Nivels)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nivel_ProyectoUsr");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Nivels)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_Nivel_Rol");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Nivels)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nivel_Usuario");
        });

        modelBuilder.Entity<Objetivo>(entity =>
        {
            entity.HasKey(e => e.IdObjetivo);

            entity.ToTable("Objetivos", "efi", tb => tb.HasComment("Tabla de Objetivos de Produccion"));

            entity.Property(e => e.IdObjetivo).HasComment("Identificacion del objetivo");
            entity.Property(e => e.IdMaster).HasComment("Identificacion de la mestra");
            entity.Property(e => e.IdUnidaObj).HasComment("unidad");
            entity.Property(e => e.Ocargador)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Ficha de la persona que cargo el objetivo")
                .HasColumnName("OCargador");
            entity.Property(e => e.OcodProd)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasComment("Codigo del producto")
                .HasColumnName("OCodProd");
            entity.Property(e => e.OcodSustit)
                .HasComment("identificacion del objetivo a la que sustituyo este registro")
                .HasColumnName("OCodSustit");
            entity.Property(e => e.Oestado)
                .HasComment("Estado del objetivo (1: Activo, 0: Inactivo)")
                .HasColumnName("OEstado");
            entity.Property(e => e.OfechCrea)
                .HasComment("Fecha de Creación del objetivo")
                .HasColumnType("datetime")
                .HasColumnName("OFechCrea");
            entity.Property(e => e.OrazDesac)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Razon por lo cual se desactivo")
                .HasColumnName("ORazDesac");
            entity.Property(e => e.Osolicitante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Ficha de la persona que solicito la creacion del objetivo")
                .HasColumnName("OSolicitante");
            entity.Property(e => e.Ovalor)
                .HasComment("Valor del objetivo")
                .HasColumnName("OValor");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.Objetivos)
                .HasForeignKey(d => d.IdMaster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Objetivos_Master");

            entity.HasOne(d => d.IdUnidaObjNavigation).WithMany(p => p.Objetivos)
                .HasForeignKey(d => d.IdUnidaObj)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Objetivos_UnidaObj");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago);

            entity.ToTable("Pago", "bon", tb => tb.HasComment("Pago finales del Bono"));

            entity.Property(e => e.IdMonedBon).HasComment("Tipo de moneda del monto");
            entity.Property(e => e.Pdia)
                .HasComment("dia que corresponde el pago")
                .HasColumnName("PDia");
            entity.Property(e => e.Pficha)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("Ficha de la persona que recibe el pago")
                .HasColumnName("PFicha");
            entity.Property(e => e.Pfregistro)
                .HasComment("Fecha en la que se realizo el registro")
                .HasColumnType("datetime")
                .HasColumnName("PFRegistro");
            entity.Property(e => e.Pgrupo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasComment("Grupo del dia")
                .HasColumnName("PGrupo");
            entity.Property(e => e.Pmonto)
                .HasComment("Monto del dia")
                .HasColumnName("PMonto");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Nombre de la persona que recibe el pago")
                .HasColumnName("PNombre");
            entity.Property(e => e.Pturno)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasComment("Turno del dia")
                .HasColumnName("PTurno");

            entity.HasOne(d => d.IdDeparBonNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdDeparBon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_DeparBon");

            entity.HasOne(d => d.IdMonedBonNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMonedBon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_MonedBon");

            entity.HasOne(d => d.IdTipIncenNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdTipIncen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_TipIncen");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais);

            entity.ToTable("Pais", "mae");

            entity.Property(e => e.Pestado).HasColumnName("PEstado");
            entity.Property(e => e.Pfecha)
                .HasColumnType("datetime")
                .HasColumnName("PFecha");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PNombre");
        });

        modelBuilder.Entity<ParaTp>(entity =>
        {
            entity.HasKey(e => e.IdParaTp).HasName("PK_ParaTP_1");

            entity.ToTable("ParaTP", "tps");

            entity.Property(e => e.IdParaTp)
                .ValueGeneratedNever()
                .HasColumnName("IdParaTP");
            entity.Property(e => e.IdTiParTp).HasColumnName("IdTiParTP");
            entity.Property(e => e.Pcodigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCodigo");
            entity.Property(e => e.Pestado).HasColumnName("PEstado");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PNombre");

            entity.HasOne(d => d.IdTiParTpNavigation).WithMany(p => p.ParaTps)
                .HasForeignKey(d => d.IdTiParTp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParaTP_TiParTP");
        });

        modelBuilder.Entity<ParsiOee>(entity =>
        {
            entity.HasKey(e => e.IdParsiOee).HasName("PK_ParsiOEE_1");

            entity.ToTable("ParsiOEE", "tps");

            entity.Property(e => e.IdParsiOee)
                .ValueGeneratedNever()
                .HasColumnName("IdParsiOEE");
            entity.Property(e => e.IdTurnoTp).HasColumnName("IdTurnoTP");
            entity.Property(e => e.Pcalidad).HasColumnName("PCalidad");
            entity.Property(e => e.Pdispo).HasColumnName("PDispo");
            entity.Property(e => e.Poee).HasColumnName("POEE");
            entity.Property(e => e.Ppbueno).HasColumnName("PPBueno");
            entity.Property(e => e.Pperdido).HasColumnName("PPerdido");
            entity.Property(e => e.Ppmalo).HasColumnName("PPMalo");
            entity.Property(e => e.Prendi).HasColumnName("PRendi");
            entity.Property(e => e.Ptrabajado).HasColumnName("PTrabajado");
            entity.Property(e => e.Pvelocidad).HasColumnName("PVelocidad");

            entity.HasOne(d => d.IdTurnoTpNavigation).WithMany(p => p.ParsiOees)
                .HasForeignKey(d => d.IdTurnoTp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParsiOEE_TurnoTP");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.IdPersonal);

            entity.ToTable("Personal", "per");

            entity.Property(e => e.PeApellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PeFicha)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.PeGrupo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PeNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Plantilla>(entity =>
        {
            entity.HasKey(e => e.IdPlantilla).HasName("PK_Plantilla_1");

            entity.ToTable("Plantilla", "per");

            entity.Property(e => e.Pcentro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PCentro");
            entity.Property(e => e.PidLinea).HasColumnName("PIdLinea");
            entity.Property(e => e.PidMaestra).HasColumnName("PIdMaestra");
            entity.Property(e => e.PidPuesto).HasColumnName("PIdPuesto");
            entity.Property(e => e.Plinea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PLinea");
            entity.Property(e => e.Ppuesto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PPuesto");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.Plantillas)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plantilla_Personal");
        });

        modelBuilder.Entity<ProNoCon>(entity =>
        {
            entity.HasKey(e => e.IdProNoCon);

            entity.ToTable("ProNoCon", "pnc");

            entity.Property(e => e.IdProNoCon).HasColumnName("idProNoCon");
            entity.Property(e => e.Pnccantida).HasColumnName("PNCCantida");
            entity.Property(e => e.Pnccargador)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PNCCargador");
            entity.Property(e => e.PnccauLibe)
                .IsUnicode(false)
                .HasColumnName("PNCCauLibe");
            entity.Property(e => e.PnccodProd)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PNCCodProd");
            entity.Property(e => e.PncdesProd)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("PNCDesProd");
            entity.Property(e => e.Pncfecha)
                .HasColumnType("datetime")
                .HasColumnName("PNCFecha");
            entity.Property(e => e.PncindLibe)
                .IsUnicode(false)
                .HasColumnName("PNCIndLibe");
            entity.Property(e => e.Pnclote)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PNCLote");
            entity.Property(e => e.PncordFabr)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PNCOrdFabr");

            entity.HasOne(d => d.IdCaUnidadNavigation).WithMany(p => p.ProNoCons)
                .HasForeignKey(d => d.IdCaUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProNoCon_CaUnidad");

            entity.HasOne(d => d.IdCausaNavigation).WithMany(p => p.ProNoCons)
                .HasForeignKey(d => d.IdCausa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProNoCon_Causa");

            entity.HasOne(d => d.IdDisDefiNavigation).WithMany(p => p.ProNoCons)
                .HasForeignKey(d => d.IdDisDefi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProNoCon_DispDefi");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.ProNoCons)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProNoCon_Estado");

            entity.HasOne(d => d.IdIdentifNavigation).WithMany(p => p.ProNoCons)
                .HasForeignKey(d => d.IdIdentif)
                .HasConstraintName("FK_ProNoCon_Identifi");

            entity.HasOne(d => d.IdLugaEvenNavigation).WithMany(p => p.ProNoCons)
                .HasForeignKey(d => d.IdLugaEven)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProNoCon_Master1");

            entity.HasOne(d => d.IdProDispNavigation).WithMany(p => p.ProNoCons)
                .HasForeignKey(d => d.IdProDisp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProNoCon_PropDisp");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.ProNoCons)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProNoCon_Tipo");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("Producto", "ase");

            entity.Property(e => e.Pcodigo)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("PCodigo");
            entity.Property(e => e.Pdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("PDescri");
            entity.Property(e => e.Pestado).HasColumnName("PEstado");
            entity.Property(e => e.PfechaCrea)
                .HasColumnType("datetime")
                .HasColumnName("PFechaCrea");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PNombre");

            entity.HasOne(d => d.IdTipoProdNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipoProd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_TipoProd");
        });

        modelBuilder.Entity<ProductoNoConformeV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductoNoConforme_V");

            entity.Property(e => e.AlternativaPropuestoDeDisposicion)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Alternativa Propuesto De Disposicion");
            entity.Property(e => e.CausaDeLiberación)
                .IsUnicode(false)
                .HasColumnName("Causa de Liberación");
            entity.Property(e => e.Causante)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CodigoDeProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Codigo de Producto");
            entity.Property(e => e.DescripcionDeProducto)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Descripcion de Producto");
            entity.Property(e => e.DisposicionDefinitiva)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Disposicion Definitiva");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FichaDelCargador)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Ficha del Cargador");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.IndicacionDeLiberacion)
                .IsUnicode(false)
                .HasColumnName("Indicacion de Liberacion");
            entity.Property(e => e.Lote)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LugarEvento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Lugar Evento");
            entity.Property(e => e.NoConformidad)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("No Conformidad");
            entity.Property(e => e.OrdenDeFabricacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Orden de Fabricacion");
            entity.Property(e => e.Tipo)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Unidad)
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

        modelBuilder.Entity<PropDisp>(entity =>
        {
            entity.HasKey(e => e.IdProDisp);

            entity.ToTable("PropDisp", "pnc");

            entity.Property(e => e.Pddescri)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PDDescri");
            entity.Property(e => e.Pdestado).HasColumnName("PDEstado");
            entity.Property(e => e.Pdnombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("PDNombre");
        });

        modelBuilder.Entity<ProyectoUsr>(entity =>
        {
            entity.HasKey(e => e.IdProyecto);

            entity.ToTable("ProyectoUsr", "mae");

            entity.Property(e => e.Pestado).HasColumnName("PEstado");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PNombre");
        });

        modelBuilder.Entity<PuesTrab>(entity =>
        {
            entity.HasKey(e => e.IdPuesTrab).HasName("PK_PuesTrab_1");

            entity.ToTable("PuesTrab", "per");

            entity.Property(e => e.Ptdescri)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PTDescri");
            entity.Property(e => e.Ptesta).HasColumnName("PTEsta");
            entity.Property(e => e.Ptnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PTNombre");
            entity.Property(e => e.Ptorden).HasColumnName("PTOrden");

            entity.HasOne(d => d.IdNivePuesNavigation).WithMany(p => p.PuesTrabs)
                .HasForeignKey(d => d.IdNivePues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PuesTrab_NivePues");
        });

        modelBuilder.Entity<Rango>(entity =>
        {
            entity.HasKey(e => e.IdRango).HasName("PK_Rago");

            entity.ToTable("Rango", "ase");

            entity.Property(e => e.Ractivo).HasColumnName("RActivo");
            entity.Property(e => e.RfechaCrea)
                .HasColumnType("datetime")
                .HasColumnName("RFechaCrea");
            entity.Property(e => e.RfechaDesa).HasColumnName("RFechaDesa");
            entity.Property(e => e.RlimMax).HasColumnName("RLimMax");
            entity.Property(e => e.RlimMin).HasColumnName("RLimMin");
            entity.Property(e => e.Rmax).HasColumnName("RMax");
            entity.Property(e => e.Rmin).HasColumnName("RMin");
            entity.Property(e => e.RmotiDesa)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("RMotiDesa");
            entity.Property(e => e.Robj).HasColumnName("RObj");
            entity.Property(e => e.Rorden).HasColumnName("ROrden");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.Rangos)
                .HasForeignKey(d => d.IdMaster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rango_Master");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Rangos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rango_Producto");

            entity.HasOne(d => d.IdVariableNavigation).WithMany(p => p.Rangos)
                .HasForeignKey(d => d.IdVariable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rango_Variable");
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
                .HasMaxLength(9)
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

        modelBuilder.Entity<RegistroPersonalV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RegistroPersonal_V");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Centro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CentroCosto)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Dia)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.FechaDeTrabajo)
                .HasColumnType("datetime")
                .HasColumnName("Fecha de Trabajo");
            entity.Property(e => e.Ficha)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Grupo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Linea)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mes)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Suplido)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.TipoDeIncidencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo de Incidencia");
            entity.Property(e => e.TipoDeSuplencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo de Suplencia");
        });

        modelBuilder.Entity<RespoReu>(entity =>
        {
            entity.HasKey(e => e.IdResReu).HasName("PK_RespoReu_1");

            entity.ToTable("RespoReu", "reu");

            entity.Property(e => e.Rrdesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RRDesc");
            entity.Property(e => e.Rrenglish)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RREnglish");
            entity.Property(e => e.Rresta).HasColumnName("RREsta");
            entity.Property(e => e.Rrnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RRNombre");
        });

        modelBuilder.Entity<Resuman>(entity =>
        {
            entity.HasKey(e => e.IdResumen);

            entity.ToTable("Resumen", "per");

            entity.Property(e => e.Rfecha)
                .HasColumnType("datetime")
                .HasColumnName("RFecha");
            entity.Property(e => e.RfechaReal)
                .HasColumnType("datetime")
                .HasColumnName("RFechaReal");
            entity.Property(e => e.Rgrupo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("RGrupo");
            entity.Property(e => e.RhoraTrab).HasColumnName("RHoraTrab");
            entity.Property(e => e.Rsuplido)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("RSuplido");
            entity.Property(e => e.Rturno).HasColumnName("RTurno");
            entity.Property(e => e.RuserVali)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RUserVali");

            entity.HasOne(d => d.IdMontosNavigation).WithMany(p => p.Resumen)
                .HasForeignKey(d => d.IdMontos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resumen_Montos");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.Resumen)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resumen_Personal");

            entity.HasOne(d => d.IdTipIncenNavigation).WithMany(p => p.Resumen)
                .HasForeignKey(d => d.IdTipIncen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resumen_TipIncen");

            entity.HasOne(d => d.IdTipSupleNavigation).WithMany(p => p.Resumen)
                .HasForeignKey(d => d.IdTipSuple)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resumen_TipSuple");
        });

        modelBuilder.Entity<ReuDiaV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ReuDia_V");

            entity.Property(e => e.KsfNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rdarea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDArea");
            entity.Property(e => e.Rdcentro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDCentro");
            entity.Property(e => e.RdcodDis)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("RDCodDis");
            entity.Property(e => e.RdcodEq)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDCodEq");
            entity.Property(e => e.Rddisc)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RDDisc");
            entity.Property(e => e.Rddiv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDDiv");
            entity.Property(e => e.RdfecCrea)
                .HasColumnType("datetime")
                .HasColumnName("RDFecCrea");
            entity.Property(e => e.RdfecReu)
                .HasColumnType("datetime")
                .HasColumnName("RDFecReu");
            entity.Property(e => e.RdfecTra)
                .HasColumnType("datetime")
                .HasColumnName("RDFecTra");
            entity.Property(e => e.RdnumDis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDNumDis");
            entity.Property(e => e.Rdobs)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("RDObs");
            entity.Property(e => e.Rdodt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDOdt");
            entity.Property(e => e.RdplanAcc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("RDPlanAcc");
            entity.Property(e => e.Rdstatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDStatus");
            entity.Property(e => e.Rdtiempo).HasColumnName("RDTiempo");
            entity.Property(e => e.Rrnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RRNombre");
        });

        modelBuilder.Entity<Reunion>(entity =>
        {
            entity.HasKey(e => e.IdReuDia).HasName("PK_ReuDia_1");

            entity.ToTable("Reunion", "reu");

            entity.Property(e => e.OrigenCal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rdarea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDArea");
            entity.Property(e => e.Rdcentro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDCentro");
            entity.Property(e => e.RdcodDis)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("RDCodDis");
            entity.Property(e => e.RdcodEq)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDCodEq");
            entity.Property(e => e.RdcodRequi)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("RDCodRequi");
            entity.Property(e => e.Rddisc)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RDDisc");
            entity.Property(e => e.Rddiv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDDiv");
            entity.Property(e => e.RdfecCrea)
                .HasColumnType("datetime")
                .HasColumnName("RDFecCrea");
            entity.Property(e => e.RdfecReu)
                .HasColumnType("datetime")
                .HasColumnName("RDFecReu");
            entity.Property(e => e.RdfecTra)
                .HasColumnType("datetime")
                .HasColumnName("RDFecTra");
            entity.Property(e => e.RdnumDis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDNumDis");
            entity.Property(e => e.Rdobs)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("RDObs");
            entity.Property(e => e.Rdodt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDOdt");
            entity.Property(e => e.RdplanAcc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("RDPlanAcc");
            entity.Property(e => e.Rdstatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDStatus");
            entity.Property(e => e.Rdtiempo).HasColumnName("RDTiempo");

            entity.HasOne(d => d.IdCausaCalNavigation).WithMany(p => p.Reunions)
                .HasForeignKey(d => d.IdCausaCal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReuDia_CausaCal");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.Reunions)
                .HasForeignKey(d => d.IdMaster)
                .HasConstraintName("FK_ReuDia_Master");

            entity.HasOne(d => d.IdResReuNavigation).WithMany(p => p.Reunions)
                .HasForeignKey(d => d.IdResReu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReuDia_RespoReu");

            entity.HasOne(d => d.IdTipReuNavigation).WithMany(p => p.Reunions)
                .HasForeignKey(d => d.IdTipReu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReuDia_TipReu");

            entity.HasOne(d => d.IdksfNavigation).WithMany(p => p.Reunions)
                .HasForeignKey(d => d.Idksf)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReuDia_KSF");
        });

        modelBuilder.Entity<Reunion2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Reunion2", "reu");

            entity.Property(e => e.IdReuDia).ValueGeneratedOnAdd();
            entity.Property(e => e.OrigenCal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rdarea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDArea");
            entity.Property(e => e.Rdcentro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDCentro");
            entity.Property(e => e.RdcodDis)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("RDCodDis");
            entity.Property(e => e.RdcodEq)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDCodEq");
            entity.Property(e => e.RdcodRequi)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("RDCodRequi");
            entity.Property(e => e.Rddisc)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("RDDisc");
            entity.Property(e => e.Rddiv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDDiv");
            entity.Property(e => e.RdfecCrea)
                .HasColumnType("datetime")
                .HasColumnName("RDFecCrea");
            entity.Property(e => e.RdfecReu).HasColumnName("RDFecReu");
            entity.Property(e => e.RdfecTra).HasColumnName("RDFecTra");
            entity.Property(e => e.RdnumDis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDNumDis");
            entity.Property(e => e.Rdobs)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("RDObs");
            entity.Property(e => e.Rdodt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDOdt");
            entity.Property(e => e.RdplanAcc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("RDPlanAcc");
            entity.Property(e => e.Rdstatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDStatus");
            entity.Property(e => e.Rdtiempo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RDTiempo");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK_Rol_1");

            entity.ToTable("Rol", "mae");

            entity.Property(e => e.Rdescri)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("RDescri");
            entity.Property(e => e.Restado).HasColumnName("REstado");
            entity.Property(e => e.Rnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RNombre");
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.HasKey(e => e.IdSeccion);

            entity.ToTable("Seccion", "ase");

            entity.Property(e => e.Sdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("SDescri");
            entity.Property(e => e.Sestado).HasColumnName("SEstado");
            entity.Property(e => e.SfechaCrea)
                .HasColumnType("datetime")
                .HasColumnName("SFechaCrea");
            entity.Property(e => e.Snombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SNombre");
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

        modelBuilder.Entity<TabuladoresBonoV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TabuladoresBono_V");

            entity.Property(e => e.AreaEscalon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Area Escalon");
            entity.Property(e => e.Linea)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Planta)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PorcentajeMax).HasColumnName("Porcentaje Max");
            entity.Property(e => e.PorcentajeMin).HasColumnName("Porcentaje Min");
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoIncidencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo Incidencia");
        });

        modelBuilder.Entity<TiParTp>(entity =>
        {
            entity.HasKey(e => e.IdTiParTp).HasName("PK_TiParTP_1");

            entity.ToTable("TiParTP", "tps");

            entity.Property(e => e.IdTiParTp)
                .ValueGeneratedNever()
                .HasColumnName("IdTiParTP");
            entity.Property(e => e.Tpcodigo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TPCodigo");
            entity.Property(e => e.Tpenglish)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TPEnglish");
            entity.Property(e => e.Tpestado).HasColumnName("TPEstado");
            entity.Property(e => e.Tpnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TPNombre");
        });

        modelBuilder.Entity<TieEjeTp>(entity =>
        {
            entity.HasKey(e => e.IdTieEjeTp).HasName("PK_TieEjeTP_1");

            entity.ToTable("TieEjeTP", "tps");

            entity.Property(e => e.IdTieEjeTp)
                .ValueGeneratedNever()
                .HasColumnName("IdTieEjeTP");
            entity.Property(e => e.IdParsiOee).HasColumnName("IdParsiOEE");
            entity.Property(e => e.Tebueno).HasColumnName("TEBueno");
            entity.Property(e => e.Teduracion).HasColumnName("TEDuracion");
            entity.Property(e => e.Tefechaf)
                .HasColumnType("datetime")
                .HasColumnName("TEFechaf");
            entity.Property(e => e.Tefechai)
                .HasColumnType("datetime")
                .HasColumnName("TEFechai");
            entity.Property(e => e.Temalo).HasColumnName("TEMalo");
            entity.Property(e => e.TenumVuelt).HasColumnName("TENumVuelt");
            entity.Property(e => e.Teproducidos).HasColumnName("TEProducidos");
            entity.Property(e => e.Tevelocidad).HasColumnName("TEVelocidad");

            entity.HasOne(d => d.IdParsiOeeNavigation).WithMany(p => p.TieEjeTps)
                .HasForeignKey(d => d.IdParsiOee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TieEjeTP_ParsiOEE");
        });

        modelBuilder.Entity<TieParTp>(entity =>
        {
            entity.HasKey(e => e.IdTieParTp).HasName("PK_TieParTP_1");

            entity.ToTable("TieParTP", "tps");

            entity.Property(e => e.IdTieParTp).HasColumnName("IdTieParTP");
            entity.Property(e => e.IdParaTp).HasColumnName("IdParaTP");
            entity.Property(e => e.IdParsiOee).HasColumnName("IdParsiOEE");
            entity.Property(e => e.Teduracion).HasColumnName("TEDuracion");
            entity.Property(e => e.Tefechaf)
                .HasColumnType("datetime")
                .HasColumnName("TEFechaf");
            entity.Property(e => e.Tefechai)
                .HasColumnType("datetime")
                .HasColumnName("TEFechai");

            entity.HasOne(d => d.IdAreAfectNavigation).WithMany(p => p.TieParTps)
                .HasForeignKey(d => d.IdAreAfect)
                .HasConstraintName("FK_TieParTP_AreAfect");

            entity.HasOne(d => d.IdParaTpNavigation).WithMany(p => p.TieParTps)
                .HasForeignKey(d => d.IdParaTp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TieParTP_ParaTP");

            entity.HasOne(d => d.IdParsiOeeNavigation).WithMany(p => p.TieParTps)
                .HasForeignKey(d => d.IdParsiOee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TieParTP_ParsiOEE");
        });

        modelBuilder.Entity<TieProgr>(entity =>
        {
            entity.HasKey(e => e.IdTieProgr);

            entity.ToTable("TieProgr", "efi", tb =>
                {
                    tb.HasComment("Tiempo programdos de las maquina");
                    tb.HasTrigger("TR_TieProgr_UpdateFechaAct");
                });

            entity.Property(e => e.IdTieProgr).HasComment("identificacion del tiempo programado");
            entity.Property(e => e.IdMaster).HasComment("identificacion de la maestra");
            entity.Property(e => e.TpfechaAct)
                .HasComment("Fecha de modificacion del registro")
                .HasColumnType("datetime")
                .HasColumnName("TPFechaAct");
            entity.Property(e => e.TpfechaPro)
                .HasComment("Fecha de las horas programadas")
                .HasColumnName("TPFechaPro");
            entity.Property(e => e.TpfechaReg)
                .HasComment("Fecha de creción del registro")
                .HasColumnType("datetime")
                .HasColumnName("TPFechaReg");
            entity.Property(e => e.TphorasPro)
                .HasComment("Horas programadas")
                .HasColumnName("TPHorasPro");
            entity.Property(e => e.Tpjustific)
                .IsUnicode(false)
                .HasComment("Justificacion de las horas programadas")
                .HasColumnName("TPJustific");
            entity.Property(e => e.Tpturno)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("TPTurno");
            entity.Property(e => e.Tpusuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Ficha del usuario que creo el registro")
                .HasColumnName("TPUsuario");

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.TieProgrs)
                .HasForeignKey(d => d.IdMaster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TieProgr_Master");
        });

        modelBuilder.Entity<TipIncen>(entity =>
        {
            entity.HasKey(e => e.IdTipIncen).HasName("PK_TipIncen_1");

            entity.ToTable("TipIncen", "per");

            entity.Property(e => e.Tidesc)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("TIDesc");
            entity.Property(e => e.Tiesta).HasColumnName("TIEsta");
            entity.Property(e => e.Tinombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TINombre");
        });

        modelBuilder.Entity<TipReu>(entity =>
        {
            entity.HasKey(e => e.IdTipReu);

            entity.ToTable("TipReu", "reu");

            entity.Property(e => e.IdTipReu).HasColumnName("idTipReu");
            entity.Property(e => e.Tpdescri)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TPDescri");
            entity.Property(e => e.Tpestado).HasColumnName("TPEstado");
            entity.Property(e => e.Tpfecha)
                .HasColumnType("datetime")
                .HasColumnName("TPFecha");
            entity.Property(e => e.Tpnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TPNombre");
        });

        modelBuilder.Entity<TipSuple>(entity =>
        {
            entity.HasKey(e => e.IdTipSuple);

            entity.ToTable("TipSuple", "per");

            entity.Property(e => e.Tscausa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TSCausa");
            entity.Property(e => e.Tsdescri)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TSDescri");
            entity.Property(e => e.Tsestado).HasColumnName("TSEstado");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.IdTipo);

            entity.ToTable("Tipo", "pnc");

            entity.Property(e => e.Tdescri)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TDescri");
            entity.Property(e => e.Testado).HasColumnName("TEstado");
            entity.Property(e => e.Tnombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("TNombre");
        });

        modelBuilder.Entity<TipoProd>(entity =>
        {
            entity.HasKey(e => e.IdTipoProd);

            entity.ToTable("TipoProd", "ase", tb => tb.HasComment("Tipo de producto"));

            entity.Property(e => e.Tpdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("TPDescri");
            entity.Property(e => e.Tpestado).HasColumnName("TPEstado");
            entity.Property(e => e.TpfechCrea)
                .HasColumnType("datetime")
                .HasColumnName("TPFechCrea");
            entity.Property(e => e.Tpnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TPNombre");
        });

        modelBuilder.Entity<TipoVar>(entity =>
        {
            entity.HasKey(e => e.IdTipoVar);

            entity.ToTable("TipoVar", "ase");

            entity.Property(e => e.Tvdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("TVDescri");
            entity.Property(e => e.Tvestado).HasColumnName("TVEstado");
            entity.Property(e => e.TvfechCrea)
                .HasColumnType("datetime")
                .HasColumnName("TVFechCrea");
            entity.Property(e => e.Tvnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TVNombre");
        });

        modelBuilder.Entity<TurnoTp>(entity =>
        {
            entity.HasKey(e => e.IdTurnoTp).HasName("PK_TurnoTP_1");

            entity.ToTable("TurnoTP", "tps");

            entity.Property(e => e.IdTurnoTp)
                .ValueGeneratedNever()
                .HasColumnName("IdTurnoTP");
            entity.Property(e => e.Tcalidad).HasColumnName("TCalidad");
            entity.Property(e => e.TcodiProdu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TCodiProdu");
            entity.Property(e => e.Tdispo).HasColumnName("TDispo");
            entity.Property(e => e.Tfecha)
                .HasColumnType("datetime")
                .HasColumnName("TFecha");
            entity.Property(e => e.Toee).HasColumnName("TOEE");
            entity.Property(e => e.ToperaFich)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TOperaFich");
            entity.Property(e => e.Tpbueno).HasColumnName("TPBueno");
            entity.Property(e => e.Tperdido).HasColumnName("TPerdido");
            entity.Property(e => e.Tpmalo).HasColumnName("TPMalo");
            entity.Property(e => e.Trendi).HasColumnName("TRendi");
            entity.Property(e => e.Ttrabajado).HasColumnName("TTrabajado");
            entity.Property(e => e.Tturno)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Tvelocidad).HasColumnName("TVelocidad");
        });

        modelBuilder.Entity<UnidaObj>(entity =>
        {
            entity.HasKey(e => e.IdUnidaObj);

            entity.ToTable("UnidaObj", "efi", tb => tb.HasComment("Unidades de los objetivos de produccion"));

            entity.Property(e => e.Uodescri)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Descripción de la unidad")
                .HasColumnName("UODescri");
            entity.Property(e => e.Uoestado)
                .HasComment("1 activo , 0 Inactivo")
                .HasColumnName("UOEstado");
            entity.Property(e => e.Uotipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Unidad")
                .HasColumnName("UOTipo");
        });

        modelBuilder.Entity<Unidad>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PK_Unidad_1");

            entity.ToTable("Unidad", "ase");

            entity.Property(e => e.Udescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("UDescri");
            entity.Property(e => e.Uestado).HasColumnName("UEstado");
            entity.Property(e => e.UfechaCrea)
                .HasColumnType("datetime")
                .HasColumnName("UFechaCrea");
            entity.Property(e => e.Unombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UNombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_Usuario_1");

            entity.ToTable("Usuario", "mae");

            entity.HasIndex(e => e.IdUsuario, "IX_Usuario").IsUnique();

            entity.HasIndex(e => e.UsUsuario, "UsUsuario_Usuario_1").IsUnique();

            entity.Property(e => e.UsApellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsClave)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.UsCorreo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsFicha)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsNombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UsPass)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsuariosV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Usuarios_V");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Centro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.División)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Linea)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Proyecto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
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

            entity.Property(e => e.Clasificacion)
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

        modelBuilder.Entity<Variable>(entity =>
        {
            entity.HasKey(e => e.IdVariable);

            entity.ToTable("Variable", "ase");

            entity.Property(e => e.Vdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("VDescri");
            entity.Property(e => e.Vestado).HasColumnName("VEstado");
            entity.Property(e => e.VfechaCrea)
                .HasColumnType("datetime")
                .HasColumnName("VFechaCrea");
            entity.Property(e => e.VisObser).HasColumnName("VIsObser");
            entity.Property(e => e.Vnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VNombre");

            entity.HasOne(d => d.IdClasiVarNavigation).WithMany(p => p.Variables)
                .HasForeignKey(d => d.IdClasiVar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variable_ClasiVar");

            entity.HasOne(d => d.IdEquiMediNavigation).WithMany(p => p.Variables)
                .HasForeignKey(d => d.IdEquiMedi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variable_EquiMedi");

            entity.HasOne(d => d.IdEstandarNavigation).WithMany(p => p.Variables)
                .HasForeignKey(d => d.IdEstandar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variable_Estandar");

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.Variables)
                .HasForeignKey(d => d.IdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variable_Seccion");

            entity.HasOne(d => d.IdTipoVarNavigation).WithMany(p => p.Variables)
                .HasForeignKey(d => d.IdTipoVar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variable_TipoVar");

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.Variables)
                .HasForeignKey(d => d.IdUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variable_Unidad");
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
