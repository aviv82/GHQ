using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;

namespace GHQ.Data.EntityServices.Services;

public class RollService : BaseService<Roll>, IRollService
{
    private readonly IGHQContext _context;
    public RollService(IGHQContext context) : base(context)
    {
        _context = context;
    }
}
