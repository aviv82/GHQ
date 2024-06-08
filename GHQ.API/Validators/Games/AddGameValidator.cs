using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.GameLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Games;

public class AddGameValidator : AbstractValidator<AddGameRequest>
{
    public AddGameValidator(IGHQContext context)
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100).Must(x => !context.Games.Any(y => y.Title == x))
          .WithMessage("The game title you provided already exists in the registry");

        RuleFor(x => x.DmId).ValidateExistence<AddGameRequest, Player>(context).WithMessage("Invalid game DM Id");
    }
}
