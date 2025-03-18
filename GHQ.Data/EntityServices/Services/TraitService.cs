using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityServices.Services;
public class TraitService : BaseService<Trait>, ITraitService
{
    private readonly IGHQContext _context;
    public TraitService(IGHQContext context) : base(context)
    {
        _context = context;
    }

    public async Task DeleteNullTraitGroupTraitsAsync(CancellationToken cancellationToken)
    {
        var traits = await _context.Traits
        .Where(x => x.TraitGroupId == null)
        .ToListAsync(cancellationToken);

        foreach (var trait in traits)
        {
            await DeleteAsync(trait.Id, cancellationToken);
        }
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var trait = await _context.Traits
            .Where(x => x.Id == id)
            .FirstAsync(cancellationToken);

        trait.TraitGroupId = null;
        await _context.SaveChangesAsync(cancellationToken);

        _context.Traits.Remove(trait);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
