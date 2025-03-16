using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GHQ.Data.EntityServices.Services;

public class TraitGroupService : BaseService<TraitGroup>, ITraitGroupService
{
    private readonly IGHQContext _context;
    private readonly ITraitService _traitService;
    public TraitGroupService(
        IGHQContext context,
        ITraitService traitService) : base(context)
    {
        _context = context;
        _traitService = traitService;
    }

    public async Task DeleteNullCharacterTraitGroupsAsync(CancellationToken cancellationToken)
    {
        var traitGroups = await _context.TraitGroups
        .Where(x => x.CharacterId == null)
        .ToListAsync(cancellationToken);

        foreach (var traitGroup in traitGroups)
        {
            await DeleteCascadeAsync(traitGroup.Id, cancellationToken);
        }
    }

    public virtual async Task DeleteCascadeAsync(int id, CancellationToken cancellationToken)
    {
        var traitGroup = await _context.TraitGroups
        .Where(x => x.Id == id)
        .Include(x => x.Character)
        .Include(x => x.Traits)
        .FirstAsync(cancellationToken);

        foreach (var trait in traitGroup.Traits)
        {
            trait.TraitGroupId = null;
        }
        await _context.SaveChangesAsync(cancellationToken);

        await _traitService.DeleteNullTraitGroupTraitsAsync(cancellationToken);

        traitGroup.CharacterId = null;
        await _context.SaveChangesAsync(cancellationToken);

        _context.TraitGroups.Remove(traitGroup);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
