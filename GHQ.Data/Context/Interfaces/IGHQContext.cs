using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.Context.Interfaces;

public interface IGHQContext
{
    DbSet<Game> Games { get; set; }
    DbSet<Character> Characters { get; set; }
    DbSet<Player> Players { get; set; }

    // DbSet<Dice> Dices { get; set; }
    DbSet<Roll> Rolls { get; set; }
    DbSet<TraitGroup> TraitGroups { get; set; }
    DbSet<Trait> Traits { get; set; }

    DbSet<PlayerGame> PlayerGames { get; set; }

    // DbSet<DiceRoll> DiceRolls { get; set; }

    DbSet<T> GetSet<T>()
         where T : BaseEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
