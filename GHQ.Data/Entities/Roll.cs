using GHQ.Common.Enums;

namespace GHQ.Data.Entities;

public class Roll : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Difficulty { get; set; }
    public int? CharacterId { get; set; }
    public Character? Character { get; set; }
    public int? PlayerId { get; set; }
    public Player? Player { get; set; }
    public int? GameId { get; set; }
    public Game? Game { get; set; } = default!;
    public List<int> DicePool { get; set; } = [];
    public List<int> Result { get; set; } = [];
}
