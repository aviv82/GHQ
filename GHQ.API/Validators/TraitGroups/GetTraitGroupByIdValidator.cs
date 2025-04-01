using FluentValidation;
using GHQ.Core.TraitGroupLogic.Queries;
using GHQ.Data.Context.Interfaces;
using GHQ.Core.Extensions;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.TraitGroups;
public class GetTraitGroupByIdValidator : AbstractValidator<GetTraitGroupByIdQuery>
{
    public GetTraitGroupByIdValidator(IGHQContext context)
    {
        RuleFor(x => x.Id)
        .ValidateExistence<GetTraitGroupByIdQuery, TraitGroup>(context)
        .WithMessage("Trait Group not found.");
    }
}