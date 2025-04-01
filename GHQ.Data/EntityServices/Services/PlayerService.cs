using GHQ.Data.Context;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class PlayerService : BaseService<Player>, IPlayerService
{
    private readonly IGHQContext _context;
    private readonly ICharacterService _characterService;
    private readonly IGameService _gameService;
    public PlayerService(GHQContext context, ICharacterService characterService, IGameService gameService) : base(context)
    {
        _context = context;
        _characterService = characterService;
        _gameService = gameService;
    }

    public virtual async Task<Player> GetPlayerByIdIncludingGamesAndCharacters(int id, CancellationToken cancellationToken)
    {
        return await _context.Players
            .Where(x => x.Id == id)
            .Include(x => x.DmGames)
            .Include(x => x.PlayerGames)
            .Include(x => x.Characters)
            .FirstAsync(cancellationToken);
    }
    public virtual async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        Player player = await _context.Players
            .Where(x => x.Id == id)
            .Include(x => x.DmGames)
            .Include(x => x.PlayerGames)
            .Include(x => x.Characters)
            .Include(x => x.Rolls)
            .FirstAsync(cancellationToken);

        foreach (var character in player.Characters)
        {
            character.PlayerId = null;
        }
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var game in player.DmGames)
        {
            game.DmId = null;
        }
        await _context.SaveChangesAsync(cancellationToken);

        await _gameService.DeleteNullDmGamesAsync(cancellationToken);

        foreach (var roll in player.Rolls)
        {
            roll.PlayerId = null;
        }
        await _context.SaveChangesAsync(cancellationToken);

        player.DmGames = [];
        player.PlayerGames = [];
        player.Rolls = [];
        await _context.SaveChangesAsync(cancellationToken);

        _context.Players.Remove(player);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
