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
}
