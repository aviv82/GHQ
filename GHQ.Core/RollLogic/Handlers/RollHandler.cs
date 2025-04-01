using AutoMapper;
using AutoMapper.QueryableExtensions;
using GHQ.Common.Enums;
using GHQ.Common.Helpers;
using GHQ.Core.Extensions;
using GHQ.Core.RollLogic.Handlers.Interfaces;
using GHQ.Core.RollLogic.Models;
using GHQ.Core.RollLogic.Queries;
using GHQ.Core.RollLogic.Requests;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using static GHQ.Core.RollLogic.Models.RollListVm;

namespace GHQ.Core.RollLogic.Handlers;

public class RollHandler : IRollHandler
{

    private readonly IMapper _mapper;
    private readonly IRollService _rollService;
    private readonly ICharacterService _characterService;
    private readonly IGameService _gameService;
    private readonly IPlayerService _playerService;
    public RollHandler(
        IMapper mapper,
        IRollService rollService,
        ICharacterService characterService,
        IGameService gameService,
        IPlayerService playerService
        )
    {
        _mapper = mapper;
        _rollService = rollService;
        _characterService = characterService;
        _gameService = gameService;
        _playerService = playerService;
    }
    public async Task<RollListVm> GetAllRolls(
        GetRollListQuery request,
        CancellationToken cancellationToken)
    {
        List<Roll> query = await _rollService.GetAllAsync(cancellationToken);

        List<RollDto> rolls = query
            .AsQueryable()
            .ApplyFiltering(request)
            .ApplySorting(request)
            .ApplyPaging(request)
            .ProjectTo<RollDto>(_mapper.ConfigurationProvider).ToList();

        return new RollListVm
        {
            RollList = rolls,
            TotalCount = query.AsQueryable().Count(),
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<RollDto> GetRollById(
    GetRollByIdQuery request,
    CancellationToken cancellationToken)
    {
        Roll? query = await _rollService.GetRollByIdIncludingGameAndCharacterAsync(request.Id, cancellationToken);

        if (query == null) { throw new Exception("Roll not found"); }

        List<Roll> rollList = new List<Roll> { query };

        var toReturn = rollList.AsQueryable().ProjectTo<RollDto>(_mapper.ConfigurationProvider);

        return toReturn.First();
    }

    public async Task<RollDto> AddRoll(
    AddRollRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            Roll rollToAdd = new Roll
            {
                Title = request.Title,
                Description = request.Description,
                Difficulty = request.Difficulty,
                GameId = request.GameId,
                CharacterId = request.CharacterId,
            };

            if (request.CharacterId != null)
            {
                var character = await _characterService.GetByIdAsync((int)request.CharacterId, cancellationToken);
                if (character != null)
                    rollToAdd.Character = character;
            }

            if (rollToAdd.Character == null && request.PlayerId != null)
            {
                var player = await _playerService.GetByIdAsync((int)request.PlayerId, cancellationToken);

                if (player != null)
                {
                    rollToAdd.PlayerId = request.PlayerId;
                    rollToAdd.Player = player;
                }
            }

            if (request.GameId != 0)
            {
                var game = await _gameService.GetByIdAsync(request.GameId, cancellationToken);
                rollToAdd.Game = game;
            }

            List<int> dicePoolToAdd = [];

            if (request.DicePool != null)
            {
                foreach (var dice in request.DicePool)
                {
                    dicePoolToAdd.Add(dice);
                }
            }

            rollToAdd.DicePool = dicePoolToAdd;

            rollToAdd.Result = DiceRollerExtensions.DicePoolRoller(rollToAdd.DicePool);

            Roll newRoll = await _rollService.InsertAsync(rollToAdd, cancellationToken);

            List<Roll> rollAsQueryable = new List<Roll> { newRoll };

            return rollAsQueryable.AsQueryable().ProjectTo<RollDto>(_mapper.ConfigurationProvider).First();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task DeleteRoll(
     DeleteRollRequest request,
     CancellationToken cancellationToken)
    {
        try
        {
            var roll = await _rollService.GetByIdAsync(request.Id, cancellationToken);

            if (roll == null) { throw new Exception("Roll not found"); }
            ;

            await _rollService.DeleteCascadeAsync(roll.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
