using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;
public interface ITraitGroupService : IBaseService<TraitGroup>
{
  Task<TraitGroup> GetTraitGroupByIdIncludingTraitsAsync(int id, CancellationToken cancellationToken);
  Task DeleteNullCharacterTraitGroupsAsync(CancellationToken cancellationToken);
  Task DeleteCascadeAsync(int id, CancellationToken cancellationToken);
}
