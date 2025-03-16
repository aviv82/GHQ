using GHQ.Common.Enums;

namespace GHQ.Core.RollLogic.Requests;

public class AddRollRequest
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int? Difficulty { get; set; }
    public int GameId { get; set; }
    public int CharacterId { get; set; }
    public List<DiceType> DicePool { get; set; } = [];
}
