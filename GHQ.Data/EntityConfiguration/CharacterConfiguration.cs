using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHQ.Data.EntityConfiguration;

public class CharacterConfiguration : BaseEntityConfiguration<Character>
{
    public CharacterConfiguration() : base($"{nameof(Character)}s")
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<Character> builder)
    {
        builder.Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(256);

        builder.Property(x => x.Image)
        .HasMaxLength(256);

        builder.HasOne(x => x.Game)
            .WithMany(y => y.Characters)
            .HasForeignKey(z => z.GameId);

        builder.HasOne(x => x.Player)
            .WithMany(y => y.Characters)
            .HasForeignKey(z => z.PlayerId);

        // builder.HasMany(e => e.TraitGroups)
        //     .WithOne(e => e.Character)
        //     .HasForeignKey(e => e.CharacterId);

        // builder.HasMany(e => e.Rolls)
        //     .WithOne(e => e.Character)
        //     .HasForeignKey(e => e.CharacterId);
    }
}
