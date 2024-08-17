using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class CharacterService : BaseService<Character>, ICharacterService
{
    private readonly IGHQContext _context;
    private readonly ITraitGroupService _traitGroupService;
    public CharacterService(IGHQContext context, ITraitGroupService traitGroupService) : base(context)
    {
        _context = context;
        _traitGroupService = traitGroupService;
    }

    public async Task<Character> GetCharacterByIdIncludingPlayerAndGame(int id, CancellationToken cancellationToken)
    {
        return await _context.Characters.Where(x => x.Id == id).Include(x => x.Player).Include(x => x.Game).FirstAsync(cancellationToken);
    }

    public virtual async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        var character = await _context.Characters
        .Where(x => x.Id == id)
        .Include(x => x.Player)
        .Include(x => x.Game)
        .Include(x => x.Rolls)
        .Include(x => x.TraitGroups)
        .FirstAsync(cancellationToken);

        // foreach (var roll in character.Rolls)
        // {
        //     _context.Rolls.Remove(roll);
        //     await _context.SaveChangesAsync(cancellationToken);
        // }

        foreach (var traitGroup in character.TraitGroups)
        {
            await _traitGroupService.DeleteCascadeAsync(traitGroup.Id, cancellationToken);
        }

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
