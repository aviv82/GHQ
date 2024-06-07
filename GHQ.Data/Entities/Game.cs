using GHQ.Common.Enums;

namespace GHQ.Data.Entities;

public class Game : BaseEntity
{
    public string Title { get; set; } = default!;
    public int DmId { get; set; }
    public GameType Type { get; set; }
    public Player Dm { get; set; } = default!;
    public ICollection<Player> Players { get; set; } = default!;
    public ICollection<Character> Characters { get; set; } = default!;
    public ICollection<Roll> Rolls { get; set; } = default!;
}
