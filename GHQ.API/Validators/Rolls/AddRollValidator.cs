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

        When(x => x.CharacterId.HasValue && x.CharacterId != null, () =>
        {
            RuleFor(x => x.CharacterId ?? 0).ValidateExistence<AddRollRequest, Character>(context).WithMessage("Invalid character Id");
        });

        When(x => x.PlayerId.HasValue && x.PlayerId != null, () =>
        {
            RuleFor(x => x.PlayerId ?? 0).ValidateExistence<AddRollRequest, Player>(context).WithMessage("Invalid player Id");
        });
    }
}
