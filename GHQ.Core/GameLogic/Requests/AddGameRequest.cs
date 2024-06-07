using GHQ.Common.Enums;

namespace GHQ.Core.GameLogic.Requests;

public class AddGameRequest
{
    public string Title { get; set; } = default!;
    public GameType Type { get; set; }
    public int DmId { get; set; }
}
