using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class RollService : BaseService<Roll>, IRollService
{
    private readonly IGHQContext _context;
    public RollService(IGHQContext context) : base(context)
    {
        _context = context;
    }

    public async Task DeleteNullGameRollsAsync(CancellationToken cancellationToken)
    {
        var rolls = await _context.Rolls
            .Where(x => x.GameId == null)
            .ToListAsync(cancellationToken);

        foreach (var roll in rolls)
        {
            await DeleteCascadeAsync(roll.Id, cancellationToken);
        }
    }

    public async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        var roll = await GetRollByIdIncludingGameAndCharacterAsync(id, cancellationToken);

        roll.GameId = null;
        roll.CharacterId = null;
        roll.PlayerId = null;

        _context.Rolls.Remove(roll);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Roll> GetRollByIdIncludingGameAndCharacterAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Rolls
            .Where(x => x.Id == id)
            .Include(x => x.Game)
            .Include(x => x.Player)
            .Include(x => x.Character)
            .FirstAsync(cancellationToken);
    }
}
