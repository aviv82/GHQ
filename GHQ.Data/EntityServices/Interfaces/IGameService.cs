using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;

public interface IGameService : IBaseService<Game>
{
    Task<Game> GetGameByIdIncludingPlayersAndCharacters(int id, CancellationToken cancellationToken);
}
