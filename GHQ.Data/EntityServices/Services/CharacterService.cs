using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;

public class CharacterService : BaseService<Character>, ICharacterService
{
    private readonly IGHQContext _context;
    public CharacterService(IGHQContext context) : base(context)
    {
        _context = context;
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

        foreach (var roll in character.Rolls)
        {
            roll.CharacterId = 0;
            roll.Character = null;
            _context.Rolls.Remove(roll);
            await _context.SaveChangesAsync(cancellationToken);
        }

        foreach (var traitGroup in character.TraitGroups)
        {
            traitGroup.CharacterId = 0;
            traitGroup.Character = null;
            _context.TraitGroups.Remove(traitGroup);
            await _context.SaveChangesAsync(cancellationToken);
        }

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
