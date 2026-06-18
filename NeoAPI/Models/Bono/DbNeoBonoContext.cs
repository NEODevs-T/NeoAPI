using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.Models.Bono;

public partial class DbNeoBonoContext : DbContext
{
    public DbNeoBonoContext()
    {
    }

    public DbNeoBonoContext(DbContextOptions<DbNeoBonoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ResumEspecial> ResumEspecials { get; set; }

    public virtual DbSet<ResumEspecialAproba> ResumEspecialAprobas { get; set; }

    public virtual DbSet<Resuman> Resumen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.20.1.60\\DESARROLLO;Initial Catalog=DbNeoII_New;\nTrustServerCertificate=True;Persist Security Info=True;User ID=UsrEncNeo;Password=L3C7U3A2023*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ResumEspecial>(entity =>
        {
            entity.HasKey(e => e.IdEspecial).HasName("PK__ResumenE__19E030606630180F");

            entity.ToTable("ResumEspecial", "per");

            entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");
            entity.Property(e => e.Motivo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioSolicita)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdResumenNavigation).WithMany(p => p.ResumEspecials)
                .HasForeignKey(d => d.IdResumen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResumenEspecial_Resumen");
        });

        modelBuilder.Entity<ResumEspecialAproba>(entity =>
        {
            entity.HasKey(e => e.IdAprobacion).HasName("PK__ResumEsp__3E675ACBF38BC251");

            entity.ToTable("ResumEspecialAproba", "per");

            entity.Property(e => e.Accion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaAccion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioAprobador)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEspecialNavigation).WithMany(p => p.ResumEspecialAprobas)
                .HasForeignKey(d => d.IdEspecial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResumenEspecialAprobacion");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
