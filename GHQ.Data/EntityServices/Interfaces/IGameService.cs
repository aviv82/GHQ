using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;

public interface IGameService : IBaseService<Game>
{
    Task<Game> GetGameByIdIncludingPlayersAndCharacters(int id, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task DeleteCascadeAsync(int id, CancellationToken cancellationToken);
}
