
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models.AS400;
using IBM.Data.Db2;

namespace NeoAPI.Models.Neo;

public partial class DbBPCSContext : DbContext
{
    public DbBPCSContext()
    {
    }

    public DbBPCSContext(DbContextOptions<DbBPCSContext> options)
        : base(options)
    {

    }
 
    public virtual DbSet<NMPP088> Nmpp088m { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NMPP088>().HasNoKey();
    }


}