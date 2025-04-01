using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.TraitLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Traits;
public class AddTraitValidator : AbstractValidator<AddTraitRequest>
{
    public AddTraitValidator(IGHQContext context)
    {
        RuleFor(x => x.TraitGroupId)
        .ValidateExistence<AddTraitRequest, TraitGroup>(context)
        .WithMessage("Invalid Trait Group Id");
    }
}