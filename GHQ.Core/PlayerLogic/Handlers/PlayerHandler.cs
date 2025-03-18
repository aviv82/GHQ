using AutoMapper;
using AutoMapper.QueryableExtensions;
using GHQ.Core.Extensions;
using GHQ.Core.PlayerLogic.Handlers.Interfaces;
using GHQ.Core.PlayerLogic.Models;
using GHQ.Core.PlayerLogic.Queries;
using GHQ.Core.PlayerLogic.Requests;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;

namespace GHQ.Core.PlayerLogic.Handlers;

public class PlayerHandler : IPlayerHandler
{
    private readonly IMapper _mapper;
    private readonly IPlayerService _playerService;
    public PlayerHandler(
        IMapper mapper,
        IPlayerService playerService
        )
    {
        _mapper = mapper;
        _playerService = playerService;
    }
    public async Task<PlayerListVm> GetAllPlayers(
        GetPlayerListQuery request,
        CancellationToken cancellationToken)
    {
        List<Player> query = await _playerService.GetAllAsync(cancellationToken);

        List<PlayerDto> players = query
            .AsQueryable()
            .ApplyFiltering(request)
            .ApplySorting(request)
            .ApplyPaging(request)
            .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider).ToList();

        return new PlayerListVm
        {
            PlayerList = players,
            TotalCount = query.AsQueryable().Count(),
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<PlayerDto> GetPlayerById(
        GetPlayerByIdQuery request,
        CancellationToken cancellationToken)
    {
        Player? query = await _playerService.GetPlayerByIdIncludingGamesAndCharacters(request.Id, cancellationToken);

        if (query == null) { throw new Exception("Player not found"); }

        List<Player> playerList = new List<Player> { query };

        var toReturn = playerList.AsQueryable().ProjectTo<PlayerDto>(_mapper.ConfigurationProvider);

        return toReturn.First();
    }

    public async Task<PlayerDto> AddPlayer(
        AddPlayerRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            Player playerToAdd = new Player
            {
                UserName = request.UserName,
                Email = request.Email ?? string.Empty,
                PasswordHash = request.PasswordHash ?? string.Empty,
            };

            Player newPlayer = await _playerService.InsertAsync(playerToAdd, cancellationToken);

            List<Player> playerAsQueryable = new List<Player> { newPlayer };

            return playerAsQueryable.AsQueryable().ProjectTo<PlayerDto>(_mapper.ConfigurationProvider).First();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<PlayerDto> UpdatePlayer(
       UpdatePlayerRequest request,
       CancellationToken cancellationToken)
    {
        try
        {
            var player = await _playerService.GetPlayerByIdIncludingGamesAndCharacters(request.Id, cancellationToken);

            if (player == null) { throw new Exception("Player not found"); }
            ;

            player.UserName = request.UserName;
            player.Email = request.Email;

            await _playerService.UpdateAsync(player, cancellationToken);
            return _mapper.Map<PlayerDto>(player);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeletePlayer(
        DeletePlayerRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var player = await _playerService.GetByIdAsync(request.Id, cancellationToken);

            if (player == null) { throw new Exception("Player not found"); }

            await _playerService.DeleteCascadeAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
