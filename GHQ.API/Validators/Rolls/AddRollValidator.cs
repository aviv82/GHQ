using FluentValidation;
using GHQ.Core.RollLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Core.Extensions;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Rolls;

public class AddRollValidator : AbstractValidator<AddRollRequest>
{
    public AddRollValidator(IGHQContext context)
    {
        RuleFor(x => x.GameId).ValidateExistence<AddRollRequest, Game>(context).WithMessage("Invalid game Id");
        RuleFor(x => x.CharacterId).ValidateExistence<AddRollRequest, Character>(context).WithMessage("Invalid character Id");
    }
}
