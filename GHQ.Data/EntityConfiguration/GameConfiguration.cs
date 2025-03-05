using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHQ.Data.EntityConfiguration;

public class GameConfiguration : BaseEntityConfiguration<Game>
{
    public GameConfiguration() : base($"{nameof(Game)}s")
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<Game> builder)
    {
        builder.Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(256);

        builder.Property(x => x.Type);

        builder.HasOne(x => x.Dm)
            .WithMany(y => y.DmGames)
            .HasForeignKey(z => z.DmId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // builder.HasMany(e => e.Players)
        //     .WithOne(x => x.Game)
        //     .HasForeignKey(x => x.GameId)
        //     .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Players)
                .WithMany(e => e.PlayerGames)
                .UsingEntity<PlayerGame>();
        // .UsingEntity<Dictionary<string, object>>(
        //     "PlayerGames",
        //     x => x.HasOne<Player>().WithMany().OnDelete(DeleteBehavior.Cascade),
        //     x => x.HasOne<Game>().WithMany().OnDelete(DeleteBehavior.NoAction));

        builder.HasMany(e => e.Characters)
            .WithOne(e => e.Game)
            .HasForeignKey(e => e.GameId)
            .OnDelete(DeleteBehavior.NoAction);

        // builder.HasMany(e => e.Rolls)
        //     .WithOne(e => e.Game)
        //     .HasForeignKey(e => e.GameId);
    }
}
