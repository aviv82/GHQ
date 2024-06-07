using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.GameLogic.Queries;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Games;

public class GetGameByIdValidator : AbstractValidator<GetGameByIdQuery>
{
    public GetGameByIdValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<GetGameByIdQuery, Game>(context);
    }
}
