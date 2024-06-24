namespace GHQ.Data.Entities;

public class Dice : BaseEntity
{
    public int Value { get; set; }
    public int Result { get; set; }
    // public ICollection<Roll> Rolls { get; set; } = [];
}