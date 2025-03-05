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

    // public async Task<Roll> GetRollByIdIncludingGameAndCharacter(int id, CancellationToken cancellationToken)
    // {
    //     return await _context.Rolls
    //     .Where(x => x.Id == id)
    //     .Include(x => x.Character)
    //     .Include(x => x.Game)
    //     .FirstAsync(cancellationToken);
    // }
}
