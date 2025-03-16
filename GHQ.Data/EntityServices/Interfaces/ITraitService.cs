using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;
public interface ITraitService : IBaseService<Trait>
{
  Task DeleteNullTraitGroupTraitsAsync(CancellationToken cancellationToken);
  Task DeleteAsync(int id, CancellationToken cancellationToken);
}
