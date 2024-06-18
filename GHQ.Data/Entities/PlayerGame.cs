namespace GHQ.Data.Entities;

public class PlayerGame
{
    public int PlayerId { get; set; }
    public int GameId { get; set; }
    public Player Player { get; set; } = default!;
    public Game Game { get; set; } = default!;
}
