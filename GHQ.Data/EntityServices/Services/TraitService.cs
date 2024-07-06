using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;

namespace GHQ.Data.EntityServices.Services;
public class TraitService : BaseService<Trait>, ITraitService
{
    private readonly IGHQContext _context;
    public TraitService(IGHQContext context) : base(context)
    {
        _context = context;
    }
}
