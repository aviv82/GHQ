using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class GameService : BaseService<Game>, IGameService
{
    private readonly IGHQContext _context;
    private readonly ICharacterService _characterService;
    private readonly IRollService _rollService;

    public GameService(IGHQContext context,
    ICharacterService characterService,
    IRollService rollService) : base(context)
    {
        _context = context;
        _characterService = characterService;
        _rollService = rollService;
    }

    public virtual async Task<Game> GetGameByIdIncludingPlayersCharactersAndRolls(int id, CancellationToken cancellationToken)
    {
        return await _context.Games
            .Where(x => x.Id == id)
            .Include(x => x.Dm)
            .Include(x => x.Players)
            .Include(x => x.Characters)
            .Include(x => x.Rolls)
            .FirstAsync(cancellationToken);
    }

    public async Task DeleteNullDmGamesAsync(CancellationToken cancellationToken)
    {
        var games = await _context.Games
            .Where(x => x.DmId == null)
            .ToListAsync(cancellationToken);

        foreach (var game in games)
        {
            await DeleteCascadeAsync(game.Id, cancellationToken);
        }
    }

    public async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        var game = await GetGameByIdIncludingPlayersCharactersAndRolls(id, cancellationToken);

        game.Dm = null;
        await _context.SaveChangesAsync(cancellationToken);


        foreach (var character in game.Characters)
        {
            character.GameId = null;
        }
        await _context.SaveChangesAsync(cancellationToken);
        await _characterService.DeleteNullGameCharactersAsync(cancellationToken);

        foreach (var roll in game.Rolls)
        {
            roll.GameId = null;
        }
        await _context.SaveChangesAsync(cancellationToken);
        await _rollService.DeleteNullGameRollsAsync(cancellationToken);

        game.Characters = [];
        game.Players = [];
        game.Rolls = [];
        await _context.SaveChangesAsync(cancellationToken);

        _context.Games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
