using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;
public interface ITraitGroupService : IBaseService<TraitGroup>
{
Task DeleteCascadeAsync(int id, CancellationToken cancellationToken);
}
