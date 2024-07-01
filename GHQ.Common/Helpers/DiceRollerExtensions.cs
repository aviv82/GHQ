using GHQ.Common.Enums;

namespace GHQ.Common.Helpers;

public static class DiceRollerExtensions
{
    public static List<int> DicePoolRoller(List<DiceType> dicePool)
    {
        List<int> results = [];

        foreach (var dice in dicePool)
        {
            results.Add(DiceRoller(dice));
        }

        return results;
    }
    public static List<int> DicePoolRoller(List<int> dicePool)
    {
        List<int> results = [];

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
