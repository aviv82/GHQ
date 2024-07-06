using GHQ.Common.Enums;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;

namespace GHQ.Core.GameLogic.Requests;

public class UpdateGameRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public GameType? Type { get; set; }
    public int? DmId { get; set; }
    public List<PlayerDto>? Players { get; set; } = [];
}
