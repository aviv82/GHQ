using GHQ.Core.TraitLogic.Models;
using GHQ.Core.TraitLogic.Requests;

namespace GHQ.Core.TraitLogic.Handlers.Interfaces;
public interface ITraitHandler
{
    Task<TraitDto> AddTrait(AddTraitRequest request, CancellationToken cancellationToken);
    Task<TraitDto> UpdateTrait(UpdateTraitRequest request, CancellationToken cancellationToken);
    Task DeleteTrait(DeleteTraitRequest request, CancellationToken cancellationToken);
}
