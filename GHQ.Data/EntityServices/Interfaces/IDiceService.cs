using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;
public interface IDiceService : IBaseService<Dice>
{
    Task<Dice?> GetDiceByValueAsync(int diceValue, CancellationToken cancellationToken);
}
