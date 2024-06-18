namespace GHQ.Data.Entities;

public class PlayerCharacter
{
    public int PlayerId { get; set; }
    public int CharacterId { get; set; }
    public Player Player { get; set; } = default!;
    public Character Character { get; set; } = default!;
}
