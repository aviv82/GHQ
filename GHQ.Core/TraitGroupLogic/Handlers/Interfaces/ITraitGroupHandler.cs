using GHQ.Core.TraitGroupLogic.Models;
using GHQ.Core.TraitGroupLogic.Queries;
using GHQ.Core.TraitGroupLogic.Requests;

namespace GHQ.Core.TraitGroupLogic.Handlers.Interfaces;
public interface ITraitGroupHandler
{
    Task<TraitGroupDto> GetTraitGroupById(GetTraitGroupByIdQuery request, CancellationToken cancellationToken);
    Task<TraitGroupDto> AddTraitGroup(AddTraitGroupRequest request, CancellationToken cancellationToken);
    Task<TraitGroupDto> UpdateTraitGroup(UpdateTraitGroupRequest request, CancellationToken cancellationToken);
    Task DeleteTraitGroup(DeleteTraitGroupRequest request, CancellationToken cancellationToken);
}
