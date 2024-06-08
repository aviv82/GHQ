using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.PlayerLogic.Queries;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Players;

public class GetPlayerByIdValidator : AbstractValidator<GetPlayerByIdQuery>
{
    public GetPlayerByIdValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<GetPlayerByIdQuery, Player>(context);
    }

}
