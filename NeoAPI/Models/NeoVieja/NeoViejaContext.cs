using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.Models.NeoVieja;

public partial class NeoViejaContext : DbContext
{
    public NeoViejaContext()
    {
    }

    public NeoViejaContext(DbContextOptions<NeoViejaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AreaCarga> AreaCargas { get; set; }

    public virtual DbSet<ClasifiTpm> ClasifiTpms { get; set; }

    public virtual DbSet<LibroNove> LibroNoves { get; set; }

    public virtual DbSet<TiParTp> TiParTps { get; set; }
    public virtual DbSet<EquipoEamViejo> EquipoEams { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AreaCarga>(entity =>
        {
            entity.HasKey(e => e.IdAreaCarg);

            entity.ToTable("AreaCarga");

            entity.Property(e => e.IdAreaCarg).HasColumnName("idAreaCarg");
            entity.Property(e => e.Acdetalle)
                .IsUnicode(false)
                .HasColumnName("ACDetalle");
            entity.Property(e => e.Acestado).HasColumnName("ACEstado");
            entity.Property(e => e.Acnombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ACNombre");
        });

        modelBuilder.Entity<ClasifiTpm>(entity =>
        {
            entity.HasKey(e => e.IdCtpm);

            entity.ToTable("ClasifiTPM");

            entity.Property(e => e.IdCtpm).HasColumnName("IdCTPM");
            entity.Property(e => e.Ctpmestado).HasColumnName("CTPMEstado");
            entity.Property(e => e.Ctpmnom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CTPMNom");
        });

        modelBuilder.Entity<LibroNove>(entity =>
        {
            entity.HasKey(e => e.IdlibrNov).HasName("PK_LibroNovedades");

            entity.ToTable("LibroNove");

            entity.Property(e => e.IdAreaCar).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IdCtpm)
                .HasDefaultValue(5)
                .HasColumnName("IdCTPM");
            entity.Property(e => e.IdEquipo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdMaquina)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IdParada)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdTipoNove).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Lndiscrepa)
                .IsUnicode(false)
                .HasColumnName("LNDiscrepa");
            entity.Property(e => e.Lnfecha)
                .HasColumnType("datetime")
                .HasColumnName("LNFecha");
            entity.Property(e => e.LnfichaRes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNFichaRes");
            entity.Property(e => e.Lngrupo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("LNGrupo");
            entity.Property(e => e.LnisPizUni).HasColumnName("LNIsPizUni");
            entity.Property(e => e.LnisResu)
                .HasDefaultValue(2)
                .HasColumnName("LNIsResu");
            entity.Property(e => e.Lnobserv)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("LNObserv");
            entity.Property(e => e.LntiePerMi).HasColumnName("LNTiePerMi");
            entity.Property(e => e.Lnturno)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("LNTurno");

            entity.HasOne(d => d.IdAreaCarNavigation).WithMany(p => p.LibroNoves)
                .HasForeignKey(d => d.IdAreaCar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroNove_AreaCarga");

            entity.HasOne(d => d.IdCtpmNavigation).WithMany(p => p.LibroNoves)
                .HasForeignKey(d => d.IdCtpm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroNove_ClasifiTPM");

            entity.HasOne(d => d.IdTipoNoveNavigation).WithMany(p => p.LibroNoves)
                .HasForeignKey(d => d.IdTipoNove)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroNove_TiParTP");
        });

        modelBuilder.Entity<TiParTp>(entity =>
        {
            entity.HasKey(e => e.IdTiParTp);

            entity.ToTable("TiParTP", tb => tb.HasComment("tipo de parada del tiempo perdido"));

            entity.Property(e => e.IdTiParTp)
                .HasComment("identificador")
                .HasColumnName("IdTiParTP");
            entity.Property(e => e.Tpcodigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("codigo del tipo parada")
                .HasColumnName("TPCodigo");
            entity.Property(e => e.Tpestado)
                .HasComment("0: Inactivo, 1:Activo")
                .HasColumnName("TPEstado");
            entity.Property(e => e.Tpnombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("nombre del centro")
                .HasColumnName("TPNombre");
        });
        modelBuilder.Entity<EquipoEamViejo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo);

            entity.ToTable("EquipoEAM");

            entity.Property(e => e.EcodEquiEam)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ECodEquiEAM");

            entity.Property(e => e.EnombreEam)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ENombreEAM");

            entity.Property(e => e.EdescriEam)
             .IsUnicode(false)
             .HasColumnName("EDescriEAM");

            entity.Property(e => e.EestaEam).HasColumnName("EEstaEAM");

        
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
