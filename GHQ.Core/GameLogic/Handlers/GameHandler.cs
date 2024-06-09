using AutoMapper;
using AutoMapper.QueryableExtensions;
using GHQ.Core.Extensions;
using GHQ.Core.GameLogic.Handlers.Interfaces;
using GHQ.Core.GameLogic.Models;
using GHQ.Core.GameLogic.Queries;
using GHQ.Core.GameLogic.Requests;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using static GHQ.Core.GameLogic.Models.GameListVm;

namespace GHQ.Core.GameLogic.Handlers;

public class GameHandler : IGameHandler
{
    private readonly IMapper _mapper;
    private readonly IGameService _gameService;
    private readonly IPlayerService _playerService;
    private readonly ICharacterService _characterService;

    public GameHandler(
        IMapper mapper,
        IGameService gameService,
        ICharacterService characterService,
        IPlayerService playerService)
    {
        _mapper = mapper;
        _gameService = gameService;
        _playerService = playerService;
        _characterService = characterService;
    }

    public async Task<GameListVm> GetAllGames(
       GetGameListQuery request,
       CancellationToken cancellationToken)
    {
        List<Game> query = await _gameService.GetAllAsync(cancellationToken);

        List<GameDto> games = query
            .AsQueryable()
            .ApplyFiltering(request)
            .ApplySorting(request)
            .ApplyPaging(request)
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider).ToList();

        return new GameListVm
        {
            GameList = games,
            TotalCount = query.AsQueryable().Count(),
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<GameDto> GetGameById(
       GetGameByIdQuery request,
       CancellationToken cancellationToken)
    {
        Game? query = await _gameService.GetGameByIdIncludingPlayersAndCharacters(request.Id, cancellationToken);

        if (query == null) { throw new Exception("Game not found"); }

        List<Game> gameList = new List<Game> { query };

        var toReturn = gameList.AsQueryable().ProjectTo<GameDto>(_mapper.ConfigurationProvider);

        return toReturn.First();
    }

    public async Task<GameDto> AddGame(
    AddGameRequest request,
    CancellationToken cancellationToken)
    {
        try
        {

            Game gameToAdd = new Game
            {
                Title = request.Title,
                Type = request.Type,
                DmId = request.DmId,
                Dm = new Player(),
                Players = new List<Player>(),
            };

            Player dm = await _playerService.GetByIdAsync(request.DmId, cancellationToken) ?? new Player();

            gameToAdd.Dm = dm;
            Game newGame = await _gameService.InsertAsync(gameToAdd, cancellationToken);

            List<Game> gameAsQueryable = new List<Game> { newGame };

            return gameAsQueryable.AsQueryable().ProjectTo<GameDto>(_mapper.ConfigurationProvider).First();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GameDto> UpdateGame(
     UpdateGameRequest request,
     CancellationToken cancellationToken)
    {
        try
        {
            var game = await _gameService.GetGameByIdIncludingPlayersAndCharacters(request.Id, cancellationToken);

            if (game == null) { throw new Exception("Game not found"); };

            game.Title = request.Title;
            game.Type = request.Type;

            if (request.DmId != 0 || game.Players != request.Players)
            {

                List<Player> playerList = await _playerService.GetAllAsync(cancellationToken);

                if (game.DmId != request.DmId)
                {
                    var newDm = playerList.FirstOrDefault(x => x.Id == request.DmId);

                    if (newDm == null) { throw new Exception("Player game DM not found"); };

                    game.Dm = newDm;
                    game.DmId = request.DmId;
                }

                if (game.Players != request.Players)
                {
                    game.Players = [];

                    request.Players.ForEach((p) =>
                    {
                        Player? listPlayer = playerList.FirstOrDefault(x => x.Id == p.Id);

                        if (listPlayer == null) { throw new Exception("Player game Player not found"); };

                        game.Players.Add(listPlayer);
                    });
                }
            }

            await _gameService.UpdateAsync(game, cancellationToken);
            return _mapper.Map<GameDto>(game);
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task DeleteGame(
    DeleteGameRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            var game = await _gameService.GetByIdAsync(request.Id, cancellationToken);

            if (game == null) { throw new Exception("Game not found"); };

            await _gameService.DeleteCascadeAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}



