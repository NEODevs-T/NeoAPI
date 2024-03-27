using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoAPI.Models.PolybaseBPCSVen;

public partial class PolybaseBPCSVenContext : DbContext
{
    public PolybaseBPCSVenContext()
    {
    }

    public PolybaseBPCSVenContext(DbContextOptions<PolybaseBPCSVenContext> options)
        : base(options)
    {
        Database.SetCommandTimeout(600);
    }

    public virtual DbSet<Fso> Fsos { get; set; }

    public virtual DbSet<Iim> Iims { get; set; }

    public virtual DbSet<Ith> Iths { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fso>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FSO", "bpcs");

            entity.Property(e => e.Sacb)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("SACB");
            entity.Property(e => e.Sclin)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("SCLIN");
            entity.Property(e => e.Scom)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("SCOM");
            entity.Property(e => e.Scord)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SCORD");
            entity.Property(e => e.Scrit)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("SCRIT");
            entity.Property(e => e.Scust)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SCUST");
            entity.Property(e => e.Sddte)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SDDTE");
            entity.Property(e => e.Secst1)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("SECST1");
            entity.Property(e => e.Secst2)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("SECST2");
            entity.Property(e => e.Secst3)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("SECST3");
            entity.Property(e => e.Secst4)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("SECST4");
            entity.Property(e => e.Seecn)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("SEECN");
            entity.Property(e => e.Seempl)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("SEEMPL");
            entity.Property(e => e.Seinus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("SEINUS");
            entity.Property(e => e.Seordt)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SEORDT");
            entity.Property(e => e.Shrsm)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("SHRSM");
            entity.Property(e => e.Shrsr)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("SHRSR");
            entity.Property(e => e.Shrss)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("SHRSS");
            entity.Property(e => e.Sid)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SID");
            entity.Property(e => e.Slast)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SLAST");
            entity.Property(e => e.Sloc)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SLOC");
            entity.Property(e => e.Soactf)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("SOACTF");
            entity.Property(e => e.Sobomm)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOBOMM");
            entity.Property(e => e.Sobuyc)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("SOBUYC");
            entity.Property(e => e.Socdt1)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOCDT1");
            entity.Property(e => e.Socdt2)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOCDT2");
            entity.Property(e => e.Socdt3)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOCDT3");
            entity.Property(e => e.Socdt4)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOCDT4");
            entity.Property(e => e.Socdt5)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOCDT5");
            entity.Property(e => e.Socell)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SOCELL");
            entity.Property(e => e.Socmpn)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("SOCMPN");
            entity.Property(e => e.Socno)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SOCNO");
            entity.Property(e => e.Socrdt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOCRDT");
            entity.Property(e => e.Socrqd)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOCRQD");
            entity.Property(e => e.Sodraw)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("SODRAW");
            entity.Property(e => e.Sodtim)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SODTIM");
            entity.Property(e => e.Sodwgs)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SODWGS");
            entity.Property(e => e.Soelin)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SOELIN");
            entity.Property(e => e.Soendt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOENDT");
            entity.Property(e => e.Soentm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SOENTM");
            entity.Property(e => e.Soentz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOENTZ");
            entity.Property(e => e.Soenus)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SOENUS");
            entity.Property(e => e.Soexdt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOEXDT");
            entity.Property(e => e.Sofac)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("SOFAC");
            entity.Property(e => e.Sofloo)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOFLOO");
            entity.Property(e => e.Sofmac)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("SOFMAC");
            entity.Property(e => e.Sofodc)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("SOFODC");
            entity.Property(e => e.Sofpln)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("SOFPLN");
            entity.Property(e => e.Sojobc)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("SOJOBC");
            entity.Property(e => e.Solot)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("SOLOT");
            entity.Property(e => e.Somndt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOMNDT");
            entity.Property(e => e.Somnpg)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SOMNPG");
            entity.Property(e => e.Somntm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SOMNTM");
            entity.Property(e => e.Somntz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOMNTZ");
            entity.Property(e => e.Somnus)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SOMNUS");
            entity.Property(e => e.Soopdt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOOPDT");
            entity.Property(e => e.Soordt)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOORDT");
            entity.Property(e => e.Sooutc)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("SOOUTC");
            entity.Property(e => e.Sopbom)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOPBOM");
            entity.Property(e => e.Sopfac)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("SOPFAC");
            entity.Property(e => e.Soplin)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("SOPLIN");
            entity.Property(e => e.Soplot)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("SOPLOT");
            entity.Property(e => e.Sopno)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("SOPNO");
            entity.Property(e => e.Sopnum)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOPNUM");
            entity.Property(e => e.Sopord)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SOPORD");
            entity.Property(e => e.Soport)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOPORT");
            entity.Property(e => e.Soprda)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("SOPRDA");
            entity.Property(e => e.Soprgm)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOPRGM");
            entity.Property(e => e.Soprio)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOPRIO");
            entity.Property(e => e.Soprtc)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("SOPRTC");
            entity.Property(e => e.Soprtf)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("SOPRTF");
            entity.Property(e => e.Soptim)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SOPTIM");
            entity.Property(e => e.Soptmz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOPTMZ");
            entity.Property(e => e.Sopurc)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("SOPURC");
            entity.Property(e => e.Sord)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SORD");
            entity.Property(e => e.Sorevl)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("SOREVL");
            entity.Property(e => e.Sortem)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SORTEM");
            entity.Property(e => e.Sosplt)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("SOSPLT");
            entity.Property(e => e.Sosts)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SOSTS");
            entity.Property(e => e.Sotcno)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SOTCNO");
            entity.Property(e => e.Sovbs)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("SOVBS");
            entity.Property(e => e.Spfcl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("SPFCL");
            entity.Property(e => e.Spofac)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("SPOFAC");
            entity.Property(e => e.Spoord)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SPOORD");
            entity.Property(e => e.Spoort)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SPOORT");
            entity.Property(e => e.Sprio)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SPRIO");
            entity.Property(e => e.Sprod)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("SPROD");
            entity.Property(e => e.Sqfin)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("SQFIN");
            entity.Property(e => e.Sqremm)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("SQREMM");
            entity.Property(e => e.Sqreq)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("SQREQ");
            entity.Property(e => e.Srdte)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SRDTE");
            entity.Property(e => e.Srsdt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SRSDT");
            entity.Property(e => e.Srstim)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SRSTIM");
            entity.Property(e => e.Srstmz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SRSTMZ");
            entity.Property(e => e.Srtime)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SRTIME");
            entity.Property(e => e.Srtmz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SRTMZ");
            entity.Property(e => e.Sstat)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("SSTAT");
            entity.Property(e => e.Sudat1)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SUDAT1");
            entity.Property(e => e.Sudat2)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("SUDAT2");
            entity.Property(e => e.Suemp1)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("SUEMP1");
            entity.Property(e => e.Suemp2)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("SUEMP2");
            entity.Property(e => e.Sutim1)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SUTIM1");
            entity.Property(e => e.Sutim2)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SUTIM2");
            entity.Property(e => e.Sutmz1)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SUTMZ1");
            entity.Property(e => e.Sutmz2)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("SUTMZ2");
            entity.Property(e => e.Swhs)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("SWHS");
            entity.Property(e => e.Swrkc)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("SWRKC");
        });

        modelBuilder.Entity<Iim>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("IIM", "bpcs");

            entity.Property(e => e.Iabbt)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("IABBT");
            entity.Property(e => e.Iabc)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IABC");
            entity.Property(e => e.Iacst)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("IACST");
            entity.Property(e => e.Iact)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IACT");
            entity.Property(e => e.Iadj)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IADJ");
            entity.Property(e => e.Iarrt)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IARRT");
            entity.Property(e => e.Iaveu)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IAVEU");
            entity.Property(e => e.Ibtch)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IBTCH");
            entity.Property(e => e.Ibusy)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IBUSY");
            entity.Property(e => e.Ibuyc)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IBUYC");
            entity.Property(e => e.Iclas)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("ICLAS");
            entity.Property(e => e.Iclng)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ICLNG");
            entity.Property(e => e.Icond)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("ICOND");
            entity.Property(e => e.Icstcg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ICSTCG");
            entity.Property(e => e.Icusa)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("ICUSA");
            entity.Property(e => e.Icwid)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ICWID");
            entity.Property(e => e.Icyc)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("ICYC");
            entity.Property(e => e.Icycf)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ICYCF");
            entity.Property(e => e.Idesc)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("IDESC");
            entity.Property(e => e.Idisc)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IDISC");
            entity.Property(e => e.Idltr)
                .HasColumnType("numeric(11, 2)")
                .HasColumnName("IDLTR");
            entity.Property(e => e.Idmc1)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IDMC1");
            entity.Property(e => e.Idmc2)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IDMC2");
            entity.Property(e => e.Idmtf)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IDMTF");
            entity.Property(e => e.Idraw)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("IDRAW");
            entity.Property(e => e.Idrf)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IDRF");
            entity.Property(e => e.Idsalw)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IDSALW");
            entity.Property(e => e.Idsce)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("IDSCE");
            entity.Property(e => e.Idscr)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IDSCR");
            entity.Property(e => e.Ifci)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IFCI");
            entity.Property(e => e.Ifcst)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("IFCST");
            entity.Property(e => e.Ifeno)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("IFENO");
            entity.Property(e => e.Ifii)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IFII");
            entity.Property(e => e.Ifrfg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IFRFG");
            entity.Property(e => e.Iglno)
                .HasMaxLength(24)
                .IsFixedLength()
                .HasColumnName("IGLNO");
            entity.Property(e => e.Igtec)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("IGTEC");
            entity.Property(e => e.Iid)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IID");
            entity.Property(e => e.Iioq)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IIOQ");
            entity.Property(e => e.Iiss)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IISS");
            entity.Property(e => e.Iityp)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IITYP");
            entity.Property(e => e.Ijit)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IJIT");
            entity.Property(e => e.Ilabl)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("ILABL");
            entity.Property(e => e.Ilcc)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("ILCC");
            entity.Property(e => e.Ildte)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("ILDTE");
            entity.Property(e => e.Ilead)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("ILEAD");
            entity.Property(e => e.Ilevl)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("ILEVL");
            entity.Property(e => e.Ilist)
                .HasColumnType("numeric(14, 4)")
                .HasColumnName("ILIST");
            entity.Property(e => e.Iloc)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ILOC");
            entity.Property(e => e.Ilots)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("ILOTS");
            entity.Property(e => e.Im3wmf)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IM3WMF");
            entity.Property(e => e.Imaall)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMAALL");
            entity.Property(e => e.Imabrv)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("IMABRV");
            entity.Property(e => e.Imafac)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMAFAC");
            entity.Property(e => e.Imainv)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMAINV");
            entity.Property(e => e.Imalot)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMALOT");
            entity.Property(e => e.Imalrq)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IMALRQ");
            entity.Property(e => e.Imamat)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("IMAMAT");
            entity.Property(e => e.Imanxn)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMANXN");
            entity.Property(e => e.Imastb)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMASTB");
            entity.Property(e => e.Imatpa)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMATPA");
            entity.Property(e => e.Imawum)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("IMAWUM");
            entity.Property(e => e.Imaxp)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IMAXP");
            entity.Property(e => e.Imaxr)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("IMAXR");
            entity.Property(e => e.Imbcom)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMBCOM");
            entity.Property(e => e.Imbdy)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMBDY");
            entity.Property(e => e.Imbgrp)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMBGRP");
            entity.Property(e => e.Imbhd)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMBHD");
            entity.Property(e => e.Imbndc)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("IMBNDC");
            entity.Property(e => e.Imbndp)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("IMBNDP");
            entity.Property(e => e.Imboxq)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMBOXQ");
            entity.Property(e => e.Imbrgc)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMBRGC");
            entity.Property(e => e.Imbupl)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMBUPL");
            entity.Property(e => e.Imbwip)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMBWIP");
            entity.Property(e => e.Imbwt)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMBWT");
            entity.Property(e => e.Imcctl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMCCTL");
            entity.Property(e => e.Imcctm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMCCTM");
            entity.Property(e => e.Imcctz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMCCTZ");
            entity.Property(e => e.Imcell)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMCELL");
            entity.Property(e => e.Imcgrp)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IMCGRP");
            entity.Property(e => e.Imcioq)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMCIOQ");
            entity.Property(e => e.Imclt1)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMCLT1");
            entity.Property(e => e.Imclt2)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMCLT2");
            entity.Property(e => e.Imclt3)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMCLT3");
            entity.Property(e => e.Imclt4)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMCLT4");
            entity.Property(e => e.Imclt5)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMCLT5");
            entity.Property(e => e.Imcmcn)
                .HasColumnType("numeric(11, 5)")
                .HasColumnName("IMCMCN");
            entity.Property(e => e.Imcmoq)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMCMOQ");
            entity.Property(e => e.Imcmth)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMCMTH");
            entity.Property(e => e.Imcnsl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMCNSL");
            entity.Property(e => e.Imcntr)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("IMCNTR");
            entity.Property(e => e.Imcntt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMCNTT");
            entity.Property(e => e.Imcnty)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMCNTY");
            entity.Property(e => e.Imcom)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMCOM");
            entity.Property(e => e.Imcomc)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("IMCOMC");
            entity.Property(e => e.Imcorp)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("IMCORP");
            entity.Property(e => e.Imcos)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("IMCOS");
            entity.Property(e => e.Imcpyn)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMCPYN");
            entity.Property(e => e.Imcqty)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMCQTY");
            entity.Property(e => e.Imcrec)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IMCREC");
            entity.Property(e => e.Imcsoq)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMCSOQ");
            entity.Property(e => e.Imcstc)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("IMCSTC");
            entity.Property(e => e.Imctz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMCTZ");
            entity.Property(e => e.Imcuom)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMCUOM");
            entity.Property(e => e.Imcusw)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("IMCUSW");
            entity.Property(e => e.Imcwum)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMCWUM");
            entity.Property(e => e.Imddif)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMDDIF");
            entity.Property(e => e.Imdihz)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMDIHZ");
            entity.Property(e => e.Imdils)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMDILS");
            entity.Property(e => e.Imdilt)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMDILT");
            entity.Property(e => e.Imdio1)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMDIO1");
            entity.Property(e => e.Imdio2)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMDIO2");
            entity.Property(e => e.Imdio3)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMDIO3");
            entity.Property(e => e.Imdio4)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMDIO4");
            entity.Property(e => e.Imdio5)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMDIO5");
            entity.Property(e => e.Imdio6)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMDIO6");
            entity.Property(e => e.Imdio7)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMDIO7");
            entity.Property(e => e.Imdit1)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMDIT1");
            entity.Property(e => e.Imdit2)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMDIT2");
            entity.Property(e => e.Imdit3)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMDIT3");
            entity.Property(e => e.Imdit4)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMDIT4");
            entity.Property(e => e.Imdit5)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMDIT5");
            entity.Property(e => e.Imdit6)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMDIT6");
            entity.Property(e => e.Imdit7)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMDIT7");
            entity.Property(e => e.Imdmcn)
                .HasColumnType("numeric(11, 5)")
                .HasColumnName("IMDMCN");
            entity.Property(e => e.Imdmun)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("IMDMUN");
            entity.Property(e => e.Imdrpc)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMDRPC");
            entity.Property(e => e.Imdsfw)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IMDSFW");
            entity.Property(e => e.Imdvsn)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMDVSN");
            entity.Property(e => e.Imecst)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("IMECST");
            entity.Property(e => e.Imendt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("IMENDT");
            entity.Property(e => e.Imentm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMENTM");
            entity.Property(e => e.Imenus)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMENUS");
            entity.Property(e => e.Imexbs)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMEXBS");
            entity.Property(e => e.Imexcs)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMEXCS");
            entity.Property(e => e.Imexhd)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMEXHD");
            entity.Property(e => e.Imexhh)
                .HasColumnType("numeric(3, 1)")
                .HasColumnName("IMEXHH");
            entity.Property(e => e.Imexpl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMEXPL");
            entity.Property(e => e.Imfadt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("IMFADT");
            entity.Property(e => e.Imfcfl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMFCFL");
            entity.Property(e => e.Imflpc)
                .HasColumnType("numeric(6, 3)")
                .HasColumnName("IMFLPC");
            entity.Property(e => e.Imflpf)
                .HasColumnType("numeric(6, 3)")
                .HasColumnName("IMFLPF");
            entity.Property(e => e.Imform)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("IMFORM");
            entity.Property(e => e.Imfpln)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMFPLN");
            entity.Property(e => e.Imfprc)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IMFPRC");
            entity.Property(e => e.Imfrmc)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IMFRMC");
            entity.Property(e => e.Imgcst)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("IMGCST");
            entity.Property(e => e.Imgpmr)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMGPMR");
            entity.Property(e => e.Imgsor)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMGSOR");
            entity.Property(e => e.Imhigh)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMHIGH");
            entity.Property(e => e.Imhrmn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMHRMN");
            entity.Property(e => e.Imhuom)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMHUOM");
            entity.Property(e => e.Imhzrd)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMHZRD");
            entity.Property(e => e.Imimcn)
                .HasColumnType("numeric(11, 5)")
                .HasColumnName("IMIMCN");
            entity.Property(e => e.Imin)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMIN");
            entity.Property(e => e.Imincl)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMINCL");
            entity.Property(e => e.Iminr)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("IMINR");
            entity.Property(e => e.Imiuom)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMIUOM");
            entity.Property(e => e.Imkban)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMKBAN");
            entity.Property(e => e.Imldgr)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IMLDGR");
            entity.Property(e => e.Imldhr)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMLDHR");
            entity.Property(e => e.Imldmn)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMLDMN");
            entity.Property(e => e.Imldte)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("IMLDTE");
            entity.Property(e => e.Imlean)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMLEAN");
            entity.Property(e => e.Imlhrs)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("IMLHRS");
            entity.Property(e => e.Imlong)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMLONG");
            entity.Property(e => e.Imlpgm)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMLPGM");
            entity.Property(e => e.Imlrtc)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMLRTC");
            entity.Property(e => e.Imltme)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMLTME");
            entity.Property(e => e.Imlttm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMLTTM");
            entity.Property(e => e.Imlttz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMLTTZ");
            entity.Property(e => e.Imltz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMLTZ");
            entity.Property(e => e.Imluom)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMLUOM");
            entity.Property(e => e.Imlusr)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMLUSR");
            entity.Property(e => e.Imlwtc)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("IMLWTC");
            entity.Property(e => e.Imlwtp)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("IMLWTP");
            entity.Property(e => e.Immfgr)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("IMMFGR");
            entity.Property(e => e.Immfmd)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMMFMD");
            entity.Property(e => e.Immibq)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMMIBQ");
            entity.Property(e => e.Immndt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("IMMNDT");
            entity.Property(e => e.Immntm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("IMMNTM");
            entity.Property(e => e.Immnus)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMMNUS");
            entity.Property(e => e.Immodl)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("IMMODL");
            entity.Property(e => e.Immstk)
                .HasColumnType("numeric(2, 1)")
                .HasColumnName("IMMSTK");
            entity.Property(e => e.Immwad)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("IMMWAD");
            entity.Property(e => e.Immwis)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("IMMWIS");
            entity.Property(e => e.Immwob)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("IMMWOB");
            entity.Property(e => e.Immwre)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("IMMWRE");
            entity.Property(e => e.Immwsa)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("IMMWSA");
            entity.Property(e => e.Imnlot)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IMNLOT");
            entity.Property(e => e.Imnnwu)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IMNNWU");
            entity.Property(e => e.Imnot1)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("IMNOT1");
            entity.Property(e => e.Imnot2)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("IMNOT2");
            entity.Property(e => e.Imnwrc)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMNWRC");
            entity.Property(e => e.Imopoc)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMOPOC");
            entity.Property(e => e.Impadj)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMPADJ");
            entity.Property(e => e.Impfmy)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("IMPFMY");
            entity.Property(e => e.Impfsg)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("IMPFSG");
            entity.Property(e => e.Impiss)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMPISS");
            entity.Property(e => e.Impkd1)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("IMPKD1");
            entity.Property(e => e.Impkd2)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("IMPKD2");
            entity.Property(e => e.Impkgi)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IMPKGI");
            entity.Property(e => e.Implc)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMPLC");
            entity.Property(e => e.Impmcn)
                .HasColumnType("numeric(11, 5)")
                .HasColumnName("IMPMCN");
            entity.Property(e => e.Impmth)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMPMTH");
            entity.Property(e => e.Impmto)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMPMTO");
            entity.Property(e => e.Impopn)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMPOPN");
            entity.Property(e => e.Impot)
                .HasColumnType("numeric(9, 4)")
                .HasColumnName("IMPOT");
            entity.Property(e => e.Impplt)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMPPLT");
            entity.Property(e => e.Imprec)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMPREC");
            entity.Property(e => e.Imprm)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMPRM");
            entity.Property(e => e.Imprqf)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMPRQF");
            entity.Property(e => e.Impsd)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMPSD");
            entity.Property(e => e.Impsp1)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMPSP1");
            entity.Property(e => e.Impsp2)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMPSP2");
            entity.Property(e => e.Impsp3)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMPSP3");
            entity.Property(e => e.Impsp4)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMPSP4");
            entity.Property(e => e.Imptyp)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMPTYP");
            entity.Property(e => e.Impuom)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMPUOM");
            entity.Property(e => e.Imqctl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMQCTL");
            entity.Property(e => e.Imqdy)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMQDY");
            entity.Property(e => e.Imqle2)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMQLE2");
            entity.Property(e => e.Imqled)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMQLED");
            entity.Property(e => e.Imqtpd)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMQTPD");
            entity.Property(e => e.Imqtyb)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMQTYB");
            entity.Property(e => e.Imrayn)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMRAYN");
            entity.Property(e => e.Imrcpr)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IMRCPR");
            entity.Property(e => e.Imrelp)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMRELP");
            entity.Property(e => e.Imrevl)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMREVL");
            entity.Property(e => e.Imrfpk)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("IMRFPK");
            entity.Property(e => e.Imrp)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMRP");
            entity.Property(e => e.Imrthd)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMRTHD");
            entity.Property(e => e.Imrthh)
                .HasColumnType("numeric(3, 1)")
                .HasColumnName("IMRTHH");
            entity.Property(e => e.Imrthr)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMRTHR");
            entity.Property(e => e.Imrtnc)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IMRTNC");
            entity.Property(e => e.Imrund)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMRUND");
            entity.Property(e => e.Imschd)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMSCHD");
            entity.Property(e => e.Imscwi)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMSCWI");
            entity.Property(e => e.Imslhr)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMSLHR");
            entity.Property(e => e.Imsls)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("IMSLS");
            entity.Property(e => e.Imsncf)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMSNCF");
            entity.Property(e => e.Imsnml)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("IMSNML");
            entity.Property(e => e.Imsnmn)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMSNMN");
            entity.Property(e => e.Imsnmx)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMSNMX");
            entity.Property(e => e.Imsohd)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMSOHD");
            entity.Property(e => e.Imsohz)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IMSOHZ");
            entity.Property(e => e.Imsorg)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IMSORG");
            entity.Property(e => e.Imspec)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("IMSPEC");
            entity.Property(e => e.Imspfc)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("IMSPFC");
            entity.Property(e => e.Imspkt)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("IMSPKT");
            entity.Property(e => e.Imsrtc)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMSRTC");
            entity.Property(e => e.Imsspc)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("IMSSPC");
            entity.Property(e => e.Imsum)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMSUM");
            entity.Property(e => e.Imtccd)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("IMTCCD");
            entity.Property(e => e.Imtdp)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMTDP");
            entity.Property(e => e.Imtdu)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMTDU");
            entity.Property(e => e.Imtlvl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMTLVL");
            entity.Property(e => e.Imtwhs)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IMTWHS");
            entity.Property(e => e.Imuma)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMUMA");
            entity.Property(e => e.Imumb)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMUMB");
            entity.Property(e => e.Imumpc)
                .HasColumnType("numeric(11, 5)")
                .HasColumnName("IMUMPC");
            entity.Property(e => e.Imumpr)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMUMPR");
            entity.Property(e => e.Imunai)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("IMUNAI");
            entity.Property(e => e.Imunop)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("IMUNOP");
            entity.Property(e => e.Imunpo)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("IMUNPO");
            entity.Property(e => e.Imunrd)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("IMUNRD");
            entity.Property(e => e.Imunrl)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("IMUNRL");
            entity.Property(e => e.Imunse)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("IMUNSE");
            entity.Property(e => e.Imupc)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("IMUPC");
            entity.Property(e => e.Imuph)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMUPH");
            entity.Property(e => e.Imusc)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMUSC");
            entity.Property(e => e.Imuscd)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IMUSCD");
            entity.Property(e => e.Imust)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMUST");
            entity.Property(e => e.Imuwtc)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("IMUWTC");
            entity.Property(e => e.Imuwtp)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("IMUWTP");
            entity.Property(e => e.Imvfry)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMVFRY");
            entity.Property(e => e.Imvmii)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IMVMII");
            entity.Property(e => e.Imvuom)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMVUOM");
            entity.Property(e => e.Imwdum)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMWDUM");
            entity.Property(e => e.Imwide)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IMWIDE");
            entity.Property(e => e.Imwrkc)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMWRKC");
            entity.Property(e => e.Imwtum)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IMWTUM");
            entity.Property(e => e.Imxin)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IMXIN");
            entity.Property(e => e.Imxpou)
                .HasColumnType("numeric(10, 1)")
                .HasColumnName("IMXPOU");
            entity.Property(e => e.Imywad)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("IMYWAD");
            entity.Property(e => e.Imywis)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("IMYWIS");
            entity.Property(e => e.Imywre)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("IMYWRE");
            entity.Property(e => e.Imywsa)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("IMYWSA");
            entity.Property(e => e.Inccfg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("INCCFG");
            entity.Property(e => e.Inseq)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("INSEQ");
            entity.Property(e => e.Ionod)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IONOD");
            entity.Property(e => e.Iopb)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IOPB");
            entity.Property(e => e.Iordc)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("IORDC");
            entity.Property(e => e.Iordp)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("IORDP");
            entity.Property(e => e.Iorign)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IORIGN");
            entity.Property(e => e.Ipack)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IPACK");
            entity.Property(e => e.Ipctk)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("IPCTK");
            entity.Property(e => e.Ipers)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IPERS");
            entity.Property(e => e.Ipfdv)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("IPFDV");
            entity.Property(e => e.Ipitm)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("IPITM");
            entity.Property(e => e.Ipody)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IPODY");
            entity.Property(e => e.Iprda)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IPRDA");
            entity.Property(e => e.Iprod)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("IPROD");
            entity.Property(e => e.Ipurc)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IPURC");
            entity.Property(e => e.Irct)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IRCT");
            entity.Property(e => e.Iref01)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("IREF01");
            entity.Property(e => e.Iref02)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("IREF02");
            entity.Property(e => e.Iref03)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("IREF03");
            entity.Property(e => e.Iref04)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("IREF04");
            entity.Property(e => e.Iref05)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("IREF05");
            entity.Property(e => e.Isact)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ISACT");
            entity.Property(e => e.Iscst)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("ISCST");
            entity.Property(e => e.Isitm)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("ISITM");
            entity.Property(e => e.Isofl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ISOFL");
            entity.Property(e => e.Istyl)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("ISTYL");
            entity.Property(e => e.Itfdy)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("ITFDY");
            entity.Property(e => e.Itgqy)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("ITGQY");
            entity.Property(e => e.Itseq)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("ITSEQ");
            entity.Property(e => e.Iumat)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("IUMAT");
            entity.Property(e => e.Iumcn)
                .HasColumnType("numeric(11, 5)")
                .HasColumnName("IUMCN");
            entity.Property(e => e.Iump)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IUMP");
            entity.Property(e => e.Iumr)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IUMR");
            entity.Property(e => e.Iumrc)
                .HasColumnType("numeric(11, 5)")
                .HasColumnName("IUMRC");
            entity.Property(e => e.Iums)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IUMS");
            entity.Property(e => e.Iuncn)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("IUNCN");
            entity.Property(e => e.Iven2)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("IVEN2");
            entity.Property(e => e.Ivend)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("IVEND");
            entity.Property(e => e.Ivuli)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IVULI");
            entity.Property(e => e.Ivulp)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("IVULP");
            entity.Property(e => e.Iwght)
                .HasColumnType("numeric(7, 3)")
                .HasColumnName("IWGHT");
            entity.Property(e => e.Iwhs)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IWHS");
            entity.Property(e => e.Iyadj)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IYADJ");
            entity.Property(e => e.Iycos)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("IYCOS");
            entity.Property(e => e.Iyiss)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IYISS");
            entity.Property(e => e.Iyrct)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IYRCT");
            entity.Property(e => e.Iysls)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("IYSLS");
            entity.Property(e => e.Iytdp)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IYTDP");
            entity.Property(e => e.Iytdu)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("IYTDU");
            entity.Property(e => e.Iyuse)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("IYUSE");
            entity.Property(e => e.Saflg)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SAFLG");
            entity.Property(e => e.Taxc1)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("TAXC1");
        });

        modelBuilder.Entity<Ith>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ITH", "bpcs");

            entity.Property(e => e.Tclas)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("TCLAS");
            entity.Property(e => e.Tcom)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("TCOM");
            entity.Property(e => e.Tdang)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("TDANG");
            entity.Property(e => e.Thacst)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("THACST");
            entity.Property(e => e.Thadte)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THADTE");
            entity.Property(e => e.Thadvn)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("THADVN");
            entity.Property(e => e.Thcby)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("THCBY");
            entity.Property(e => e.Thcdt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THCDT");
            entity.Property(e => e.Thcmt)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("THCMT");
            entity.Property(e => e.Thcmtb)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THCMTB");
            entity.Property(e => e.Thcmtg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THCMTG");
            entity.Property(e => e.Thcntr)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("THCNTR");
            entity.Property(e => e.Thcom)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("THCOM");
            entity.Property(e => e.Thcoo)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("THCOO");
            entity.Property(e => e.Thcsgn)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THCSGN");
            entity.Property(e => e.Thctm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("THCTM");
            entity.Property(e => e.Thctz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("THCTZ");
            entity.Property(e => e.Thcurr)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("THCURR");
            entity.Property(e => e.Thcwum)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("THCWUM");
            entity.Property(e => e.Thdslp)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THDSLP");
            entity.Property(e => e.Thecor)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THECOR");
            entity.Property(e => e.Thecst)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("THECST");
            entity.Property(e => e.Thert2)
                .HasColumnType("numeric(15, 7)")
                .HasColumnName("THERT2");
            entity.Property(e => e.Thesor)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THESOR");
            entity.Property(e => e.Thfac)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("THFAC");
            entity.Property(e => e.Thfamt)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("THFAMT");
            entity.Property(e => e.Thglrt)
                .HasColumnType("numeric(15, 7)")
                .HasColumnName("THGLRT");
            entity.Property(e => e.Thgrt2)
                .HasColumnType("numeric(15, 7)")
                .HasColumnName("THGRT2");
            entity.Property(e => e.Thjseq)
                .HasColumnType("numeric(16, 0)")
                .HasColumnName("THJSEQ");
            entity.Property(e => e.Thkan)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("THKAN");
            entity.Property(e => e.Thlbtk)
                .HasColumnType("numeric(7, 0)")
                .HasColumnName("THLBTK");
            entity.Property(e => e.Thlby)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("THLBY");
            entity.Property(e => e.Thldt)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THLDT");
            entity.Property(e => e.Thlin)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("THLIN");
            entity.Property(e => e.Thlpgm)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("THLPGM");
            entity.Property(e => e.Thltm)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("THLTM");
            entity.Property(e => e.Thltz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("THLTZ");
            entity.Property(e => e.Thmach)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("THMACH");
            entity.Property(e => e.Thman)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("THMAN");
            entity.Property(e => e.Thmfgf)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THMFGF");
            entity.Property(e => e.Thmfgr)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("THMFGR");
            entity.Property(e => e.Thmlot)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("THMLOT");
            entity.Property(e => e.Thmnwh)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("THMNWH");
            entity.Property(e => e.Thmrb)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("THMRB");
            entity.Property(e => e.Thnii)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THNII");
            entity.Property(e => e.Thonly)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THONLY");
            entity.Property(e => e.Thopno)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("THOPNO");
            entity.Property(e => e.Thord)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THORD");
            entity.Property(e => e.Thpkgg)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THPKGG");
            entity.Property(e => e.Thpmrb)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("THPMRB");
            entity.Property(e => e.Thprsh)
                .HasColumnType("numeric(7, 0)")
                .HasColumnName("THPRSH");
            entity.Property(e => e.Thpsor)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("THPSOR");
            entity.Property(e => e.Thqum)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("THQUM");
            entity.Property(e => e.Thrmfl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THRMFL");
            entity.Property(e => e.Thrno)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("THRNO");
            entity.Property(e => e.Thrseq)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("THRSEQ");
            entity.Property(e => e.Thrsq)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("THRSQ");
            entity.Property(e => e.Thsfbf)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("THSFBF");
            entity.Property(e => e.Thshft)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THSHFT");
            entity.Property(e => e.Thshtm)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("THSHTM");
            entity.Property(e => e.Thstme)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("THSTME");
            entity.Property(e => e.Thstz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("THSTZ");
            entity.Property(e => e.Thtexr)
                .HasColumnType("numeric(15, 7)")
                .HasColumnName("THTEXR");
            entity.Property(e => e.Thtime)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("THTIME");
            entity.Property(e => e.Thtotw)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("THTOTW");
            entity.Property(e => e.Thtowh)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("THTOWH");
            entity.Property(e => e.Thttz)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("THTTZ");
            entity.Property(e => e.Thtum)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("THTUM");
            entity.Property(e => e.Thupi)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("THUPI");
            entity.Property(e => e.Thuser)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("THUSER");
            entity.Property(e => e.Thvlot)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("THVLOT");
            entity.Property(e => e.Thwrkc)
                .HasColumnType("numeric(6, 0)")
                .HasColumnName("THWRKC");
            entity.Property(e => e.Thws)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("THWS");
            entity.Property(e => e.Tid)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("TID");
            entity.Property(e => e.Tloct)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("TLOCT");
            entity.Property(e => e.Tlot)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("TLOT");
            entity.Property(e => e.Toqty)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("TOQTY");
            entity.Property(e => e.Tpfdv)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("TPFDV");
            entity.Property(e => e.Tprflg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("TPRFLG");
            entity.Property(e => e.Tpric)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("TPRIC");
            entity.Property(e => e.Tprod)
                .HasMaxLength(35)
                .IsFixedLength()
                .HasColumnName("TPROD");
            entity.Property(e => e.Tqty)
                .HasColumnType("numeric(11, 3)")
                .HasColumnName("TQTY");
            entity.Property(e => e.Tref)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("TREF");
            entity.Property(e => e.Trefm)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("TREFM");
            entity.Property(e => e.Tres)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("TRES");
            entity.Property(e => e.Tscst)
                .HasColumnType("numeric(15, 5)")
                .HasColumnName("TSCST");
            entity.Property(e => e.Tsdte)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("TSDTE");
            entity.Property(e => e.Tseq)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("TSEQ");
            entity.Property(e => e.Tstat)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("TSTAT");
            entity.Property(e => e.Ttdte)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("TTDTE");
            entity.Property(e => e.Ttype)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("TTYPE");
            entity.Property(e => e.Tval)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("TVAL");
            entity.Property(e => e.Tvend)
                .HasColumnType("numeric(8, 0)")
                .HasColumnName("TVEND");
            entity.Property(e => e.Twhs)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("TWHS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
