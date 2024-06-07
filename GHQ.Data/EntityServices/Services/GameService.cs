using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class GameService : BaseService<Game>, IGameService
{
    private readonly IGHQContext _context;
    public GameService(IGHQContext context) : base(context)
    {
        _context = context;
    }

    public virtual async Task<Game> GetGameByIdIncludingPlayersAndCharacters(int id, CancellationToken cancellationToken)
    {
        return await _context.Games.Where(x => x.Id == id).Include(x => x.Dm).Include(x => x.Players).Include(x => x.Characters).FirstAsync(cancellationToken);
    }
}
