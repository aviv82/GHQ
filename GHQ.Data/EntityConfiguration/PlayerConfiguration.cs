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
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Email).HasMaxLength(256);
        builder.Property(x => x.PasswordHash).HasMaxLength(1000);

        builder.HasMany(e => e.PlayerGames)
            .WithMany(e => e.Players)
            .UsingEntity<PlayerGame>();

        builder.HasMany(e => e.DmGames)
            .WithOne(e => e.Dm)
            .HasForeignKey(e => e.DmId);

        builder.HasMany(e => e.Characters)
            .WithOne(e => e.Player)
            .HasForeignKey(e => e.PlayerId);
    }
}
