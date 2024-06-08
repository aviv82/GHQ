namespace GHQ.Core.CharacterLogic.Requests;

public class UpdateCharacterRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Image { get; set; } = default!;
}
