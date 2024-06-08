using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHQ.Data.EntityConfiguration;

public class GameConfiguration : BaseEntityConfiguration<Game>
{
    public GameConfiguration() : base($"{nameof(Game)}s")
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<Game> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Type);

        builder.HasOne(x => x.Dm)
            .WithMany(y => y.DmGames)
            .HasForeignKey(z => z.DmId)
            .IsRequired();

        builder.HasMany(e => e.Players)
            .WithMany(e => e.PlayerGames)
            .UsingEntity<PlayerGame>();

        builder.HasMany(e => e.Characters)
            .WithOne(e => e.Game)
            .HasForeignKey(e => e.GameId);

        builder.HasMany(e => e.Rolls)
            .WithOne(e => e.Game)
            .HasForeignKey(e => e.GameId);
    }
}
