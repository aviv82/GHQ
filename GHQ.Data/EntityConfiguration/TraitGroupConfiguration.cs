using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHQ.Data.EntityConfiguration;

public class TraitGroupConfiguration : BaseEntityConfiguration<TraitGroup>
{
  public TraitGroupConfiguration() : base($"{nameof(TraitGroup)}s")
  {
  }

  protected override void ConfigureEntity(EntityTypeBuilder<TraitGroup> builder)
  {
    builder.Property(x => x.TraitGroupName)
    .IsRequired()
    .HasMaxLength(256);

    builder.Property(x => x.Type);

    builder.HasOne(x => x.Character)
     .WithMany(y => y.TraitGroups)
     .HasForeignKey(z => z.CharacterId);

    builder.HasMany(e => e.Traits)
        .WithOne(e => e.TraitGroup)
        .HasForeignKey(e => e.TraitGroupId);
  }
}
