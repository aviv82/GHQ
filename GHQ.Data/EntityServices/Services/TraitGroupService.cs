using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;

namespace GHQ.Data.EntityServices.Services;

public class TraitGroupService : BaseService<TraitGroup>, ITraitGroupService
{
    private readonly IGHQContext _context;
    public TraitGroupService(IGHQContext context) : base(context)
    {
        _context = context;
    }
}
