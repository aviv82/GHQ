using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHQ.Data.EntityConfiguration;

// public class RollConfiguration : BaseEntityConfiguration<Roll>
// {
// public RollConfiguration() : base($"{nameof(Roll)}s")
// {
// }

// protected override void ConfigureEntity(EntityTypeBuilder<Roll> builder)
// {
//     builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
//     builder.Property(x => x.Description).HasMaxLength(256);
//     builder.Property(x => x.Difficulty);
//     builder.Property(x => x.DicePool);
//     builder.Property(x => x.Result);

//     builder.HasOne(x => x.Character)
//      .WithMany(y => y.Rolls)
//      .HasForeignKey(z => z.CharacterId);

//     builder.HasOne(x => x.Game)
//      .WithMany(y => y.Rolls)
//      .HasForeignKey(z => z.GameId);
// }
// }
