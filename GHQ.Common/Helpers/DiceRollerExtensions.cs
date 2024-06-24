using GHQ.Common.Enums;

namespace GHQ.Common.Helpers;

public static class DiceRollerExtensions
{
    public static ICollection<int> DicePoolRoller(ICollection<DiceType> dicePool)
    {
        ICollection<int> results = [];

        foreach (var dice in dicePool)
        {
            results.Add(DiceRoller(dice));
        }

        return results;
    }
    public static ICollection<int> DicePoolRoller(ICollection<int> dicePool)
    {
        ICollection<int> results = [];

        foreach (var dice in dicePool)
        {
            results.Add(DiceRoller(dice));
        }

        return results;
    }
    public static int DiceRoller(DiceType dice)
    {
        Random result = new Random();

        return result.Next(1, (int)dice + 1);
    }
    public static int DiceRoller(int dice)
    {
        Random result = new Random();

        return result.Next(1, dice + 1);
    }
}
