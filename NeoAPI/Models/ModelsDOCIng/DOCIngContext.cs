using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.ModelsDOCIng;

public partial class DOCIngContext : DbContext
{
    public DOCIngContext()
    {
    }

    public DOCIngContext(DbContextOptions<DOCIngContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RotaCalidum> RotaCalida { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AZTDTDB03\\DBVEN01;Initial Catalog=DOC_IngI;TrustServerCertificate=True;Persist Security Info=True;User ID=usrDocIng;Password=usrDoc08*Sq*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RotaCalidum>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Rcfecha).HasColumnName("RCFecha");
            entity.Property(e => e.Rcgrupo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("RCGrupo");
            entity.Property(e => e.RcidRotCal).HasColumnName("RCIdRotCal");
            entity.Property(e => e.Rcturno).HasColumnName("RCTurno");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
