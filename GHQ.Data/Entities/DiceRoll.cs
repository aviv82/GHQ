namespace GHQ.Data.Entities;

public class DiceRoll
{
    public int DiceId { get; set; }
    public int RollId { get; set; }
    public Dice Dice { get; set; } = null!;
    public Roll Roll { get; set; } = null!;
}
