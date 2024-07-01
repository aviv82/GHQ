using FluentValidation;
using GHQ.Core.CharacterLogic.Queries;
using GHQ.Core.Extensions;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Characters;

public class GetCharacterByIdValidator : AbstractValidator<GetCharacterByIdQuery>
{
    public GetCharacterByIdValidator(IGHQContext context)
    {

        RuleFor(x => x.Id)
        .ValidateExistence<GetCharacterByIdQuery, Character>(context)
        .WithMessage("Character not found");

    }
}
