using FluentValidation;
using GHQ.Core.CharacterLogic.Requests;
using GHQ.Core.Extensions;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Characters;

public class UpdateCharacterValidator : AbstractValidator<UpdateCharacterRequest>
{
    public UpdateCharacterValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<UpdateCharacterRequest, Character>(context).WithMessage("Character Id does not appear in the game records");

        RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithMessage("Invalid Character Title");
        RuleFor(x => x.Name).Must(x => !context.Games.Any(y => y.Title == x))
         .WithMessage("The character title you provided already exists in the registry");

        RuleFor(x => x.Image).MaximumLength(200).WithMessage("Invalid Image Url");
    }
}
