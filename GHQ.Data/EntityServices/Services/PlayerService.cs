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
        return await _context.Players.Where(x => x.Id == id).Include(x => x.DmGames).Include(x => x.PlayerGames).Include(x => x.Characters).FirstAsync(cancellationToken);
    }
    public virtual async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        Player player = await GetPlayerByIdIncludingGamesAndCharacters(id, cancellationToken);

        foreach (var character in player.Characters)
        {
            // _context.Characters.Remove(character);
            // await _context.SaveChangesAsync(cancellationToken);
            await _characterService.DeleteCascadeAsync(character.Id, cancellationToken);
        }

        // foreach (var game in player.PlayerGames)
        // {
        // var g = await _context.Games.Where(x => x.Id == game.Id).Include(x => x.Players).FirstAsync(cancellationToken);
        // g.Players.Remove(player);
        // await _context.SaveChangesAsync(cancellationToken);
        // }

        foreach (var game in player.DmGames)
        {
            var g = await _context.Games
            .Where(x => x.Id == game.Id)
            .Include(x => x.Dm)
            .Include(x => x.Players)
            .Include(x => x.Characters)
            .Include(x => x.Rolls)
            .FirstAsync(cancellationToken);

            // foreach (var roll in g.Rolls)
            // {
            //     _context.Rolls.Remove(roll);
            //     await _context.SaveChangesAsync(cancellationToken);
            // }

            // foreach (var character in g.Characters)
            // {
            // _context.Characters.Remove(character);
            // await _context.SaveChangesAsync(cancellationToken);
            // await _characterService.DeleteCascadeAsync(character.Id, cancellationToken);
            // }

            // g.Players = [];
            // g.Dm.DmGames.Remove(g);

            // _context.Games.Remove(g);
            // await _context.SaveChangesAsync(cancellationToken);
            await _gameService.DeleteCascadeAsync(g.Id, cancellationToken);
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
