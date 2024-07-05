using FluentValidation;
using GHQ.Core.TraitGroupLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Core.Extensions;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.TraitGroups;
public class UpdateTraitGroupValidator : AbstractValidator<UpdateTraitGroupRequest>
{
    public UpdateTraitGroupValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<UpdateTraitGroupRequest, TraitGroup>(context).WithMessage("Trait Group Id does not appear in the records");

        RuleFor(x => x.TraitGroupName).MaximumLength(100).NotEmpty().WithMessage("Invalid Trait Group Title");
    }
}
