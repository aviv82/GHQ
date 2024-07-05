using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHQ.Core.TraitGroupLogic.Queries;

namespace GHQ.API.Validators.TraitGroups;
public class GetTraitGroupByIdValidator : AbstractValidator<GetTraitGroupByIdQuery>
{
    public GetCharacterByIdValidator(IGHQContext context)
    {
        RuleFor(x => x.Id)
        .ValidateExistence<GetTraitGroupByIdQuery, Character>(context)
        .WithMessage("Trait Group not found.");
    }
}