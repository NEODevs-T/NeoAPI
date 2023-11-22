using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.Models;

public partial class DbNeoIiContext : DbContext
{
    public DbNeoIiContext()
    {
    }

    public DbNeoIiContext(DbContextOptions<DbNeoIiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asentum> Asenta { get; set; }

    public virtual DbSet<BloqRang> BloqRangs { get; set; }

    public virtual DbSet<Bloque> Bloques { get; set; }

    public virtual DbSet<Categori> Categoris { get; set; }

    public virtual DbSet<Centro> Centros { get; set; }

    public virtual DbSet<ClasiVar> ClasiVars { get; set; }

    public virtual DbSet<CorteDi> CorteDis { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EquiMedi> EquiMedis { get; set; }

    public virtual DbSet<EquipoEam> EquipoEams { get; set; }

    public virtual DbSet<Estandar> Estandars { get; set; }

    public virtual DbSet<InfoAse> InfoAses { get; set; }

    public virtual DbSet<InfoCali> InfoCalis { get; set; }

    public virtual DbSet<Linea> Lineas { get; set; }

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rago> Ragos { get; set; }

    public virtual DbSet<Seccion> Seccions { get; set; }

    public virtual DbSet<TipoProd> TipoProds { get; set; }

    public virtual DbSet<TipoVar> TipoVars { get; set; }

    public virtual DbSet<Unidad> Unidads { get; set; }

    public virtual DbSet<Unidad1> Unidads1 { get; set; }

    public virtual DbSet<Variable> Variables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.20.1.60\\DESARROLLO;Initial Catalog=DbNeoII;TrustServerCertificate=True;Persist Security Info=True;User ID=UsrEncuesta;Password=Enc2022**Ing");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asentum>(entity =>
        {
            entity.HasKey(e => e.IdAsenta);

            entity.ToTable("Asenta", "ase");

            entity.Property(e => e.AisActivo).HasColumnName("AIsActivo");
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

        modelBuilder.Entity<Categori>(entity =>
        {
            entity.HasKey(e => e.IdCategori);

            entity.ToTable("Categori", "ase");

            entity.Property(e => e.Cdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("CDescri");
            entity.Property(e => e.Cesta).HasColumnName("CEsta");
            entity.Property(e => e.CfechaCrea)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("CFechaCrea");
            entity.Property(e => e.Cnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CNombre");
        });

        modelBuilder.Entity<Centro>(entity =>
        {
            entity.HasKey(e => e.IdCentro);

            entity.ToTable("Centro", "mae");

            entity.Property(e => e.Cdetalle)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("CDetalle");
            entity.Property(e => e.Cestado).HasColumnName("CEstado");
            entity.Property(e => e.Cnom)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CNom");
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

        modelBuilder.Entity<CorteDi>(entity =>
        {
            entity.HasKey(e => e.IdCorteDis);

            entity.ToTable("CorteDis", "ase");

            entity.Property(e => e.CdaccCorr)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CDAccCorr");
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

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.IdDivision).HasName("PK_Division_1");

            entity.ToTable("Division", "mae");

            entity.Property(e => e.Ddetalle)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DDetalle");
            entity.Property(e => e.Destado).HasColumnName("DEstado");
            entity.Property(e => e.Dnombre)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DNombre");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK_Empresa_1");

            entity.ToTable("Empresa", "mae");

            entity.Property(e => e.Edescri)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("EDescri");
            entity.Property(e => e.Eestado).HasColumnName("EEstado");
            entity.Property(e => e.Enombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ENombre");
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
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ECodEquiEAM");
            entity.Property(e => e.EdescriEam)
                .IsUnicode(false)
                .HasColumnName("EDescriEAM");
            entity.Property(e => e.EestaEam).HasColumnName("EEstaEAM");
            entity.Property(e => e.EnombreEam)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ENombreEAM");

            entity.HasOne(d => d.IdLineaNavigation).WithMany(p => p.EquipoEams)
                .HasForeignKey(d => d.IdLinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquipoEAM_Linea");
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

        modelBuilder.Entity<InfoAse>(entity =>
        {
            entity.HasKey(e => e.IdInfoAse);

            entity.ToTable("InfoAse", "ase");

            entity.Property(e => e.IafechCrea).HasColumnName("IAFechCrea");
            entity.Property(e => e.Iaficha)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("IAFicha");
            entity.Property(e => e.IafichaCor)
                .HasMaxLength(5)
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

        modelBuilder.Entity<Linea>(entity =>
        {
            entity.HasKey(e => e.IdLinea).HasName("PK_Linea_1");

            entity.ToTable("Linea", "mae");

            entity.Property(e => e.LcenCos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LCenCos");
            entity.Property(e => e.Ldetalle)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("LDetalle");
            entity.Property(e => e.Lestado).HasColumnName("LEstado");
            entity.Property(e => e.Lnom)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LNom");
            entity.Property(e => e.Lofic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOFIC");
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => e.IdMaster);

            entity.ToTable("Master", "mae");

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

            entity.HasOne(d => d.IdLineaNavigation).WithMany(p => p.Masters)
                .HasForeignKey(d => d.IdLinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Master_Linea");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Masters)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Master_Pais");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais);

            entity.ToTable("Pais", "mae");

            entity.Property(e => e.Pestado).HasColumnName("PEstado");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PNombre");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("Producto", "ase");

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

        modelBuilder.Entity<Rago>(entity =>
        {
            entity.HasKey(e => e.IdRango);

            entity.ToTable("Rago", "ase");

            entity.Property(e => e.Ractivo).HasColumnName("RActivo");
            entity.Property(e => e.RfechaCrea).HasColumnName("RFechaCrea");
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

            entity.HasOne(d => d.IdMasterNavigation).WithMany(p => p.Ragos)
                .HasForeignKey(d => d.IdMaster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rago_Master");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Ragos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rago_Producto");

            entity.HasOne(d => d.IdVariableNavigation).WithMany(p => p.Ragos)
                .HasForeignKey(d => d.IdVariable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rago_Variable");
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
            entity.Property(e => e.SfechaCrea).HasColumnName("SFechaCrea");
            entity.Property(e => e.Snombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SNombre");
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

        modelBuilder.Entity<Unidad>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PK_Unidad_1");

            entity.ToTable("Unidad", "ase");

            entity.Property(e => e.Udescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("UDescri");
            entity.Property(e => e.Uestado).HasColumnName("UEstado");
            entity.Property(e => e.UfechaCrea).HasColumnName("UFechaCrea");
            entity.Property(e => e.Unombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UNombre");
        });

        modelBuilder.Entity<Unidad1>(entity =>
        {
            entity.HasKey(e => e.IdUnidad);

            entity.ToTable("Unidad", "pnc");

            entity.Property(e => e.IdUnidad).ValueGeneratedNever();
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

        modelBuilder.Entity<Variable>(entity =>
        {
            entity.HasKey(e => e.IdVariable);

            entity.ToTable("Variable", "ase");

            entity.Property(e => e.Vdescri)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("VDescri");
            entity.Property(e => e.Vestado).HasColumnName("VEstado");
            entity.Property(e => e.VfechaCrea).HasColumnName("VFechaCrea");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
