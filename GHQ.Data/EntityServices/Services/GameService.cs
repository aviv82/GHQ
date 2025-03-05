﻿using System.Data.Common;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class GameService : BaseService<Game>, IGameService
{
    private readonly IGHQContext _context;
    private readonly ICharacterService _characterService;

    public GameService(IGHQContext context, ICharacterService characterService) : base(context)
    {
        _context = context;
        _characterService = characterService;
    }

    public virtual async Task<Game> GetGameByIdIncludingPlayersAndCharacters(int id, CancellationToken cancellationToken)
    {
        return await _context.Games.Where(x => x.Id == id).Include(x => x.Dm).Include(x => x.Players).Include(x => x.Characters).FirstAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var game = await _context.Games.Where(x => x.Id == id).SingleOrDefaultAsync();

        if (game != null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return;
    }

    public virtual async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        var game = await _context.Games
        .Where(x => x.Id == id)
        .Include(x => x.Dm)
        .Include(x => x.Players)
        .Include(x => x.Characters)
        // .Include(x => x.Rolls)
        .FirstAsync(cancellationToken);

        foreach (var character in game.Characters)
        {
            await _characterService.DeleteCascadeAsync(character.Id, cancellationToken);
        }

        game.Players = [];
        game.Dm.DmGames.Remove(game);

        _context.Games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
