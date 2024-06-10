namespace GHQ.Core.RollLogic.Requests;

public class AddRollRequest
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? Result { get; set; }
    public int GameId { get; set; }
    public int CharacterId { get; set; }
}
