using FluentValidation;
using GHQ.Core.CharacterLogic.Requests;
using GHQ.Core.Extensions;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Characters;

public class DeleteCharacterValidator : AbstractValidator<DeleteCharacterRequest>
{
    public DeleteCharacterValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<DeleteCharacterRequest, Character>(context).WithMessage("Character does not exist."); ;
    }
}
