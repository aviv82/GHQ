using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;

public interface IRollService : IBaseService<Roll>
{
  Task<Roll> GetRollByIdIncludingGameAndCharacterAsync(int id, CancellationToken cancellationToken);
  Task DeleteNullGameRollsAsync(CancellationToken cancellationToken);
  Task DeleteCascadeAsync(int id, CancellationToken cancellationToken);
}
