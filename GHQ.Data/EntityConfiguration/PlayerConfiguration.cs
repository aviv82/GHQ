using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHQ.Data.EntityConfiguration;

public class PlayerConfiguration : BaseEntityConfiguration<Player>
{
    public PlayerConfiguration() : base($"{nameof(Player)}s")
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<Player> builder)
    {
        builder.Property(x => x.UserName)
        .IsRequired()
        .HasMaxLength(256);

        builder.Property(x => x.Email).HasMaxLength(256);
        builder.Property(x => x.PasswordHash).HasMaxLength(1000);

        // builder.HasMany(e => e.PlayerGames)
        //     .WithOne(e => e.Player)
        //     .HasForeignKey(x => x.PlayerId)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.PlayerGames)
                .WithMany(e => e.Players)
                .UsingEntity<PlayerGame>();

        // .UsingEntity<Dictionary<string, object>>(
        //     "PlayerGames",
        //     x => x.HasOne<Game>().WithMany().OnDelete(DeleteBehavior.NoAction),
        //     x => x.HasOne<Player>().WithMany().OnDelete(DeleteBehavior.Cascade));

        builder.HasMany(e => e.DmGames)
            .WithOne(e => e.Dm)
            .HasForeignKey(e => e.DmId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.Characters)
            .WithOne(e => e.Player)
            .HasForeignKey(e => e.PlayerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
