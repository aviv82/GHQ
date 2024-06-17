using GHQ.Core.RollLogic.Models;
using GHQ.Core.RollLogic.Queries;
using GHQ.Core.RollLogic.Requests;
using static GHQ.Core.RollLogic.Models.RollListVm;

namespace GHQ.Core.RollLogic.Handlers.Interfaces;
public interface IRollHandler
{
    Task<RollListVm> GetAllRolls(GetRollListQuery request, CancellationToken cancellationToken);
    Task<RollDto> GetRollById(GetRollByIdQuery request, CancellationToken cancellationToken);
    Task<RollDto> AddRoll(AddRollRequest request, CancellationToken cancellationToken);
    Task DeleteRoll(DeleteRollRequest request, CancellationToken cancellationToken);
}
