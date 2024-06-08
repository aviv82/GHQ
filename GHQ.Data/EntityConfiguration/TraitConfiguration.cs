using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHQ.Data.EntityConfiguration;

public class TraitConfiguration : BaseEntityConfiguration<Trait>
{
    public TraitConfiguration() : base($"{nameof(Trait)}s")
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<Trait> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Value);
        builder.Property(x => x.Level);

        builder.HasOne(x => x.TraitGroup)
            .WithMany(y => y.Traits)
            .HasForeignKey(z => z.TraitGroupId)
            .IsRequired(true);
    }
}
