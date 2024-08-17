using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GHQ.Data.EntityServices.Services;

public class TraitGroupService : BaseService<TraitGroup>, ITraitGroupService
{
    private readonly IGHQContext _context;
    public TraitGroupService(IGHQContext context) : base(context)
    {
        _context = context;
    }

    public virtual async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        var traitGroup = await _context.TraitGroups
        .Where(x => x.Id == id)
        .Include(x => x.Traits)
        .FirstAsync(cancellationToken);

        foreach (var trait in traitGroup.Traits)
        {
            _context.Traits.Remove(trait);
            // await _context.SaveChangesAsync(cancellationToken);
        }

        _context.TraitGroups.Remove(traitGroup);
        // await _context.SaveChangesAsync(cancellationToken);
    }
}
