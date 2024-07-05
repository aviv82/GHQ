using FluentValidation;
using GHQ.Core.TraitGroupLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Core.Extensions;

namespace GHQ.API.Validators.TraitGroups;
public class DeleteTraitGroupValidator : AbstractValidator<DeleteTraitGroupRequest>
{
    public DeleteTraitGroupValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<DeleteTraitGroupRequest, TraitGroup>(context).WithMessage("Trait group does not exist."); ;
    }
}
