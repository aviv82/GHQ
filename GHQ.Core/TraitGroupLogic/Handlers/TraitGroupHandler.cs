using AutoMapper;
using AutoMapper.QueryableExtensions;
using GHQ.Core.TraitGroupLogic.Handlers.Interfaces;
using GHQ.Core.TraitGroupLogic.Models;
using GHQ.Core.TraitGroupLogic.Queries;
using GHQ.Core.TraitGroupLogic.Requests;
using GHQ.Core.TraitLogic.Models;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;

namespace GHQ.Core.TraitGroupLogic.Handlers;
public class TraitGroupHandler : ITraitGroupHandler
{
    private readonly ITraitGroupService _traitGroupService;
    // private readonly ITraitService _traitService;
    private readonly IMapper _mapper;

    public TraitGroupHandler(
        ITraitGroupService traitGroupService,
        // ITraitService traitService,
        IMapper mapper
        )
    {
        _traitGroupService = traitGroupService;
        // _traitService = traitService;
        _mapper = mapper;
    }

    public async Task<TraitGroupDto> GetTraitGroupById(
    GetTraitGroupByIdQuery request,
    CancellationToken cancellationToken)
    {
        TraitGroup? query = await _traitGroupService.GetByIdAsync(request.Id, cancellationToken);

        if (query == null) { throw new Exception("Trait Group not found"); }

        List<TraitGroup> traitGroupList = new List<TraitGroup> { query };

        var toReturn = traitGroupList.AsQueryable().ProjectTo<TraitGroupDto>(_mapper.ConfigurationProvider);

        return toReturn.First();
    }

    public async Task<TraitGroupDto> AddTraitGroup(
    AddTraitGroupRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            TraitGroup traitGroupToAdd = new TraitGroup
            {
                TraitGroupName = request.TraitGroupName,
                Type = request.Type ?? null,
                CharacterId = request.CharacterId
            };

            TraitGroup newTraitGroup = await _traitGroupService.InsertAsync(traitGroupToAdd, cancellationToken);

            List<TraitGroup> traitGroupAsQueryable = new List<TraitGroup> { newTraitGroup };

            return traitGroupAsQueryable.AsQueryable().ProjectTo<TraitGroupDto>(_mapper.ConfigurationProvider).First();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TraitGroupDto> UpdateTraitGroup(
     UpdateTraitGroupRequest request,
     CancellationToken cancellationToken)
    {
        try
        {
            var traitGroup = await _traitGroupService.GetByIdAsync(request.Id, cancellationToken);

            if (traitGroup == null) { throw new Exception("Trait Group not found"); };

            traitGroup.TraitGroupName = request.TraitGroupName;
            if (request.Type != null)
            {
                traitGroup.Type = request.Type;
            }

            // if (traitGroup.Traits != request.Traits && request.Traits != null)
            // {
            //     traitGroup.Traits = [];

            //     List<Trait> traits = await _traitService.GetAllAsync(cancellationToken);

            //     foreach (TraitDto trait in request.Traits)
            //     {
            //         Trait? listTrait = traits.FirstOrDefault(x => x.Id == trait.Id);

            //         if (listTrait == null) { throw new Exception("Trait Group Trait not found"); };

            //         traitGroup.Traits.Add(listTrait);
            //     }
            // }

            await _traitGroupService.UpdateAsync(traitGroup, cancellationToken);
            return _mapper.Map<TraitGroupDto>(traitGroup);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteTraitGroup(
    DeleteTraitGroupRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            var traitGroup = await _traitGroupService.GetByIdAsync(request.Id, cancellationToken);

            if (traitGroup == null) { throw new Exception("Trait Group not found"); };

            await _traitGroupService.DeleteAsync(traitGroup, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
