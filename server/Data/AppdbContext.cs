using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using ArchiveCorr.Models.Appdb;

namespace ArchiveCorr.Data
{
  public partial class AppdbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public AppdbContext(DbContextOptions<AppdbContext> options):base(options)
    {
    }

    public AppdbContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ArchiveCorr.Models.Appdb.Courrier>()
              .HasOne(i => i.Structure)
              .WithMany(i => i.Courriers)
              .HasForeignKey(i => i.Structurid)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<ArchiveCorr.Models.Appdb.Courrier>()
              .HasOne(i => i.TypesCourrier)
              .WithMany(i => i.Courriers)
              .HasForeignKey(i => i.Typeid)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<ArchiveCorr.Models.Appdb.Courrier>()
              .HasOne(i => i.Category)
              .WithMany(i => i.Courriers)
              .HasForeignKey(i => i.Categorid)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<ArchiveCorr.Models.Appdb.Courrier>()
              .HasOne(i => i.Correspondant)
              .WithMany(i => i.Courriers)
              .HasForeignKey(i => i.Correspid)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<ArchiveCorr.Models.Appdb.Document>()
              .HasOne(i => i.Courrier)
              .WithMany(i => i.Documents)
              .HasForeignKey(i => i.Courriid)
              .HasPrincipalKey(i => i.courid);

        builder.Entity<ArchiveCorr.Models.Appdb.Courrier>()
              .Property(p => p.Objet)
              .HasDefaultValueSql("''::text");

        this.OnModelBuilding(builder);
    }


    public DbSet<ArchiveCorr.Models.Appdb.Category> Categories
    {
      get;
      set;
    }

    public DbSet<ArchiveCorr.Models.Appdb.Correspondant> Correspondants
    {
      get;
      set;
    }

    public DbSet<ArchiveCorr.Models.Appdb.Courrier> Courriers
    {
      get;
      set;
    }

    public DbSet<ArchiveCorr.Models.Appdb.Document> Documents
    {
      get;
      set;
    }

    public DbSet<ArchiveCorr.Models.Appdb.Structure> Structures
    {
      get;
      set;
    }

    public DbSet<ArchiveCorr.Models.Appdb.TypesCourrier> TypesCourriers
    {
      get;
      set;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
    }
  }
}
