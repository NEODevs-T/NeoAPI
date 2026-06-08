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
