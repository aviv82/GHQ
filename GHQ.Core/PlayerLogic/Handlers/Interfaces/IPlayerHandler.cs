using GHQ.Core.PlayerLogic.Models;
using GHQ.Core.PlayerLogic.Queries;
using GHQ.Core.PlayerLogic.Requests;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;

namespace GHQ.Core.PlayerLogic.Handlers.Interfaces;

public interface IPlayerHandler
{
    Task<PlayerListVm> GetAllPlayers(GetPlayerListQuery request, CancellationToken cancellationToken);
    Task<PlayerDto> GetPlayerById(GetPlayerByIdQuery request, CancellationToken cancellationToken);
    Task<PlayerDto> AddPlayer(AddPlayerRequest request, CancellationToken cancellationToken);
    Task<PlayerDto> UpdatePlayer(UpdatePlayerRequest request, CancellationToken cancellationToken);
    Task DeletePlayer(DeletePlayerRequest request, CancellationToken cancellationToken);
}
