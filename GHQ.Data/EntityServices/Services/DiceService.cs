using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;
public class DiceService : BaseService<Dice>, IDiceService
{
    private readonly IGHQContext _context;
    public DiceService(IGHQContext context) : base(context)
    {
        _context = context;
    }

    // public async Task<Dice?> GetDiceByValueAsync(int diceValue, CancellationToken cancellationToken)
    // {
    // return await _context.Dices.Where(x => x.Value == diceValue).FirstOrDefaultAsync(cancellationToken);
    // }
}