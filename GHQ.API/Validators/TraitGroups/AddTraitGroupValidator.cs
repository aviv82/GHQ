using FluentValidation;
using GHQ.Core.TraitGroupLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Core.Extensions;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.TraitGroups;
public class AddTraitGroupValidator : AbstractValidator<AddTraitGroupRequest>
{
    public AddTraitGroupValidator(IGHQContext context)
    {
        RuleFor(x => x.CharacterId).ValidateExistence<AddTraitGroupRequest, Character>(context).WithMessage("Invalid character Id");
    }
}
