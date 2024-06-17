using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;

public interface IRollService : IBaseService<Roll>
{
  Task<Roll> GetRollByIdIncludingGameAndCharacter(int id, CancellationToken cancellationToken);

}
