using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class CharacterService : BaseService<Character>, ICharacterService
{
    private readonly IGHQContext _context;
    private readonly ITraitGroupService _traitGroupService;
    public CharacterService(IGHQContext context,
    ITraitGroupService traitGroupService
    ) : base(context)
    {
        _context = context;
        _traitGroupService = traitGroupService;
    }

    public async Task<Character> GetCharacterByIdIncludingTraitGroupsAndTraits(int id, CancellationToken cancellationToken)
    {
        return await _context.Characters
            .Where(x => x.Id == id)
            .Include(x => x.TraitGroups)
            .ThenInclude(x => x.Traits)
            .FirstAsync(cancellationToken);
    }
    public async Task<Character> GetCharacterByIdIncludingPlayerAndGame(int id, CancellationToken cancellationToken)
    {
        return await _context.Characters
            .Where(x => x.Id == id)
            .Include(x => x.Player)
            .Include(x => x.Game)
            .FirstAsync(cancellationToken);
    }

    public async Task DeleteNullGameCharactersAsync(CancellationToken cancellationToken)
    {
        var characters = await _context.Characters
        .Where(x => x.GameId == null)
        .ToListAsync(cancellationToken);

        foreach (var character in characters)
        {
            await DeleteCascadeAsync(character.Id, cancellationToken);
        }
    }

    public virtual async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        var character = await _context.Characters
            .Where(x => x.Id == id)
            .Include(x => x.TraitGroups)
            .Include(x => x.Rolls)
            .FirstAsync(cancellationToken);

        foreach (var roll in character.Rolls)
        {
            roll.CharacterId = null;
        }
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var traitGroup in character.TraitGroups)
        {
            traitGroup.CharacterId = null;
        }
        await _context.SaveChangesAsync(cancellationToken);
        await _traitGroupService.DeleteNullCharacterTraitGroupsAsync(cancellationToken);

        character.GameId = null;
        character.PlayerId = null;
        character.Rolls = [];
        await _context.SaveChangesAsync(cancellationToken);

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
