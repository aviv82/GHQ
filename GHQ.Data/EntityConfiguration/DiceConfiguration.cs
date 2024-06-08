using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHQ.Data.EntityConfiguration;

public class DiceConfiguration : BaseEntityConfiguration<Dice>
{
    public DiceConfiguration() : base($"{nameof(Dice)}s")
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<Dice> builder)
    {
        builder.Property(x => x.Value).IsRequired();
        builder.Property(x => x.Result);

        builder.HasMany(e => e.Rolls)
            .WithMany(e => e.DicePool)
            .UsingEntity<DiceRoll>();
    }
}
