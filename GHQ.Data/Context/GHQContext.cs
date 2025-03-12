using System.Security.Cryptography.X509Certificates;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.Context;

public class GHQContext : DbContext, IGHQContext
{
    public GHQContext() { }

    public GHQContext(DbContextOptions<GHQContext> options) : base(options) { }

    public virtual DbSet<Game> Games { get; set; }
    public virtual DbSet<Character> Characters { get; set; }
    public virtual DbSet<Player> Players { get; set; }

    // public virtual DbSet<Roll> Rolls { get; set; }
    // public virtual DbSet<TraitGroup> TraitGroups { get; set; }
    // public virtual DbSet<Trait> Traits { get; set; }

    public DbSet<PlayerGame> PlayerGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GHQContext).Assembly);

        modelBuilder.Entity<PlayerGame>()
        .HasIndex(e => e.GameId);
        modelBuilder.Entity<PlayerGame>()
        .HasIndex(e => e.PlayerId);

        modelBuilder.Entity<PlayerGame>()
            .HasIndex(e => new { e.PlayerId, e.GameId })
            .IsUnique();

        RemoveCascadeDeleteBehaviors(modelBuilder);
    }

    public DbSet<T> GetSet<T>()
      where T : BaseEntity
    {
        return Set<T>();
    }

    private static void RemoveCascadeDeleteBehaviors(ModelBuilder modelBuilder)
    {
        var foreignKeysWithCascadeDelete = modelBuilder.Model.GetEntityTypes()
            .SelectMany(x => x.GetForeignKeys())
            .Where(x => !x.IsOwnership
                        && x.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var foreignKey in foreignKeysWithCascadeDelete)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
