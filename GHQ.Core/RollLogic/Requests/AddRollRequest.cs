using GHQ.Common.Enums;

namespace GHQ.Core.RollLogic.Requests;

public class AddRollRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Difficulty { get; set; }
    public int GameId { get; set; } = 0;
    public int? PlayerId { get; set; }
    public int? CharacterId { get; set; }
    public List<int> DicePool { get; set; } = [];
}
