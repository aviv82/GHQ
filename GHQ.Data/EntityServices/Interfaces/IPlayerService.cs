using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;

public interface IPlayerService : IBaseService<Player>
{
    Task<Player> GetPlayerByIdIncludingGamesAndCharacters(int id, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task DeleteCascadeAsync(int id, CancellationToken cancellationToken);
}
