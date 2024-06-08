namespace GHQ.Data.Entities;

public class PlayerCharacter
{
    public int PlayerId { get; set; }
    public int CharacterId { get; set; }
    public Player Player { get; set; } = null!;
    public Character Character { get; set; } = null!;
}
