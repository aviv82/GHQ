using GHQ.Common.Enums;

namespace GHQ.Data.Entities;

public class Game : BaseEntity
{
    public string Title { get; set; } = default!;
    public GameType Type { get; set; }
    public int? DmId { get; set; }
    public Player? Dm { get; set; } = default!;
    public ICollection<Player> Players { get; set; } = [];
    // public ICollection<PlayerGame> Players { get; set; } = [];
    public ICollection<Character> Characters { get; set; } = [];

    // public ICollection<Roll> Rolls { get; set; } = [];
}
