using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.PlayerLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Players;

public class DeletePlayerValidator : AbstractValidator<DeletePlayerRequest>
{
    public DeletePlayerValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<DeletePlayerRequest, Player>(context);
    }
}

