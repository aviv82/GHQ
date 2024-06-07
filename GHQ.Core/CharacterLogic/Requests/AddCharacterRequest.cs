namespace GHQ.Core.CharacterLogic.Requests;

public class AddCharacterRequest
{
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
    public int GameId { get; set; }
    public int PlayerId { get; set; }
}
