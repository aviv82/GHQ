using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public RollHandler(
        IMapper mapper,
        IRollService rollService
        )
    {
        _mapper = mapper;
        _rollService = rollService;
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

    public async Task<RollDto> AddRoll(
    AddRollRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            Roll rollToAdd = new Roll
            {
                Title = request.Title,
                Description = request.Description ?? "",
                Result = request.Result,
                GameId = request.GameId,
                Game = new Game(),
                CharacterId = request.CharacterId,
                Character = new Character(),
            };

            // Character character = await _characterService.GetByIdAsync(request.PlayerId, cancellationToken) ?? new Player();
            // characterToAdd.Character = character;

            // Game game = await _gameService.GetByIdAsync(request.GameId, cancellationToken) ?? new Game();
            // characterToAdd.Game = game;


            Roll newRoll = await _rollService.InsertAsync(rollToAdd, cancellationToken);

            List<Roll> rollAsQueryable = new List<Roll> { newRoll };

            return rollAsQueryable.AsQueryable().ProjectTo<RollDto>(_mapper.ConfigurationProvider).First();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
