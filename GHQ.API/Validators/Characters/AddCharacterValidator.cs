using FluentValidation;
using GHQ.Core.CharacterLogic.Requests;
using GHQ.Core.Extensions;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Characters;

public class AddCharacterValidator : AbstractValidator<AddCharacterRequest>
{
  public AddCharacterValidator(IGHQContext context)
  {
    RuleFor(x => x.Name).NotEmpty().Must(x => !context.Characters.Any(y => y.Name == x))
      .WithMessage("The Character name you provided already exists in the registry");

    RuleFor(x => x.GameId).ValidateExistence<AddCharacterRequest, Game>(context).WithMessage("Invalid game Id");
    RuleFor(x => x.PlayerId).ValidateExistence<AddCharacterRequest, Player>(context).WithMessage("Invalid player Id");
  }
}
