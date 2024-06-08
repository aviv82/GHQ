using GHQ.Data.Context;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class PlayerService : BaseService<Player>, IPlayerService
{
    private readonly IGHQContext _context;
    public PlayerService(GHQContext context) : base(context)
    {
        _context = context;
    }

    public virtual async Task<Player> GetPlayerByIdIncludingGamesAndCharacters(int id, CancellationToken cancellationToken)
    {
        return await _context.Players.Where(x => x.Id == id).Include(x => x.DmGames).Include(x => x.PlayerGames).Include(x => x.Characters).FirstAsync(cancellationToken);
    }
}
