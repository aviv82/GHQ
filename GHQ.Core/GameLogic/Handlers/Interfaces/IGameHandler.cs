using GHQ.Core.GameLogic.Models;
using GHQ.Core.GameLogic.Queries;
using GHQ.Core.GameLogic.Requests;
using static GHQ.Core.GameLogic.Models.GameListVm;

namespace GHQ.Core.GameLogic.Handlers.Interfaces;

public interface IGameHandler
{
    Task<GameListVm> GetAllGames(GetGameListQuery request, CancellationToken cancellationToken);
    Task<GameDto> GetGameById(GetGameByIdQuery request, CancellationToken cancellationToken);
    Task<GameDto> AddGame(AddGameRequest request, CancellationToken cancellationToken);
    Task<GameDto> UpdateGame(UpdateGameRequest request, CancellationToken cancellationToken);
    Task DeleteGame(DeleteGameRequest request, CancellationToken cancellationToken);
}
