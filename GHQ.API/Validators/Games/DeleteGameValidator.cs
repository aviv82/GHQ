using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.GameLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Games;

public class DeleteGameValidator : AbstractValidator<DeleteGameRequest>
{
    public DeleteGameValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<DeleteGameRequest, Game>(context).WithMessage("Game does not exist.");
    }
}
