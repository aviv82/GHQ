using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;

public interface IGameService : IBaseService<Game>
{
    Task<Game> GetGameByIdIncludingPlayersCharactersAndRolls(int id, CancellationToken cancellationToken);
    Task DeleteNullDmGamesAsync(CancellationToken cancellationToken);
    Task DeleteCascadeAsync(int id, CancellationToken cancellationToken);
}
