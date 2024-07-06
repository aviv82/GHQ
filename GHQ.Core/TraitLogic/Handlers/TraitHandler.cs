using AutoMapper;
using AutoMapper.QueryableExtensions;
using GHQ.Core.TraitLogic.Handlers.Interfaces;
using GHQ.Core.TraitLogic.Models;
using GHQ.Core.TraitLogic.Requests;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;

namespace GHQ.Core.TraitLogic.Handlers;
public class TraitHandler : ITraitHandler
{
    private readonly ITraitService _traitService;
    private readonly IMapper _mapper;

    public TraitHandler(
        ITraitService traitService,
        IMapper mapper
        )
    {
        _traitService = traitService;
        _mapper = mapper;
    }
    public async Task<TraitDto> AddTrait(
    AddTraitRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            Trait traitToAdd = new Trait
            {
                Name = request.Name,
                Value = request.Value ?? null,
                Level = request.Level ?? null,
                TraitGroupId = request.TraitGroupId
            };

            Trait newTrait = await _traitService.InsertAsync(traitToAdd, cancellationToken);

            List<Trait> traitGroupAsQueryable = new List<Trait> { newTrait };

            return traitGroupAsQueryable.AsQueryable().ProjectTo<TraitDto>(_mapper.ConfigurationProvider).First();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<TraitDto> UpdateTrait(
     UpdateTraitRequest request,
     CancellationToken cancellationToken)
    {
        try
        {
            var trait = await _traitService.GetByIdAsync(request.Id, cancellationToken);

            if (trait == null) { throw new Exception("Trait not found"); };

            trait.Name = request.Name;
            if (request.Value != null)
            {
                trait.Value = request.Value;
            }
            if (request.Level != null)
            {
                trait.Level = request.Level;
            }

            await _traitService.UpdateAsync(trait, cancellationToken);
            return _mapper.Map<TraitDto>(trait);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task DeleteTrait(
    DeleteTraitRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            var trait = await _traitService.GetByIdAsync(request.Id, cancellationToken);

            if (trait == null) { throw new Exception("Trait not found"); };

            await _traitService.DeleteAsync(trait, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
