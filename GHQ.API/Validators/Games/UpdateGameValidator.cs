using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.GameLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Games;

public class UpdateGameValidator : AbstractValidator<UpdateGameRequest>
{
    public UpdateGameValidator(IGHQContext context)
    {
        RuleFor(x => x.Id)
        .ValidateExistence<UpdateGameRequest, Game>(context)
        .WithMessage("Game Id does not appear in the game records");

        RuleFor(x => x.Title).MaximumLength(100).NotEmpty().WithMessage("Invalid Game Title");
        RuleFor(x => x.Title).Must(x => !context.Games.Any(y => y.Title == x))
         .WithMessage("The game title you provided already exists in the registry");

        When(x => x.DmId.HasValue && x.DmId != null, () =>
        {
            RuleFor(x => x.DmId ?? 0)
            .ValidateExistence<UpdateGameRequest, Player>(context)
            .WithMessage("Game's Dm Id does not appear in the player records");
        });

        RuleForEach(x => x.Players)
        .Must(p => context.Players
        .Any(cp => cp.Id == p.Id))
        .WithMessage("Game's Player Id does not appear in the player records");
    }
}
